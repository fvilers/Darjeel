/// <binding ProjectOpened='watch' />
var gulp = require("gulp");
var sourcemaps = require("gulp-sourcemaps");
var concat = require("gulp-concat");
var ngAnnotate = require("gulp-ng-annotate");
var uglify = require("gulp-uglify");
var config = {
    js: {
        src: [
            "bower_components/angular/angular.js",
            "bower_components/angular-route/angular-route.js",
            "app/catalog/**/*.js",
            "app/app.module.js",
            "app/app.*.js"
        ],
        bundle: {
            path: "assets/js",
            fileName: "app.min.js"
        }
    }
}

gulp.task("js", function () {
    return gulp
		.src(config.js.src)
		.pipe(sourcemaps.init())
		.pipe(concat(config.js.bundle.fileName, { newLine: ";" }))
		.pipe(ngAnnotate({ add: true }))
		.pipe(uglify({ mangle: true }))
		.pipe(sourcemaps.write("maps"))
		.pipe(gulp.dest(config.js.bundle.path));
});

gulp.task("bundles", ["js"], function () {
});

gulp.task("watch", function () {
    gulp.watch([config.js.src], ["default"]);
});

gulp.task("default", ["bundles"], function () {
});