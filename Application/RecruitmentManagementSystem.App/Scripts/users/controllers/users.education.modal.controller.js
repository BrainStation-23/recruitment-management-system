(function(app) {
    "use strict";

    app.controller("EducationModalController", [
        "$http", "$uibModalInstance", "data", function($http, $uibModalInstance, data) {
            var vm = this;

            $http.get("/Institution/").success(function(response) {
                vm.institutions = response;

                if (data) {
                    vm.institution = _.find(vm.institutions, { id: data.institution.id });
                }
            });

            if (data) {
                vm.degree = data.degree;
                vm.fieldOfStudy = data.fieldOfStudy;
                vm.grade = data.grade;
                vm.startDate = new Date(data.startDate);
                vm.endDate = new Date(data.endDate);
                vm.present = data.present;
                vm.activities = data.activities;
                vm.notes = data.notes;
            }

            vm.save = function() {
                vm.form.submitted = true;

                if (vm.form.$valid) {
                    var education = {
                        degree: vm.degree,
                        fieldOfStudy: vm.fieldOfStudy,
                        grade: vm.grade,
                        institution: vm.institution,
                        startDate: vm.startDate,
                        present: vm.present,
                        activities: vm.activities,
                        notes: vm.notes
                    };

                    if (!education.present) {

                        education.endDate = vm.endDate;
                    }

                    if (data && data.$$hashKey) {
                        education.$$hashKey = data.$$hashKey;
                    }

                    $uibModalInstance.close(education);
                }
            };

            vm.cancel = function() {
                $uibModalInstance.dismiss("cancel");
            };
        }
    ]);
})(angular.module("candidates"));