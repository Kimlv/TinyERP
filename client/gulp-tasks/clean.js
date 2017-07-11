var gulp = require('gulp');
var mkdir= require("mkdirp");
var config = require('../gulp.config')();
var del = require('del');

/* Run all clean tasks */
gulp.task('clean', ['clean-build', 'prepare-folder']);

gulp.task("prepare-folder", function(){
    mkdir(config.build.path);
});
/* delete dist folder */
gulp.task('clean-build', function () {
    return del([config.build.path]);
});