(function() {

 //   var mailSent = false;

    $(document).ready(function() {

        var hideMessages = function() {
            $("#mailSuccess").hide();
            $("#mailSent").hide();
            $("#mailError").hide();
        };

        hideMessages();

        /*  Commenting out since PostBack forces the issue and this isn't actually run.
        var sendEmail = function() {
            var fromEmail = $("#EmailAddress").value;
            var firstName = $("#FirstName").value;
            var lastName = $("#LastName").value;
            var subject = $("#Subject").value;
            var msgBody = $("#EmailBody").value;
            var phone = $("#PhoneNumber").value;

            $(form).ajaxSubmit({
                type: "POST",
                dataType: "json",
                url: "/api/contact/email",
                data: { FirstName: firstName, LastName: lastName, PhoneNumber: phone, EmailAddress: fromEmail, EmailBody: msgBody },
                success: function (data) {
                    hideMessages();
                    ("#msgSuccess").show();
                },
                error: function (response) {
                    $("#msgError").show();
                    //$("#errorBody").append(response);
                }
            });
        } */
    });
})();