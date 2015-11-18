var gulp = require("gulp"),
    plugins = require("gulp-load-plugins")({
        lazy: true,
        rename: {
            "gulp-scss-lint": "scsslint"
        }
    }),
    runSequence = require("run-sequence");

var assets = {
    scss: "./Content/Sass/**/*.scss",
    scssMain: "./Content/Sass/main.scss",
    js: ["./Scripts/**/*.js", "!Scripts/_references.js", "gulpfile.js"]
};

gulp.task("jshint", function() {
    "use strict";

    return gulp.src(assets.js)
        .pipe(plugins.print())
        .pipe(plugins.jscs())
        .pipe(plugins.jshint())
        .pipe(plugins.jshint.reporter("jshint-stylish", { verbose: true }))
        .pipe(plugins.jshint.reporter("fail"));
});

gulp.task("scss-lint", function() {
    "use strict";

    return gulp.src(assets.scss)
        .pipe(plugins.print())
        .pipe(plugins.scsslint({
            "config": "scss-lint.yml"
        }))
        .pipe(plugins.scsslint.failReporter());
});

gulp.task("scss-compile", function() {
    "use strict";

    return gulp.src(assets.scssMain)
        .pipe(plugins.sass())
        .pipe(gulp.dest("./Content/Styles/"))
        .pipe(plugins.notify("SCSS files are compiled!"));
});

gulp.task("watch", function() {
    "use strict";

    gulp.watch(assets.js, ["jshint"]);
    gulp.watch(assets.scss, ["scss-lint", "scss-compile"]);
});

gulp.task("lint", function(done) {
    "use strict";

    runSequence("scss-lint", "jshint", done);
});

gulp.task("default", function(done) {
    "use strict";

    runSequence("lint", "scss-compile", ["watch"], done);
});