:root {
    --primary-color: #800000;
    --secondary-color: #004225;
    --background-color: #f5f5f5;
    --text-color: white;
    --overlay-color: rgba(0, 0, 0, 0.5);
  }
  
  * {
    margin: 0;
    padding: 0;
    box-sizing: border-box;
  }
  
  body {
    font-family: 'Arial', sans-serif;
    background-color: var(--background-color);
    color: var(--text-color);
    display: flex;
    flex-direction: column;
    min-height: 100vh;
  }
  
  header {
    background-color: var(--primary-color);
    padding: 1em;
    text-align: center;
  }
  
  h1 {
    font-size: 2.5rem;
  }
  
  main {
    flex: 1;
    position: relative;
  }
  
  .cuzco img {
    width: 100%;
    height: 100vh;
    object-fit: cover;
    position: absolute;
    top: 0;
    left: 0;
    z-index: 1;
  }
  
  .data, .weather {
    position: absolute;
    background-color: var(--overlay-color);
    padding: 1.5em;
    border-radius: 10px;
    z-index: 2;
  }
  
  .data {
    top: 20px;
    left: 20px;
    width: 30%;
  }
  
  .weather {
    bottom: 60px;
    right: 20px;
    width: 30%;
  }
  
  h2 {
    background-color: var(--secondary-color);
    padding: 0.5em;
    border-radius: 5px;
    text-align: center;
  }
  
  ul {
    list-style: none;
    margin-top: 1em;
  }
  
  footer {
    background-color: var(--primary-color);
    text-align: center;
    padding: 2em;
    color: var(--text-color);
    position: absolute;
    bottom: 0;
    left: 0;
    width: 100%;
    z-index: 2;
  }
  
  @media (max-width: 768px) {
    .data, .weather {
      width: 80%;
      left: 50%;
      transform: translateX(-50%);
    }
  
    .data {
      top: 10px;
    }
  
    .weather {
      bottom: 10px;
    }
  }
  
  
  .foot {
    margin-bottom: -8%;
  }