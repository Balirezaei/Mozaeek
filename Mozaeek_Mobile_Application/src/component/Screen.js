import React, {useState} from 'react';
import {StyleSheet, SafeAreaView, View, Modal} from 'react-native';
import {AnimatedModal} from '.';
import Header from './header/Header';
import Menu from './header/Menu';
import Points from './header/Points';

function Screen({children, style, header}) {
  const [modalVisible, setModalVisible] = useState(false);
  const [modalType, setModalType] = useState(-1);
  function _renderModal() {
    switch (modalType) {
      case 1:
        return <View style={{backgroundColor: 'black', flex: 1}} />;
      case 2:
        return <Points />;
      case 3:
        return <Menu />;
    }
  }
  return (
    <SafeAreaView style={[styles.screen, style]}>
      <View style={[styles.view, style]}>
        {header ? (
          <View>
            
            <Header         setModalType={(type) => [ setModalType(type), setModalVisible(true)]} />
            <Modal
              visible={modalVisible}
              onRequestClose={() => setModalVisible(false)}
              transparent={true}>
              <AnimatedModal
                close={() => [setModalVisible(false), setModalType(-1)]}>
                {_renderModal()}
              </AnimatedModal>
            </Modal>
          </View>
        ) : null}
        {children}
      </View>
    </SafeAreaView>
  );
}

const styles = StyleSheet.create({
  screen: {
    flex: 1,
  },
  view: {
    flex: 1,
  },
});

export default Screen;
