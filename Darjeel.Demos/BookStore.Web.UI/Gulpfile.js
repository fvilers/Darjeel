/// <binding ProjectOpened='watch' />
var gulp = require("gulp");
var sourcemaps = require("gulp-sourcemaps");
var concat = require("gulp-concat");
var ngAnnotate = require("gulp-ng-annotate");
var uglify = require("gulp-uglify");
var minifyCss = require("gulp-minify-css");
var concatCss = require('gulp-concat-css');
var config = {
    js: {
        src: [
            "bower_components/jquery/dist/jquery.js",
            "bower_components/bootstrap/dist/js/bootstrap.js",
            "bower_components/bootstrap-growl-forked/bootstrap-growl.js",
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
    },
    css: {
        src: [
            "bower_components/bootstrap/dist/css/bootstrap.css",
            "assets/css/custom.css"
        ],
        bundle: {
            path: "assets/css",
            fileName: "app.min.css"
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

gulp.task("css", function () {
    return gulp
		.src(config.css.src)
		.pipe(concatCss(config.css.bundle.fileName))
		.pipe(minifyCss())
		.pipe(gulp.dest(config.css.bundle.path));

});

gulp.task("bundles", ["js", "css"], function () {
});

gulp.task("watch", function () {
    gulp.watch([config.js.src], ["default"]);
});

gulp.task("default", ["bundles"], function () {
});