function updateTotalCredits() {
    const courses = document.querySelectorAll('.course'); // Get all course buttons
    const totalCredits = Array.from(courses)
      .filter(course => course.style.display === 'inline-block') // Filter only visible courses
      .reduce((total, course) => {
        const credits = parseInt(course.getAttribute('data-credits'), 10) || 0; // Get credits from 'data-credits' attribute
        return total + credits;
      }, 0);
  
    // Display the total credits in the DOM
    const creditDisplay = document.querySelector('#total-credits');
    if (creditDisplay) {
      creditDisplay.textContent = Total Credits: ${totalCredits};
    }
  }
  
  /*Function to filter my courses */
  function filterCourses(category) {
    const courses = document.querySelectorAll('.course');
    
    courses.forEach(course => {
      if (category === 'all') {
        course.style.display = 'inline-block';
      } else {
        course.style.display = course.classList.contains(category) ? 'inline-block' : 'none';
      }
    });
  
    // Update the total credits
    updateTotalCredits();
  }
  
  // Add event listeners to display individual course credits
  document.querySelectorAll('.course').forEach(course => {
    course.addEventListener('click', () => {
      const credits = course.getAttribute('data-credits');
      const courseName = course.textContent;
  
      // Display the individual credits in the span
      const individualCreditDisplay = document.querySelector('#individual-credits');
      if (individualCreditDisplay) {
        individualCreditDisplay.textContent = / - Selected Course: ${courseName}, Credits: ${credits};
      }
    });
  });
  
  // Display current year in the footer
  document.getElementById("currentyear").textContent = new Date().getFullYear();
  
  // Display the last modified date in the footer
  document.getElementById("lastModified").textContent = Last Modification: ${document.lastModified};
  
  
  
  
  
  
  // function updateTotalCredits() {
  //   const courses = document.querySelectorAll('.course'); // Get all course buttons
  //   const totalCredits = Array.from(courses)
  //     .filter(course => course.style.display === 'inline-block') // Filter only visible courses
  //     .reduce((total, course) => {
  //       const credits = parseInt(course.getAttribute('data-credits'), 10) || 0; // Get credits from 'data-credits' attribute
  //       return total + credits;
  //     }, 0);
  
  //   // Display the total credits in the DOM
  //   const creditDisplay = document.querySelector('#total-credits'); // Assuming you have a span or div with this ID
  //   if (creditDisplay) {
  //     creditDisplay.textContent = Total Credits: ${totalCredits};
  //   }
  // }
  
  
  
  
  // function filterCourses(category) {
  //   const courses = document.querySelectorAll('.course');
    
  //   courses.forEach(course => {
  //     if (category === 'all') {
  //       course.style.display = 'inline-block';
  //     } else {
  //       course.style.display = course.classList.contains(category) ? 'inline-block' : 'none';
  //     }
  //   });
  
  //   // Call the function to update the credit total
  //   updateTotalCredits();
  // }
  
  
  
  
  
  
  
  
  
  
  
  // // Function to filter courses based on category
  // function filterCourses(category) {
  //     const courses = document.querySelectorAll('.course');
    
  //     courses.forEach(course => {
  //       if (category === 'all') {
  //         course.style.display = 'inline-block';
  //       } else {
  //         course.style.display = course.classList.contains(category) ? 'inline-block' : 'none';
  //       }
  //     });
  //   }
  
  //   // Display current date in the footer
  // // Get the current year for the footer
  // document.getElementById("currentyear").textContent = new Date().getFullYear();
  
  // // Get the last modified date for the footer
  // document.getElementById("lastModified").textContent = Last Modification: ${document.lastModified};