// redirect ajax and json
$(function () {
    $(document).ajaxComplete(function (event, xhr, options) {
        var contentType = xhr.getResponseHeader('content-type') || '';
        if (contentType.indexOf('json') > -1) {
            var data = $.parseJSON(xhr.responseText);
            if (data) {
                if (data.Erro)
                {
                    swal(data.Msg);
                }
                else if (data.Msg != null)
                {
                    swal(data.Msg);
                }
                else if (data.redirect) {
                    Completo();
                    if (data.update) {
                        var ajaxCallback = $.Deferred();
                        var modalCallback = $.Deferred();
                        $.when(ajaxCallback, modalCallback).done(function (ajaxResult) {
                            $(data.update).html(ajaxResult);
                        });
                        $.ajax({
                            url: data.redirect,
                            type: "GET"
                        }).done(function(data) {
                            ajaxCallback.resolve(data);
                        }).fail(function () {
                            ajaxCallback.reject();
                        });
                        var modal = $('.modal');
                        if (modal.length == 0) {
                            modalCallback.resolve();
                        } else {
                            modal.on('hidden.bs.modal', function (e) {
                                modalCallback.resolve();
                            }).modal('hide');
                        }
                    }

                }
            }
        }
    });
});

// error handling
$(function () {
    $(document).ajaxComplete(function (event, xhr, options) {
        if (xhr.status === 500) {
            var contentType = xhr.getResponseHeader('content-type') || '';
            if (contentType.indexOf('html') > -1) {
                $('body').html(xhr.responseText);
            }
            else if (contentType.indexOf('json') > -1) {
                var data = $.parseJSON(xhr.responseText);
                if (data && data.error) {
                    var errorwindow = $('#div-shared-messages');
                    errorwindow.find('#span-message-placeholder').text(data.error);
                    errorwindow.removeClass('hide');
                }
            }
        }
    });
    $(document).ajaxStart(function (event, xhr, options) {
        Carregando();
    });
});

// validation and ajax
$(function () {
    $(document).ajaxComplete(function (event, xhr, options) {
        $('form').removeData('validator');
        $('form').removeData('unobtrusiveValidation');
        $.validator.unobtrusive.parse('form');
    });
});

// auto show modal
$(function () {
    function autoShowModal() { 
        $('.modal[data-show]').removeAttr('data-show').modal('show');
    };
    autoShowModal();
    $(document).ajaxComplete(function (event, xhr, options) {
        autoShowModal();
    });
});

// hide alert instead of removing it
$(function () {
    $(document).on('click', '[data-hide]', function () {
        $(this).closest("." + $(this).attr("data-hide")).addClass('hide');
    });
});