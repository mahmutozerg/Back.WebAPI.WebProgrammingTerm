document.addEventListener("DOMContentLoaded", function () {
    initializeCarousel("productCarousel");
});

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

        var prevButton = document.createElement('Button');
        prevButton.className = 'carousel-prev-btn';
        prevButton.addEventListener('click', function () {
            prevSlide(container);
        });

        var prevImg = document.createElement('img');
        prevImg.src = '../../Content/WebAssets/arrow_left.svg';
        prevImg.style.width = '40px';
        prevImg.style.height = '40px';
        prevButton.appendChild(prevImg);
        container.appendChild(prevButton);

        var nextButton = document.createElement('Button');
        nextButton.className = 'carousel-next-btn';
        nextButton.addEventListener('click', function () {
            nextSlide(container);
        });

        var nextImg = document.createElement('img');
        nextImg.src = '../../Content/WebAssets/arrow_right.svg';
        nextImg.style.width = '40px';
        nextImg.style.height = '40px';
        nextButton.appendChild(nextImg);
        container.appendChild(nextButton);

        function showSlide(index) {
            var carouselInner = container.querySelector('.product-carousel-inner');
            var slideWidth = container.querySelector('.product-carousel-item').clientWidth;
            var totalSlides = container.querySelectorAll('.product-carousel-item').length;

            if (index < 0) {
                index = totalSlides - 1;
            } else if (index >= totalSlides) {
                index = 0;
            }

            carouselInner.style.transform = `translateX(-${index * slideWidth}px)`;
            currentIndex = index;
        }

        function prevSlide() {
            showSlide(currentIndex - 4);
        }

        function nextSlide() {
            showSlide(currentIndex + 4);
        }
    }
}