$(document).ready(function () {
    //Adds nanoScroller to 'nano' classes
    $(".nano").nanoScroller();

    //Adds tipsy element to all 'form .cell. text-field' classes
    $('form').find(".cell").find('.text-field').tipsy({ gravity: 'n' });
    $(function () {
        $('form').addTriggersToJqueryValidate().triggerElementValidationsOnFormValidation();
        $('input').elementValidation(function (element) {
            highlightElement(element);
        });
        $('select').elementValidation(function (element) {
            highlightElement(element);
        });
    });

    //Displays login panel on clicking 'Log In' button
    $('#button-login').live('click', function () {
        $('#hidden-login').css('display', 'block');
    });

    //Hides login panel on clicking 'Cancel' button
    $('#button-cancel').live('click', function () {
        $('#hidden-login').css('display', 'none');
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

    $('#beautiful-girls-button').live('click', function () {
        goToPage('/Girls/Index/?isBeautiful=true');
    });

    $('#all-girls-button').live('click', function () {
        goToPage('/Girls/Index/?isBeautiful=false');
    });

    //Allows insert in textbox area only positive integer numbers
    $(".positive-double").numeric({ decimal: false, negative: false });
});

//Adds sweetPages to #news-list item
function sweetPages() {
    if ($('#news-list')[0] !== undefined) {
        $('#news-list').sweetPages({ perPage: countOfNewsOnPage() });
        var controls = $('#news-list .outer-center').detach();
        controls.appendTo('#news-main');
    }
};

//Converts JSON DateTime object to convenient form
function GetDate(jsonDate) {
    var value = new Date(parseInt(jsonDate.substr(6)));
    return value.getMonth() + 1 + "/" + value.getDate() + "/" + value.getFullYear();
}

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

//Calculates number of news items on #news panel
function countOfNewsOnPage() {
    return ($('#news').outerHeight(true) - $('#news h2').outerHeight(true)
                - $('#news-list a').outerHeight(true)) / ($('#news-list li').outerHeight(true));
}

//Go to current page
function goToPage(page) {
    window.location = page;
}

//Deletes news item by ID form #news panel and reloads it
function deleteNewsPanelItem(id) {
    if (confirm('Delete?') === false)
        return false;
    $.ajax({
        url: "/News/Delete/",
        type: "POST",
        data: { id: id },
        success: function (data) {
            var url = data.url;
            if (url !== undefined) {
                alert("Item is not deleted! Item with this id: " + id + " is not found");
                goToPage(url);
                return false;
            }
            $('#news').html(data);
            reloadNewsPager();
            return false;
        }
    });
    return false;
}

//Deletes news item by ID and goes to /News page
function deleteNewsItem(id) {
    if (confirm('Delete?') === false)
        return false;
    $.ajax({
        url: "/News/Delete/",
        type: "POST",
        data: { id: id },
        success: function (data) {
            var url = data.url;
            if (url !== undefined) {
                alert("Item is not deleted! Item with this id: " + id + " is not found");
                goToPage(url);
                return false;
            }
            goToPage('/News');
            alert("Item is deleted");
            return false;
        }
    });
    return false;
}

//Deletes girls item by ID
function deleteGirlsItem(id) {
    if (confirm('Delete?') === false)
        return false;
    $.ajax({
        url: "/Girls/Delete/",
        type: "POST",
        data: { id: id },
        success: function (data) {
            var url = data.url;
            if (url !== undefined) {
                alert("Item is not deleted! Item with this id: " + id + " is not found");
                goToPage(url);
                return false;
            }
            $('#content').html(data);
            return false;
        }
    });
    return false;
}

//Reloads #news panel
function reloadNewsPager() {
    $('.swSlider .clear').remove();
    var cnt1 = $(".swSlider").contents();
    $(".swSlider").replaceWith(cnt1);
    var cnt2 = $(".swPage").contents();
    $(".swPage").replaceWith(cnt2);
    $('#news-main .outer-center').remove();
    sweetPages();
}

window.onresize = function () { if ($('#news-list')[0] !== undefined) reloadNewsPager(); };