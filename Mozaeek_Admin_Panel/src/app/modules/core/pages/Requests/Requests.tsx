import { PlusOutlined, ReloadOutlined } from '@ant-design/icons';
import { Button, Card, Space, Tabs } from 'antd';
import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';
import { useDispatch } from 'react-redux';
import { Link } from 'react-router-dom';
import { useImmer } from 'use-immer';

import { useMount } from '../../../../../features/hooks';
import { Translations } from '../../../../../features/localization';
import { sharedSlice } from '../../../shared';
import AnnouncementRequestsList from '../../components/Requests/AnnouncementRequestsList/AnnouncementRequestsList';
import RequestList from '../../components/Requests/RequestsList/RequestList';
import { CorePaths } from '../../CoreRoutes';

type Props = {};
const Requests: React.VFC<Props> = React.memo(() => {
  const { t } = useTranslation();
  const dispatch = useDispatch();

  const [refreshTime, updateRefreshTime] = useImmer({ announcementRequests: Date.now(), requestList: Date.now() });
  const [activeTab, setActiveTab] = useState<string>('requestList');

  useMount(() => {
    dispatch(
      sharedSlice.actions.setDisplayPath({
        title: t(Translations.Core.Requests),
        fontawesomeIcon: 'bullseye',
        breadcrumbs: [{ title: t(Translations.Core.Request) }],
      })
    );

    return () => {
      dispatch(sharedSlice.actions.setDisplayPath(null));
    };
  });

  const handleRefresh = () => {
    switch (activeTab) {
      case 'requestList':
        updateRefreshTime((draft) => {
          draft.requestList = Date.now();
        });
        break;
      case 'announcementRequestList':
        updateRefreshTime((draft) => {
          draft.announcementRequests = Date.now();
        });
        break;
    }
  };

  return (
    <>
      <Card>
        <Tabs
          activeKey={activeTab}
          onChange={setActiveTab}
          tabBarExtraContent={
            <Space>
              {activeTab === 'requestList' && (
                <Link to={CorePaths.RequestCreadit}>
                  <Button type="primary" htmlType="button" icon={<PlusOutlined />}>
                    {t(Translations.Common.Create)}
                  </Button>
                </Link>
              )}
              <Button type="default" htmlType="button" icon={<ReloadOutlined />} onClick={handleRefresh}>
                {t(Translations.Common.Refresh)}
              </Button>
            </Space>
          }>
          <Tabs.TabPane key="requestList" tab={t(Translations.Core.Requests)}>
            <RequestList refreshTime={refreshTime.requestList} />
          </Tabs.TabPane>
          <Tabs.TabPane key="announcementRequestList" tab={t(Translations.Core.AnnouncementRequests)}>
            <AnnouncementRequestsList refreshTime={refreshTime.announcementRequests} />
          </Tabs.TabPane>
        </Tabs>
      </Card>
    </>
  );
});

export default Requests;
