angular.module("candidates").controller("CandidatesController", [
    "$http", "$uibModal", function($http, $uibModal) {
        var vm = this;

        vm.institutions = [];

        vm.educations = [];

        vm.discardEducation = function(index) {
            vm.educations.splice(index, 1);
        };

        vm.create = function() {
            vm.form.submitted = true;

            for (var idx = 0; idx < vm.educations.length; idx++) {
                vm.educations[idx].institutionId = vm.educations[idx].institution.Id;
                delete vm.educations[idx].institution;
            }

            var model = {
                firstName: vm.firstName,
                lastName: vm.lastName,
                email: vm.email,
                phoneNumber: vm.phoneNumber,
                educations: vm.educations,
                avatar: vm.avatar,
                resume: vm.resume,
                others: vm.others,
                website: vm.website
            };

            $http.post("/Candidate/Create", model).success(function(data) {
                if (data.Succeeded) {
                    location.href = "/Candidate";
                }
            });
        };

        vm.openEducationModal = function() {
            var modalInstance = $uibModal.open({
                animation: true,
                backdrop: "static",
                templateUrl: "EducationModalContent.html",
                controller: "EducationModalInstanceController",
                controllerAs: "education"
            });

            modalInstance.result.then(function(row) {
                console.log(row);
                vm.educations.push(row);
            });
        };
    }
]);

angular.module("candidates").controller("EducationModalInstanceController", [
    "$http", "$uibModalInstance", function($http, $uibModalInstance) {
        var vm = this;

        vm.institutions = [];

        $http.get("/Institution/").success(function(data) {
            vm.institutions = data;
        });

        vm.add = function() {
            vm.form.submitted = true;

            if (vm.form.$valid) {
                var education = {
                    degree: vm.degree,
                    fieldOfStudy: vm.fieldOfStudy,
                    grade: vm.grade,
                    institution: vm.institution,
                    firstYear: vm.firstYear,
                    lastYear: vm.lastYear,
                    activites: vm.activites,
                    description: vm.description
                };
                $uibModalInstance.close(education);
            }
        };
        
        vm.cancel = function() {
            $uibModalInstance.dismiss("cancel");
        };
    }
]);