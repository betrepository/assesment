const { Alert } = require("bootstrap");

function addToCart(productId) {

    $.post("/Cart/AddToCartAsync", { productId: productId }, function (data) {      
        $("#spanTotal_" + productId).html(data.carts);
        $("#spanTotalPrice").html("Total Price : R" + data.price);
    });
}

function removeFromCart(productId) {
    $.post("/Cart/RemoveFromCartAsync", { productId: productId }, function (data) {
        $("#spanTotal_" + productId).html(data.carts);
        $("#spanTotalPrice").html("Total Price : R" + data.price);
    });
}


function checkOut() {
    $.get("/Cart/ChectOut", function (data) {
        $(".span_cart_totals").html("0");
        $("#spanTotalPrice").html("Total Price : R0.00");
        alert(data);
    });
}