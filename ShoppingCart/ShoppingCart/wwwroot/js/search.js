let searchbtn = document.getElementById("searchBtn")
let searchinput = document.getElementById("searchInput")
searchbtn.addEventListener("click", function () {
    var search_elem = document.getElementById("searchInput").value;
    shop.innerHTML = shopItemsData.map((x) => {
        let { id, name, price, desc, img } = x;
        let search = basket.find((y) => y.id === parseInt(x.id)) || [];
        console.log(search_elem);
        if (search_elem === x.name) {
            return `<div id=product-id-${id} class="item">
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
                </div > ` ;
        }

    }).join("");
});

searchinput.addEventListener("change", function () {
    var search_elem = searchinput.value;
    shop.innerHTML = shopItemsData.map((x) => {
        let { id, name, price, desc, img } = x;
        let search = basket.find((y) => y.id === parseInt(x.id)) || [];
        console.log(search_elem);

        if (search_elem === "") {
            // �����Ϊ�գ�չʾ������Ʒ��Ϣ
            return `<div id=product-id-${id} class="item">
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
            </div>`;
        }
    }).join("");
})