// Common styles
import {StyleSheet, Dimensions} from 'react-native';
import fonts from './fonts';
import colors from './colors';
import sizes from './sizes';
import {
  widthPercentageToDP as wp,
  heightPercentageToDP as hp,
} from 'react-native-responsive-screen';
const HEIGHT = Dimensions.get('window').height;
const WIDTH = Dimensions.get('window').width;

export default (GlobalStyle = StyleSheet.create({
  centerContainer: {
    flex: 1,
    justifyContent: 'center',
    alignItems: 'center',
  },
  text: {
    ...Platform.select({
      ios: {
        fontFamily: fonts.fontFamily_Ios,
      },
      android: {
        fontFamily: fonts.fontFamilyMedium_Android,
        
      },
    }),
  },
  textBold: {
    ...Platform.select({
      ios: {
        fontFamily: fonts.fontFamily_Ios,
        // fontWeight: 'bold',
      },
      android: {
        fontFamily: fonts.fontFamilyBold_Android,
        // fontWeight:'bold'
      },
    }),
  },
  textInput:{
    borderWidth: 1,
    borderColor: colors.gray,
    borderRadius:sizes.input.radius,
    paddingHorizontal : sizes.input.paddingHorizontal
    
  },
  button: {
    backgroundColor: colors.blue,
    width: wp('30%'),
    borderColor: colors.blue,
    alignSelf: 'center',
  },
  
}));
