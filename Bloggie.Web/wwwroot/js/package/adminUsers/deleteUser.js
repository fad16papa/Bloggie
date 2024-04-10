//DeleteUser
$("#deleteUserForm").submit(function (event) {
  event.preventDefault(); // Prevent default form submission

  var form = $(this); // Reference to the form that was submitted

  // Disable submit button to prevent multiple clicks
  form.find("#btnDeleteUser").prop("disabled", true);

  // Show loading spinner
  form.find(".spinner-border").removeClass("d-none");

  // Check if a submission is already in progress
  if (form.data("isSubmitting")) {
    // If a submission is in progress, return without making a new request
    return;
  }

  // Get the anti-forgery token value from the cookie
  var antiForgeryToken = $('input[name="__RequestVerificationToken"]').val();

  // Set flag to indicate submission is in progress
  form.data("isSubmitting", true);

  // Serialize the form data
  var id = form.find("#userId").text();

  // Make AJAX request
  setTimeout(function () {
    $.ajax({
      url: "/AdminUsers/Delete",
      type: "POST",
      headers: {
        RequestVerificationToken: antiForgeryToken,
      },
      data: {
        id: id,
      },
      success: function (response) {
        // Handle success
        form.find(".spinner-border").addClass("d-none");
        form.find("#btnDeleteUser").prop("disabled", false);
        window.location.href = "/AdminUsers/List";
      },
      error: function (xhr, status, error) {
        // Handle error
      },
      complete: function () {
        // Reset flag and enable submit button
        form.data("isSubmitting", false);
      },
    });
  }, 2000); //put delay 3 seconds
});
