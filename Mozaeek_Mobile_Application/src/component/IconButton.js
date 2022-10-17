import React, {useEffect, useState} from 'react';
import {Text, StyleSheet, TouchableOpacity, Animated, View} from 'react-native';
import LinearGradient from 'react-native-linear-gradient';
import {SvgXml} from 'react-native-svg';
import colors from '../utils/colors';
import GlobalStyle from '../utils/GlobalStyle';
import {
    widthPercentageToDP as wp,
    heightPercentageToDP as hp,
  } from 'react-native-responsive-screen';
import sizes from '../utils/sizes';

const IconButton = (Props) => {
  return (
    <TouchableOpacity onPress={Props.onPress} style={Props.containerStyle}>
      <SvgXml style={styles.icon} xml={Props.xml} />
    </TouchableOpacity>
  );
};

export default IconButton;
const styles = StyleSheet.create({
  icon: {
    height: sizes.icon.height,
    width: sizes.icon.width,
  
  },
});
