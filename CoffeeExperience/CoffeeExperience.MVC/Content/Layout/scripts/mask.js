$(function () {

    $(".ddd").mask("(99)");
    $(".data").mask("99/99/9999");
    $(".cep").mask("99999-999");
    $(".cpf").mask("999.999.999-99");
    $(".cartao").mask("999999999999999?9");
    $(".cvc").mask("999?9");
    $(".cep").mask("99999-999");    
    $('.tel').keyup(function () {

        var phone, element;
        element = $(this);
        phone = element.val().replace(/D/g, '');
        phone = jQuery.trim(phone);
        phone = phone.replace('(', '');
        phone = phone.replace(')', '');
        phone = phone.replace('-', '');
        phone = phone.replace('_', '');
        phone = phone.replace('_', '');
        phone = phone.replace('_', '');
        phone = phone.replace('_', '');
        phone = phone.replace('_', '');
        phone = phone.replace('_', '');
        phone = phone.replace('_', '');
        phone = phone.replace('_', '');
        phone = phone.replace('_', '');
        phone = phone.replace('_', '');

        if (phone.length == 8) {
            element.unmask();
            element.mask("9999-9999?9");
        } else if (phone.length == 9) {
            element.unmask();
            element.mask("9?9999-9999");
        } else if (phone.length == 0) {
            element.unmask();
            element.mask("9999-9999?9");
        }
    }

	).trigger('keyup');

});