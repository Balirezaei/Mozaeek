import React, {useState,useEffect} from 'react';
import {View, StyleSheet, Text, TextInput} from 'react-native';
import {useDispatch, useSelector} from 'react-redux';
import GlobalStyle from '../../../utils/GlobalStyle';
import sizes from '../../../utils/sizes';
import {Strings} from '../../../utils/Strings';
import {
  widthPercentageToDP as wp,
  heightPercentageToDP as hp,
} from 'react-native-responsive-screen';
import colors from '../../../utils/colors';
import {Button, Keyboard} from '../../../component';
import {OTPStart} from '../../../redux/Reducer/User';
const _ = require('lodash');

const MobileInput = (Props) => {
  const [mobile, setMobile] = useState('');
  const [loading, setLoading] = useState(false);
  const {OTP_status} = useSelector((state) => state.user);
  const dispatch = useDispatch();

  function _clearInput() {
    const array = mobile.split('');
    setMobile(_.dropRight(array).join(''));
  }

  useEffect(() => {
   console.warn(OTP_status)
   OTP_status ? [setLoading(false),Props.submit(mobile)]:null
  }, [OTP_status])
  function _OTPStart() {
    setLoading(true)
    dispatch(
      OTPStart({path: 'otp/start/', params: {mobileNo: mobile}, apiType: 1}),
    );
  }
  return (
    <View style={{flex: 1}}>
      <View style={styles.secondViewContainer}>
        <Text style={[GlobalStyle.text, {marginTop: wp('2%')}]}>
          {Strings.loginText}
        </Text>
        <TextInput
          placeholder={mobile.length !== 0 ? mobile.toString() : Strings.mobile}
          style={[GlobalStyle.textInput, GlobalStyle.text, styles.inputStyle]}
          //onFocus={() => setViewType(2)}
          showSoftInputOnFocus={false}
          value={mobile}
        />
        <Button
        loading={loading}
          active={true}
          title={Strings.submit}
          style={styles.button}
          onPress={() => _OTPStart() /*Props.submit()*/}
        />
      </View>
      <View style={{flex: 1, backgroundColor: colors.keyboardBack}}>
        <Keyboard
          onPress={(item) => setMobile(mobile + item.toString())}
          clear={() => _clearInput()}
        />
      </View>
    </View>
  );
};
export default MobileInput;
const styles = StyleSheet.create({
  secondViewContainer: {
    marginVertical: sizes.containerMargin.vertical,
    marginHorizontal: sizes.containerMargin.horizontal,
    justifyContent: 'space-around',
    flex: 1,
  },
  inputStyle: {
    marginVertical: sizes.containerMargin.vertical,
    marginTop: wp('8%'),
  },
  button: {
    backgroundColor: colors.blue,
    width: wp('30%'),
    borderColor: colors.blue,
    alignSelf: 'center',
  },
});
