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
                $uibModalInstance.close(vm.education);
            }
        };

        vm.cancel = function() {
            $uibModalInstance.dismiss("cancel");
        };
    }
]);