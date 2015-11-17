var gulp = require("gulp"),
    plugins = require("gulp-load-plugins")(),
    runSequence = require("run-sequence");

var assets = {
    sass: "./Content/Sass/main.scss",
    js: "./Scripts/**/*.js"
};

gulp.task("jshint", function() {
    return gulp.src([
            assets.js,
            "!Scripts/_references.js"
        ])
        .pipe(plugins.jshint())
        .pipe(plugins.jshint.reporter("default"))
        .pipe(plugins.jshint.reporter("fail"));
});

gulp.task("sass", function() {
    return gulp.src(assets.sass)
        .pipe(plugins.sass())
        .pipe(gulp.dest("./Content/Styles/"));
});

gulp.task("watch", function() {
    gulp.watch(assets.js, ["jshint"]);
    gulp.watch(assets.sass, ["sass"]);
});

gulp.task("default", function(done) {
    runSequence("jshint", "sass", ["watch"], done);
});