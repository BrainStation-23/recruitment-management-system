var gulp = require("gulp"),
    plugins = require("gulp-load-plugins")({
        lazy: true
    }),
    runSequence = require("run-sequence");

var assets = {
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

gulp.task("watch", function() {
    "use strict";

    gulp.watch(assets.js, ["jshint"]);
});

gulp.task("lint", function(done) {
    "use strict";

    runSequence("jshint", done);
});

gulp.task("default", function(done) {
    "use strict";

    runSequence("lint", ["watch"], done);
});
