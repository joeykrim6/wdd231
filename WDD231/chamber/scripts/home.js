const apiKey = "d7d2608c68805ea70eedc4949b1dbe69";
const city = "Lima"; // Replace with your chamber location
const membersURL = "data/members.json";

// Weather API
async function fetchWeather() {
    const response = await fetch(
        https://api.openweathermap.org/data/2.5/forecast?q=${city}&units=imperial&appid=${apiKey}
    );
    const data = await response.json();

    document.getElementById("weather-location").textContent = Location: ${data.city.name};
    document.getElementById("weather-temp").textContent = Temperature: ${data.list[0].main.temp.toFixed(1)}°F;
    document.getElementById("weather-condition").textContent = Condition: ${data.list[0].weather[0].description};

    const forecast = document.getElementById("forecast");
    forecast.innerHTML = "";
    for (let i = 0; i < 3; i++) {
        const day = data.list[i * 8]; // Every 8th index ~ 24 hrs
        const li = document.createElement("li");
        li.textContent = Day ${i + 1}: ${day.main.temp.toFixed(1)}°F - ${day.weather[0].description};
        forecast.appendChild(li);
    }
}

// Spotlights
async function loadSpotlights() {
    const response = await fetch(membersURL);
    const members = await response.json();

    const eligibleMembers = members.filter(
        (member) => member.membership === "Gold" || member.membership === "Silver"
    );
    const randomMembers = eligibleMembers
        .sort(() => 0.5 - Math.random())
        .slice(0, 3);

    const container = document.getElementById("spotlights-container");
    container.innerHTML = "";

    randomMembers.forEach((member) => {
        const div = document.createElement("div");
        div.classList.add("spotlight");

        div.innerHTML = `
            <img src="${member.logo}" alt="${member.name} logo">
            <h3>${member.name}</h3>
            <p>Phone: ${member.phone}</p>
            <p>Address: ${member.address}</p>
            <p>Website: <a href="${member.website}" target="_blank">${member.website}</a></p>
            <p>Membership: ${member.membership}</p>
        `;
        container.appendChild(div);
    });
}

// Initialize page
function init() {
    fetchWeather();
    loadSpotlights();

    document.getElementById("lastModified").textContent = Last Updated: ${document.lastModified};
    document.getElementById("year").textContent = new Date().getFullYear();
}

document.addEventListener("DOMContentLoaded", init);