/ Get the current year dynamically and display it in the footer
const currentYear = new Date().getFullYear();
document.getElementById('currentyear').textContent = currentYear;

// Get the last modified date of the document and display it in the footer
const lastModifiedDate = document.lastModified;
document.getElementById('lastModified').textContent = 'Last Updated: ' + lastModifiedDate;