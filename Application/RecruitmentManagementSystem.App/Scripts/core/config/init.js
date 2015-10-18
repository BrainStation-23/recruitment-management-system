"use strict";

angular.module(ApplicationConfiguration.applicationModuleName, ApplicationConfiguration.applicationModuleVendorDependencies);

angular.module(ApplicationConfiguration.applicationModuleName).config([
    "$httpProvider", function($httpProvider) {
        $httpProvider.defaults.headers.common["X-Requested-With"] = "XMLHttpRequest";
    }
]);

angular.element(document).ready(function() {
    angular.bootstrap(document, [ApplicationConfiguration.applicationModuleName]);
});