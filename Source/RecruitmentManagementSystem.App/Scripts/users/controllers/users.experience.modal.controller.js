﻿(function(app) {
    "use strict";

    app.controller("ExperienceModalController", [
        "$uibModalInstance", "data", function($uibModalInstance, data) {
            var vm = this;

            if (data) {
                vm.organization = data.organization;
                vm.jobTitle = data.jobTitle;
                vm.startDate = new Date(data.startDate);
                vm.endDate = new Date(data.endDate);
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
                        present: vm.present,
                        description: vm.description,
                        notes: vm.notes
                    };

                    if (!experience.present) {
                        experience.endDate = vm.endDate;
                    }

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
})(angular.module("users"));