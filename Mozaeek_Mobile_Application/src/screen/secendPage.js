import React, {useEffect, useState} from 'react';
import {
  Image,
  View,
  StyleSheet,
  Dimensions,
  ScrollView,
  Text,
  Modal,
  TouchableOpacity,
  FlatList,
} from 'react-native';
import colors from '../utils/colors';
// import HeaderComponent from '../component/headerComponent';
import Noti from 'react-native-vector-icons/AntDesign';

const HEIGHT = Dimensions.get('window').height;
const WIDTH = Dimensions.get('window').width;

const data = [
  {id: 1, colors: '#a6a6a6', text: 'بازنشسته تامین اجتمایی'},
  {id: 1, colors: '#ed7d31', text: 'سازمان سنجش'},
  {id: 1, colors: '#5b9bd5', text: 'پایان کار ساختمان'},
  {id: 1, colors: '#70ad47', text: 'دانشجوی ارشد'},
  {id: 1, colors: '#4472c4', text: 'معملات سهام'},
  {id: 1, colors: '#ffc000', text: 'تسهیلات مسکن'},
  {id: 1, colors: '#c00000', text: 'مراد/خواستگاه/برچسب/زمینه'},
  {id: 1, colors: '#7030a0', text: 'مراد/خواستگاه/برچسب/زمینه'},
  {id: 1, colors: '#a1cb8c', text: 'مراد/خواستگاه/برچسب/زمینه'},
];

const renderitem=(item)=>{
  return(
    <TouchableOpacity
    style={{
      width: 100,
      height: 100,
      borderRadius: 100,
      backgroundColor:item.colors,
      alignSelf: 'center',
      justifyContent:'center',
      alignItems:'center',
      marginTop:'3%'
    }}>
    <Text style={[styles.text,{textAlign:'center'}]}>{item.text}</Text>
  </TouchableOpacity>

  )

}
const SecendPage = (props) => {
  return (
    <View style={[styles.container]}>
      {/* <HeaderComponent /> */}
      <View
        style={{
          width: '90%',
          alignSelf: 'center',
          marginTop: '5%',
         
        }}>
        <View
          style={{
            justifyContent: 'center',
            alignItems: 'center',
            width: 120,
            height: 120,
            borderRadius: 100,
            backgroundColor: '#040a60',
            alignSelf: 'center',
          }}>
          <Noti name="notification" size={80} color={colors.white} />
        </View>
        <View style={styles.menu}>

        {
          data.map((item)=>{
            return(
              renderitem(item)
            )
          })
        }
        </View>
        {/* <FlatList
          data={data}
          renderItem={({item}) => 
           renderitem(item)
          }
          keyExtractor={item => item.id}
        /> */}
      </View>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    backgroundColor: colors.white,
    flex: 1,
  },
  menu:{
    marginTop:'3%',
    flexWrap:'wrap',
    flexDirection:'row'
    ,width:'100%',
    justifyContent:'space-around'
  }
});

export default SecendPage;
