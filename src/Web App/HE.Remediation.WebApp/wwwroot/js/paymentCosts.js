$(document).ready(function () {

    $(".current-cost-input").keypress(function (e)
    {
        var enteredChar = e.charCode;        
        if ((enteredChar >= 65 && enteredChar <= 90) ||
            (enteredChar >= 97 && enteredChar <= 122) ||
            (enteredChar == 32) ||
            (enteredChar == 0))
        {
            e.preventDefault();
        }
    });

    $(".monthly-costs-table").on('input', '.monthly-cost-input, .current-cost-input', function () {

        UpdateCosts();
    });

    $(".current-cost-input").on('focusout', function ()
    {
        UpdateCosts();
    });

    function UpdateCosts()
    {
        const monthlyCostsArray = [];
        $("input.monthly-cost-input").each(function () {
            const num = +$(this).val().replace(/,/g, '');
            if (!isNaN(num)) {
                monthlyCostsArray.push(Math.trunc(parseFloat(num)));
            }
        });

        $.ajax({
            url: "/ScheduleOfWorks/api/calculateCosts",
            type: "POST",
            contentType: "application/json",
            data: JSON.stringify({
                approvedGrantFunding: $("input.approved-grant-funding").length > 0 ? parseFloat($("input.approved-grant-funding").first().val()) : 0,
                grantPaidToDate: $("input.grant-paid-to-date").length > 0 ? parseFloat($("input.grant-paid-to-date").first().val()) : 0,
                monthlyCosts: monthlyCostsArray,
                finalCost: $("input.final-cost-input").length > 0 ? parseFloat($("input.final-cost-input").first().val().replace(/,/g, '')) : 0,
                currentCost: $("input.current-cost-input").val().length > 0 ? parseFloat($("input.current-cost-input").first().val().replace(/,/g, '')) : 0,
                additionalCost: $("input.additional-cost-input").length > 0 ? parseFloat($("input.additional-cost-input").first().val().replace(/,/g, '')) : 0,
            })
        }).done(function (response) {
            $(".monthly-costs-total").text(response.totalMonthlyCosts.toLocaleString('en-GB', { maximumFractionDigits: 0 }));
            $(".final-cost-total").text(response.finalCost.toLocaleString('en-GB', { maximumFractionDigits: 0 }));
            $(".payment-request-total").text(response.totalCurrentCost.toLocaleString('en-GB', { maximumFractionDigits: 0 }));

            const unprofiledAmount = response.unprofiledAmount;
            let unprofiledFundingText = unprofiledAmount < 0 ? "-&pound;" : "&pound;"
            unprofiledFundingText += Math.abs(unprofiledAmount).toLocaleString('en-GB', { maximumFractionDigits: 0 });

            $(".unprofiled-funding").html(unprofiledFundingText);
            if (unprofiledAmount < 0) {
                $(".unprofiled-funding").addClass("out-of-range-number-text");
                $("p.unprofiled-funding").closest("p").removeClass("govuk-body");
            } else {
                $(".unprofiled-funding").removeClass("out-of-range-number-text");
                $("p.unprofiled-funding").addClass("govuk-body");
            }
        }).fail(function (error) {
            console.log("Error status " + error.status + " was encountered. " + error.errorText);
        });
    }    
});
