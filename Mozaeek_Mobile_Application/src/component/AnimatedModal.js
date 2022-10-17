import React from 'react';
import {StyleSheet, TouchableOpacity, View} from 'react-native';
import Animated, {Easing} from 'react-native-reanimated';
import {
  heightPercentageToDP as hp,
  widthPercentageToDP as wp,
} from 'react-native-responsive-screen';
import colors from '../utils/colors';

const AnimatedModal = ({children, close}) => {
  // clock = new Clock();
  // and use runTiming method defined above to create a node that is going to be mapped
  // to the translateX transform.

  return (
    <View style={styles.container}>
      <TouchableOpacity
        style={{backgroundColor: 'transparent', height: hp('13%')}}
        onPress={()=>close()}
      />
      <View style={styles.box}>{children}</View>
    </View>
  );
};
export default AnimatedModal;
const styles = StyleSheet.create({
  container: {
    backgroundColor: 'transparent',
  },
  box: {
    backgroundColor: 'white',
    height: hp('75%'),
    width: wp('100%'),
    borderBottomColor: colors.white,
    borderBottomWidth: wp('6%'),
    borderBottomEndRadius: wp('6%'),
    borderBottomStartRadius: wp('6%'),
  },
});
