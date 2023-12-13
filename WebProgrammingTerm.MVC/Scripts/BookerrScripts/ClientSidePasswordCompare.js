$(document).ready(function() {
    $('form').submit(function() {
        var password = $('#password').val();
        var confirmPassword = $('#confirmPassword').val();

        // Check if the values match
        if (password !== confirmPassword) {
            if ($('#passwordWarning').length === 0) {
                var warningMessage = $('<p id="passwordWarning" style="color: red;">Password and Confirm Password must match</p>');

                $('form').before(warningMessage);
            }

            return false;
        }

        $('#passwordWarning').remove();

        return true;
    });
});
