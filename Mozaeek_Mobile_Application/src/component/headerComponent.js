import React, { useState } from 'react';
import { TouchableOpacity,Text, StyleSheet, StatusBar, Modal, View,Button,Image, TextInput } from 'react-native';
// import { Container, Header, Left, Body, Right, Button, Title } from 'native-base';
import colors from '../utils/colors';
import GlobalStyle from '../utils/GlobalStyle';
import sizes from '../utils/sizes';
import { useNavigation } from '@react-navigation/native'
import IconMenu from 'react-native-vector-icons/MaterialIcons';
import Marker from 'react-native-vector-icons/FontAwesome';
import { heightPercentageToDP as hp } from 'react-native-responsive-screen';


const HeaderComponent = props => {
  const [modal, setModal] = useState(0);
  const [type, setType] = useState();


  const navigation = useNavigation()
  return (
   
    <View style={[styles.header]}>
    
      <Marker
   name="user-o"
   size={25}
   color={colors.black}
   style={{paddingLeft:'2%'}}
 />
     
   
 <View style={styles.box}>
 <TextInput style={styles.input} placeholder='خدمت، سازمان' placeholderTextColor={colors.black}/>   
   <Text style={styles.text}>کرج</Text>
   <Marker
   name="map-marker"
   size={25}
   color={'#656565'}
 />
 </View>
    <TouchableOpacity style={{paddingRight:"2%"}}  onPress={() => navigation.openDrawer()}>
      <Image source={require('../images/imageicon.png')} style={{width:45,height:45}}/>
    </TouchableOpacity>


    </View>
  );
};

const styles = StyleSheet.create({
  header: {
    width:'100%',
    backgroundColor:colors.white,
    elevation: 0,
    height: hp('5%'),
    flexDirection:'row',
    justifyContent:'space-between',
    alignItems:'center'
  },
  button: {
    flexDirection: 'row',
    // backgroundColor:'red'
  },
  text: {
    ...GlobalStyle.text,
    color: colors.black,
    fontSize: 16,
  },

  bottomArrow: {
    width: 10,
    height: 10,
  },

  logo: {
    width: sizes.WIDTH / 2.8,
    height: 70,
    alignSelf: 'center',
  },
  title: {
    ...GlobalStyle.textBold,
    color: colors.black,
    fontSize: 16,
    // flex:1,
    alignSelf: 'center',
  },

  input:{
    width:'80%',
    textAlign:'right',
    paddingRight:10,
    borderWidth:2,
    borderColor:'#656565'
  },
  box:{
    width:'70%',
    flexDirection:'row',
    justifyContent:'space-between',
    alignItems:'center',
    height:40,
    fontSize:18
  }
});



export default HeaderComponent;


{/* <View style={{ flex: 1 }}>


<TouchableOpacity
  style={styles.button}
  transparent
  onPress={() =>navigation.goBack()}>
  <IconMenu
    name="keyboard-arrow-left"
    size={40}
    color={props.colorBackIcon}
  />
</TouchableOpacity>



</View>
<View style={{ flex: 1 }}>
{
  <Text style={[styles.title,{color:props.colorTitle}]}>
    {props.text}
  </Text>
}
{props.showLogo &&
console.warn('1')
  // <Image
  //   style={styles.logo}
  //   source={images.logo}
  //   resizeMode={FastImage.resizeMode.contain}
  // />
}
</View>
<View>

{props.disable &&
 <TouchableOpacity
 
 style={styles.button}
//  transparent
 onPress={() => navigation.openDrawer()}>
 <IconMenu
   name="menu"
   size={30}
   color={props.colorMenuIcon}
 />
</TouchableOpacity>  
}



</View> */}
