
const storedUser = {
    username: "admin",
    password: "password123"
};

document.getElementById("loginForm").addEventListener("submit", function(event) {
    event.preventDefault(); 

    const username = document.getElementById("username").value;
    const password = document.getElementById("password").value;
    const errorMessage = document.getElementById("errorMessage");

    if (username === storedUser.username && password === storedUser.password) {
        window.location.href = "success.html";
    } else {
        errorMessage.textContent = "Invalid username or password";
    }
});
