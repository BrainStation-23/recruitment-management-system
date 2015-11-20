(function(app) {
    "use strict";

    app.controller("AccountModalController", [
        "$uibModalInstance", "data", "$http", "notifierService", function($uibModalInstance, data, $http, notifierService) {
            var vm = this;

            if (data) {
                vm.firstName = data.firstName;
                vm.lastName = data.lastName;
                vm.email = data.email;
            }

            vm.save = function() {
                vm.form.submitted = true;

                if (vm.form.$valid) {

                    var model = {
                        firstName: vm.firstName,
                        lastName: vm.lastName,
                        email: vm.email
                    };

                    $http.post("/Manage/AccountDetails", model).success(function() {
                        $uibModalInstance.close(model);
                    }).error(function(response) {
                        var erroMessages = [];

                        if (Object.prototype.toString.call(response) === "[object Array]") {
                            erroMessages = _.map(response, function(error) {
                                return error.errorMessage;
                            });
                        } else {
                            erroMessages.push("Something happened! Please try again.");
                        }

                        notifierService.notifyError(erroMessages);
                    });


                }
            };

            vm.cancel = function() {
                $uibModalInstance.dismiss("cancel");
            };
        }
    ]);
})(angular.module("accounts"));