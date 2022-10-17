import { Card, Form, Input, InputNumber, Spin, Switch } from 'antd';
import React, { useRef } from 'react';
import { useTranslation } from 'react-i18next';
import { useDispatch } from 'react-redux';
import { useLocation } from 'react-router';
import { useHistory } from 'react-router-dom';

import { useAntdValidation, useHttpCall, useMount } from '../../../../../features/hooks';
import { Translations } from '../../../../../features/localization';
import { fromQueryString } from '../../../../../utils/helpers';
import { rssCreateHttp, rssGetByIdHttp, rssUpdateHttp } from '../../../../http/core/core-http';
import { AppErrorAlert, BackToListButton, FormItemActions, sharedSlice } from '../../../shared';
import { RssItem } from '../../apiTypes';
import { CorePaths } from '../../CoreRoutes';
import { RssCreaditQueryString } from '../../types';

type CreaditRssFormValues = {
  url: string;
  isActive: boolean;
  source: string;
  intervalDataReceiveHours: number;
};
type Props = {};
const RssCreadit: React.VFC<Props> = React.memo(() => {
  const { t } = useTranslation();
  const dispatch = useDispatch();
  const location = useLocation();
  const history = useHistory();

  const queryStringObj = fromQueryString<RssCreaditQueryString>(location.search);
  const createMode = queryStringObj.id === undefined;

  const rssItemRef = useRef<RssItem>();

  const rssGetByIdApi = useHttpCall(rssGetByIdHttp);
  const rssCreateApi = useHttpCall(rssCreateHttp);
  const rssUpdateApi = useHttpCall(rssUpdateHttp);

  const [form] = Form.useForm<CreaditRssFormValues>();
  const { labelWithRules } = useAntdValidation(form);

  useMount(() => {
    if (createMode) {
      //#region Breadcrumb
      dispatch(
        sharedSlice.actions.setDisplayPath({
          title: `${t(Translations.Common.Creation)}`,
          disableAutoLastBreadcrumb: true,
          fontawesomeIcon: 'rss',
          breadcrumbs: [
            {
              title: t(Translations.Core.Announcements),
            },
            {
              title: t(Translations.Core.RSS),
              path: CorePaths.Rss,
            },
            {
              title: t(Translations.Common.Creation),
            },
          ],
        })
      );
      //#endregion
    } else {
      if (isNaN(+queryStringObj.id)) {
        history.push(CorePaths.Rss);
      } else {
        dispatch(sharedSlice.actions.setDisplayPath('Skeleton'));

        rssGetByIdApi.call<RssItem>({ id: +queryStringObj.id }).then((response) => {
          if (response && response.data) {
            const rssItem = response.data;

            rssItemRef.current = rssItem;

            form.setFieldsValue({
              source: rssItem.source,
              url: rssItem.url,
              isActive: rssItem.isActive,
              intervalDataReceiveHours: rssItem.intervalDataReceiveHours,
            });

            //#region Breadcrumb
            dispatch(
              sharedSlice.actions.setDisplayPath({
                title: `${t(Translations.Common.Edition)}`,
                disableAutoLastBreadcrumb: true,
                fontawesomeIcon: 'rss',
                breadcrumbs: [
                  {
                    title: t(Translations.Core.BasicData),
                  },
                  {
                    title: t(Translations.Core.RSS),
                    path: CorePaths.Rss,
                  },
                  {
                    title: t(Translations.Common.Edition),
                  },
                  {
                    title: rssItem.source,
                  },
                ],
              })
            );
            //#endregion
          }
        });
      }
    }
  });

  const handleSubmit = (values: CreaditRssFormValues) => {
    if (createMode) {
      rssCreateApi.call<number>({ ...values }).then((response) => {
        if (response && !response.error) {
          history.push(CorePaths.Rss);
        }
      });
    } else {
      rssUpdateApi.call<RssItem>({ id: +queryStringObj.id, ...values }).then((response) => {
        if (response && !response.error) {
          history.push(CorePaths.Rss);
        }
      });
    }
  };

  return (
    <Card
      title={`${createMode ? t(Translations.Common.Create) : t(Translations.Common.Edit)} ${t(Translations.Core.RSS)}`}
      extra={
        <>
          <BackToListButton url={CorePaths.Rss} pageName={t(Translations.Core.RSS)} />
        </>
      }>
      <AppErrorAlert error={rssCreateApi.error || rssUpdateApi.error || rssGetByIdApi.error} />
      <Spin spinning={!createMode && rssGetByIdApi.pending}>
        <Form form={form} labelCol={{ span: 4 }} onFinish={handleSubmit}>
          <Form.Item name="source" {...labelWithRules({ label: t(Translations.Common.Source), rules: [{ type: 'Required' }] })}>
            <Input />
          </Form.Item>
          <Form.Item name="url" {...labelWithRules({ label: t(Translations.Common.Address), rules: [{ type: 'Required' }] })}>
            <Input type="url" dir="ltr" className="ltr" placeholder="https://example.com" />
          </Form.Item>
          {createMode && (
            <Form.Item
              name="intervalDataReceiveHours"
              {...labelWithRules({ label: t(Translations.Common.IntervalTimeVar, { Time: t(Translations.Common.Hour) }), rules: [{ type: 'Required' }] })}>
              <InputNumber />
            </Form.Item>
          )}
          {!createMode && (
            <Form.Item name="isActive" {...labelWithRules({ label: t(Translations.Common.Status), rules: [{ type: 'Required' }] })} valuePropName="checked">
              <Switch />
            </Form.Item>
          )}
          <FormItemActions
            formInstance={form}
            submitText={createMode ? t(Translations.Common.Creation) : t(Translations.Common.Edition)}
            submitPending={rssCreateApi.pending || rssUpdateApi.pending}
          />
        </Form>
      </Spin>
    </Card>
  );
});

export default RssCreadit;
