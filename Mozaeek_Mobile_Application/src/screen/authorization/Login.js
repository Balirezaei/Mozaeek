import React, {useEffect, useState, useRef} from 'react';
import {
  Image,
  View,
  StyleSheet,
  Dimensions,
  ScrollView,
  Text,
  Modal,
  TouchableOpacity,
  TextInput,
} from 'react-native';
import colors from '../../utils/colors';
import ButtonComponent from '../../component/Button';
import Idcard from 'react-native-vector-icons/AntDesign';
import Mobile from 'react-native-vector-icons/FontAwesome';
import InputComponent from '../../component/inputComponent';
import {validate} from '../../utils/validation';
import Icon from 'react-native-vector-icons/FontAwesome';
import NetInfo from '@react-native-community/netinfo';
import {Button, IconButton, Keyboard, Screen} from '../../component';
import {
  widthPercentageToDP as wp,
  heightPercentageToDP as hp,
} from 'react-native-responsive-screen';
import {Svg, Ellipse, SvgXml} from 'react-native-svg';
import AppIntroSlider from 'react-native-app-intro-slider';
import {XmlFile} from '../../utils/Xml';
import GlobalStyle from '../../utils/GlobalStyle';
import {StaticArray} from '../../utils/StaticArray';
import {Strings} from '../../utils/Strings';
// import {color} from 'react-native-reanimated';
import sizes from '../../utils/sizes';
import CodeInput from './component/CodeInput';
import MobileInput from './component/MobileInput';
import Welcome from './component/Welcome';

const Login = (Props) => {
  const slider = useRef();
  const [viewType, setViewType] = useState(0);
  const [mobile, setMobile] = useState(null);
  const [getCode, setGetCode] = useState(false);
  const [keyboardItem, setKeyboardItem] = useState(null)
  let page = 0;
  useEffect(() => {
    setInterval(() => {
      _slider()
    }, 3000);
  }, []);

  function _slider() {
    if (slider.current!==null) {
      slider.current.goToSlide(page); 
    page += 1;
    if (page == StaticArray.sliderLogin.length) {
      page = 0;
    }
    }
    
  }
  function _renderItem(item) {
    console.warn(item);
    return (
      <View style={{alignItems: 'center', marginTop: wp('10%')}}>
        <SvgXml width={wp('80%')} height={wp('80%')} xml={item.xml} />
        <Text style={[GlobalStyle.textBold]}>{item.title}</Text>
        <Text style={[GlobalStyle.text]}>{item.hintText}</Text>
      </View>
    );
  }

  function _renderMainView() {
    switch (viewType) {
      case 0:
        return (
          <View style={styles.loginInputContainer}>
           <Welcome onPress={()=>[setViewType(1),setGetCode(false)]}/>
          </View>
        );
      case 1:
        return (
          <View
            style={[
              styles.loginInputContainer,
              {height: hp('29%')},
              styles.shadowContainer,
            ]}>
            <View style={styles.secondViewContainer}>
              <Text style={[GlobalStyle.text, {marginTop: wp('2%')}]}>
                {Strings.loginText}
              </Text>
              <TouchableOpacity
                activeOpacity={1}
                style={[
                  GlobalStyle.textInput,
                  GlobalStyle.text,
                  styles.inputStyle,
                ]}
                onPress={() => setViewType(2)}>
                <Text
                  style={[
                    GlobalStyle.text,styles.customTextStyle
                  ]}>
                  {Strings.mobile}
                </Text>
              </TouchableOpacity>
              <Button
                active={true}
                title={Strings.submit}
                style={styles.button}
              />
            </View>
          </View>
        );
      case 2:
        return (
          <View
            style={[
              styles.loginInputContainer,
              {height: hp('85%')},
              styles.shadowContainer,
            ]}>
              {getCode ? <CodeInput mobile={mobile} /> : <MobileInput submit={(mobile)=> [setGetCode(true),setMobile(mobile)]} />}
           
          </View>
        );

    }
  }
  return (
    <Screen>
      <View style={{flex: 1, backgroundColor: colors.blue}}>
        {viewType !== 0 ? (
          <IconButton
            containerStyle={styles.iconContainer}
            xml={XmlFile.backIcon}
            onPress={() =>[ setViewType(viewType - 1), setGetCode(false)]}
          />
        ) : null}

        <View style={{height: hp('75%'), backgroundColor: colors.white}}>
          {viewType !== 2 ? (
            <AppIntroSlider
              renderItem={({item}) => _renderItem(item)}
              data={StaticArray.sliderLogin}
              ref={slider}
            />
          ) : null}
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
        {_renderMainView()}
      </View>
    </Screen>
  );
};

export default Login;
const styles = StyleSheet.create({
  loginInputContainer: {
    width: wp('80%'),
    height: hp('15%'),
    position: 'absolute',
    alignSelf: 'center',
    bottom: 0,
    backgroundColor: colors.white,
    borderWidth: 2,
    borderColor: colors.white,
    borderTopEndRadius: 10,
    borderTopStartRadius: 10,
    zIndex: 100,
  },
  iconContainer: {
    position: 'absolute',
    top: wp('4%'),
    left: wp('5%'),
    zIndex: 100,
  },
  shadowContainer: {
    shadowColor: '#000',
    shadowOffset: {
      width: 10,
      height: 10,
    },
    shadowOpacity: 1,
    shadowRadius: wp('1%'),
    elevation: 10,
    borderWidth: 1,
    borderColor: colors.gray,
  },
  secondViewContainer: {
    marginVertical: sizes.containerMargin.vertical,
    marginHorizontal: sizes.containerMargin.horizontal,
  },
  inputStyle: {
 
  },
  button: {
    backgroundColor: colors.blue,
    width: wp('30%'),
    borderColor: colors.blue,
    alignSelf: 'center',
  },
  customTextStyle:{
    marginTop: wp('2%'),
    color: colors.gray,
    textAlignVertical: 'center',
    paddingTop: wp('1%'),
    paddingBottom: wp('3%'),
  }
});
