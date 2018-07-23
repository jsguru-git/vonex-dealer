var gulp = require("gulp");
gulp.task('copy', function () {
    gulp.src('./node_modules/syncfusion-javascript/**')
        .pipe(gulp.dest('./wwwroot/lib/syncfusion-javascript'));
});
gulp.task('copy', function () {
    gulp.src('./node_modules/jsrender/jsrender.min.js')
        .pipe(gulp.dest('./wwwroot/lib/jsrender'));
});
