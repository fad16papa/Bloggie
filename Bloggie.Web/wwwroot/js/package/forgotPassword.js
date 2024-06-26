// forgotPassword.js
$("#sendRequestChangePassword").submit(function (event) {
  event.preventDefault(); // Prevent default form submission

  // Disable submit button to prevent multiple clicks
  $("#btnForgetPassword").prop("disabled", true);

  // Show loading spinner
  $("#btnForgetPassword .spinner-border").removeClass("d-none");

  // Check if a submission is already in progress
  if ($(this).data("isSubmitting")) {
    // If a submission is in progress, return without making a new request
    return;
  }

  // Clear input on modal close
  $("#successEmailSentModal").on("hidden.bs.modal", function () {
    $("#email").val("");
  });

  // Set flag to indicate submission is in progress
  $(this).data("isSubmitting", true);

  // Make AJAX request
  $.ajax({
    url: "/Account/ForgotPassword",
    type: "POST",
    data: $(this).serialize(), // Serialize form data
    success: function (response) {
      // Handle success
      $("#btnForgetPassword .spinner-border").addClass("d-none");
      $("#btnForgetPassword").prop("disabled", false);
      // Show modal on success
      $("#successEmailSentModal").modal("show");
    },
    error: function (xhr, status, error) {
      // Handle error
    },
    complete: function () {
      // Reset flag and enable submit button
      $("#sendRequestChangePassword").data("isSubmitting", false);
    },
  });
});
