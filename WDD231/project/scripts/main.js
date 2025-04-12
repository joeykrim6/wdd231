const api = {
    key: '9e122cd782b2d0333f5fe4e7fa192062',
    url: https://api.openweathermap.org/data/2.5/weather
  }
  
  const card = document.getElementById('card')
  
  const city = document.getElementById('city');
  const date = document.getElementById('date');
  const tempImg = document.getElementById('temp-img');
  const temp = document.getElementById('temp');
  const weather = document.getElementById('weather');
  const range = document.getElementById('range');
  
  function updateImages(data) {
    const temp = toCelsius(data.main.temp);
    let src = 'images/temp-mid.png';
    if (temp > 26) {
      src = 'images/temp-high.png';
    } else if (temp < 20) {
      src = 'images/temp-low.png';
    }
    tempImg.src = src;
  }
  
  async function search(query) {
    try {
      const response = await fetch(${api.url}?q=${query}&appid=${api.key}&lang=en);
      const data = await response.json();
      card.style.display = 'block';
      city.innerHTML = ${data.name}, ${data.sys.country};
      data.innerHTML = (new Date()).toLocaleDateString();
      temp.innerHTML = ${toCelsius(data.main.temp)}c;
      weather.innerHTML = data.weather[0].description;
      range.innerHTML = ${toCelsius(data.main.temp_min)}c / ${toCelsius(data.main.temp_max)}c;
      updateImages(data);
    } catch (err) {
      console.log(err);
      alert('There was an error, pleae try again!');
    }
  }
  
  function toCelsius(kelvin) {
    return Math.round(kelvin - 273.15);
  }
  
  function onSubmit(event) {
    event.preventDefault();
    search(searchbox.value);
  }
  
  const searchform = document.getElementById('search-form');
  const searchbox = document.getElementById('searchbox');
  searchform.addEventListener('submit', onSubmit, true);
  
  
  
  /........ */
  
  document.addEventListener("DOMContentLoaded", () => {
    const dateElement = document.getElementById("date");
  
    // Obtener la fecha actual
    const today = new Date();
    const day = String(today.getDate()).padStart(2, '0');
    const month = String(today.getMonth() + 1).padStart(2, '0'); // Meses empiezan desde 0
    const year = today.getFullYear();
  
    // Formatear la fecha como DD/MM/YYYY
    const formattedDate = ${day}/${month}/${year};
  
    // Actualizar el elemento con la fecha
    dateElement.textContent = formattedDate;
  });