const sign_in_btn = document.querySelector("#sign-in-btn");
const sign_up_btn = document.querySelector("#sign-up-btn");
const container = document.querySelector(".container");

sign_up_btn.addEventListener("click", () => {
    container.classList.add("sign-up-mode");
});

sign_in_btn.addEventListener("click", () => {
    container.classList.remove("sign-up-mode");
});

function checkUsername() {
    var username = document.getElementById("username").value; // 获取里面的值

    let xhr = new XMLHttpRequest();
    xhr.open("POST", "/Login/IsUsernameOkay");
    xhr.setRequestHeader("Content-Type", "application/json; charset=utf8");
    xhr.onreadystatechange = function () {
        if (this.readyState == XMLHttpRequest.DONE && this.status == 200) {
            let data = JSON.parse(this.responseText);
            
            if (data.isOkay == false) {
                alert("Username is invalid.");
            }
        }
    }
    xhr.send(JSON.stringify(username));

}