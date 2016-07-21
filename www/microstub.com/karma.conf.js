module.exports = function(config) {
  config.set({
    frameworks: ['jasmine'],

    files: ['karma.entry.js'],

    preprocessors: {
      'karma.entry.js': ['webpack', 'sourcemap']
    },

    webpack: require('./webpack.config'),

    webpackServer: {
      noInfo: true
    },

    reporters: ['mocha'],

    logLevel: config.LOG_INFO,

    autoWatch: true,

    singleRun: false,

    browsers: ['Chrome']
  });
};
