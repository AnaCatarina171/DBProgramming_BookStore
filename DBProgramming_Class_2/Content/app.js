/*function searchCustomer() {
    var searchTermName = document.getElementById('txtName').value;

    searchTermName = searchTermName.trim();

    window.location.href = "/Customers/CustomersList/?searchTermName=" + searchTermName;
}*/

function getProductDetails(productCode) {
    $.get('/Home/ProductDetails/' + productCode, function (data) { //study!
        $('#exampleModal').modal('show');
        $(".modal-body").html(data);
    });
}