(function () {
    window.dataLayer = window.dataLayer || [];
    function gtag() { dataLayer.push(arguments); }
        
    gtag('js', new Date());

    gtag('consent', 'default', {
        'ad_storage': 'denied',
        'analytics_storage': 'denied'
    });

    var analyticsId = $("#sitejs").data("analytics-id");
    gtag('config', analyticsId);

    var preferencesSet = getCookie('cookies_preferences_set');

    if (preferencesSet) {
        var cookiePolicy = getCookie('cookies_policy');
        var cookiePolicyParsed = JSON.parse(cookiePolicy);

        var consented = cookiePolicyParsed.AllowMeasureWebsite ? 'granted' : 'denied';

        gtag('consent', 'update', {
            'ad_storage': consented,
            'analytics_storage': consented
        });
    }

    function getCookie(cname) {
        let name = cname + "=";
        let decodedCookie = decodeURIComponent(document.cookie);
        let ca = decodedCookie.split(';');
        for (let i = 0; i < ca.length; i++) {
            let c = ca[i];
            while (c.charAt(0) == ' ') {
                c = c.substring(1);
            }
            if (c.indexOf(name) == 0) {
                return c.substring(name.length, c.length);
            }
        }
        return "";
    }
})();
