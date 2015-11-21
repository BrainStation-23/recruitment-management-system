(function (app) {
    "use strict";

    app.constant("Enums", {
        questionType: { MCQ: 1, Descriptive: 2}
    });
    
})(angular.module("questions"));