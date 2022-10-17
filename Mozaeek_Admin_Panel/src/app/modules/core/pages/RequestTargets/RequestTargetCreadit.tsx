import { Card, Form, Input, Spin, Switch, TreeSelect } from 'antd';
import React, { useMemo, useRef, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { useDispatch } from 'react-redux';
import { useLocation } from 'react-router';
import { useHistory } from 'react-router-dom';

import { useAntdValidation, useHttpCall, useMount } from '../../../../../features/hooks';
import { Translations } from '../../../../../features/localization';
import { filterTreeNodeByString, fromQueryString, getTreeData } from '../../../../../utils/helpers';
import { requestTargetCreateHttp, requestTargetGetByIdHttp, requestTargetGetInitDtoHttp, requestTargetUpdateHttp } from '../../../../http/core/core-http';
import { AppErrorAlert, BackToListButton, FormItemActions, getCreaditTitle, sharedSlice, successfulResponse } from '../../../shared';
import {
  LabelItem,
  RequestOrganizationItem,
  RequestTargetCreateRequest,
  RequestTargetGetByIdResponse,
  RequestTargetGetInitDtoResponse,
  SubjectItem,
} from '../../apiTypes';
import { CorePaths } from '../../CoreRoutes';
import { RequestTargetCreaditQueryString } from '../../types';

type RequestTargetCreaditFormValues = {
  title: string;
  labels: number[];
  subjects: number[];
  isDocument: boolean;
};
type Props = {};
const RequestTargetCreadit: React.VFC<Props> = React.memo(() => {
  const { t } = useTranslation();
  const dispatch = useDispatch();
  const history = useHistory();
  const location = useLocation();

  const queryStringObj = useMemo<RequestTargetCreaditQueryString>(() => {
    return fromQueryString<RequestTargetCreaditQueryString>(location.search);
  }, [location.search]);
  const createMode = queryStringObj.id === undefined;

  const initDtoResponseRef = useRef<RequestTargetGetInitDtoResponse>();
  const [initDto, setInitDto] = useState<{ requestOrganizations: RequestOrganizationItem[]; subjects: SubjectItem[]; labels: LabelItem[] }>();
  const [currentItem, setCurrentItem] = useState<RequestTargetGetByIdResponse>();

  const getInitDtoApi = useHttpCall(requestTargetGetInitDtoHttp);
  const getByIdApi = useHttpCall(requestTargetGetByIdHttp);
  const createApi = useHttpCall(requestTargetCreateHttp);
  const updateApi = useHttpCall(requestTargetUpdateHttp);

  const [form] = Form.useForm<RequestTargetCreaditFormValues>();
  const { labelWithRules } = useAntdValidation(form);

  useMount(() => {
    if (createMode) {
      dispatch(
        sharedSlice.actions.setDisplayPath({
          title: t(Translations.Common.Creation),
          breadcrumbs: [{ title: t(Translations.Core.BasicData) }, { title: t(Translations.Core.RequestTarget) }],
          fontawesomeIcon: 'bullseye',
        })
      );
    } else {
      dispatch(sharedSlice.actions.setDisplayPath('Skeleton'));

      getByIdApi.call<RequestTargetGetByIdResponse>({ id: queryStringObj.id }).then((response) => {
        if (successfulResponse(response)) {
          const requestTarget = response!.data;

          setCurrentItem(requestTarget);

          form.setFieldsValue({
            title: requestTarget.title,
            labels: requestTarget.labels,
            subjects: requestTarget.subjects,
            isDocument: requestTarget.isDocument,
          });

          dispatch(
            sharedSlice.actions.setDisplayPath({
              title: t(Translations.Common.Edition),
              disableAutoLastBreadcrumb: true,
              breadcrumbs: [
                { title: t(Translations.Core.BasicData) },
                { title: t(Translations.Core.RequestTarget) },
                {
                  title: t(Translations.Common.Edition),
                },
                {
                  title: response!.data.title,
                },
              ],
              fontawesomeIcon: 'bullseye',
            })
          );
        }
      });
    }

    getInitDtoApi.call<RequestTargetGetInitDtoResponse>(undefined).then((response) => {
      if (successfulResponse(response)) {
        const result = response!.data;
        initDtoResponseRef.current = result;
        setInitDto({ labels: result.labels, requestOrganizations: result.requestOrgs, subjects: result.subjects });
      }
    });

    return () => {
      dispatch(sharedSlice.actions.setDisplayPath(null));
    };
  });

  const handleSubmit = (values: RequestTargetCreaditFormValues) => {
    const obj: RequestTargetCreateRequest = {
      title: values.title,
      labels: values.labels?.map((item) => ({ id: item })),
      subjects: values.subjects.map((item) => ({ id: item })),
      isDocument: values.isDocument,
    };

    if (createMode) {
      createApi.call(obj).then((response) => {
        if (successfulResponse(response)) {
          history.push(CorePaths.RequestTargets);
        }
      });
    } else {
      updateApi.call({ id: +queryStringObj.id, ...obj }).then((response) => {
        if (successfulResponse(response)) {
          history.push(CorePaths.RequestTargets);
        }
      });
    }
  };

  return (
    <>
      {getInitDtoApi.error || getByIdApi.error ? (
        <AppErrorAlert error={getInitDtoApi.error || getByIdApi.error} />
      ) : (
        <Card
          title={getCreaditTitle(t, createMode ? 'Create' : 'Edit', t(Translations.Core.RequestTarget), currentItem?.title, getByIdApi.pending)}
          extra={
            <>
              <BackToListButton url={CorePaths.RequestTargets} pageName={t(Translations.Core.RequestTargets)} />
            </>
          }>
          <AppErrorAlert error={createApi.error || updateApi.error} />
          <Spin spinning={getByIdApi.pending || getInitDtoApi.pending}>
            <Form form={form} labelCol={{ span: 3 }} onFinish={handleSubmit}>
              <Form.Item name="title" {...labelWithRules({ label: t(Translations.Common.Title), rules: [{ type: 'Required' }] })}>
                <Input />
              </Form.Item>
              <Form.Item name="labels" label={t(Translations.Core.Labels)}>
                <TreeSelect multiple treeData={getTreeData<LabelItem>(initDto?.labels, 'title', 'id')} filterTreeNode={filterTreeNodeByString} allowClear />
              </Form.Item>
              <Form.Item name="subjects" {...labelWithRules({ label: t(Translations.Core.Subjects), rules: [{ type: 'Required' }] })}>
                <TreeSelect multiple treeData={getTreeData<SubjectItem>(initDto?.subjects, 'title', 'id')} filterTreeNode={filterTreeNodeByString} allowClear />
              </Form.Item>
              <Form.Item name="isDocument" label={t(Translations.Core.RequestTargetIsDocumentField)} valuePropName="checked">
                <Switch />
              </Form.Item>
              <FormItemActions formInstance={form} creaditMode={createMode ? 'Create' : 'Edit'} submitPending={createApi.pending || updateApi.pending} />
            </Form>
          </Spin>
        </Card>
      )}
    </>
  );
});

export default RequestTargetCreadit;
