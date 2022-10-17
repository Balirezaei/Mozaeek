import { Card, Tabs } from 'antd';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { useDispatch } from 'react-redux';

import { useMount } from '../../../../../features/hooks';
import { Translations } from '../../../../../features/localization';
import { sharedSlice } from '../../../shared';
import ProcessedNewsTab from '../../components/News/ProcessedNewsTab/ProcessedNewsTab';
import UnprocessedNewsTab from '../../components/News/UnprocessedNewsTab/UnprocessedNewsTab';

type Props = {};
const News: React.VFC<Props> = React.memo(() => {
  const { t } = useTranslation();
  const dispatch = useDispatch();

  useMount(() => {
    dispatch(
      sharedSlice.actions.setDisplayPath({
        breadcrumbs: [{ title: t(Translations.Core.Announcements) }],
        title: t(Translations.Core.News),
        fontawesomeIcon: 'newspaper',
      })
    );

    return () => {
      dispatch(sharedSlice.actions.setDisplayPath(null));
    };
  });

  return (
    <Card>
      <Tabs>
        <Tabs.TabPane key="processed" tab={t(Translations.Core.Announcements)}>
          <ProcessedNewsTab />
        </Tabs.TabPane>
        <Tabs.TabPane key="unprocessed" tab={t(Translations.Core.UnprocessedNews)}>
          <UnprocessedNewsTab />
        </Tabs.TabPane>
      </Tabs>
    </Card>
  );
});

export default News;
