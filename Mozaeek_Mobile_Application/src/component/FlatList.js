import React from 'react';
import {View,FlatList as FL} from 'react-native';
import { widthPercentageToDP as wp } from 'react-native-responsive-screen';
import colors from '../utils/colors';
const FlatList = (Props) => {
  return (
    <FL
    style={{backgroundColor:colors.white, margin:wp('5%')}}
      data={Props.data}
      renderItem={({item}) => Props.item(item)}
      showsVerticalScrollIndicator={false}
      ItemSeparatorComponent={() => (
        <View style={{height: 2, backgroundColor: colors.keyboardBack, marginHorizontal:wp('5%')}} />
      )}
    />
  );
};
export default FlatList;
