// !get items
const menuSmall = document.getElementById("menu-small");
const closeMenuSmall = document.getElementById("close-menu-small");
const responsiveMenu = document.getElementById("responsive-menu");






// ! menu responsive
menuSmall.addEventListener("click", function () {
    responsiveMenu.style.display = "flex";
})

closeMenuSmall.onclick = () => {
    if (responsiveMenu.style.display !== "none") {
        responsiveMenu.style.display = "none";
    }
}
