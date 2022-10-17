import React, {useState, useEffect, useContext} from 'react';
import {Text, View, StyleSheet, TextInput} from 'react-native';
import {Button, Keyboard} from '../../../component';
import colors from '../../../utils/colors';
import GlobalStyle from '../../../utils/GlobalStyle';
import {Strings} from '../../../utils/Strings';
import {
  widthPercentageToDP as wp,
  heightPercentageToDP as hp,
} from 'react-native-responsive-screen';
import {useDispatch, useSelector} from 'react-redux';
import sizes from '../../../utils/sizes';
import {StaticArray} from '../../../utils/StaticArray';
import AppContext from '../../../context';
import {OTPVerify} from '../../../redux/Reducer/User';
import Storage from '../../../utils/Storage';
import {TOKEN} from '../../../utils/Constant';
const _ = require('lodash');

const CodeInput = (Props) => {
  const [counter, setCounter] = useState(60);
  const [validCode, setValidCode] = useState('');
  const [loading, setLoading] = useState(false);
  const dispatch = useDispatch();
  const {OTP_verify} = useSelector((state) => state.user);
  const {setUser} = useContext(AppContext);

  const [inputState, setInputState] = useState([
    {text: '-', id: 0},
    {text: '-', id: 1},
    {text: '-', id: 2},
    {text: '-', id: 3},
  ]);

  useEffect(() => {
    let myInterval = setInterval(() => {
      if (counter > 0) {
        setCounter(counter - 1);
      }
    }, 1000);
    return () => {
      clearInterval(myInterval);
    };
  });

  useEffect(() => {}, [Props.input]);

  function _setValidCode(item) {
    const array = inputState;
    const index = inputState.findIndex((x) => x.text === '-');
    if (index !== -1) {
      array[index].text = item;
      setInputState(array);
    }
  }
  async function _clearInput() {
    const array = inputState;

    let index = _.findLastIndex(array, function (o) {
      return o.text !== '-';
    });

    if (index !== -1) {
      array[index].text = '-';
      setInputState(array);
    }
  }

  useEffect(() => {
    let myInterval=null
    console.warn(OTP_verify);
    if (OTP_verify.hasOwnProperty('token')) {
      setLoading(false);
      setUser(TOKEN, OTP_verify.token);
      Storage.set(TOKEN, OTP_verify.token);
       myInterval = setInterval(() => {
        if (counter > 0) {
          setCounter(counter - 1);
        }
      }, 1000);
    } else if (OTP_verify === false) {
      setLoading(false);
    }
  }, [OTP_verify]);

  function _OTPVerify() {
    setLoading(true);
    let code = '';
    for (value of inputState) {
      console.log(value);
      code += value.text;
    }
    dispatch(
      OTPVerify({
        path: 'OTP/verify/',
        params: {code: code, mobileNo: Props.mobile},
        apiType: 1,
      }),
    );
  }

  return (
    <View
      style={{
        // justifyContent: 'center',
        flex: 1,
      }}>
      <View
        style={{
          flex: 1,

          marginVertical: sizes.containerMargin.vertical,
          marginHorizontal: sizes.containerMargin.horizontal,
        }}>
        <Text style={GlobalStyle.text}>{Strings.confirmCodeText}</Text>
        <View
          style={{
            flexDirection: 'row',
            alignSelf: 'center',
            marginTop: wp('5%'),
          }}>
          {inputState.map((value) => (
            <View style={[styles.maskedView]}>
              <Text style={[GlobalStyle.text, {textAlign: 'center'}]}>
                {value.text}
              </Text>
            </View>
          ))}
        </View>
        <Button
          active={inputState.findIndex((x) => x.text === '-') === -1}
          title={Strings.submit}
          style={
            inputState.findIndex((x) => x.text === '-') === -1
              ? GlobalStyle.button
              : {width: wp('30%')}
          }
          loading={loading}
          onPress={() => _OTPVerify() /*Props.submit()*/}
        />
        <Text
          style={[
            GlobalStyle.text,
            {textAlign: 'center'},
          ]}>{`00:${counter}`}</Text>
        <Text style={[GlobalStyle.text, {textAlign: 'center'}]}>
          {Strings.sendAgain}
        </Text>
      </View>
      <View style={{flex: 1, backgroundColor: colors.keyboardBack}}>
        <Keyboard
          onPress={(item) => _setValidCode(item)}
          clear={() => _clearInput()}
        />
      </View>
    </View>
  );
};
export default CodeInput;
const styles = StyleSheet.create({
  maskedView: {
    borderColor: colors.gray,
    borderWidth: 1,
    width: wp('12%'),
    height: wp('15%'),
    borderRadius: 5,
    alignSelf: 'center',
    marginEnd: wp('2%'),
    justifyContent: 'center',
  },
});
