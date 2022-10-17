import React from 'react';
import {Text, View, TextInput, StyleSheet} from 'react-native';
import { widthPercentageToDP as wp} from 'react-native-responsive-screen';
import {IconButton} from '..';
import colors from '../../utils/colors';
import GlobalStyle from '../../utils/GlobalStyle';
import sizes from '../../utils/sizes';
import {Strings} from '../../utils/Strings';
import {XmlFile} from '../../utils/Xml';
const Header = (Props) => {
  return (
    <View style={{flexDirection: 'row', alignItems:'center',justifyContent:'space-between', marginHorizontal:wp('2%')}}>
      <IconButton xml={XmlFile.profile} onPress={() => Props.setModalType(3)} containerStyle={{alignSelf:'center', marginTop:wp('4%'),}}  />
      <TextInput
        placeholder={Strings.placeholderInput}
        style={[GlobalStyle.textInput, GlobalStyle.text, styles.inputStyle]}
        showSoftInputOnFocus={false}
        value={''}
      />
      <Text onPress={()=>Props.setModalType(2)} style={[GlobalStyle.textBold, {color: colors.blue, fontSize:wp('5%'), marginEnd:wp('1%')}]}>تهران</Text>
      <IconButton xml={XmlFile.mozaeek} onPress={() => null} />
    </View>
  );
};
export default Header;
const styles = StyleSheet.create({
   
    inputStyle: {
      marginVertical: sizes.containerMargin.vertical,
      marginTop: wp('8%'),
      width:wp('65%'),
      marginHorizontal:wp('2%')
    },
    
  });
