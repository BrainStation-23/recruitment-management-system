angular.module("candidates").controller("EducationModalInstanceController", [
    "$http", "$uibModalInstance", function ($http, $uibModalInstance) {
    	var vm = this;

    	vm.institutions = [];

    	$http.get("/Institution/").success(function (data) {
    		vm.institutions = data;
    	});

    	vm.add = function () {
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

    	vm.cancel = function () {
    		$uibModalInstance.dismiss("cancel");
    	};
    }
]);