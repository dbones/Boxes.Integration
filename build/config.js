var args = require('yargs').argv;
var path = require('path');

module.exports = {

    outputDir : path.resolve('./output'),
    toolsDir : path.resolve('./tools'),
    buildVersion : args.buildNumber ? '0.1.' + args.buildNumber  : undefined,

    //command config
    command:{
        package: {
            dependencyNameOverride: '//x:dependency[starts-with(@x:id, \'Boxes.\')]/@x:version',
            //configFile: null
        }
    }
};