let basket;
let shopItemsData;
var shoppingCartUrl;
var successfulPayUrl;
var loginUrl;
let label;
let shoppingCart;


window.onload = function () {
    // 同样需要获取shopItemsData, 因为在首页里我们已经向服务器请求过了，所以不需要再请求一次
    shopItemsData = JSON.parse(localStorage.getItem("shopItemsData"));

    // 方便在js里访问
    shoppingCartUrl = document.getElementById("shoppingCartLink").getAttribute("data-url");
    loginUrl = document.getElementById("loginLink").getAttribute("data-url");
    successfulPayUrl = document.getElementById("successfulPay").getAttribute("data-url");

    label = document.getElementById("label");

    shoppingCart = document.getElementById("shopping-cart");

    basket = JSON.parse(localStorage.getItem("data")) || [];


    calculation(); // 每次打开页面都会更新右上角购物车数字
    // 这里需要把shopItemsData作为参数传进去
    generateCartItems();
    TotalAmount();


}


window.generateCartItems = () => {
    if (basket.length !== 0) {
        return (shoppingCart.innerHTML = basket.map((x) => {
            let { id, item } = x;
            let search = shopItemsData.find((x) => parseInt(x.id) === id) || [];
            let { _id, name, price, desc, img } = search;

            return `
            <div class="cart-item">
            <img  width="180" src=${img} alt="">
            <div class="details">
                <div class="title-price-x">
                    <h4 class="title-price">
                        <p>${name}</p>
                        <p class="cart-item-price">$ ${price}</p>
                    </h4>
                    <i onclick="removeItem(${id})" class="bi bi-x-lg"></i>
                </div>
        
                <div class="button">
                    <i onclick="decrement(${id})" class="bi bi-dash-square-fill"></i>
                    <div id=${id} class="quantity">
                        ${item}
                    </div>
                    <i onclick="increment(${id})" class="bi bi-plus-square-fill"></i>
                </div>
        
                <h3>$ ${item * price}</h3>
            </div>
         </div>
            `;
        }).join(""));
    } else {
        shoppingCart.innerHTML = ``;
        label.innerHTML = `
        <h2>Cart is empty</h2>
        <a href="${shoppingCartUrl}">
            <button class="homeBtn" >Back to Home</button>
        </a>
        `;
    }
};


window.calculation = () => {
    document.getElementById("cartAmount").innerHTML = basket.map((x) => x.item).reduce((x, y) => x + y, 0);
};



window.increment = (id) => {
    let search = basket.find((x) => x.id === id);
    if (search == undefined) {
        basket.push({
            id: id,
            item: 1
        });
    } else {
        search.item++;
    }
    generateCartItems(shopItemsData, label, shoppingCart); // 添加此行以在增加商品数量后刷新购物车
    localStorage.setItem("data", JSON.stringify(basket));
    update(id);
    calculation();
    TotalAmount();
}

window.decrement = (id) => {
    let search = basket.find((x) => x.id === id);

    if (search === undefined) return;
    else if (search.item === 0) return;
    else {
        search.item--;
    }

    update(id);
    basket = basket.filter((x) => x.item !== 0);
    generateCartItems(); // 添加此行以在减少商品数量后刷新购物车
    localStorage.setItem("data", JSON.stringify(basket));
    TotalAmount();
    calculation();
}

window.removeItem = (id) => {
    basket = basket.filter((x) => x.id !== id);
    localStorage.setItem("data", JSON.stringify(basket));
    generateCartItems(); // 添加此行以在删除商品后刷新购物车
    TotalAmount();
    calculation();
}

window.clearCart = () => {
    basket = [];
    generateCartItems();
    TotalAmount();
    localStorage.setItem("data", JSON.stringify(basket));
    calculation();
}


// 在这里处理数据回传的界面 重要
window.TotalAmount = () => {
    if (basket.length !== 0) {
        let amount = basket.map((x) => {
            let { id, item } = x;
            let search = shopItemsData.find((y) => parseInt(y.id) === id) || [];
            return item * search.price;
        })
            .reduce((x, y) => x + y, 0);
        label.innerHTML = `
        <h2>Total Bill: $ ${amount} </h2>
        
        <a id="checkoutLink" href="#">
            <button onclick="SendCartDataAndLogin()" class="checkout">Checkout</button>
        </a>
        <button onclick="clearCart()" class="removeAll">Clear Cart</button>
        `;
    } else return;
}


window.update = (id) => {
    let search = basket.find((x) => x.id === id);
    document.getElementById(id).innerHTML = search.item;
    calculation();
};


// 逻辑 先用AJAX技术提交，如果成功 _ 如果不成功 _
// 根据条件重定向
// 当满足特定条件时，onclick 函数会修改 href 属性，并通过模拟点击链接的方式执行页面跳转
window.SendCartDataAndLogin = () => {
    let xhr = new XMLHttpRequest();
    xhr.open("POST", "/Database/TryToCheckout"); // 直接提交给数据库模块
    xhr.setRequestHeader("Content-Type", "application/json; charset=utf8");
    xhr.onreadystatechange = function ()
    {
        if (this.readyState == XMLHttpRequest.DONE && this.status == 200)
        {
            let data = JSON.parse(this.responseText);
            alert(data.status);

            let checkoutLink = document.getElementById("checkoutLink");
            if (data.status == "Unsuccessful")
            {
                // do nothing
                checkoutLink.setAttribute("href", loginUrl);
                checkoutLink.click();
            } else {

                // 清空basket 在本地更新
                basket = [];
                localStorage.setItem("data", JSON.stringify(basket));

                checkoutLink.setAttribute("href", successfulPayUrl);
                checkoutLink.click();
            }
        }
    }
    xhr.send(JSON.stringify(basket));
}