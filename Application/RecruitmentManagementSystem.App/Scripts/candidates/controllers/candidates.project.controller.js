(function(app) {
    "use strict";

    app.controller("ProjectModalController", [
        "$uibModalInstance", "data", function($uibModalInstance, data) {
            var vm = this;

            if (data) {
                vm.title = data.title;
                vm.url = data.url;
                vm.description = data.description;
                vm.notes = data.notes;
            }

            vm.save = function() {
                vm.form.submitted = true;

                if (vm.form.$valid) {

                    var project = {
                        title: vm.title,
                        url: vm.url,
                        description: vm.description,
                        notes: vm.notes
                    };

                    if (data && data.$$hashKey) {
                        project.$$hashKey = data.$$hashKey;
                    }

                    $uibModalInstance.close(project);
                }
            };

            vm.cancel = function() {
                $uibModalInstance.dismiss("cancel");
            };
        }
    ]);
})(angular.module("candidates"));