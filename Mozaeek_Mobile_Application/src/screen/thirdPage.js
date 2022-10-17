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
} from 'react-native';
import colors from '../utils/colors';


const HEIGHT = Dimensions.get('window').height;
const WIDTH = Dimensions.get('window').width;

const ThirddPage = (props) => {

  return (
    <View style={[styles.container]}>
           <Text>tes2</Text>
    </View>
  );
};

const styles = StyleSheet.create({
  container: {
    backgroundColor: colors.lightwhite,
    flex: 1,
  },

});

export default ThirddPage;
