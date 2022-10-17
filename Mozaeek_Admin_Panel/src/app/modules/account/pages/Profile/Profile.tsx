import { KeyOutlined, UserOutlined } from '@ant-design/icons';
import { Card, Tabs } from 'antd';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { useDispatch } from 'react-redux';

import { useMount } from '../../../../../features/hooks';
import { Translations } from '../../../../../features/localization';
import { TabHeaderWithIcon, sharedSlice } from '../../../shared';
import ChangePassword from '../../components/ChangePassword/ChangePassword';
import EditAccount from '../../components/EditAccount/EditAccount';

const Profile: React.VFC = () => {
  const { t } = useTranslation();
  const dispatch = useDispatch();

  useMount(() => {
    dispatch(sharedSlice.actions.setDisplayPath({ title: t(Translations.Account.MyProfile), breadcrumbs: [{ title: t(Translations.Account.Account) }] }));
  });

  return (
    <>
      <Card className={'box-shadow'}>
        <Tabs tabPosition="left" className="show-overflow">
          <Tabs.TabPane tab={<TabHeaderWithIcon icon={<UserOutlined />} title={t(Translations.Account.AccountInformation)} />} key={2}>
            <EditAccount />
          </Tabs.TabPane>
          <Tabs.TabPane tab={<TabHeaderWithIcon icon={<KeyOutlined />} title={t(Translations.Account.ChangePassword)} />} key={1}>
            <ChangePassword />
          </Tabs.TabPane>
        </Tabs>
      </Card>
    </>
  );
};

export default Profile;
