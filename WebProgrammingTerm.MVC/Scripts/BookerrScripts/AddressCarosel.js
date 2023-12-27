function initializeCarousel(containerClass) {
    var containers = document.getElementsByClassName(containerClass);

    if (containers.length === 0) {
        console.error("Containers not found:", containerClass);
        return;
    }

    for (var i = 0; i < containers.length; i += 1) {
        setupCarousel(containers[i]);
    }

    function setupCarousel(container) {
        var currentIndex = 0;

        var prevButton = document.createElement('button');
        prevButton.className = 'carousel-prev-btn';
        prevButton.addEventListener('click', function () {
            prevSlide();
        });

        var prevImg = document.createElement('img');
        prevImg.src = '../../Content/WebAssets/arrow_left.svg';
        prevImg.style.width = '40px';
        prevImg.style.height = '40px';
        prevImg.style.opacity = '0.7'
        prevButton.appendChild(prevImg);
        container.appendChild(prevButton);

        var nextButton = document.createElement('button');
        nextButton.className = 'carousel-next-btn';
        nextButton.addEventListener('click', function () {
            nextSlide();
        });

        var nextImg = document.createElement('img');
        nextImg.src = '../../Content/WebAssets/arrow_right.svg';
        nextImg.style.width = '40px';
        nextImg.style.height = '40px';
        nextImg.style.opacity = '0.7'

        nextButton.appendChild(nextImg);
        container.appendChild(nextButton);

        function showSlide(index) {
            var carouselInner = document.querySelector("body > div > div > div.address > div.my_address");
            var slides = document.querySelectorAll("body > div > div > div.address > div.my_address > .addressInfo");



            if (!carouselInner || slides.length === 0) {
                console.error("Carousel elements not found within container:", carouselInner);
                return;
            }

            var totalSlides = slides.length;
            var slideWidth = slides[0].clientWidth;
            

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
}

document.addEventListener("DOMContentLoaded", function () {
    initializeCarousel("address");
});
