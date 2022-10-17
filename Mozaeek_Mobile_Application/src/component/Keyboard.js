import React from 'react';
import {FlatList, Text, View, StyleSheet} from 'react-native';
import {TouchableOpacity} from 'react-native-gesture-handler';
import colors from '../utils/colors';
import {StaticArray} from '../utils/StaticArray';
import {
    widthPercentageToDP as wp,
    heightPercentageToDP as hp,
  } from 'react-native-responsive-screen';
import GlobalStyle from '../utils/GlobalStyle';
import IconButton from './IconButton';
import { XmlFile } from '../utils/Xml';


const Keyboard = (Props) => {
  function _renderItem(item) {
      if (item !== 10) {
        return (
            <TouchableOpacity onPress={ ()=>   Props.onPress(item) }
             activeOpacity={1} style={ [styles.button, item===10 ? {backgroundColor:'transparent', borderColor:'transparent'}:styles.shadow]}>
              <Text style={[GlobalStyle.text,{textAlign:'center',alignSelf:'center', fontSize:wp('5%')}]}>{item}</Text>
            </TouchableOpacity>
          );
      }else{
          return(
              <IconButton onPress={()=> Props.clear()} xml={XmlFile.keyboardClear}  containerStyle={ [styles.button,  {backgroundColor:'transparent', borderColor:'transparent', alignItems:'center'}]}/>
          )
      }
    
  }

  return (
    <View>
      <FlatList
        numColumns={3}
        data={StaticArray.keyboard}
        style={{alignSelf:'center'}}
        renderItem={({item}) => _renderItem(item)}
      />
    </View>
  );
};
export default Keyboard;
const styles = StyleSheet.create({
    button:{

        backgroundColor:colors.white,
        width:wp('23%'),
        marginHorizontal:wp('1%'),
        marginVertical:wp('2%'),
        height:wp('10%'),
        justifyContent:'center',
        borderWidth:2,
        borderColor:colors.white,
        borderRadius:wp('2%'),
        
       
    },
    shadow:{
        shadowColor: '#000',
        shadowOffset: {
          width: 10,
          height: 10,
        },
        shadowOpacity: 1,
        shadowRadius: wp('1%'),
        elevation: 3,
    }
})
