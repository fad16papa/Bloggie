//AdminUser
const buttonCancelUserCreateElement = document.getElementById(
  "btn-userCreateCancel"
);
const buttonCreateUserElement = document.getElementById("btn-userCreate");
const errorMessageDiv = document.getElementById("errorMessages");

// Initialize Bootstrap Tooltip
var tooltipTriggerList = [].slice.call(
  document.querySelectorAll('[data-bs-toggle="tooltip"]')
);
var tooltipList = tooltipTriggerList.map(function (tooltipTriggerEl) {
  return new bootstrap.Tooltip(tooltipTriggerEl);
});

// Get the anti-forgery token value from the cookie
var antiForgeryToken = $('input[name="__RequestVerificationToken"]').val();

async function postData() {
  // Serialize the form data
  var formData = $("#userCreateForm").serialize();

  try {
    // Make an asynchronous AJAX request using async/await
    const result = await $.ajax({
      url: "/AdminUsers/List",
      method: "POST",
      headers: {
        RequestVerificationToken: antiForgeryToken,
      },
      data: formData,
    });

    if (result.success) {
      // Process success, close the modal, etc.
      $("#modalCreateUser").modal("hide");
      window.location.href = "/AdminUsers/List";
    } else {
      // Process errors and update the modal with error messages
      displayErrors(result.errors);
    }
  } catch (error) {
    // Handle the error response
  }
}

function displayErrors(errors) {
  // Assuming you have a div with id="errorMessages" in your modal
  var errorContainer = $("#errorMessages");
  errorContainer.empty();

  $.each(errors, function (fieldName, fieldErrors) {
    var errorHtml =
      '<p id="errorMessage" class="text-danger">' + fieldErrors + "</p>";
    errorContainer.append(errorHtml);
  });

  // Show the modal with errors
  $("#modalCreateUser").modal("show");
}

// Attach the async function to the form submission event
$(document).ready(function () {
  $("#userCreateForm").submit(function (e) {
    e.preventDefault();
    postData();
  });
});

function removeInputDetails() {
  errorMessageDiv.remove();
  $("#userCreateForm").trigger("reset");
}

buttonCancelUserCreateElement.addEventListener("click", removeInputDetails);
buttonCreateUserElement.addEventListener("click", postData);
