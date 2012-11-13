//Converts JSON DateTime object to convenient form
function GetDate(jsonDate) {
    var value = new Date(parseInt(jsonDate.substr(6)));
    return value.getMonth() + 1 + "/" + value.getDate() + "/" + value.getFullYear();
}