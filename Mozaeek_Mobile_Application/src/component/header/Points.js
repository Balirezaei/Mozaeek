import React,{useState} from 'react';
import {
  View,
  TouchableOpacity,
  Text,
  StyleSheet,
  TextInput,
} from 'react-native';
import {widthPercentageToDP as wp} from 'react-native-responsive-screen';
import {SvgXml} from 'react-native-svg';
import _ from 'lodash'
import {FlatList} from '..';
import GlobalStyle from '../../utils/GlobalStyle';
import sizes from '../../utils/sizes';
import {StaticArray} from '../../utils/StaticArray';
import {Strings} from '../../utils/Strings';
import {XmlFile} from '../../utils/Xml';
const Points = () => {
    const [cities, setCities] = useState(StaticArray.cities)


    function _searchCities(text){

        if (text.length!==0) {
            const array = cities.filter(x=> x.title.toString().includes(text))
                
            setCities(array)
        }else{
            setCities(StaticArray.cities)
        }

    }
  function _renderItem(item) {
    return (
      <TouchableOpacity
        style={{
          flexDirection: 'row-reverse',
          paddingHorizontal: wp('5%'),
          paddingVertical: wp('3%'),
        }}>
        <Text
          style={[
            GlobalStyle.text,
            {marginEnd: wp('2%'), fontSize: sizes.font.middle},
          ]}>
          {item.title}
        </Text>
      </TouchableOpacity>
    );
  }

  return (
    <View>
      <View style={{flexDirection:'row', justifyContent:'flex-end',marginEnd:wp('7%')}}>
        <SvgXml style={{alignSelf:'center', }} xml={XmlFile.search} />
        <TextInput
          placeholder={Strings.search}
          style={[GlobalStyle.textInput, GlobalStyle.text, styles.inputStyle]}
          showSoftInputOnFocus={true}
          onChangeText={(text)=> _searchCities(text)}
        />
      </View>
      <FlatList data={cities} item={(item) => _renderItem(item)} />
    </View>
  );
};
export default Points;
const styles = StyleSheet.create({
  inputStyle: {
    marginVertical: sizes.containerMargin.vertical,
    marginTop: wp('8%'),
    width: wp('65%'),
    marginHorizontal: wp('2%'),
  },
});
