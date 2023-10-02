// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function loadView(status) {
  var apiUrl = "/api/login/defaultview";
  if (status === "authview") apiUrl = "/api/login/authview";
  if (status === "error") apiUrl = "/api/login/error";
  if (status === "account") apiUrl = "/api/account/view";
  if (status === "logout") apiUrl = "/api/logout";

  console.log("Hello " + apiUrl);

  fetch(apiUrl)
    .then((response) => {
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      return response.text();
    })
    .then((data) => {
      // Handle the data from the API
      document.getElementById("main").innerHTML = data;
      displayLogout();
    })
    .catch((error) => {
      // Handle any errors that occurred during the fetch
      console.error("Fetch error:", error);
    });
}

function performAuth() {
  var name = document.getElementById("LoginUsername").value;
  var password = document.getElementById("LoginPassword").value;
  var data = {
    Username: name,
    Password: password,
  };
  console.error(data);
  const apiUrl = "/api/login/auth";

  const headers = {
    "Content-Type": "application/json", // Specify the content type as JSON if you're sending JSON data
    // Add any other headers you need here
  };

  const requestOptions = {
    method: "POST",
    headers: headers,
    body: JSON.stringify(data), // Convert the data object to a JSON string
  };

  fetch(apiUrl, requestOptions)
    .then((response) => {
      if (!response.ok) {
        throw new Error("Network response was not ok");
      }
      return response.json();
    })
    .then((data) => {
      // Handle the data from the API
      const jsonObject = data;
      if (jsonObject.login) {
        loadView("authview");
        displayLogout();
      } else {
        loadView("error");
      }
    })
    .catch((error) => {
      // Handle any errors that occurred during the fetch
      console.error("Fetch error:", error);
    });
}

function hasSessionID() {
  const cookies = document.cookie.split(";");
  let result = false
  cookies.forEach((cookie) => {
    if (cookie.trim().startsWith("SessionID")) {
      result = true;
    }
  });
  return result;
}

function displayLogout() {
  console.log("Have SessionID: " + hasSessionID());
      if (hasSessionID()) {
        document.getElementById("LogoutButton").style.display = "block";
        document.getElementById("LoginButton").style.display = "none";
      } else {
        document.getElementById("LogoutButton").style.display = "none";
        document.getElementById("LoginButton").style.display = "block";
      }
}

document.addEventListener("DOMContentLoaded", displayLogout);
// document.addEventListener("DOMContentLoaded", loadView);