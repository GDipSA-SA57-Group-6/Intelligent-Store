
/*
在JavaScript中，反引号 ` ` 是用来创建模板字符串（template literals）的。模板字符串是一种特殊的字符串，它允许你在字符串中嵌入变量或表达式，以便更方便地构建复杂的字符串。
在函数式编程中，模板字符串可以用来创建HTML代码片段，这对于自动生成HTML文件非常有用。你可以将HTML标签、属性和变量嵌入到模板字符串中，然后将它们插入到HTML文档中
*/

// 数据文件，在index.html引入了，放在src/data.js文件中


/*
* 购物篮子 保存点击了哪些商品 及其数量
* 先从本地检查是否有数据，有的话作为初始化
*/
let basket = JSON.parse(localStorage.getItem("data")) || [];

/** 
* 生成橱窗界面
*!注意这里JSON.parse把Json转变成js格式以后，纯数字的会变成int类型，不能直接和string比较
**/

// 保证它能够传进去一个对象数组
/*
let shop = document.getElementById("shop");
let generateShop = () => {
    return (shop.innerHTML = shopItemsData.map(
        (x) => {
            let { id, name, price, desc, img } = x;
            let search = basket.find((y) => y.id === parseInt(x.id)) || [];

            return `
            <div id=product-id-${id} class="item">
                    <img width="220" src=${img} alt="">
                    <div class="details">
                        <h3>${name}</h3>
                        <p>${desc}</p>
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
generateShop();
*/

let generateShop = (shopItemsData) => {
    return (shop.innerHTML = shopItemsData.map(
        (x) => {
            let { id, name, price, desc, img } = x;
            let search = basket.find((y) => y.id === parseInt(x.id)) || [];

            return `
            <div id=product-id-${id} class="item">
                    <img width="220" src=${img} alt="">
                    <div class="details">
                        <h3>${name}</h3>
                        <p>${desc}</p>
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


/*
* 点击按钮增加商品数量
*/
let increment = (id) => {

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

/*
* 点击按钮减少商品数量
*/
let decrement = (id) => {
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

/*
* 点击按钮之后 在数字处更新商品数量
*/
let update = (id) => {
    let search = basket.find((x) => x.id === id);
    document.getElementById(id).innerHTML = search.item;
    calculation();
};

/*
* 购物车计算总商品数量
*/
let calculation = () => {
    document.getElementById("cartAmount").innerHTML = basket.map((x) => x.item).reduce((x, y) => x + y, 0);
    // console.log(basket.map( (x)=>x.item ).reduce( (x,y) => x+y) );
};
// calculation(); // 每次打开页面都会更新右上角购物车数字

