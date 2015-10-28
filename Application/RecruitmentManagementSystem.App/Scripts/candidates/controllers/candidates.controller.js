angular.module("candidates").controller("CandidatesController", [
    "$http", "$uibModal", function ($http, $uibModal) {
        var vm = this;
        vm.institutions = [];
        vm.educations = [
            {
                degree: "",
                fieldOfStudy: "",
                grade: "",
                activites: "",
                description: "",
                firstYear: "",
                lastYear: "",
                institutionId: ""
            }
        ];

        vm.addNewEducation = function() {
            vm.educations.push({
                degree: "",
                fieldOfStudy: "",
                grade: "",
                activites: "",
                description: "",
                firstYear: "",
                lastYear: "",
                institutionId: ""
            });
        };

        vm.discardEducation = function(index) {
            vm.educations.splice(index, 1);
        };

        vm.findInstitutions = function() {
            $http.get("/Institution/").success(function(data) {
                vm.institutions = data;
            });
        };

        vm.create = function() {
            vm.form.submitted = true;

            var model = {
                firstName: vm.firstName,
                lastName: vm.lastName,
                email: vm.email,
                phoneNumber: vm.phoneNumber,
                educations: vm.educations,
                //avatar: vm.avatar,
                //resume: vm.resume,
                others: vm.others,
                website: vm.website
            };

            $http.post("/Candidate/Create", model).success(function(data) {
                if (data.Succeeded) {
                    location.href = "/Candidate";
                }
            });
        };

        vm.open = function(size) {

            var modalInstance = $uibModal.open({
                animation: vm.animationsEnabled,
                templateUrl: 'myModalContent.html',
                controllerAs: 'ModalInstanceCtrl',
                size: size
            });

            modalInstance.result.then(function(selectedItem) {
                vm.selected = selectedItem;
            }, function() {
                console.log('Modal dismissed at: ' + new Date());
            });

            console.log(modalInstance);
        };

        vm.toggleAnimation = function() {
            vm.animationsEnabled = !vm.animationsEnabled;
        };
    }
]);

angular.module("candidates").controller("ModalInstanceCtrl", [
    "$uibModalInstance", function($uibModalInstance) {
        var vm = this;

        console.log($uibModalInstance);

        vm.ok = function() {
            $uibModalInstance.close(vm.selected.item);
        };

        vm.cancel = function() {
            $uibModalInstance.dismiss('cancel');
        };
    }
]);