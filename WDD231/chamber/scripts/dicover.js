document.addEventListener("DOMContentLoaded", async () => {
    // Visitor Message
    const visitorMessage = document.getElementById("visitor-message");
    const lastVisit = localStorage.getItem("lastVisit");
    const now = Date.now();

    if (!lastVisit) {
        visitorMessage.textContent = "Welcome! Let us know if you have any questions.";
    } else {
        const timeDiff = now - parseInt(lastVisit, 10);
        const daysDiff = Math.floor(timeDiff / (1000 * 60 * 60 * 24));
        if (daysDiff < 1) {
            visitorMessage.textContent = "Back so soon! Are you ready to embark on an unforgettable journey through the heart of Peru? Join our exclusive community of travelers and experience the magic of the Andes, the Amazon, and the Pacific coast. ";
        } else {
            visitorMessage.textContent = You last visited ${daysDiff} day${daysDiff > 1 ? "s" : ""} ago.;
        }
    }
    localStorage.setItem("lastVisit", now);

    // Fetch images from Pexels API and populate the gallery
    const galleryContainer = document.getElementById("gallery-container");
    const API_URL = "https://api.pexels.com/v1/search?query=landscape&per_page=6";
    const API_KEY = "BfIgI2C2VY1DPp2BmIHJPT3LtaAN9JCTVkH3YHkm6P1CPD1v95EbeaKJ";

    async function fetchImages() {
        try {
            const response = await fetch(API_URL, {
                headers: {
                    Authorization: API_KEY,
                },
            });
            if (!response.ok) {
                throw new Error(HTTP error! status: ${response.status});
            }
            const data = await response.json();

            // Populate the gallery
            data.photos.forEach(photo => {
                const imgElement = document.createElement("img");
                imgElement.dataset.src = photo.src.medium; // Use lazy loading
                imgElement.alt = photo.photographer || "Image from Pexels";
                imgElement.classList.add("lazy");
                galleryContainer.appendChild(imgElement);
            });

            // Initialize lazy loading for the fetched images
            const lazyImages = document.querySelectorAll("img.lazy");

            const lazyLoad = (entries, observer) => {
                entries.forEach(entry => {
                    if (entry.isIntersecting) {
                        const img = entry.target;
                        img.src = img.dataset.src;
                        img.onload = () => img.classList.add("lazy-loaded");
                        observer.unobserve(img);
                    }
                });
            };

            const observer = new IntersectionObserver(lazyLoad, { threshold: 0.1 });

            lazyImages.forEach(img => observer.observe(img));
        } catch (error) {
            console.error("Failed to fetch images:", error);
            galleryContainer.innerHTML = "<p>Error loading images. Please try again later.</p>";
        }
    }

    await fetchImages();
});