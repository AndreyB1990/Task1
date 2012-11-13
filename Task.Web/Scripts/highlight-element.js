//Displays or hides error hints
function highlightElement(input) {
    var el = $(input);
    if (el.hasClass('input-validation-error')) {
        el.parents('div.cell, tr.cell').addClass('error');
        var fieldDiv = el.parents('div.cell, tr.cell').find('.text-field');
        var text = fieldDiv.find('span.field-validation-error').find('span').text();
        fieldDiv.attr("original-title", text.toString());
    } else {
        el.parents('div.cell, tr.cell').removeClass('error');
        el.parents('div.cell, tr.cell').find('.text-field').removeAttr("original-title");
    }
}