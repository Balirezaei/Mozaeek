import { UploadOutlined } from '@ant-design/icons';
import { Button, Card, Checkbox, Form, Image, Input, Spin, TreeSelect, Upload } from 'antd';
import { RcFile } from 'antd/es/upload';
import React, { useMemo, useRef, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { useDispatch } from 'react-redux';
import { useLocation } from 'react-router';
import { useHistory } from 'react-router-dom';

import { useAntdValidation, useHttpCall, useMount } from '../../../../../features/hooks';
import { Translations } from '../../../../../features/localization';
import { CreaDitMode } from '../../../../../types';
import { filterTreeNodeByString, fromQueryString, getTreeData } from '../../../../../utils/helpers';
import {
  announcementCreateHttp,
  announcementGetByIdHttp,
  announcementGetInitDtoHttp,
  announcementGetNewsByIdHttp,
  announcementUpdateHttp,
} from '../../../../http/core/core-http';
import { AppErrorAlert, BackToListButton, FormItemActions, getCreaditTitle, sharedSlice, successfulResponse } from '../../../shared';
import {
  AnnouncementGetByIdResponse,
  AnnouncementGetInitDtoResponse,
  AnnouncementNewsItem,
  LabelItem,
  PointItem,
  RequestOrganizationItem,
  SubjectItem,
} from '../../apiTypes';
import { CorePaths } from '../../CoreRoutes';
import { AnnouncementNewsCreaditQueryString } from '../../types';

type NewsCreaditFormValues = {
  title: string;
  description: string;
  summary: string;
  points: number[];
  labels: number[];
  requestOrganizations: number[];
  subjects: number[];
  photo: FileList;
  hasRequest: boolean;
};
type Props = {};
const NewsCreadit: React.VFC<Props> = React.memo(() => {
  const { t } = useTranslation();
  const dispatch = useDispatch();
  const location = useLocation();
  const history = useHistory();

  const queryStringObj = useMemo(() => {
    return fromQueryString(location.search) as AnnouncementNewsCreaditQueryString;
  }, [location.search]);
  const creaditMode: CreaDitMode = queryStringObj.newsId !== undefined ? 'Create' : 'Edit';

  const getNewsByIdApi = useHttpCall(announcementGetNewsByIdHttp);
  const getByIdApi = useHttpCall(announcementGetByIdHttp);
  const getInitDtoApi = useHttpCall(announcementGetInitDtoHttp);
  const createApi = useHttpCall(announcementCreateHttp);
  const updateApi = useHttpCall(announcementUpdateHttp);

  const [initDto, setInitDto] = useState<AnnouncementGetInitDtoResponse>();
  const [announcementItem, setAnnouncementItem] = useState<AnnouncementNewsItem | AnnouncementGetByIdResponse>();

  const [form] = Form.useForm<NewsCreaditFormValues>();
  const { labelWithRules } = useAntdValidation(form);

  const photoFileRef = useRef<RcFile>();

  useMount(() => {
    if (isNaN(queryStringObj.id as unknown as number) && isNaN(queryStringObj.newsId as unknown as number)) {
      history.push(CorePaths.News);
    } else {
      getInitDtoApi.call<AnnouncementGetInitDtoResponse>(undefined).then((response) => {
        if (successfulResponse(response)) {
          setInitDto(response!.data);
        }
      });

      if (creaditMode === 'Create') {
        //#region Breadcrumb
        dispatch(
          sharedSlice.actions.setDisplayPath({
            title: `${t(Translations.Common.Creation)}`,
            disableAutoLastBreadcrumb: true,
            fontawesomeIcon: 'newspaper',
            breadcrumbs: [
              {
                title: t(Translations.Core.Announcements),
              },
              {
                title: t(Translations.Core.News),
                path: CorePaths.Rss,
              },
              {
                title: t(Translations.Common.Creation),
              },
            ],
          })
        );
        //#endregion

        getNewsByIdApi.call<AnnouncementNewsItem>({ id: +queryStringObj.newsId! }).then((response) => {
          if (successfulResponse(response)) {
            const data = response!.data;
            form.setFieldsValue({
              title: data.title,
              description: data.description,
            });
            setAnnouncementItem(data);
          }
        });
      } else {
        getByIdApi.call<AnnouncementGetByIdResponse>({ id: +queryStringObj.id! }).then((response) => {
          if (successfulResponse(response)) {
            const data = response!.data;
            form.setFieldsValue({
              title: data.title,
              description: data.description,
              summary: data.summary,
              points: data.points,
              labels: data.labels,
              subjects: data.subjects,
              requestOrganizations: data.requestOrgs,
              hasRequest: data.hasRequest,
            });
            setAnnouncementItem(data);
          }
        });
      }
    }
  });

  const handleSubmit = (values: NewsCreaditFormValues) => {
    const addArrayToFormData = (array: any[], fieldName: string) => {
      if (array && array.length > 0) {
        for (const item of array) {
          formData.append(`${fieldName}[]`, item.toString());
        }
      }
    };

    const formData = new FormData();
    if (creaditMode === 'Create') {
      formData.set('NewsId', queryStringObj.newsId!);
    } else {
      formData.set('Id', queryStringObj.id!.toString());
    }

    if (photoFileRef.current) {
      formData.set('photo', photoFileRef.current!);
    }

    formData.set('Title', values.title);
    formData.set('Description', values.description);
    formData.set('Summary', values.summary);
    if (values.hasRequest) {
      formData.set('HasRequest', values.hasRequest.toString());
    }

    addArrayToFormData(values.points, 'Points');
    addArrayToFormData(values.requestOrganizations, 'RequestOrgs');
    addArrayToFormData(values.labels, 'Labels');
    addArrayToFormData(values.subjects, 'Subjects');

    if (creaditMode === 'Create') {
      createApi.call<undefined>(formData).then((response) => {
        if (successfulResponse(response)) {
          history.push(CorePaths.News);
        }
      });
    } else {
      updateApi.call<undefined>(formData).then((response) => {
        if (successfulResponse(response)) {
          history.push(CorePaths.News);
        }
      });
    }
  };

  return (
    <>
      {getInitDtoApi.error || getNewsByIdApi.error ? (
        <AppErrorAlert error={getInitDtoApi.error || getNewsByIdApi.error} />
      ) : (
        <Card
          title={getCreaditTitle(t, creaditMode, t(Translations.Core.NewsSingular), announcementItem?.title, getNewsByIdApi.pending)}
          extra={
            <>
              <BackToListButton url={CorePaths.News} pageName={t(Translations.Core.News)} />
            </>
          }>
          <AppErrorAlert error={createApi.error || updateApi.error} />
          <Spin spinning={getNewsByIdApi.pending || getInitDtoApi.pending}>
            <Form form={form} labelCol={{ span: 4 }} onFinish={handleSubmit}>
              <Form.Item name="title" {...labelWithRules({ label: t(Translations.Common.Title), rules: [{ type: 'Required' }] })}>
                <Input />
              </Form.Item>
              <Form.Item name="labels" label={t(Translations.Core.Labels)}>
                <TreeSelect multiple treeData={getTreeData<LabelItem>(initDto?.labels, 'title', 'id')} filterTreeNode={filterTreeNodeByString} allowClear />
              </Form.Item>
              <Form.Item name="subjects" {...labelWithRules({ label: t(Translations.Core.Subjects), rules: [{ type: 'Required' }] })}>
                <TreeSelect multiple treeData={getTreeData<SubjectItem>(initDto?.subjects, 'title', 'id')} filterTreeNode={filterTreeNodeByString} allowClear />
              </Form.Item>
              <Form.Item name="points" label={t(Translations.Core.Points)}>
                <TreeSelect multiple treeData={getTreeData<PointItem>(initDto?.points, 'title', 'id')} filterTreeNode={filterTreeNodeByString} allowClear />
              </Form.Item>
              <Form.Item name="requestOrganizations" label={t(Translations.Core.RequestOrganizations)}>
                <TreeSelect
                  multiple
                  treeData={getTreeData<RequestOrganizationItem>(initDto?.requestOrgs, 'title', 'id')}
                  filterTreeNode={filterTreeNodeByString}
                  allowClear
                />
              </Form.Item>
              {creaditMode === 'Edit' && <Image width={200} src={(announcementItem as AnnouncementGetByIdResponse)?.imagePath} />}
              <Form.Item name="photo" label={t(Translations.Common.Photo)} required={creaditMode === 'Create'}>
                <Upload
                  beforeUpload={(file) => {
                    photoFileRef.current = file;
                    return false;
                  }}
                  accept="image/*"
                  maxCount={1}>
                  <Button icon={<UploadOutlined />}>{t(Translations.Common.SelectFile)}</Button>
                </Upload>
              </Form.Item>
              {creaditMode === 'Create' && (
                <>
                  <Form.Item name="hasRequest" label={t(Translations.Core.IsRequestAnnouncement)} valuePropName="checked">
                    <Checkbox />
                  </Form.Item>
                </>
              )}
              <Form.Item name="summary" {...labelWithRules({ label: t(Translations.Common.Summary), rules: [{ type: 'Required' }] })}>
                <Input.TextArea autoSize={{ minRows: 3 }} />
              </Form.Item>
              <Form.Item name="description" {...labelWithRules({ label: t(Translations.Common.Description2), rules: [{ type: 'Required' }] })}>
                <Input.TextArea autoSize={{ minRows: 3 }} />
              </Form.Item>
              <FormItemActions formInstance={form} creaditMode={creaditMode} submitPending={createApi.pending || updateApi.pending} />
            </Form>
          </Spin>
        </Card>
      )}
    </>
  );
});

export default NewsCreadit;
