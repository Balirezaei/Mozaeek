import React, {Component} from 'react';
import {
  SafeAreaView,
  StyleSheet,
  ScrollView,
  View,
  Text,
  StatusBar,
} from 'react-native';
import {Provider} from 'react-redux';
import ContextProvider from './src/context/ContextProvider';

import {Route} from './src/navigation/route';
import {store} from './src/redux/store';

class App extends Component {
  render() {
    return (
      <Provider store={store}>
        <ContextProvider>
          <View style={{flex: 1}}>
            <Route />
          </View>
        </ContextProvider>
      </Provider>
    );
  }
}

export default App;
