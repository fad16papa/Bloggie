//DeleteUser
$("#deleteUserForm").submit(function (event) {
  event.preventDefault(); // Prevent default form submission

  // Disable submit button to prevent multiple clicks
  $("#btnDeleteUser").prop("disabled", true);

  // Show loading spinner
  $("#btnDeleteUser .spinner-border").removeClass("d-none");

  // Check if a submission is already in progress
  if ($(this).data("isSubmitting")) {
    // If a submission is in progress, return without making a new request
    return;
  }
  // Set flag to indicate submission is in progress
  $(this).data("isSubmitting", true);

  // Serialize the form data
  var id = $("#userId").text();

  // Make AJAX request
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
      $("#btnDeleteUser .spinner-border").addClass("d-none");
      $("#btnDeleteUser").prop("disabled", false);
      window.location.href = "/AdminUsers/List";
    },
    error: function (xhr, status, error) {
      // Handle error
    },
    complete: function () {
      // Reset flag and enable submit button
      $("#deleteUserForm").data("isSubmitting", false);
    },
  });
});
