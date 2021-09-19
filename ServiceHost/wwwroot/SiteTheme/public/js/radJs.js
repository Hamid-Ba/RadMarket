// !get Items
const offer = document.getElementById("offers");
const thumbnail = document.getElementsByClassName('thumbnail');
const bests = document.getElementById("bests");
const nextOffer = document.getElementById("right");
const prevOffer = document.getElementById("left");
const nextbests = document.getElementById("right-bests");
const prevbests = document.getElementById("left-bests");


// !offers
prevOffer.addEventListener('click', function () {
    offer.scrollLeft -= 225;
})

nextOffer.addEventListener('click', function () {
    offer.scrollLeft = 0;
})

const maxScrollLeft = offer.scrollWidth - offer.clientWidth;

function autoPlay() {
    if (offer.scrollLeft > (maxScrollLeft - 1)) {
        offer.scrollLeft = 0;
    } else {
        offer.scrollLeft -= 1
    }
}

let play = setInterval(autoPlay, 50)

for (let i = 0; i < thumbnail.length; i++) {
    thumbnail[i].addEventListener('mouseover', () => {
        clearInterval(play)
    })
    thumbnail[i].addEventListener('mouseout', () => {
        return play = setInterval(autoPlay, 50);
    })
}


// !bests
prevbests.addEventListener('click', function () {
    bests.scrollLeft -= 225;
})

nextbests.addEventListener('click', function () {
    bests.scrollLeft += 225;
})




