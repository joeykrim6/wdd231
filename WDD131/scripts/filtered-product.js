document.addEventListener("DOMContentLoaded", () => {
    const hamburger = document.querySelector('.hamburger');
    const navLinks = document.querySelector('.nav-links');
    const lastModified = document.getElementById("lastModified");
    const currentYear = document.getElementById("currentyear");
  
    // Toggle nav menu on hamburger click
    hamburger.addEventListener('click', () => {
        navLinks.classList.toggle('show');
        hamburger.textContent = navLinks.classList.contains('show') ? '✖' : '☰';
    });
  
    // Update footer with last modified date
    lastModified.textContent = Last Modified: ${document.lastModified};
  
    // Update footer with current year
    currentYear.textContent = new Date().getFullYear();
  
    // Handle zoom responsivity (resize event listener)
    window.addEventListener('resize', () => {
        if (window.innerWidth >= 768) {
            navLinks.classList.remove('show');
            hamburger.textContent = '☰';
        }
    });
  });
  
  const products = [
    {
      productName: "Coca-Cola Can",
      description: "330 ml can of Coca-Cola, refreshing and fizzy.",
      productionDate: "2023, February, 8",
      capacity: 330,
      imageUrl: "data:image/jpeg;base64,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",
      altText: "330 ml can of Coca-Cola"
    },
    {
      productName: "Pepsi Can",
      description: "330 ml can of Pepsi, delicious cola flavor.",
      productionDate: "2023, January, 10",
      capacity: 330,
      imageUrl: "data:image/jpeg;base64,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",
      altText: "330 ml can of Pepsi"
    },
    {
      productName: "Red Bull Can",
      description: "250 ml can of Red Bull, energy drink to boost your day.",
      productionDate: "2022, December, 5",
      capacity: 250,
      imageUrl: "data:image/jpeg;base64,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",
      altText: "250 ml can of Red Bull"
    },
    {
      productName: "Sprite Can",
      description: "330 ml can of Sprite, lemon-lime soda for a refreshing taste.",
      productionDate: "2022, July, 22",
      capacity: 330,
      imageUrl: "https://caffeinecam.com/cdn/shop/files/f88b8acf95b21f97bd9ab007b42488979bcaa42f84106ed1efe11337f8039493__80768.1614056584.1280.1280.jpg?v=1689679486",
      altText: "330 ml can of Sprite"
    },
    {
      productName: "Fanta Orange Can",
      description: "330 ml can of Fanta Orange, fruity and fun.",
      productionDate: "2022, October, 30",
      capacity: 330,
      imageUrl: "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcSt3wlc8MmQWHD5E5I2-O97fuzU3mCevMK9oA&s",
      altText: "330 ml can of Fanta Orange"
    }
  ];
  
  
  
  function createProductCard(productsArray) {
  const gallery = document.querySelector(".gallery");
  gallery.innerHTML = ""; // Clear the gallery before generating new cards
  
  productsArray.forEach(product => {
    let card = document.createElement("section");
    let name = document.createElement("h3");
    let description = document.createElement("p");
    let productionDate = document.createElement("p");
    let capacity = document.createElement("p");
    let img = document.createElement("img");
  
    name.textContent = product.productName;
    description.innerHTML = <span class="label">Description:</span> ${product.description};
    productionDate.innerHTML = <span class="label">Production Date:</span> ${product.productionDate};
    capacity.innerHTML = <span class="label">Capacity:</span> ${product.capacity} ml;
    img.setAttribute("src", product.imageUrl);
    img.setAttribute("alt", ${product.productName} Product);
    img.setAttribute("loading", "lazy");
  
    card.appendChild(name);
    card.appendChild(description);
    card.appendChild(productionDate);
    card.appendChild(capacity);
    card.appendChild(img);
  
    gallery.appendChild(card);
  });
  }
  
  // Initial call to load all product cards
  createProductCard(products);
  
  // Search functionality
  const searchInput = document.getElementById("search");
  searchInput.addEventListener("input", function () {
  const searchTerm = searchInput.value.toLowerCase();
  const filteredProducts = products.filter(product =>
    product.productName.toLowerCase().includes(searchTerm) ||
    product.description.toLowerCase().includes(searchTerm)
  );
  createProductCard(filteredProducts); // Display filtered results
  });
  
  // Filter functionality
  const filterButtons = document.querySelectorAll(".filter-btn");
  filterButtons.forEach(button => {
  button.addEventListener("click", function () {
    const filter = button.getAttribute("data-filter");
    let filteredProducts;
    
    if (filter === "old") {
      filteredProducts = products.filter(product => new Date(product.productionDate).getFullYear() < 2020);
    } else if (filter === "new") {
      filteredProducts = products.filter(product => new Date(product.productionDate).getFullYear() >= 2020);
    } else if (filter === "large") {
      filteredProducts = products.filter(product => product.capacity > 1000);
    } else if (filter === "small") {
      filteredProducts = products.filter(product => product.capacity <= 1000);
    } else {
      filteredProducts = products;
    }
  
    createProductCard(filteredProducts); // Display filtered products
  });
  });
  
  
  // document.addEventListener("DOMContentLoaded", () => {
  //     const hamburger = document.querySelector('.hamburger');
  //     const navLinks = document.querySelector('.nav-links');
  //     const lastModified = document.getElementById("lastModified");
  //     const currentYear = document.getElementById("currentyear");
  
  //     // Toggle nav menu on hamburger click
  //     hamburger.addEventListener('click', () => {
  //         navLinks.classList.toggle('show');
  //         hamburger.textContent = navLinks.classList.contains('show') ? '✖' : '☰';
  //     });
  
  //     // Update footer with last modified date
  //     lastModified.textContent = Last Modified: ${document.lastModified};
  
  //     // Update footer with current year
  //     currentYear.textContent = new Date().getFullYear();
  
  //     // Handle zoom responsivity (resize event listener)
  //     window.addEventListener('resize', () => {
  //         if (window.innerWidth >= 768) {
  //             navLinks.classList.remove('show');
  //             hamburger.textContent = '☰';
  //         }
  //     });
  // });
  
  // const temples = [
  //   {
  //     templeName: "Aba Nigeria",
  //     location: "Aba, Nigeria",
  //     dedicated: "2005, August, 7",
  //     area: 11500,
  //     imageUrl:
  //     "https://content.churchofjesuschrist.org/templesldsorg/bc/Temples/photo-galleries/aba-nigeria/400x250/aba-nigeria-temple-lds-273999-wallpaper.jpg"
  //   },
  //   {
  //     templeName: "Manti Utah",
  //     location: "Manti, Utah, United States",
  //     dedicated: "1888, May, 21",
  //     area: 74792,
  //     imageUrl:
  //     "https://content.churchofjesuschrist.org/templesldsorg/bc/Temples/photo-galleries/manti-utah/400x250/manti-temple-768192-wallpaper.jpg"
  //   },
  //   {
  //     templeName: "Payson Utah",
  //     location: "Payson, Utah, United States",
  //     dedicated: "2015, June, 7",
  //     area: 96630,
  //     imageUrl:
  //     "https://content.churchofjesuschrist.org/templesldsorg/bc/Temples/photo-galleries/payson-utah/400x225/payson-utah-temple-exterior-1416671-wallpaper.jpg"
  //   },
  //   {
  //     templeName: "Yigo Guam",
  //     location: "Yigo, Guam",
  //     dedicated: "2020, May, 2",
  //     area: 6861,
  //     imageUrl:
  //     "https://content.churchofjesuschrist.org/templesldsorg/bc/Temples/photo-galleries/yigo-guam/400x250/yigo_guam_temple_2.jpg"
  //   },
  //   {
  //     templeName: "Washington D.C.",
  //     location: "Kensington, Maryland, United States",
  //     dedicated: "1974, November, 19",
  //     area: 156558,
  //     imageUrl:
  //     "https://content.churchofjesuschrist.org/templesldsorg/bc/Temples/photo-galleries/washington-dc/400x250/washington_dc_temple-exterior-2.jpeg"
  //   },
  //   {
  //     templeName: "Lima Perú",
  //     location: "Lima, Perú",
  //     dedicated: "1986, January, 10",
  //     area: 9600,
  //     imageUrl:
  //     "https://content.churchofjesuschrist.org/templesldsorg/bc/Temples/photo-galleries/lima-peru/400x250/lima-peru-temple-evening-1075606-wallpaper.jpg"
  //   },
  //   {
  //     templeName: "Mexico City Mexico",
  //     location: "Mexico City, Mexico",
  //     dedicated: "1983, December, 2",
  //     area: 116642,
  //     imageUrl:
  //     "https://content.churchofjesuschrist.org/templesldsorg/bc/Temples/photo-galleries/mexico-city-mexico/400x250/mexico-city-temple-exterior-1518361-wallpaper.jpg"
  //   },
  //   // Add more temple objects here...
  // ];
  
  // function createTempleCard(templesArray) {
  //   const gallery = document.querySelector(".gallery");
  //   gallery.innerHTML = ""; // Limpiamos la galería antes de generar nuevas tarjetas
  
  //   templesArray.forEach(temple => {
  //     let card = document.createElement("section");
  //     let name = document.createElement("h3");
  //     let location = document.createElement("p");
  //     let dedication = document.createElement("p");
  //     let area = document.createElement("p");
  //     let img = document.createElement("img");
  
  //     name.textContent = temple.templeName;
  //     location.innerHTML = <span class="label">Location:</span> ${temple.location};
  //     dedication.innerHTML = <span class="label">Dedicated:</span> ${temple.dedicated};
  //     area.innerHTML = <span class="label">Size:</span> ${temple.area} sq ft;
  //     img.setAttribute("src", temple.imageUrl);
  //     img.setAttribute("alt", ${temple.templeName} Temple);
  //     img.setAttribute("loading", "lazy");
  
  //     card.appendChild(name);
  //     card.appendChild(location);
  //     card.appendChild(dedication);
  //     card.appendChild(area);
  //     card.appendChild(img);
  
  //     gallery.appendChild(card);
  //   });
  // }
  
  // // Llamada inicial para cargar todas las tarjetas de templos
  // createTempleCard(temples);
  
  // // Función de búsqueda
  // const searchInput = document.getElementById("search");
  // searchInput.addEventListener("input", function () {
  //   const searchTerm = searchInput.value.toLowerCase();
  //   const filteredTemples = temples.filter(temple =>
  //     temple.templeName.toLowerCase().includes(searchTerm) ||
  //     temple.location.toLowerCase().includes(searchTerm)
  //   );
  //   createTempleCard(filteredTemples); // Mostrar resultados filtrados
  // });
  
  // // Funcionalidad para el botón de filtro
  // const filterButtons = document.querySelectorAll(".filter-btn");
  // filterButtons.forEach(button => {
  //   button.addEventListener("click", function () {
  //     const filter = button.getAttribute("data-filter");
  //     let filteredTemples;
      
  //     if (filter === "old") {
  //       filteredTemples = temples.filter(temple => new Date(temple.dedicated).getFullYear() < 2000);
  //     } else if (filter === "new") {
  //       filteredTemples = temples.filter(temple => new Date(temple.dedicated).getFullYear() >= 2000);
  //     } else if (filter === "large") {
  //       filteredTemples = temples.filter(temple => temple.area > 50000);
  //     } else if (filter === "small") {
  //       filteredTemples = temples.filter(temple => temple.area <= 50000);
  //     } else {
  //       filteredTemples = temples;
  //     }
  
  //     createTempleCard(filteredTemples); // Mostrar los templos filtrados
  //   });
  // });