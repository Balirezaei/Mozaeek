import React, {useEffect, useState} from 'react';
import {Text, StyleSheet, TouchableOpacity, Animated, View, ActivityIndicator} from 'react-native';
import LinearGradient from 'react-native-linear-gradient';
import colors from '../utils/colors';
import GlobalStyle from '../utils/GlobalStyle';
import sizes from '../utils/sizes';

const Button = (Props) => {
  return (
    <TouchableOpacity onPress={Props.onPress} style={[ Props.active  ? styles.buttonContainer: styles.buttonContainerInactive,Props.style]}>
      {Props.loading ? <ActivityIndicator color={'white'}/>:<Text style={[styles.textButton,{color:Props.active ?colors.white : colors.black}]}>{Props.title}</Text>}
    </TouchableOpacity>
  );
};

export default Button;
const styles = StyleSheet.create({
  textButton: {
    ...GlobalStyle.text,
    color: colors.whiteTwo,
    fontSize: sizes.font.bold,
    alignSelf: 'center',
  },
  buttonContainer: {
    
    borderRadius: sizes.button.radius,
    backgroundColor: colors.darkGreen,
    borderWidth: 1.5,
    borderColor: colors.darkGreen,
    flexDirection: 'row',
    justifyContent: 'space-around',
    alignItems: 'center',
    marginTop: sizes.marginBetweenInput.top,
    marginBottom: sizes.marginBetweenInput.bottom,
    marginHorizontal: sizes.marginBetweenInput.horizontal,
    minHeight: sizes.button.height, 
  },
  buttonContainerInactive:{
    borderRadius: sizes.button.radius,
    backgroundColor: colors.white,
    borderWidth: 1.5,
    borderColor: colors.gray,
    flexDirection: 'row',
    justifyContent: 'space-around',
    alignItems: 'center',
    marginTop: sizes.marginBetweenInput.top,
    marginBottom: sizes.marginBetweenInput.bottom,
    marginHorizontal: sizes.marginBetweenInput.horizontal,
    minHeight: sizes.button.height,
    alignSelf:'center'
  },
  button: {
    height: sizes.button.height,
    width: sizes.button.width,
    borderRadius: sizes.button.radius,
    justifyContent: 'center',
    alignItems: 'center',
  },
  buttonAnimated: {
    height: sizes.button.height,
    width: sizes.button.height,
    borderRadius: sizes.button.height / 2,
    backgroundColor: 'rgba(0,0,0,0)',
    justifyContent: 'center',
    alignItems: 'center',
    position: 'absolute',
  },
});
