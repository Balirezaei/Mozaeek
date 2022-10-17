import React, {useContext, useEffect, useState} from 'react';
import {NavigationContainer} from '@react-navigation/native';
import {createStackNavigator, HeaderTitle,} from '@react-navigation/stack';
import { createDrawerNavigator } from '@react-navigation/drawer';
import {TOKEN} from '../utils/Constant'
import OtpLogin from '../screen/otpLogin';
import Login from '../screen/authorization/Login';
import Home from '../screen/Home/Home';
import Storage from '../utils/Storage';
import AppContext from '../context';



const AuthStack = createStackNavigator();
const Drawer = createDrawerNavigator();
const Stack = createStackNavigator();



const authNavigator = () => {
  return (
      <AuthStack.Navigator>
   
        <AuthStack.Screen
          name="Login"
          component={Login}
          options={(navigation) => {
            return {
              headerShown: false,
            };
          }}
        />
             <AuthStack.Screen
          name="SecendPage"
          component={Route}
          options={(navigation) => {
            return {
              headerShown: false,
            };
          }}
        />
           <AuthStack.Screen
          name="OtpLogin"
          component={OtpLogin}
          options={(navigation) => {
            return {
              headerShown: false,
            };
          }}
        />
      </AuthStack.Navigator>
  );
};

const HomeNavigator = ()=>{
  return(
    <Stack.Navigator>
      <Stack.Screen
        name="Home"
          component={Home}
          options={(navigation) => {
            return {
              headerShown: false,
            };
          }}
      />
    </Stack.Navigator>
  )
}

const Route =()=> {
  const {user, setUser} = useContext(AppContext);

  const restoreUser = async () => { 
    const user = await Storage.get(TOKEN);
    if (user) setUser(user);
  };
  useEffect(() => {
   restoreUser()
  }, [])
 

  return (
    <NavigationContainer>
    <Stack.Navigator >
     
     {user ?  <Stack.Screen
        name="Home"
        component={HomeNavigator}
        options={{headerShown: false}}
      /> : <Stack.Screen
        name="Authorize"
        component={authNavigator}
        options={{headerShown: false}}
      /> }
     
     
     
    </Stack.Navigator>
  </NavigationContainer>
  );
}


export {Route}