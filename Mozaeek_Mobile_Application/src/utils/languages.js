// ES6 module syntax
import LocalizedStrings from 'react-native-localization';
import global from '../utils/global'

// CommonJS syntax
// let LocalizedStrings  = require ('react-native-localization');

export let languages = new LocalizedStrings(
  global.languageValue
);
