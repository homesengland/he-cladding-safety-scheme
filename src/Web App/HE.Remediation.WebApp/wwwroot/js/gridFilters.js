$(document).ready(function () {

    anyFilterBoxesTickedShowClearFilters();

    $('.govuk-link.clear-filters').on('click', function () {
        clearFilters($(this)[0]);
    });

    $('.govuk-button.govuk-button--secondary.remove-buttons').on('click', function () {
        removeFilter($(this)[0]);
    });

});

function anyFilterBoxesTickedShowClearFilters() {
    const clearFiltersLink = $('.govuk-link.clear-filters');
    if ($(".govuk-button.govuk-button--secondary.remove-buttons").length > 0) {
        clearFiltersLink.show();
    }
    else {
        clearFiltersLink.text(' ');
        clearFiltersLink.css('margin-bottom', '45px');
    }
}

function clearFilters(button) {
    setSourceAndSubmitForm(button, 'clear');
}

function removeFilter(button) {
    setSourceAndSubmitForm(button, 'unselect');
}

function setSourceAndSubmitForm(button, source) {
    button.closest('form').querySelector('input[name=source]').value = source;
    button.closest('form').submit();
}
