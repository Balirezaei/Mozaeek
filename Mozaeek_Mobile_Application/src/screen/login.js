import React, {useEffect, useState} from 'react';
import {
  Image,
  View,
  StyleSheet,
  Dimensions,
  ScrollView,
  Text,
  Modal,
  TouchableOpacity,
} from 'react-native';
import colors from '../utils/colors';

import {Screen} from '../component';
import {
  widthPercentageToDP as wp,
  heightPercentageToDP as hp,
} from 'react-native-responsive-screen';
import {Svg, Ellipse, SvgXml} from 'react-native-svg';
import AppIntroSlider from 'react-native-app-intro-slider';
import {XmlFile} from '../utils/Xml';
import GlobalStyle from '../utils/GlobalStyle';
import {StaticArray} from '../utils/StaticArray';

const Login = (Props) => {
  function _renderItem(item) {
    console.warn(item);
    return (
      <View style={{alignItems: 'center', marginTop: wp('10%')}}>
        <SvgXml xml={item.xml} />
        <Text style={[GlobalStyle.textBold]}>{item.title}</Text>
        <Text style={[GlobalStyle.text]}>{item.hintText}</Text>
      </View>
    );
  }
  return (
    <Screen>
      <View style={{flex: 1, backgroundColor: colors.blue}}>
        <View style={{height: hp('70%'), backgroundColor: colors.white}}>
          <AppIntroSlider
            renderItem={({item}) => _renderItem(item)}
            data={StaticArray.sliderLogin}
          />
        </View>
        <Svg height={wp('10%')} width={wp('100%')}>
          <Ellipse
            cx="50%"
            cy="2%"
            rx="50%"
            ry="30"
            stroke={colors.white}
            strokeWidth="2"
            fill={colors.white}
          />
        </Svg>
        <View
          style={styles.loginInputContainer}>
          <Text>jhgdjkfhgd</Text>
        </View>
      </View>
    </Screen>
  );
};

export default Login;
const styles = StyleSheet.create({
  loginInputContainer:{
    width: wp('80%'),
    height: hp('20%'),
    position: 'absolute',
    alignSelf: 'center',
    bottom: 0,
    backgroundColor: colors.white,
    borderWidth: 2,
    borderTopEndRadius: 10,
    borderTopStartRadius: 10,
  }
})