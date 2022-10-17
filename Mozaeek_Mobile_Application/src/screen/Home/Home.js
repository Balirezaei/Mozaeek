import React, {useState} from 'react';
import {Modal, Text, TouchableOpacity, View} from 'react-native';
import {TabView, SceneMap, TabBar} from 'react-native-tab-view';
import {
  widthPercentageToDP as wp,
  heightPercentageToDP as hp,
} from 'react-native-responsive-screen';
import {AnimatedModal, Screen} from '../../component';
import Announcement from './Announcement';
import Dashboard from './Dashboard';
import {Strings} from '../../utils/Strings';
import colors from '../../utils/colors';
import GlobalStyle from '../../utils/GlobalStyle';
import sizes from '../../utils/sizes';

const initialLayout = {width: wp('100%')};

const Home = () => {
  const [index, setIndex] = useState(0);
  // const [modalVisible, setModalVisible] = useState(false);
  // const [modalType, setModalType] = useState(-1);
  const [routes] = useState([
    {key: 'Dashboard', title: Strings.dashboard},
    {key: 'Announcement', title: Strings.announcement},
  ]);
  const renderScene = SceneMap({
    Dashboard: Dashboard,
    Announcement: Announcement,
  });
  function _renderTabBar() {
    return (
      <View style={{flexDirection: 'row'}}>
        {routes.map((value, indexIn) => (
          <TouchableOpacity
            onPress={() => setIndex(indexIn)}
            style={{
              flex: 1,
              borderBottomWidth: 2,
              borderBottomColor:
                index === indexIn ? colors.blue : 'transparent',
            }}>
            <Text
              style={[
                GlobalStyle.textBold,
                {
                  textAlign: 'center',
                  fontSize: sizes.font.bold,
                  color: index === indexIn ? colors.blue : colors.gray,
                },
              ]}>
              {value.title}
            </Text>
          </TouchableOpacity>
        ))}
      </View>
    );
  }
  // function _renderModal() {
  //   switch (modalType) {
  //     case 1:
  //       return <View style={{backgroundColor: 'black', flex:1}} />;
  //     case 2:
  //       return <Points />;
  //     case 3:
  //       return <Menu />;
  //   }
  // }
  
  return (
    <Screen header={true}>
      {/* <Header
        setModalType={(type) => [ setModalType(type), setModalVisible(true)]}
      />
      <Modal
        visible={modalVisible}
        onRequestClose={() => setModalVisible(false)}
        transparent={true}>
        <AnimatedModal close={()=>[setModalVisible(false), setModalType(-1)]}>{ _renderModal()}</AnimatedModal>
      </Modal> */}
      <TabView
        navigationState={{index, routes}}
        renderScene={renderScene}
        onIndexChange={setIndex}
        initialLayout={initialLayout}
        renderTabBar={() => _renderTabBar()}
      />
    </Screen>
  );
};
export default Home;
