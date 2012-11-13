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
                window.location = url;
                return false;
            }
            $('#content').html(data);
            return false;
        }
    });
    return false;
}