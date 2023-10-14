
// 全局变量
var shopItemsData;
var basket;

// 一开始没有成功是因为缓存的问题和数据库循环引用的问题 报错代码500
// https://www.cnblogs.com/ykbb/p/15015265.html

window.onload = function () {

    let xhr = new XMLHttpRequest();

    xhr.open("GET", "/Goods/GetAllGoods"); 


    xhr.onreadystatechange = function () {
        if (this.readyState == XMLHttpRequest.DONE) {
            if (this.status != 200) return;
            // alert(this.responseText);
            shopItemsData = JSON.parse(this.responseText);
            localStorage.setItem("shopItemsData", this.responseText);
            generateShop(shopItemsData);
        }
    }

    xhr.send();

    //shopItemsData = JSON.parse(localStorage.getItem("shopItemsData"));

    basket = JSON.parse(localStorage.getItem("data")) || [];

    // 更新右上角
    calculation();
}




window.generateShop = (shopItemsData) => {
    return (shop.innerHTML = shopItemsData.map(
        (x) => {
            let { id, name, price, description, img } = x;

            let search = basket.find((y) => y.id === parseInt(x.id)) || [];

            return `
            <div id=product-id-${id} class="item">
                    <img width="220" src=${img} alt="">
                    <div class="details">
                        <h3>${name}</h3>
                        <p>${description}</p>
                        <div class="price-quantity">
                            <h2 class="price">$ ${price}</h2>
                            <div class="button">
                                <i onclick="decrement(${id})" class="bi bi-dash-square-fill"></i>
                                <div id=${id} class="quantity">
                                    ${search.item === undefined ? 0 : search.item}
                                </div>
                                <i onclick="increment(${id})" class="bi bi-plus-square-fill"></i>
                            </div>
                        </div> 
                    </div>
                </div>
            `;
        }
    ).join("")
    );
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


    localStorage.setItem("data", JSON.stringify(basket));
    update(id);
}




window.decrement = (id) => {
    let search = basket.find((x) => x.id === id);

    if (search === undefined) return; // 找不到的时候直接结束
    else if (search.item === 0) return;
    else {
        search.item--;
    }

    update(id);
    basket = basket.filter((x) => x.item !== 0);

    localStorage.setItem("data", JSON.stringify(basket));
}




window.update = (id) => {
    let search = basket.find((x) => x.id === id);
    document.getElementById(id).innerHTML = search.item;
    calculation();
};




window.calculation = () => {
    document.getElementById("cartAmount").innerHTML = basket.map((x) => x.item).reduce((x, y) => x + y, 0);
    // console.log(basket.map( (x)=>x.item ).reduce( (x,y) => x+y) );
};



/*
window.onload = function () {

    let shopItemsData = [];

    let xhr = new XMLHttpRequest();

    xhr.open("POST", "/Goods/GetAllGoods", false); // synchronus

    xhr.setRequestHeader("Content-Type", "application/json; charset=utf8");

    xhr.onreadystatechange = function () {
        if (this.readyState == XMLHttpRequest.DONE) {
            if (this.status != 200) return;
            localStorage.setItem("shopItemsData", this.responseText);     
        }
    }

    xhr.send();


    // 更新右上角
    let basket = JSON.parse(localStorage.getItem("data")) || [];
    calculation();


    // 生成橱窗界面
    shopItemsData = JSON.parse(localStorage.getItem("shopItemsData"));
    // alert(JSON.stringify(shopItemsData));
    generateShop(shopItemsData);
}
*/






/*
let shopItemsData = [
    {
        id:"1", // 一定要unique
        name:"FOPCS",
        price: 100,
        desc: "FOPCS's description",
        img: "/img/1.jpg"
    },
    {
        id:"2", // 一定要unique
        name:"OOPCS",
        price: 80,
        desc: "OOPCS's description",
        img: "/img/2.jpg"
    },
    {
        id:"3", // 一定要unique
        name:"C",
        price: 200,
        desc: "C's description",
        img: "/img/3.jpg"
    },
    {
        id:"4", // 一定要unique
        name:"Algorithm",
        price: 2000,
        desc: "Algorithm's description",
        img: "/img/4.jpg"
    },
    {
        id:"5", // 一定要unique
        name:"SQL",
        price: 300,
        desc: "SQL's description",
        img: "/img/5.jpg"
    },
    {
        id:"6", // 一定要unique
        name:"OS",
        price: 500,
        desc: "OS's description",
        img: "/img/6.jpg"
    }
];
*/