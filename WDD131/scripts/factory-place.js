document.addEventListener("DOMContentLoaded", () => {
    // Update current year and last modified date
    const currentYear = new Date().getFullYear();
    document.getElementById("currentYear").textContent = currentYear;
    document.getElementById("lastModified").textContent = document.lastModified;
  
    // Static temperature and wind speed values
    const temperature = 5; // Celsius
    const windSpeed = 20; // km/h
  
    // Calculate and display wind chill if applicable
    const windChillElement = document.getElementById("windChill");
    const windChill = calculateWindChill(temperature, windSpeed);
    if (windChill) {
      windChillElement.textContent = ${windChill.toFixed(1)} °C;
    } else {
      windChillElement.textContent = "N/A";
    }
  });
  
  // Function to calculate wind chill
  function calculateWindChill(temp, windSpeed) {
    // Wind chill formula for Celsius
    if (temp <= 10 && windSpeed > 4.8) {
      return 13.12 + 0.6215 * temp - 11.37 * Math.pow(windSpeed, 0.16) + 0.3965 * temp * Math.pow(windSpeed, 0.16);
    }
    return null;
  }