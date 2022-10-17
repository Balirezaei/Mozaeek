import React, {useState, useEffect} from 'react';
import {FlatList, View, TouchableOpacity, Text} from 'react-native';
import colors from '../../utils/colors';
import GlobalStyle from '../../utils/GlobalStyle';
import {StaticArray} from '../../utils/StaticArray';
import {heightPercentageToDP as hp, widthPercentageToDP as wp} from 'react-native-responsive-screen';
import _ from 'lodash';

const Announcement = () => {
  const [dashboardList, setDashboardList] = useState([
    ...StaticArray.dashboardArray,
  ]);
  useEffect(() => {
      const counter =Math.floor( hp('100%') / wp('33.3%')) + Math.floor(StaticArray.dashboardArray.length /2)
    if (StaticArray.dashboardArray.length < 8) {
      const array = _.fill(new Array((counter-1)*2- StaticArray.dashboardArray.length), {
        id: '',
      });

      setDashboardList([...dashboardList, ...array]);
    }
  }, []);
  function _renderItem(item, index) {
    const colorIndex = index >= 6 ? index - 6 : index;
    return (
      <TouchableOpacity
        style={{
          backgroundColor: StaticArray.announcementColors[colorIndex],
          flex: 1,
          height: wp('33.3%'),
          justifyContent: 'center',
        }}
        activeOpacity={0.9}>
        <Text
          style={[
            GlobalStyle.textBold,
            {
              textAlign: 'center',
              color: colors.white,
              alignSelf: 'center',
              marginTop: 10,
              fontSize: item.title !== undefined ? wp('4%') : wp('6%'),
            },
          ]}>
          {item.title !== undefined ? item.title : '+'}
        </Text>
      </TouchableOpacity>
    );
  }
  return (
    <View style={{ flex: 1}}>
      <FlatList
        numColumns={2}
        data={dashboardList}
        renderItem={({item, index}) => _renderItem(item, index)}
        keyExtractor={(item) => item.id.toString()}
      />
    </View>
  );
};
export default Announcement;
