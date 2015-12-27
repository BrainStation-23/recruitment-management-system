"use strict";

angular.module(ApplicationConfiguration.applicationModuleName, ApplicationConfiguration.applicationModuleVendorDependencies);

angular.module(ApplicationConfiguration.applicationModuleName).config([
    "$httpProvider", function($httpProvider) {
        $httpProvider.defaults.headers.common["X-Requested-With"] = "XMLHttpRequest";

        $httpProvider.interceptors.push(function($q) {
            return {
                "response": function(response) {
                    return response;
                },
                "responseError": function(rejection) {
                    if (rejection.status === 401) {
                        window.location = "/Account/Login";
                    }
                    if (rejection.status === 404) {
                        window.location = "/Error/PageNotFound";
                    }
                    return $q.reject(rejection);
                }
            };
        });
    }
]);

angular.element(document).ready(function() {
    angular.bootstrap(document, [ApplicationConfiguration.applicationModuleName]);
});