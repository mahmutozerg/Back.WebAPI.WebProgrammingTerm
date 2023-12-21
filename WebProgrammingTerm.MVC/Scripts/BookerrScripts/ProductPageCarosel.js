
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

        // Add thumbnails
        var thumbnailContainer = document.createElement('div');
        thumbnailContainer.className = 'thumbnail-container';
        container.appendChild(thumbnailContainer);

        var slides = container.querySelectorAll('.product-carousel-item');
        console.log(slides.length);
        for (var i = 0; i < slides.length; i++) {
            var thumbnail = document.createElement('img');
            thumbnail.className = 'thumbnail';
            thumbnail.src = slides[i].querySelector('img').src; // Use the same image source for thumbnails
            thumbnail.addEventListener('click', function (index) {
                return function () {
                    showSlide(container, index);
                };
            }(i));
            thumbnailContainer.appendChild(thumbnail);
        }

        function showSlide(container, index) {
            var carouselInner = container.querySelector('.product-carousel-inner');
            var thumbnails = container.querySelectorAll('.thumbnail');

            if (index < 0) {
                index = slides.length - 1;
            } else if (index >= slides.length) {
                index = 0;
            }

            for (var i = 0; i < slides.length; i++) {
                slides[i].style.display = i === index ? 'block' : 'none';
                thumbnails[i].classList.toggle('active', i === index);
            }

            currentIndex = index;
        }

        function prevSlide(container) {
            showSlide(container, currentIndex - 1);
            removeThumbnail(currentIndex);
        }

        function nextSlide(container) {
            showSlide(container, currentIndex + 1);
            removeThumbnail(currentIndex);
        }

        // Initial display
        showSlide(container, currentIndex);
    }

    function removeThumbnail(index) {
        // Function to remove the thumbnail based on the index
        var thumbnails = container.querySelectorAll('.thumbnail');
        if (thumbnails.length > index) {
            thumbnails[index].parentNode.removeChild(thumbnails[index]);
        }
    }
}
