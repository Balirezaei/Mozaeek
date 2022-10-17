import {Strings} from './Strings';
import {XmlFile} from './Xml';

export const StaticArray = {
  sliderLogin: [
    {
      title: Strings.sliderTitle1,
      hintText: Strings.sliderHint1,
      xml: XmlFile.xmlFirstIntoImage,
    },
    {
      title: Strings.sliderTitle2,
      hintText: Strings.sliderHint2,
      xml: XmlFile.xmlSecondIntroImage,
    },
    {
      title: Strings.sliderTitle3,
      hintText: Strings.sliderHint3,
      xml: XmlFile.thirdIntroImage,
    },
  ],

  keyboard: [1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 0],
  dashboardColorArray: [
    '#FED018',
    '#3AB54A',
    '#FF7F26',
    '#8BBEFF',
    '#BF1E2D',
    '#262163',
    '#EE2A7A',
    '#58595B',
    '#1D74BB',
    '#00A79D',
    '#662E93',
    '#8DC73F',
  ],
  announcementColors: [
    '#29C3FF',
    '#7CCD4C',
    '#FFBB46',
    '#FE763C',
    '#755FEE',
    '#92A6C1',
  ],

  Menu: [
    {
      name: '',
      icon: XmlFile.user,
      screen: '',
      id: 0,
    },
    {
      name: Strings.profile,
      icon: XmlFile.pen,
      screen: '',
      id: 1,
    },
    {
      name: Strings.Inventory,
      icon: XmlFile.perce,
      screen: '',
      id: 2,
    },
    {
      name: Strings.recentTopic,
      icon: XmlFile.recent,
      screen: '',
      id: 3,
    },
    {
      name: Strings.customize,
      icon: XmlFile.custom,
      screen: '',
      id: 4,
    },
    {
      name: Strings.reward,
      icon: XmlFile.reward,
      screen: '',
      id: 5,
    },
    {
      name: Strings.messages,
      icon: XmlFile.message,
      screen: '',
      id: 6,
    },
    {
      name: Strings.support,
      icon: XmlFile.support,
      screen: '',
      id: 7,
    },
    {
      name: Strings.completeId,
      icon: XmlFile.identifing,
      screen: '',
      id: 8,
    },
  ],

  ////test array

  dashboardArray: [
    {
      id: 0,
      title: 'تامین اجتماعی',
      entityType: 1,
      entityTypeDescription: 'string',
      userId: 0,
      entityId: 0,
      iconName: 'group',
    },
    {
      id: 1,
      title: 'پایان کار ساختمان',
      entityType: 1,
      entityTypeDescription: 'string',
      userId: 0,
      entityId: 0,
      iconName: 'building',
    },
    {
      id: 2,
      title: 'تسهیلات مسکن',
      entityType: 1,
      entityTypeDescription: 'string',
      userId: 0,
      entityId: 0,
      iconName: 'home',
    },
  ],
  cities: [
    {
      id: 0,
      title: 'آبادان',
    },
    {
      id: 1,
      title: 'آمل',
    },
    {
      id: 2,
      title: 'اهواز',
    },
    {
      id: 3,
      title: 'بابل',
    },
    {
      id: 4,
      title: 'بندرعباس',
    },
    {
      id: 5,
      title: 'بوشهر',
    },
  ],
};
