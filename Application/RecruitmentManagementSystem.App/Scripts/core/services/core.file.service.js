(function(app) {
    "use strict";

    app.factory("fileService", [
        "Upload", function($upload) {

            var postMultipartForm = function(config) {
                return $upload.upload({
                    url: config.url,
                    method: config.method || "POST",
                    data: config.data
                });
            };

            var getOriginalFileName = function(dbFileName) {
                var newGuidLength = 36;
                return dbFileName.substring(newGuidLength);
            };

            return {
                postMultipartForm: postMultipartForm,
                getOriginalFileName: getOriginalFileName
            };
        }
    ]);
})(angular.module("core"));