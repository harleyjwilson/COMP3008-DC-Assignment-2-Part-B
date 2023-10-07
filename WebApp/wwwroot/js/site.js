// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function loadView(status) {
  var apiUrl = "";
  if (status === "login") apiUrl = "/api/login/defaultview";
  if (status === "authview") apiUrl = "/api/login/authview";
  if (status === "error") apiUrl = "/api/login/error";
  if (status === "adminlogin") apiUrl = "/api/adminlogin/defaultview";
  if (status === "adminauthview") apiUrl = "/api/adminlogin/authview";
  if (status === "adminerror") apiUrl = "/api/adminlogin/error";
  if (status === "account") apiUrl = "/api/account/view";
  if (status === "admin") apiUrl = "/api/admin/view";
  if (status === "adminusers") apiUrl = "/api/admin/users";
  // if (status === "admintransactions") apiUrl = "/api/admin/transactions";
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
        
      } else {
        loadView("error");
      }
      displayLogout();
    })
    .catch((error) => {
      // Handle any errors that occurred during the fetch
      console.error("Fetch error:", error);
    });
}

function performAdminAuth() {
    var name = document.getElementById("LoginUsername").value;
    var password = document.getElementById("LoginPassword").value;
    var data = {
        Username: name,
        Password: password,
    };
    console.error(data);
    const apiUrl = "/api/adminlogin/auth";

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
                loadView("adminauthview");
                displayLogout();
            } else {
                loadView("adminerror");
            }
        })
        .catch((error) => {
            // Handle any errors that occurred during the fetch
            console.error("Fetch error:", error);
        });
}

function updateAdminContact() {
  var phone = document.getElementById("AdminPhone").value;
  var email = document.getElementById("AdminEmail").value;
  
   var data = {
     Phone: phone,
     Email: email,
  };
   console.error(data);
   const apiUrl = "/api/admin/contact";

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
         loadView("admin");
         displayLogout();
       } else {
         loadView("admin");
       }
     })
     .catch((error) => {
       // Handle any errors that occurred during the fetch
       console.error("Fetch error:", error);
     });
}

function updateAdminPassword() { 
  var password1 = document.getElementById("AdminPassword1").value;
  var password2 = document.getElementById("AdminPassword2").value;
  if (password1 != password2) {
    alert("Passwords do not match!");
    return;
  }
  var data = {
    Password: password1,
  };
  console.error(data);
  const apiUrl = "/api/admin/password";

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
        loadView("admin");
        displayLogout();
      } else {
        loadView("admin");
      }
    })
    .catch((error) => {
      // Handle any errors that occurred during the fetch
      console.error("Fetch error:", error);
    });
}

function adminCreateUser() {
 var username = document.getElementById("CreateUsername").value;
 var name = document.getElementById("CreateName").value;
 var email = document.getElementById("CreateEmail").value;
 var address = document.getElementById("CreateAddress").value;
 var phone = document.getElementById("CreatePhone").value;
 var password = document.getElementById("CreatePassword").value;

 var data = {
   Username: username,
   Name: name === "" ? null : name,
   Email: email === "" ? null : email,
   Address: address === "" ? null : address,
   Phone: phone === "" ? null : phone,
   Picture: null,
   Password: password === "" ? null : password,
 };
 console.error(data);
 const apiUrl = "/api/admin/users/create";

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
     //  const jsonObject = data;
     if (data.success) {
       console.log('New user created.');
     } else {
       console.log('Error creating new user');
     }
     loadView("adminusers");
   })
   .catch((error) => {
     // Handle any errors that occurred during the fetch
     console.error("Fetch error:", error);
   });
}

function adminEditUser() {
  var username = document.getElementById("EditUsername").value;
  var name = document.getElementById("EditName").value;
  var email = document.getElementById("EditEmail").value;
  var address = document.getElementById("EditAddress").value;
  var phone = document.getElementById("EditPhone").value;
  var picture = null;
  var data = {
   Username: username,
   Name: name === "" ? null : name,
   Email: email === "" ? null : email,
   Address: address === "" ? null : address,
   Phone: phone === "" ? null : phone,
   Picture: picture === "" ? null : picture,
 };
  console.error(data);
  const apiUrl = "/api/admin/users/edit";

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
      if (data.success) {
        console.log("User edited.");
      } else {
        console.log("Error in editing user.");
      }
      loadView("adminusers");
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
      if (hasSessionID()) {
        document.getElementById("LogoutButton").style.display = "block";
        document.getElementById("LoginButton").style.display = "none";
      } else {
        document.getElementById("LogoutButton").style.display = "none";
        document.getElementById("LoginButton").style.display = "block";
      }
}

function showEditForm() {
    console.log("Button Clicked!"); // Log a message to the console when the function is called
    document.getElementById("editProfileForm").style.display = "block";
    document.getElementById("editProfileBtn").style.display = "none";
}

