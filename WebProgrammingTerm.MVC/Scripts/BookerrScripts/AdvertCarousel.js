document.addEventListener("DOMContentLoaded", function () {
    initializeAdvertCarousel("advertCarousel");
});

function initializeAdvertCarousel(containerClass) {
    var containers = document.getElementsByClassName(containerClass);

    if (containers.length === 0) {
        console.error("Containers not found:", containerClass);
        return;
    }

    var container = containers[0];
    var currentIndex = 0;


    var prevButton = document.createElement('Button');
    prevButton.className = 'advert-carousel-prev-btn';
    prevButton.addEventListener('click', prevSlide);

    var prevImg = document.createElement('img');
    prevImg.src = '../../Content/WebAssets/arrow_left.svg';
    prevImg.style.width = '40px';
    prevImg.style.height = '40px';
    prevButton.appendChild(prevImg);
    container.appendChild(prevButton);

    var nextButton = document.createElement('Button');
    nextButton.className = 'advert-carousel-next-btn';
    nextButton.addEventListener('click', nextSlide);

    var nextImg = document.createElement('img');
    nextImg.src = '../../Content/WebAssets/arrow_right.svg';
    nextImg.style.width = '35px';
    nextImg.style.height = '35px';
    nextButton.appendChild(nextImg)
    container.appendChild(nextButton);


    function showSlide(index) {
        var carouselInner = container.querySelector('.advert-carousel-inner');
        var slideWidth = container.querySelector('.advert-carousel-item').clientWidth;
        var totalSlides = container.querySelectorAll('.advert-carousel-item').length;

        if (index < 0) {
            index = totalSlides - 1;
        } else if (index >= totalSlides) {
            index = 0;
        }

        carouselInner.style.transform = `translateX(-${index * slideWidth}px)`;
        currentIndex = index;
    }

    function prevSlide() {
        showSlide(currentIndex - 1);
    }

    function nextSlide() {
        showSlide(currentIndex + 1);
    }
}


