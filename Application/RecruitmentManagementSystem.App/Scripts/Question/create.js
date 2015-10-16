(function () {
    
    $(document).ready(function () {
        $("#achr-add-choice").hide();
    });

    var choices = [];

    $(document).on("change", "#drop-down-question-type", function() {
        if ($("#drop-down-question-type").val() === "1") {
            $("#achr-add-choice").show();
        } else {
            $("#achr-add-choice").hide();
        }
    });

    $(document).on("click", "#btn-add-choice", function() {
        var choiceValue = $("#choice").val();

        if (choiceValue) {
            choices.push(choiceValue);
            $("#choice").val("");
        }
    });

    var answers = [];

    $(document).on("click", "#btn-add-answer", function () {
        var answerValue = $("#answer").val();

        if (answerValue) {
            answers.push(answerValue);
            $("#answer").val("");
        }
    });

    $(document).on("click", "#btn-add-question", function() {
        var data = {
            Title: $("#Title").val(),
            QuestionType: $("#drop-down-question-type").val(),
            Choices: choices,
            Notes: $("#Notes").val(),
            Answers: answers,
            CategoryId: $("#CategoryId").val()
        };

        $.ajax({
            url: "/Question/Create",
            method: "POST",
            dataType: "application/json",
            data: data,
            success: function(result) {
                console.log(result.Success);
                location.href = "/Question/Index";
            }
        });
    });
})();