document.addEventListener("DOMContentLoaded", function () {
  showCookieConsentIfNeeded();
});

function showCookieConsentIfNeeded() {
  const hasAcceptedCookies = localStorage.getItem("hasAcceptedCookies");

  if (!hasAcceptedCookies) {
    showCookieConsent();
  }
}

function showCookieConsent() {
  const cookieConsent = document.getElementById("cookie-consent");
  cookieConsent.style.display = "block";

  const acceptCookiesButton = document.getElementById("accept-cookies");
  acceptCookiesButton.addEventListener("click", function () {
    acceptCookies();
  });
}

function acceptCookies() {
  const cookieConsent = document.getElementById("cookie-consent");
  cookieConsent.style.display = "none";

  // Set a flag in localStorage to remember that the user has accepted cookies
  localStorage.setItem("hasAcceptedCookies", true);
}
