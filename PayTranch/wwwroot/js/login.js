(function () {
  // Expose for inline onclick in Login.cshtml
  window.togglePwd = function () {
    const input = document.getElementById("password");
    const btn = document.getElementById("pwdBtn");
    if (!input || !btn) return;

    if (input.type === "password") {
      input.type = "text";
      btn.textContent = "Masquer";
    } else {
      input.type = "password";
      btn.textContent = "Voir";
    }
  };

  const emailInput = document.getElementById("email");
  if (emailInput) {
    emailInput.addEventListener("input", function () {
      this.style.borderColor = "";
    });
  }

  const form = document.querySelector("form[action*='/Account/Login']");
  const loginBtn = document.getElementById("loginBtn");
  if (form && loginBtn) {
    form.addEventListener("submit", function () {
      loginBtn.classList.add("loading");
      loginBtn.disabled = true;
    });
  }
})();