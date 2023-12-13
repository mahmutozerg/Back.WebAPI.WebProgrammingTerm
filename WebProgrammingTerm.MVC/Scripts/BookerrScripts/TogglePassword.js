function togglePasswordVisibility(inputId) {
    var passwordInput = document.getElementById(inputId);

    passwordInput.type = (passwordInput.type === 'password') ? 'text' : 'password';
}