function updateProfile() {
    const username = document.getElementById("Username").value;
    const name = document.getElementById("name").value;
    const email = document.getElementById("email").value;
    const phone = document.getElementById("phone").value;
    const address = document.getElementById("address").value;
    const password = document.getElementById("password").value;
    const sessionID = document.getElementById("sessionID").value;
    const picture = document.getElementById("picture").value;

    const updatedUser = {
        Username: username,
        Name: name,
        Email: email,
        Phone: phone,
        Address: address,
        Password: password,
        SessionID: sessionID,
        Picture: picture
    };

    const apiUrl = `http://localhost:5181/api/Users/${username}`; // Replace with the actual URL of your WebAPI

    const headers = {
        "Content-Type": "application/json",
    };

    const requestOptions = {
        method: "PUT",
        headers: headers,
        body: JSON.stringify(updatedUser),
    };

    fetch(apiUrl, requestOptions)
        .then((response) => {
            if (!response.ok) {
                throw new Error("Network response was not ok");
            }
            // Check if there is any JSON to parse
            const contentType = response.headers.get("content-type");
            if (contentType && contentType.indexOf("application/json") !== -1) {
                return response.json();
            } else {
                // No JSON to parse, return a default value or empty object
                return {};
            }
        })
        .then((data) => {
            
            console.log("Profile updated successfully:", data);
            alert("Profile updated successfully!"); // Notify user
            loadView('account'); // Reload account view
        })
        .catch((error) => {
            // Handle any errors that occurred during the fetch
            console.error("Fetch error:", error);
            alert("Failed to update profile: " + error.message); // Notify user
        });
}

function showTransferForm() {
    document.getElementById('transferMoneyForm').style.display = 'block';
}

function transferMoney() {
    var fromAccount = document.getElementById('fromAccount').value;
    var toAccount = document.getElementById('toAccount').value;
    var amount = document.getElementById('amount').value;
    var description = document.getElementById('description').value;

    // Validate input values on client side (Optional but recommended)
    if (!fromAccount || !toAccount || !amount) {
        alert('All fields are required!');
        return;
    }
    if (isNaN(amount) || amount <= 0) {
        alert('Please enter a valid amount!');
        return;
    }

    // Send a POST request to your API
    fetch('http://localhost:5181/api/bankaccounts/transfer', {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json'
        },
        body: JSON.stringify({
            FromAccountNumber: fromAccount,
            ToAccountNumber: toAccount,
            Amount: amount,
            Description: description
        })
    })
        .then(response => {
            if (!response.ok) {
                return response.text().then(text => { throw new Error(text) });
            }
            return response.json();
        })
        .then(data => {
            // Handle response from server
            alert('Money transferred successfully!');
            // Optionally: Update UI, show a success message, etc.
        })
        .catch((error) => {
            // Handle error, show an error message, etc.
            console.error('Error:', error);
            alert('Error: ' + error.message);
        });
}

var sortAscending = true;

function fetchAndDisplayTransactions() {
    var selectedAccountNumber = document.getElementById("accountSelector").value;

    fetch(`http://localhost:5181/api/BankAccounts/${selectedAccountNumber}/transactions`)
        .then(response => {
            if (!response.ok) {
                throw new Error("Network response was not ok");
            }
            return response.json();
        })
        .then(transactions => {
            displayTransactions(transactions);
        })
        .catch(error => {
            console.error("Fetch error:", error);
            document.getElementById("message").innerText = `Failed to fetch transaction history: ${error.message}`;
        });
}

function displayTransactions(transactions) {
    // Display transactions to user
    let transactionHistoryDiv = document.getElementById("transactionHistory");
    transactionHistoryDiv.innerHTML = ""; // Clear any existing transaction history

    let table = document.createElement("table");
    table.setAttribute("id", "transactionTable");
    table.setAttribute("class", "transaction-table");

    let headerRow = document.createElement("tr");

    ["Amount", "From Account", "To Account", "Description", "Timestamp"].forEach(headerText => {
        let header = document.createElement("th");
        header.textContent = headerText;
        headerRow.appendChild(header);
    });

    // Add a "Sort by Date" button
    let sortButton = document.createElement("button");
    sortButton.textContent = "Sort by Date";
    sortButton.addEventListener("click", function () {
        sortAscending = !sortAscending; // Toggle between ascending and descending
        transactions.sort((a, b) => {
            const dateA = new Date(a.timestamp);
            const dateB = new Date(b.timestamp);
            return sortAscending ? dateA - dateB : dateB - dateA;
        });
        displayTransactions(transactions);
    });

    transactionHistoryDiv.appendChild(sortButton);
    table.appendChild(headerRow);

    transactions.forEach(transaction => {
        let row = document.createElement("tr");
        [transaction.amount, transaction.fromAccountNumber, transaction.toAccountNumber, transaction.description, transaction.timestamp].forEach(text => {
            let cell = document.createElement("td");
            cell.textContent = text;
            row.appendChild(cell);
        });
        table.appendChild(row);
    });

    transactionHistoryDiv.appendChild(table);
}

// Sorting function
function sortTable(data, column) {
    data.sort((a, b) => {
        if (a[column] < b[column]) {
            return -1;
        }
        if (a[column] > b[column]) {
            return 1;
        }
        return 0;
    });
}


document.addEventListener("DOMContentLoaded", displayLogout);
// document.addEventListener("DOMContentLoaded", loadView);
document.addEventListener("DOMContentLoaded", function () {
    document.getElementById('showTransferBtn').addEventListener('click', showTransferForm);
    document.getElementById('performTransferBtn').addEventListener('click', transferMoney);
});
