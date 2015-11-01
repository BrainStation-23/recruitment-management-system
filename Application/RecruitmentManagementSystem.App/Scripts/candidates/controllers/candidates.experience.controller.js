angular.module("candidates").controller("ExperienceModalController", [
    "$uibModalInstance", "data", function($uibModalInstance, data) {
        var vm = this;

        if (data) {
            vm.organization = data.organization;
            vm.jobTitle = data.jobTitle;
            vm.startDate = data.startDate;
            vm.endDate = data.endDate;
            vm.present = data.present;
            vm.description = data.description;
            vm.notes = data.notes;
        }

        vm.save = function() {
            vm.form.submitted = true;

            if (vm.form.$valid) {

                var experience = {
                    organization: vm.organization,
                    jobTitle: vm.jobTitle,
                    startDate: vm.startDate,
                    endDate: vm.present ? null : vm.endDate,
                    present: vm.present,
                    description: vm.description,
                    notes: vm.notes
                };

                if (data && data.$$hashKey) {
                    experience.$$hashKey = data.$$hashKey;
                }

                $uibModalInstance.close(experience);
            }
        };

        vm.cancel = function() {
            $uibModalInstance.dismiss("cancel");
        };
    }
]);