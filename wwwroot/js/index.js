console.log("Hallo!");

var theForm = document.getElementById("theForm");
theForm.hidden = true;

var button = document.getElementById("buyButton");
    button.addEventListener("click", function () {
    console.log("Buying item");
    });

var productInfo = document.getElementsByClassName("product-props");
var listItems = productInfo.item(0).children;