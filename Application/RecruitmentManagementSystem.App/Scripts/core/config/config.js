"use strict";

var ApplicationConfiguration = (function() {
    var applicationModuleName = "rms";
    var applicationModuleVendorDependencies = ["ui.bootstrap", "ngMessages"];

    var registerModule = function(moduleName, dependencies) {
        angular.module(moduleName, dependencies || []);

        angular.module(applicationModuleName).requires.push(moduleName);
    };

    return {
        applicationModuleName: applicationModuleName,
        applicationModuleVendorDependencies: applicationModuleVendorDependencies,
        registerModule: registerModule
    };
})();