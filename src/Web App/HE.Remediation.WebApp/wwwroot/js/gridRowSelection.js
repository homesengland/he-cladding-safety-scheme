$(document).ready(function () {

    updateBtnUpdateState();

    // Restore checked state from sessionStorage
    const selected = JSON.parse(sessionStorage.getItem('AppId') || '[]');
    selected.forEach(function (id) {
        const checkbox = $('input[name="AppId"][value="' + id + '"]');
        if (checkbox.length) {
            checkbox.prop('checked', true);
        }
    });

    // Listen for changes and update sessionStorage
    $('input[name="AppId"]').on('change', function () {
        let selected = JSON.parse(sessionStorage.getItem('AppId') || '[]');
        const value = $(this).val();
        if ($(this).is(':checked')) {
            if (!selected.includes(value)) {
                selected.push(value);
            }
        } else {
            selected = selected.filter(x => x !== value);
        }
        sessionStorage.setItem('AppId', JSON.stringify(selected));

        updateBtnUpdateState();
    });

    // On submit, add hidden inputs for only those selected IDs not already checked on the current page
    $('form[id="MpGrid"]').on('submit', function (e) {

        // Remove any previous hidden inputs to avoid duplicates
        $(this).find('input[name="AppId"][type="hidden"]').remove();

        const selected = JSON.parse(sessionStorage.getItem('AppId') || '[]');
        selected.forEach(function (id) {
            // If there is no checked checkbox for this ID on the current page, add a hidden input
            const checkbox = $('input[name="AppId"][type="checkbox"][value="' + id + '"]');
            if (!(checkbox.length && checkbox.is(':checked'))) {
                $('<input>')
                    .attr('type', 'hidden')
                    .attr('name', 'AppId')
                    .val(id)
                    .appendTo($(e.target));
            }
        });

        // Clear the session values after preparing the form
        sessionStorage.removeItem('AppId');
    });

    // Function to enable/disable the Update button based on selection (including sessionStorage)
    function updateBtnUpdateState() {
        // Get all selected AppIds from sessionStorage
        const selected = JSON.parse(sessionStorage.getItem('AppId') || '[]');
        // Enable if at least one is selected, otherwise disable
        $('#btnUpdate').prop('disabled', selected.length === 0);
    }
});
