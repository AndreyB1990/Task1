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
                window.location = url;
                return false;
            }
            $('#news').html(data);
            reloadNewsPager();
            return false;
        }
    });
    return false;
}