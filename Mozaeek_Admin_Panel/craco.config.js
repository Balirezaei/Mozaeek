const CracoLessPlugin = require('craco-less');

module.exports = {
  plugins: [
    {
      plugin: CracoLessPlugin,
      options: {
        lessLoaderOptions: {
          lessOptions: {
            modifyVars: {
              'font-size-base': '13px',
              '@font-size-lg': '13px',
              '@font-size-sm': '13px'
            },
            javascriptEnabled: true
          }
        }
      }
    }
  ]
};