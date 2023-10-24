var label;
var historyCart;
var logoutUrl;
var shopItemsData;

window.onload = function () {
    label = document.getElementById("label");
    historyCart = document.getElementById("history-cart");
    logoutUrl = document.getElementById("logoutLink").getAttribute("data-url");

    label.innerHTML = 
    `
    <a href="${logoutUrl}">
        <button class="homeBtn" >Log out</button>
    </a>
    `;


    // 展示商品购买记录的页面 需要后台数据库接口
    let xhr = new XMLHttpRequest;
    xhr.open("POST", "/Database/GetUserHistory");
    xhr.setRequestHeader("Content-Type", "application/json; charset=utf8");
    xhr.onreadystatechange = function () {
        if (this.readyState == XMLHttpRequest.DONE && this.status == 200)
        {
            localStorage.setItem("historyItems", this.responseText, false); // 打开同步
        }
    }
    xhr.send();

    historyCartItems = JSON.parse(localStorage.getItem("historyItems"));

    // alert(JSON.stringify(historyCart));

    shopItemsData = JSON.parse(localStorage.getItem("shopItemsData"));
    
    // alert(JSON.stringify(shopItemsData));

    generateHistoryCartItems();

    basket = JSON.parse(localStorage.getItem("data")) || [];

    calculation(); // 每次打开页面都会更新右上角购物车数字
}


window.generateHistoryCartItems = () => {
    if (historyCart.length !== 0) {
        return (historyCart.innerHTML = historyCartItems.map((x) => {
            let { goodId, serialNumbers} = x; // 解析
            let search = shopItemsData.find((x) => parseInt(x.id) === goodId) || [];
            let { id, name, price, description, img } = search; // 这里名字要一样才能解析

            return `
            <div class="cart-item">
            <img  width="180" src=${img} alt="">
            <div class="details">
                <div class="title-price-x">
                    <h4 class="title-price">
                        <p>${name}</p>
                    </h4>
                </div>
                <div class="dropdown-section">
                    <select name="dropdown">
                        ${serialNumbers.map(serialNumber => `<option value="${serialNumber}">${serialNumber}</option>`).join("") }
                    </select>
                </div>
            </div>
         </div>
            `;
        }).join(""));
    }
};



window.calculation = () => {
    document.getElementById("cartAmount").innerHTML = basket.map((x) => x.item).reduce((x, y) => x + y, 0);
};