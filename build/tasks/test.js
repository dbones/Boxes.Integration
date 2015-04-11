var nunit = require('gulp-nunit-runner');
var gulp = require('gulp');
var request = require('request');
var fs = require('fs');
var unzip = require('unzip');
var gulpif = require('gulp-if');
var mkdirp = require('mkdirp');
var config = require('../config');

var nunitLocation= 'http://github.com/nunit/nunitv2/releases/download/2.6.4/NUnit-2.6.4.zip';
var toolsDir = config.toolsDir;
var nunitDir = toolsDir + '/nunit';
var zipFileName = toolsDir + '/nunit.zip';
var runnerFileName =  nunitDir + '/NUnit-2.6.4/bin/nunit-console.exe';


gulp.task('test', ['get-nunit'], function () {
    return gulp
        .src(['**/bin/**/*Test.dll'], { read: false })
        .pipe(nunit({
			executable: runnerFileName,
			framework: 'net-4.0'
		}));
});


gulp.task('get-nunit', ['nunit-unzip'], function(done) {
	done();
});

gulp.task('nunit-unzip', ['nunit-download'], function(done) {

	var file = runnerFileName;
	var fileExists = fs.existsSync(file);

	if(fileExists) {
		done();
		return;
	}
	
	fs.createReadStream(zipFileName)
		.pipe(unzip.Extract({ path: nunitDir }))
		.on('close', function () {done();});
});


gulp.task('nunit-download', function() {

	mkdirp.sync(toolsDir);

	var file = zipFileName;
	var zipDoesNotExist = !(fs.existsSync(nunitDir));

	return gulp.src(file, { read: false })
		.pipe(gulpif(zipDoesNotExist, request(nunitLocation)))
		.pipe(gulpif(zipDoesNotExist, fs.createWriteStream(file)));
		
});