import React, {useEffect, useState} from 'react';
import {View, Text, StyleSheet, TextInput} from 'react-native';

import GlobalStyle from './../utils/GlobalStyle';
import colors from './../utils/colors';
import sizes from '../utils/sizes';

const inputComponent = props => {
  const [values, setValue] = useState();
  const [color, setcolor] = useState(colors.gray);

  const onChangeText = value => {
    setValue(value);
    props.onChangeInput(value);
  };
  return (
    <View style={[styles.container, {...props.containertotal}]}>
      <View style={[styles.labelContainer, {...props.lableStyle}]}>
        {/* show icon for lable if  props.iconLable exist */}
        {props.iconLable !== undefined && props.iconLable}

        <Text style={[styles.lable, {...props.stylelable}]}>{props.lable}</Text>
      </View>

      <View
        style={[
          styles.inputContainer,
          {...props.style},
          // {
          //   borderColor:
          //     props.errorMessage !== undefined &&
          //     props.errorMessage !== '' &&
          //     props.errorMessage !== null
          //       ? colors.orange
          //       : colors.lightGray,
          // },
        ]}>
        {/* show icon for textInput if  props.iconLable exist */}

        <TextInput
          editable={props.disable}
          autoFocus={false}
          multiline={true}
          onBlur={() => {
            setcolor(colors.lightGray);
          }}
          onFocus={() => {
            setcolor(colors.green);
          }}
          style={[styles.input, {...props.input}]}
          placeholder={props.placeholder}
          onChangeText={text => onChangeText(text)}
          defaultValue={
            values == undefined
              ? props.defaultValue != undefined
                ? props.defaultValue
                : ''
              : values
          }
          keyboardType={
            props.keyboardType !== undefined && props.keyboardType == 'numeric'
              ? 'numeric'
              : 'default'
          }
          maxLength={props.maxLength}
          multiline={true}
          numberOfLines={12}
          textAlignVertical="top"
        />
        {props.icons !== undefined && props.icons}
      </View>
      {props.errorMessage !== undefined &&
        props.errorMessage !== '' &&
        props.errorMessage !== null && (
          <Text style={[styles.error,props.eror]}>{props.errorMessage}</Text>
        )}
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    marginBottom: sizes.marginBetweenInput.bottom,
  },
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
    textAlign: 'right',
  },
  iconLabel: {
    height: sizes.icon.height,
    width: sizes.icon.width,
    // backgroundColor:'red'
  },

  inputContainer: {
    flexDirection: 'row',
    position: 'relative',
    height: sizes.input.height,
    // width: sizes.input.width,
    width: '100%',
    // backgroundColor: colors.white,
    // borderBottomWidth: 1,
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
    // height: 40,
    ...GlobalStyle.text,
    textAlign: 'right',
    paddingRight: 20,
    flex: 1,
  },
  error: {
    ...GlobalStyle.text,
    textAlign: 'right',
    paddingRight: 10,
    marginTop: 10,
    color: colors.black,
  },
  icon: {
    height: sizes.icon.height,
    width: sizes.icon.width,
  },
});

export default inputComponent;
