
import React, {useEffect, useState} from 'react';
import {
  View,
  Text,
  StyleSheet,
  Dimensions,
  TouchableOpacity,
  ScrollView,
  BackHandler,
  Image,
} from 'react-native';
import CodeInputComponent from '../component/codeInputComponent';
// import ButtonComponent from '../component/buttonComponent';
import colors from '../utils/colors';
import GlobalStyle from '../utils/GlobalStyle';
import BackgroundTimer from 'react-native-background-timer';
import NetInfo from '@react-native-community/netinfo';


const HEIGHT = Dimensions.get('window').height;
const WIDTH = Dimensions.get('window').width;

const pad = n => {
  return n < 10 ? '0' + n : n;
};

const otpLogin = props => {
  const [enableTimer, setEnableTimer] = useState(false);
  const [disableBtnResendCode, setDisableBtnResendCode] = useState(true);
  const [timer, setSTimer] = useState(120);
  const [timerText, setTimerText] = useState('02:00');
  const [code, setCode] = useState();

  const [net, setNet] = useState();
  const [error, setError] = useState();

  // const [status, setstatus] = useState(props.language.status)

  // props.setloading(false);
  useEffect(() => {
    BackHandler.addEventListener('hardwareBackPress', () =>
      _backHandlerOnAndroid(),
    );

    return () => {
      BackHandler.removeEventListener('hardwareBackPress', () =>
        _backHandlerOnAndroid(),
      );
    };
  }, []);

  const _backHandlerOnAndroid = () => {
    props.navigation.goBack();
    return true;
  };

  useEffect(() => {
    BackgroundTimer.runBackgroundTimer(() => {
      setSTimer(prev => {
        var m = parseInt(prev / 60);
        var s = parseInt(prev % 60);

        setTimerText(`${pad(m)}:${pad(s)}`);

        if (prev == 0) {
          BackgroundTimer.stopBackgroundTimer();
          setDisableBtnResendCode(false);
          setTimerText('');
          return 0;
        } else {
          setTimerText(`${pad(m)}:${pad(s)}`);
        }
        return prev - 1;
      });
    }, 1000);

    return () => BackgroundTimer.stopBackgroundTimer();
  }, [enableTimer]);
  function resendVerifyCode() {
    // props.loginAction(props.navigation);
    if (timer <= 0) {
      //props.login(phoneNum).then(res => {
      console.log('resssssssssssss');
      // if (res) {
      setTimerText('02:00');
      setSTimer(120);
      setEnableTimer(enableTimer => !enableTimer);
      //  }
      // });
    }
  }

  //     // if (res) {
  //     setTimerText('02:00');
  //     setSTimer(120);
  //     setEnableTimer(enableTimer => !enableTimer);
  //     //  }
  //     // });
  //   }
  // }

  const saveCode = code => {
    setCode(code);
    // props.setCodeAction(code);
  };

  const navigate = () => {
    if (code !== undefined && code !== '' && code.length == 4)
   
      NetInfo.addEventListener((state) => {
        if (state.isConnected) {
          // setTimeout(() => {
          //     props.navigation.navigate('Index')
          // },4000);
          setNet(true);
          // props.navigation.navigate('Index')
  
          fetch('http://37.152.181.14:300/api/otp/verify', {
            method: 'POST',
            // headers: {
            //   Accept: 'application/json',
            //   'Content-Type': 'application/json',
            // },
            body: JSON.stringify({
              code: code,
              mobileNo: "09193462561"
            }),
          })
            .then((response) => response.json())
            // if (res.status === 'success') {
            //     console.warn(res)
            //   return  props.navigation.navigate('Index')
            .then((responseJson) => {
              if (responseJson.status == 'success') {
                // console.warn(responseJson);
                // AsyncStorage.setItem('login', JSON.stringify(1));
                props.navigation.navigate('OtpLogin');
              } else {
                // setError(responseJson.message[0]);
                props.navigation.navigate('SecendPage');
              }
            });
        } else {
          setNet(false);
          setError('لطفا ابتدا اینترنت خود را روشن کنید سپس مجددا تلاش کنید');
        }
      });


      // props.setloading(true),
      // props.otpAction(props.navigation)
      // props.navigation.navigate('Home');
    else alert('کد را وارد کنید', code);
  };

  return (
    <View style={[styles.container]}>

      <Text
        style={[
          styles.title,
          {color: colors.navyblue, textAlign: 'center', marginTop: '15%'},
        ]}>
        کد چهار رقمی را وارد کنید
      </Text>
      <CodeInputComponent
        style={{alignSelf: 'center'}}
        setCode={code => saveCode(code)}
        textStyle={{color:colors.white}}
      />
      <TouchableOpacity
        disabled={disableBtnResendCode}
        onPress={resendVerifyCode}>
        <Text style={[styles.timer, {color:colors.navyblue,alignSelf: 'center'}]}>
          ارسال مجدد {timerText}
        </Text>
      </TouchableOpacity>
      <View style={{marginTop: 20, alignSelf: 'center'}}>
        {/* <ButtonComponent
          buttonStyle={{
            borderRadius: 10,
            backgroundColor: colors.blue,
            borderColor: colors.blue,
          }}
          onClick={() => navigate()}
          titleStyle={{fontSize: 16}}
          titleStyle={{color: colors.white, fontSize: 18}}
          title={'ثبت'}
        /> */}
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    flex: 1,
    backgroundColor: colors.lightGray,
  },
  header: {
    justifyContent: 'center',
    alignItems: 'center',
    width: WIDTH,
    height: '10%',
  },
  city: {
    marginTop: '10%',
    width: '90%',
    alignSelf: 'center',
    borderBottomWidth: 1,
    borderBottomColor: colors.gray,
  },
  boxtotal: {
    alignSelf: 'center',
    width: '35%',
    borderBottomWidth: 1,
    borderBottomColor: colors.gray,
    // backgroundColor:'blue'
  },
  boxcontainer: {
    width: '100%',
    flexDirection: 'row',
    justifyContent: 'space-around',
  },
  title: {
    ...GlobalStyle.textBold,
    color: colors.gray,
    fontSize: 20,
  },
  box: {
    flexDirection: 'row',
    // backgroundColor:'red',
    width: '100%',
    justifyContent: 'space-between',
    alignItems: 'center',
    height: 62,
  },
  timer: {
    ...GlobalStyle.text,
    color: colors.darkGray,
    fontSize: 13,
    //marginBottom: WIDTH * 0.05,
  },
});

export default otpLogin;

{
  /* <CountDown
  size={15}
  until={120}
  onFinish={() => alert('Finished')}
  digitStyle={{ direction: 'ltr' }}
  digitTxtStyle={{ color: 'rgb(150,150,150)', direction: 'ltr', }}
  timeLabelStyle={{ color: 'red', fontWeight: 'bold', direction: 'rtl', }}
  separatorStyle={{ color: '#cccccc' }}
  timeToShow={['S', 'M']}
  timeLabels={{ m: null, s: null }}
  showSeparator
/> */
}
