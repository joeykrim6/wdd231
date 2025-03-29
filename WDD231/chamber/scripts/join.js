// JavaScript functions for handling modals and dynamic content

// Function to show a modal
function showModal(modalId) {
    const modal = document.getElementById(modalId);
    if (modal) {
        modal.style.display = 'block';
    }
}

// Function to close a modal
function closeModal(modalId) {
    const modal = document.getElementById(modalId);
    if (modal) {
        modal.style.display = 'none';
    }
}

// Function to update the last modified date in the footer
function updateLastModified() {
    const lastModified = document.getElementById('lastModified');
    if (lastModified) {
        lastModified.textContent = document.lastModified;
    }
}

// Function to update the current year in the footer
function updateYear() {
    const year = document.getElementById('year');
    if (year) {
        const currentYear = new Date().getFullYear();
        year.textContent = currentYear;
    }
}

// Add event listeners for page load
window.addEventListener('load', () => {
    updateLastModified();
    updateYear();
});