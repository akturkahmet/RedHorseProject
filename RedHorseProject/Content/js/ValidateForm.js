function validateForm(id) {
    let invalidFields = [];

    $(`#${id}`)
        .find(':input')
        .not(':button, :submit,.notNecessary')
        .each(function () {
            if (!$(this).val() || $.trim($(this).val()) === '') {
                invalidFields.push($(this).attr('name') || $(this).attr('id'));
            }
        });

    if (invalidFields.length > 0) {

        return false;
    }

    return true;
}

