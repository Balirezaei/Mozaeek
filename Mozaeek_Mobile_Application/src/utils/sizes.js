import {Dimensions} from 'react-native';
import {widthPercentageToDP as wp,
  heightPercentageToDP as hp,} from 'react-native-responsive-screen'
export default {
  HEIGHT: Dimensions.get('window').height,
  WIDTH: Dimensions.get('window').width,
  button: {
    height: 40,
    width: 330,
    radius: 15,
  },
  input: {
    height: 50,
    width: 350,
    radius: 12,
    paddingHorizontal : wp('2%')
  },
  icon: {
    height: wp('1%'),
    width: wp('1%'),
  },

  font: {
    normal: 14,
    middle: 18,
    bold: 20,
  },

  // Space on both sides of the pages
  padding: {
    right: 20,
    left: 20,
    pagination: 50,
  },
  marginBetweenInput: {
    top: 15,
    bottom: 15,
    horizontal:wp('4%')
  },

  header:180,
  mainheader:80,
  footer: 70,

  lengthPagination: [1, 2, 3],
  widthPagination: Dimensions.get('window').width - 100,
  heightPagination: 50,
  heightC: 30,
  widthC: 30,

  inProcessHeight: 180,
  titleContainerInProcess: 30,
  imageInProcess: 70,

  userRequestHeight: 200,
  imageRequest: 100,

  listInfoHeight:80,
  containerMargin:{
    vertical:wp('4%'),
    horizontal:wp('5%')
  }
};
