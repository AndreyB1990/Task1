$(document).ready(function () {
    //Adds nanoScroller to 'nano' classes
    $('.nano').nanoScroller();

    //Adds tipsy element to all 'form .cell. text-field' classes
    $('form .cell .text-field').tipsy({ gravity: 'n' });

    //Adds triggers to jQuery Validation
    $('form').addTriggersToJqueryValidate().triggerElementValidationsOnFormValidation();

    $('input').elementValidation(function (element) {
        highlightElement(element);
    });
    $('select').elementValidation(function (element) {
        highlightElement(element);
    });

    //Displays login panel on clicking 'Log In' button
    $('#button-login').live('click', function () {
        $('#hidden-login').css('display', 'block');
    });

    //Hides login panel on clicking 'Cancel' button
    $('#login-button-cancel').live('click', function () {
        $('#hidden-login').css('display', 'none');
    });

    //Goes to main page on clicking 'Cancel' button
    $('#button-cancel').live('click', function () {
        window.location = '/Home/Index/';
    });

    //Goes to girls main page on clicking 'Cancel' button
    $('#girls-button-cancel').live('click', function () {
        window.location = '/Girls/Index/';
    });

    //Goes to news main page on clicking 'Cancel' button
    $('#news-button-cancel').live('click', function () {
        window.location = '/News/Index/';
    });

    //Listens clicks of buttons for deleting girls item with specific id
    $('.delete-girls-item').live('click', function () {
        var id = $(this).parent('td').parent('tr').attr('data-id');
        return deleteGirlsItem(id);
    });

    //Listens clicks of buttons for deleting news-panel item with specific id
    $('.delete-news-panel-item').live('click', function () {
        var id = $(this).parent('li').attr('data-id');
        return deleteNewsPanelItem(id);
    });

    //Listens clicks of buttons for deleting news item with specific id
    $('.delete-news-item').live('click', function () {
        var id = $(this).parent('div').attr('data-id');
        return deleteNewsItem(id);
    });

    //Listens clicks of buttons for displaying beautiful girls
    $('#beautiful-girls-button').live('click', function () {
        window.location = '/Girls/Index/?isBeautiful=true';
    });

    //Listens clicks of buttons for displaying all girls
    $('#all-girls-button').live('click', function () {
        window.location = '/Girls/Index/?isBeautiful=false';
    });

    //Allows insert in textbox area only positive integer numbers
    $(".positive-double").numeric({ decimal: false, negative: false });
});