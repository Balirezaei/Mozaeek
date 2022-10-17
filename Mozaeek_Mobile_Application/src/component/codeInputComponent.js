/* component for code input
 * see allStyle from src/utils/allStyle
 * see colors from src/utils/colors
 * see sizes from src/utils/sizes
 * @props  lable ,style
 */
import React, {useEffect, useState} from 'react';
import {View, Text, StyleSheet, TextInput} from 'react-native';
import GlobalStyle from '../utils/GlobalStyle';
import colors from '../utils/colors';
import sizes from '../utils/sizes'
import SmoothPinCodeInput from 'react-native-smooth-pincode-input';

const codeInputComponent = props => {

  const [code,setCode]=useState()

  const changeTextCode=(val)=>{
    setCode(val)
    props.setCode(val)
  }


  return (
    <View>
      <View style={styles.labelContainer}>
        <Text style={styles.lable}>{props.lable}</Text>
      </View>

      <View style={[styles.inputContainer, {...props.style}]}>
        <SmoothPinCodeInput
          cellStyle={{
            borderBottomWidth: 1,
            borderColor: colors.blue,
            height:sizes.input.height/3,
          
          }}
          cellStyleFocused={{
            borderColor: 'black',
          }}
          textStyle={[styles.textStyle]}
          cellSize={30}
          codeLength={4}
          value={code}
          onTextChange={code => changeTextCode(code)}
        />

      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {},
  labelContainer: {
    flexDirection: 'row',
    width: sizes.input.width,
    marginBottom: 5,
    paddingRight: 5,
    paddingLeft: 15,
  },
  lable: {
    ...GlobalStyle.textBold,
    color: colors.darkGray,
    flex: 1,
  },
  textStyle:{
    ...GlobalStyle.textBold,
    color: colors.darkGray,
    fontSize:20
  },

  inputContainer: {
    flexDirection: 'row',
    height: sizes.input.height,
    width: sizes.input.width,
    position: 'relative',
    marginBottom: sizes.marginBetweenInput.bottom,
    borderRadius: sizes.button.radius,
    backgroundColor: colors.white,
    // borderWidth: 0.2,
    // borderColor: colors.lightGray,
    // shadowColor: '#000',
    // shadowOffset: {
    //   width: 0,
    //   height: 2,
    // },
    // shadowOpacity: 0.25,
    // shadowRadius: 3.84,
    // elevation: 3,
    justifyContent: 'center',
    alignItems: 'center',
    paddingRight: 10,
    paddingLeft: 15,
  },
  input: {
    ...GlobalStyle.text,
    textAlign: 'right',
    paddingRight: 10,
    flex: 1,
  },
  icon: {
    height: sizes.icon.height,
    width: sizes.icon.width,
  },
});

export default codeInputComponent;
