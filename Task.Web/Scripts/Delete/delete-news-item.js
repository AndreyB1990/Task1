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
                window.location = url;
                return false;
            }
            window.location = '/News';
            alert("Item is deleted");
            return false;
        }
    });
    return false;
}