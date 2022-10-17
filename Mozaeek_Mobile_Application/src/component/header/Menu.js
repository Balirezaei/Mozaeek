import React from 'react';
import {TouchableOpacity, View,Text} from 'react-native';
import { widthPercentageToDP as wp } from 'react-native-responsive-screen';
import {SvgXml} from 'react-native-svg';
import { FlatList } from '..';
import GlobalStyle from '../../utils/GlobalStyle';
import sizes from '../../utils/sizes';
import { StaticArray } from '../../utils/StaticArray';
const Menu = (Props) => {
  function _renderItem(item) {
    return (
      <TouchableOpacity style={{flexDirection: 'row-reverse', paddingHorizontal:wp('5%'), paddingVertical:wp('3%')}}>
        <SvgXml style={{alignSelf:'center'}} xml={item.icon} />
        <Text style={[GlobalStyle.text,{marginEnd:wp('2%'), fontSize:sizes.font.middle}]}>{item.name}</Text>
      </TouchableOpacity>
    );
  }
  return (
    <View style={{flex:1}}>
      <FlatList data={StaticArray.Menu} item={(item) => _renderItem(item)} />
    </View>
  );
};
export default Menu;
