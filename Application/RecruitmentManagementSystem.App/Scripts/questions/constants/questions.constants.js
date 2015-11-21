(function(app) {
    "use strict";

    app.constant("questionConstants", {
        questionType: { MCQ: 1, Descriptive: 2 }
    });

})(angular.module("questions"));