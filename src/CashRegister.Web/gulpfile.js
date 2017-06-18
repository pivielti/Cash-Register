var gulp = require("gulp");
var gutil = require("gulp-util");
var webpack = require("webpack");
var webpackConf = require('./webpack.config.js');

var compiler = webpack(webpackConf);
gulp.task("webpack", function (callback) {
    compiler.run(function (err, stats) {
        if (err) throw new gutil.PluginError("webpack", err);
        gutil.log("[webpack]", stats.toString());
        callback();
    });
});

gulp.task('default', ["webpack"], function () {
    gulp.watch('./App/**/*', ['webpack']);
});