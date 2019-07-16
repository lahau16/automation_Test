/// <binding AfterBuild='copy-modules' ProjectOpened='copy-modules' />
// <binding AfterBuild='copy-modules' />
"use strict";

var gulp = require('gulp'),
    clean = require('gulp-clean'),
    flatten = require('gulp-flatten')

var listModules = [
    "Test"
];

var listInterface = [
    "Test.UI"
];

var listExtensions = [
];

var paths = {
    source: {
        wwwroot: "./wwwroot/",
        moduleBin: "/bin/",
        modules: "../../Modulars/",
        interfaces: "../../Presentations/",
        extensions: "../../Extensions/"
    },
    destination: {
        wwwroot: "./wwwroot/",
        modules: "./Modulars/",
        extension: "./ExtensionFolder/",
        moduleBin: "/bin/",
        moduleRun: "./bin/Debug/netcoreapp2.2/",
    },
    config: {
        buildEnvairament: "Debug",
        dotnetcoreVersion: "netcoreapp2.2"
    }
};

gulp.task('clean-module', function () {
    //gulp.src("./Views/", { read: false })
    //    .pipe(clean());

    for (var i = 0; i < listModules.length; i++) {
        console.log("module: " + paths.destination.moduleRun + "Modulars");
        gulp.src(paths.destination.moduleRun + "Modulars/" + listModules[i] + ".dll", { read: false })
            .pipe(clean());
    }
    for (var j = 0; j < listInterface.length; j++) {
        gulp.src(paths.destination.moduleRun + "Modulars/" + listInterface[j] + ".dll", { read: false })
            .pipe(clean());
    }

    gulp.src(paths.destination.moduleRun + "Modulars", { read: false })
        .pipe(clean());

    gulp.src("./ExtensionFolder/", { read: false })
        .pipe(clean());

    return gulp.src(paths.destination.modules + '*', { read: false })
        .pipe(clean());
});

gulp.task('copy-static', function () {

    for (var i = 0; i < listModules.length; i++) {
        gulp.src(paths.source.modules + listModules[i] + '/Views/**/*.*')
            .pipe(gulp.dest("./Modulars/" + listModules[i] + "/Views/"));
        gulp.src(paths.source.modules + listModules[i] + '/wwwroot/**/*.*')
            .pipe(gulp.dest(paths.destination.wwwroot));
    }
});

gulp.task('copy-modules', ['clean-module'], function () {
    gulp.start(['copy-static']);

    for (var i = 0; i < listModules.length; i++) {
        console.log("module: " + listModules[i]);
        console.log("____ " + paths.source.modules + listModules[i] + paths.source.moduleBin);
        console.log("____ " + paths.source.modules + listModules[i] + paths.source.moduleBin + paths.config.buildEnvairament + '/netcoreapp*/*.*');

        gulp.src(paths.source.modules + listModules[i] + paths.source.moduleBin + paths.config.buildEnvairament + '/' + paths.config.dotnetcoreVersion + '/*.*')
            .pipe(gulp.dest(paths.destination.modules + listModules[i] + paths.destination.moduleBin));
        gulp.src(paths.source.modules + listModules[i] + paths.source.moduleBin + paths.config.buildEnvairament + '/' + paths.config.dotnetcoreVersion + '/*.*')
            .pipe(gulp.dest(paths.destination.moduleRun + "Modulars/" + listModules[i] + "/bin"));
        gulp.src(paths.source.modules + listModules[i] + paths.source.moduleBin + paths.config.buildEnvairament + '/' + paths.config.dotnetcoreVersion + '/' + listModules[i] + '.*')
            .pipe(gulp.dest(paths.destination.moduleRun));
    }

    for (var j = 0; j < listInterface.length; j++) {
        console.log("interface: " + listInterface[j]);
        console.log("____ " + paths.source.modules + listInterface[j] + paths.source.moduleBin);

        gulp.src(paths.source.interfaces + listInterface[j] + paths.source.moduleBin + paths.config.buildEnvairament + '/' + paths.config.dotnetcoreVersion + '/*.*')
            .pipe(gulp.dest(paths.destination.modules + listInterface[j] + paths.destination.moduleBin));
        gulp.src(paths.source.interfaces + listInterface[j] + paths.source.moduleBin + paths.config.buildEnvairament + '/' + paths.config.dotnetcoreVersion + '/*.*')
            .pipe(gulp.dest(paths.destination.moduleRun + "Modulars/" + listInterface[j] + "/bin"));
        gulp.src(paths.source.interfaces + listInterface[j] + paths.source.moduleBin + paths.config.buildEnvairament + '/' + paths.config.dotnetcoreVersion + '/' + listModules[i] + '.*')
            .pipe(gulp.dest(paths.destination.moduleRun));
    }

    for (var t = 0; t < listExtensions.length; t++) {
        console.log("extension: " + listExtensions[t]);
        console.log("____ " + paths.source.modules + listExtensions[t] + paths.source.moduleBin);

        gulp.src(paths.source.extensions + listExtensions[t] + paths.source.moduleBin + paths.config.buildEnvairament + '/' + paths.config.dotnetcoreVersion + '/' + listExtensions[t] + '.dll')
            .pipe(flatten())
            .pipe(gulp.dest(paths.destination.extension));
    }
});
