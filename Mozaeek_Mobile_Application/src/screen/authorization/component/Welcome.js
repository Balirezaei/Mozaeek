import React from 'react'
import { View } from 'react-native'
import { Button } from '../../../component'
import { Strings } from '../../../utils/Strings'
import {
    widthPercentageToDP as wp,
    heightPercentageToDP as hp,
  } from 'react-native-responsive-screen';
const Welcome = Props=>{
    return(
        <View
        style={{
          flexDirection: 'row',
          justifyContent: 'space-around',
          marginTop: wp('6%'),
        }}>
        <Button
          active={false}
          title={Strings.signup}
          onPress={() => Props.onPress()}
          style={{flex: 1}}
        />
        <Button
          active={true}
          title={Strings.login}
          onPress={() => Props.onPress()}
          style={{flex: 1}}
        />
      </View>
    )
}
export default Welcome