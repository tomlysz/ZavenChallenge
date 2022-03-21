$(function () {
    $('#jobForm').submit(function () {
        return validate();
    })

    function validate() {
        let validateNameResult = validateName();
        let validateDoAfterResult = validateDoAfter();

        if (validateNameResult && validateDoAfterResult)
            return true;
        else
            return false;
    }

    function validateName() {
        let name = $('input[name="Name"]').val();
        let valTextSelector = '#nameVal'

        if (name === '') {
            showText(valTextSelector, 'Name cannot be empty.');
            return false;
        } else {
            showText(valTextSelector, '');
            return true;
        }
    }

    function validateDoAfter() {
        let doAfter = $('input[name="DoAfter"]').val();
        let dateRegex = /^\d{4}-(0[1-9]|1[0-2])-(0[1-9]|[12][0-9]|3[01])$/;
        let isDate = doAfter.match(dateRegex);
        let valTextSelector = '#doAfterVal'

        if (doAfter !== '') {
            if (!isDate) {
                showText(valTextSelector, 'It is not a valid date. Example: yyyy-mm-dd');
                return false;
            } else {
                showText(valTextSelector, '');
                return true;
            }
        } else {
            showText(valTextSelector, '');
            return true;
        }
    }

    function showText(selector, text) {
        if (text !== '') {
            $(selector).show();
            $(selector).html(text);
        } else {
            $(selector).hide();
            $(selector).html(text);
        }
    }
})