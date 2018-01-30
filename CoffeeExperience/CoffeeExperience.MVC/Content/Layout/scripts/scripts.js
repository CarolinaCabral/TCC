function postAjax(url, data) {
    $.ajax({
        type: "POST",
        url: url,
        data: data,
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        async: true,
        success: function (data) {
            Sucesso(data);
        },
        complete: function () {
            Completo();
        },
        beforeSend: function () {
            Carregando();
        },
        error: function () {
            Falha();
        }
    });
}
function Falha() {
    swal('Ocorreu um erro inesperado! - Sistema indisponível no momento por favor, tente novamente mais tarde.');
}
function Carregando() {
    swal({
        title: "Carregando..",
        showConfirmButton: false
    });
}


function SucessoCadastro(data) {
    if (!data.Erro) {

        if (!data.NaoLimpar) {
            $("input:text").val("");
            $("input:password").val("");
            $("select").val("");
            $("textarea").val("");
        }
        if (data.Msg) {
            swal(data.Msg);
        }
        else if (data.Ajax) {
            $.ajax({
                type: "POST",
                url: data.Ajax,
                success: function (data) {
                    Sucesso(data);
                },
                complete: function () {
                    Completo();
                },
                beforeSend: function () {
                    Carregando();
                },
                error: function () {
                    Falha();
                }
            });
        }
        else {
            swal('Redirecionando...');
            if (data.Url) {
                window.location = data.Url;
            } else {
                window.location = ".";
            }
        }
    }
    else {
        swal(data.Msg);
    }
}

function SucessoCadastroLot(data) {
    if (data.Modal)
    {
        swal.close();
        var modal = $('#' + data.Id);
        console.log(modal);
        modal.modal('show'); 
    }
    else
    {
        if (data.redirect) {
            if (data.update) {
                var ajaxCallback = $.Deferred();
                var modalCallback = $.Deferred();
                $.when(ajaxCallback, modalCallback).done(function (ajaxResult) {
                    $(data.update).html(ajaxResult);
                });
                $.ajax({
                    url: data.redirect,
                    type: "GET"
                }).done(function (data) {
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
            } else {
                window.location.href = data.redirect;
            }
        }
        else if (!data.Erro) {

            if (!data.NaoLimpar) {
                $("input:text").val("");
                $("input:password").val("");
                $("select").val("");
                $("textarea").val("");
            }
            if (data.Msg) {
                swal(data.Msg);
            }
            else if (data.Ajax) {

            }
            else {
                swal('Redirecionando...');
                if (data.Url) {
                    window.location = data.Url;
                } else {
                    window.location = ".";
                }
            }
        }
        else {
            swal(data.Msg);
        }
    }
    
}

function Sucesso(data) {
    if (!data.Erro) {

        if (!data.NaoLimpar) {
            $("input:text").val("");
            $("input:password").val("");
            $("select").val("");
            $("textarea").val("");
        }
        if (data.Msg) {
            swal(data.Msg);
        }
        else if (data.Ajax) {
            $.ajax({
                type: "POST",
                url: data.Ajax,
                success: function (data) {
                    Sucesso(data);
                },
                complete: function () {
                    Completo();
                },
                beforeSend: function () {
                    Carregando();
                },
                error: function () {
                    Falha();
                }
            });
        }
        else {
            swal('Redirecionando...');
            if (data.Url) {
                window.location = data.Url;
            } else {
                window.location = ".";
            }
        }
    }
    else {
        swal(data.Msg);
    }
}

function SucessoRemove(data) {
    if (!data.Erro) {
        if (data.Msg) {
            swal(data.Msg);
        }
        else {
            swal('Redirecionando...');
            window.location = data.Url;
        }
    }
    else {
        swal(data.Msg);
    }
}

function SucessoNoRemove(data) {
    if (!data.Erro) {
        if (data.Msg) {
            swal(data.Msg);
        }
        else {
            swal('Redirecionando...');
            window.location = data.Url;
        }
    }
    else {
        swal(data.Msg);
    }
}
function Completo() {
    $("input").removeAttr("disabled");
    $("select").removeAttr("disabled");
    $("textarea").removeAttr("disabled");
    $(".btn").removeAttr("disabled");
}
function CompletoSemMensagem() {
    $("input").removeAttr("disabled");
    $("select").removeAttr("disabled");
    $("textarea").removeAttr("disabled");
    $(".btn").removeAttr("disabled");
}
function CarregandoSimulado() {
    swal('Carregando...');
    $("input").attr("disabled", "disabled");
    $("select").attr("disabled", "disabled");
    $("textarea").attr("disabled", "disabled");
    $(".btn-simulado").attr("disabled", "disabled");
}
function CompletoSemMensagemSimulado() {
    $("input").removeAttr("disabled");
    $("select").removeAttr("disabled");
    $("textarea").removeAttr("disabled");
    $(".alternativa").removeAttr("disabled");
    $(".btn-simulado").removeAttr("disabled");
    toastr.remove();
}
function CompletoSimulado() {
    $("input").removeAttr("disabled");
    $("select").removeAttr("disabled");
    $("textarea").removeAttr("disabled");
    $(".btn-simulado").removeAttr("disabled");
}
function PaginarAjax(url, mdiv) {

    $.ajax({
        type: "GET",
        url: url,
        data: "",
        contentType: "application/x-www-form-urlencoded; charset=UTF-8",
        async: true,
        success: function (data) {
            SucessoListagem();
            $("#" + mdiv).html(data);
        },
        complete: function () {
            Completo();
        },
        beforeSend: function () {
            Carregando();
        },
        error: function () {
            Falha();
        }
    });
}

function FecharAlerta()
{
    swal.close();
    $('.overlay').remove();
}
function SucessoListagem(data)
{
    if (data.Erro != null && data.Erro)
    {
        swal(data.Msg);
    }
    else
    {
        FecharAlerta();
    }
}
