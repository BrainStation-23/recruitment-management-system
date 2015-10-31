angular.module("candidates").controller("ExperienceModalInstanceController", [
    "$http", "$uibModalInstance", function ($http, $uibModalInstance) {
        var vm = this;

        vm.add = function () {
            vm.form.submitted = true;

            if (vm.form.$valid) {
                var experience = {
                    organization: vm.organization,
                    jobTitle: vm.jobTitle,
                    from: vm.from,
                    to: vm.to,
                    stillWorking: vm.stillWorking,
                    description: vm.description
                };
                $uibModalInstance.close(experience);
            }
        };

        vm.cancel = function () {
            $uibModalInstance.dismiss("cancel");
        };
    }
]);