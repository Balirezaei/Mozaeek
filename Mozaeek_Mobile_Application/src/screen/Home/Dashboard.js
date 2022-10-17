import React, {useEffect, useState} from 'react';
import {FlatList, Text, TouchableOpacity, View} from 'react-native';
import {StaticArray} from '../../utils/StaticArray';
import _ from 'lodash';
import { widthPercentageToDP as wp, heightPercentageToDP as hp} from 'react-native-responsive-screen';
import GlobalStyle from '../../utils/GlobalStyle';
import colors from '../../utils/colors';
import  Icon  from 'react-native-vector-icons/FontAwesome';
const Dashboard = () => {
  const [dashboardList, setDashboardList] = useState([
    ...StaticArray.dashboardArray,
  ]);
  useEffect(() => {
    const counter =Math.floor( hp('100%') / wp('33.3%')) + Math.floor(StaticArray.dashboardArray.length /2)

    if (StaticArray.dashboardArray.length < 12) {
      
      const array = _.fill(
        new Array((counter-1)*3 - StaticArray.dashboardArray.length),
        {id:''},
      );

      setDashboardList([...dashboardList, ...array]);
    }
  }, []);

  function _renderItem(item, index) {
      const colorIndex = index>=12 ? index-12 : index
    return (
      <TouchableOpacity
        style={{
          backgroundColor: StaticArray.dashboardColorArray[colorIndex],
          flex: 1,
          height:wp('33.3%'),
          justifyContent:'center'
        }}
        activeOpacity={0.9}>
        <View style={{alignSelf:'center', alignItems:'center'}}>
       { item.iconName!==undefined ?<Icon name={item.iconName} size={wp('10%')} style={{alignSelf:'center'}} color={colors.white} />: null}
        <Text style={[GlobalStyle.textBold,{textAlign:'center',
         color:colors.white, alignSelf:'center', marginTop:10, fontSize:item.title !== undefined ? wp('3%'):wp('6%')}]}>{item.title !== undefined ? item.title : '+'}</Text>
        </View>
      </TouchableOpacity>
    );
  }
  console.warn(dashboardList);
  return (
    <View style={{flex: 1}}>
      <FlatList
        numColumns={3}
        data={dashboardList}
        renderItem={({item, index}) => _renderItem(item, index)}
        keyExtractor={(item) => item.id.toString()}
      />
    </View>
  );
};
export default Dashboard;
