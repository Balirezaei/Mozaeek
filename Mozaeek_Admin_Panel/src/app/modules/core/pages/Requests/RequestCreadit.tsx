import { DeleteRowOutlined, PlusOutlined } from '@ant-design/icons';
import { Button, Card, Col, Form, Input, Row, Select, Space, Spin, Switch, Table, TreeSelect } from 'antd';
import OrderBy from 'lodash/orderBy';
import remove from 'lodash/remove';
import React, { Dispatch, SetStateAction, useMemo, useRef, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { useDispatch } from 'react-redux';
import { useLocation } from 'react-router';
import { useHistory } from 'react-router-dom';

import { useAntdValidation, useHttpCall, useMount } from '../../../../../features/hooks';
import { Translations } from '../../../../../features/localization';
import { filterOptionByString, filterTreeNodeByString, fromQueryString, getSelectOptions, getTreeData } from '../../../../../utils/helpers';
import { requestCreateHttp, requestGetByIdHttp, requestGetInitDtoHttp, requestSearchRequestTargetHttp, requestUpdateHttp } from '../../../../http/core/core-http';
import { AppErrorAlert, BackToListButton, DatePicker, FormItemActions, ImprovedAutoComplete, sharedSlice, successfulResponse } from '../../../shared';
import {
  RequestAction,
  RequestCreateRequest,
  RequestGetByIdResponse,
  RequestGetInitDtoResponse,
  RequestOrganizationItem,
  RequestSearchRequestTargetRs,
} from '../../apiTypes';
import { CorePaths } from '../../CoreRoutes';
import { RequestCreaditQueryString } from '../../types';

type RequestCreaditFormValues = {
  requestTarget: number;
  requestAct: number;
  points: number[];
  requestActions: string[];
  requestNecessities: string[];
  requestDocuments: string[];
  requestQualifications: string[];
  startDate: string;
  expireDate: string;
  fullOnline: boolean;
  regulation: string;
  summary: string;
  requestOrganizations: number[];
  definiteRequestOrganizations: number[];
};
type Props = {};
const RequestCreadit: React.VFC<Props> = React.memo(() => {
  const { t } = useTranslation();
  const dispatch = useDispatch();
  const location = useLocation();
  const history = useHistory();

  const queryStringObj = useMemo(() => {
    if (location.search) {
      return fromQueryString(location.search) as RequestCreaditQueryString;
    }
  }, [location.search]);
  const createMode = queryStringObj?.id === undefined;

  const getByIdApi = useHttpCall(requestGetByIdHttp);
  const getInitDtoApi = useHttpCall(requestGetInitDtoHttp);
  const createApi = useHttpCall(requestCreateHttp);
  const updateApi = useHttpCall(requestUpdateHttp);

  const [initDto, setInitDto] = useState<RequestGetInitDtoResponse>();
  const [fullOnline, setFullOnline] = useState<boolean>(false);

  const [form] = Form.useForm<RequestCreaditFormValues>();
  const { labelWithRules } = useAntdValidation(form);

  const necessitiesState = useState<string[]>([]);
  const qualificationsState = useState<string[]>([]);
  const actionsState = useState<string[]>([]);
  const documentsState = useState<{ requestTargetId: number; title: string; description: string }[]>([]);

  useMount(() => {
    if (createMode) {
      getInitDto();

      dispatch(
        sharedSlice.actions.setDisplayPath({
          title: t(Translations.Common.Creation),
          fontawesomeIcon: 'bullseye',
          breadcrumbs: [{ title: t(Translations.Core.Request) }],
        })
      );

      form.setFieldsValue({
        requestActions: [''],
        requestNecessities: [''],
        requestDocuments: [''],
        requestQualifications: [''],
      });
    } else {
      if (queryStringObj?.id === undefined) {
        history.push(CorePaths.Requests);
      } else {
        getInitDto();

        getByIdApi.call<RequestGetByIdResponse>({ id: +queryStringObj.id }).then((response) => {
          if (successfulResponse(response)) {
            const result = response!.data;

            const normalizeDynamicDataArray = (array: RequestAction[]) => {
              return array && array.length > 0 ? OrderBy(array, 'priority')?.map((item) => item.description) : [''];
            };

            const requestActions = normalizeDynamicDataArray(result.requestActions);
            const requestNecessities = normalizeDynamicDataArray(result.requestNecessities);
            const requestQualifications = normalizeDynamicDataArray(result.requestQualifications);
            const requestDocuments = result.connectionDtos?.map((item) => ({
              requestTargetId: item.requestTargetId,
              title: item.requestTargetTitle,
              description: item.description,
            }));

            actionsState[1](requestActions);
            necessitiesState[1](requestNecessities);
            qualificationsState[1](requestQualifications);
            documentsState[1](requestDocuments);

            form.setFieldsValue({
              requestTarget: result.requestTargetId,
              requestAct: result.requestActId,
              fullOnline: result.fullOnline,
              points: result.points,
              startDate: result.requestStartDate,
              expireDate: result.requestExpiredDate,
              requestActions: requestActions,
              //requestDocuments: normalizeDynamicDataArray(result.requestDocuments),
              requestNecessities: requestNecessities,
              requestQualifications: requestQualifications,
              regulation: result.regulation,
              summary: result.summary,
            });
            if (result.fullOnline) {
              setFullOnline(true);
            }

            dispatch(
              sharedSlice.actions.setDisplayPath({
                title: t(Translations.Common.Edition),
                disableAutoLastBreadcrumb: true,
                fontawesomeIcon: 'bullseye',
                breadcrumbs: [{ title: t(Translations.Core.Request) }],
              })
            );
          }
        });
      }
    }

    return () => {
      dispatch(sharedSlice.actions.setDisplayPath(null));
    };
  });

  const getInitDto = () => {
    getInitDtoApi.call<RequestGetInitDtoResponse>(undefined).then((response) => {
      if (successfulResponse(response)) {
        setInitDto(response!.data);
      }
    });
  };

  const handleSubmit = (values: RequestCreaditFormValues) => {
    const createDynamicObjects = (dynamicValues: string[] | undefined) => {
      return dynamicValues
        ?.map((item, index) => {
          if (item) {
            return { description: item, priority: index + 1 };
          }
          return undefined;
        })
        .filter((f) => f !== undefined);
    };

    const data = {
      requestActId: values.requestAct,
      requestTargetId: values.requestTarget,
      requestStartDate: values.startDate,
      requestExpiredDate: values.expireDate,
      requestActions: createDynamicObjects(actionsState[0]),
      requestNecessities: createDynamicObjects(necessitiesState[0]),
      requestQualifications: createDynamicObjects(qualificationsState[0]),
      connectionDtos: documentsState[0].map((item) => ({ requestTargetId: item.requestTargetId, requestTargetTitle: item.title, description: item.description })),
      fullOnline: values.fullOnline,
      summary: values.summary,
      regulation: values.regulation,
      points: !values.fullOnline ? values.points?.map((item) => ({ id: item })) : undefined,
      requestOrgs: values.fullOnline ? values.requestOrganizations?.map((item) => ({ id: item })) : undefined,
      definiteRequestOrgDtos: !values.fullOnline ? values.definiteRequestOrganizations?.map((item) => ({ id: item })) : undefined,
    } as RequestCreateRequest;

    if (createMode) {
      createApi.call(data).then((response) => {
        if (successfulResponse(response)) {
          history.push(CorePaths.Requests);
        }
      });
    } else {
      updateApi.call({ ...data, id: +queryStringObj!.id! }).then((response) => {
        if (successfulResponse(response)) {
          history.push(CorePaths.Requests);
        }
      });
    }
  };

  const handleFullOnlineChanged = (checked: boolean) => {
    setFullOnline(checked);
  };

  return (
    <Card
      extra={
        <Space>
          <BackToListButton url={CorePaths.Requests} />
        </Space>
      }>
      {getByIdApi.error || getInitDtoApi.error ? (
        <AppErrorAlert error={getByIdApi.error || getInitDtoApi.error} />
      ) : (
        <Spin spinning={getByIdApi.pending || getInitDtoApi.pending || createApi.pending || updateApi.pending}>
          <AppErrorAlert error={createApi.error || updateApi.error} />
          <Form form={form} labelCol={{ span: 4 }} wrapperCol={{ span: 18 }} onFinish={handleSubmit}>
            <Form.Item name="requestAct" {...labelWithRules({ label: t(Translations.Core.RequestAct), rules: [{ type: 'Required' }] })}>
              <Select options={getSelectOptions(initDto?.requestActs, 'title', 'id')} filterOption={filterOptionByString} showSearch allowClear />
            </Form.Item>
            <Form.Item name="requestTarget" {...labelWithRules({ label: t(Translations.Core.RequestTarget), rules: [{ type: 'Required' }] })}>
              <Select options={getSelectOptions(initDto?.requestTargets, 'title', 'id')} filterOption={filterOptionByString} showSearch allowClear />
            </Form.Item>
            <Form.Item name="fullOnline" label={t(Translations.Core.FullOnline)} valuePropName="checked">
              <Switch onChange={handleFullOnlineChanged} checked={fullOnline} />
            </Form.Item>
            {!fullOnline ? (
              <Form.Item name="definiteRequestOrganizations" label={t(Translations.Core.DefiniteRequestOrganizations)}>
                <Select
                  mode="multiple"
                  options={getSelectOptions(initDto?.definiteRequestOrgs, 'title', 'id')}
                  filterOption={filterOptionByString}
                  showSearch
                  allowClear
                />
              </Form.Item>
            ) : (
              <Form.Item name="requestOrganizations" label={t(Translations.Core.RequestOrganizations)}>
                <TreeSelect
                  multiple
                  treeData={getTreeData<RequestOrganizationItem>(initDto?.requestOrgs, 'title', 'id')}
                  filterTreeNode={filterTreeNodeByString}
                  allowClear
                />
              </Form.Item>
            )}
            <Form.Item name="points" label={t(Translations.Core.Points)}>
              <TreeSelect
                treeData={getTreeData(initDto?.points, 'title', 'id')}
                multiple
                filterTreeNode={filterTreeNodeByString}
                showSearch
                disabled={fullOnline}
                allowClear
              />
            </Form.Item>
            <Form.Item name="summary" label={t(Translations.Common.Description2)}>
              <Input.TextArea rows={4} />
            </Form.Item>
            <Form.Item name="regulation" label={t(Translations.Common.Regulation)}>
              <Input.TextArea rows={4} />
            </Form.Item>

            <Form.Item name="startDate" label={t(Translations.Common.StartDate)}>
              <DatePicker color="green" />
            </Form.Item>
            <Form.Item name="expireDate" label={t(Translations.Common.ExpireDate)}>
              <DatePicker color="red" />
            </Form.Item>

            <DynamicFormItem parentState={necessitiesState} cardName={t(Translations.Core.RequestNecessities)} fieldName={t(Translations.Core.RequestNecessity)} />

            <DynamicFormItem
              parentState={qualificationsState}
              cardName={t(Translations.Core.RequestQualifications)}
              fieldName={t(Translations.Core.RequestQualification)}
            />
            <DynamicFormItem parentState={actionsState} cardName={t(Translations.Core.RequestActions)} fieldName={t(Translations.Core.RequestAction)} />
            <RequestDocumentsCard parentState={documentsState} />

            <FormItemActions
              formInstance={form}
              submitPending={createApi.pending || updateApi.pending}
              submitText={createMode ? t(Translations.Common.Creation) : t(Translations.Common.Edition)}
              resetButton={false}
            />
          </Form>
        </Spin>
      )}
    </Card>
  );
});

export default RequestCreadit;

const DynamicFormItem = React.memo((props: { parentState: [string[], Dispatch<SetStateAction<string[]>>]; cardName: string; fieldName: string }) => {
  const { t } = useTranslation();

  const [dynamicInputValue, setDynamicInputValue] = useState<string>();

  const handleDynamicItemDeleteClicked = (item: string) => {
    const data = [...props.parentState[0]];
    remove(data, (element) => element === item);
    props.parentState[1](data);
  };

  const handleDynamicItemEnterClicked = (event: React.KeyboardEvent<HTMLInputElement>) => {
    event.preventDefault();
    if (dynamicInputValue) {
      const data = [...props.parentState[0]];
      props.parentState[1]([...data, event.currentTarget.value]);
      setDynamicInputValue('');
    }
  };

  const handleDynamicItemAddClicked = () => {
    if (dynamicInputValue) {
      const data = [...props.parentState[0]];
      props.parentState[1]([...data, dynamicInputValue]);
      setDynamicInputValue('');
    }
  };

  const array = props.parentState[0];

  const dataSource = useMemo(() => {
    return array.map((item, index) => ({ index: index + 1, value: item }));
  }, [array]);

  return (
    <Card title={props.cardName} size="small" className="mb-3">
      <Row justify="center" gutter={[16, 16]}>
        <Col span={18}>
          <Input
            value={dynamicInputValue}
            onChange={(event) => setDynamicInputValue(event.currentTarget.value)}
            onPressEnter={handleDynamicItemEnterClicked}
            placeholder={t(Translations.Common.InputVarTitle, { item: props.fieldName })}
          />
        </Col>
        <Col>
          <Button type="primary" icon={<PlusOutlined />} onClick={handleDynamicItemAddClicked} />
        </Col>
      </Row>
      <Table key="" size="small" dataSource={dataSource} pagination={false} className="mt-2 small-margin-empty-table antd-no-border-tr">
        <Table.Column title={t(Translations.Common.Row)} dataIndex="index" width="5%" align="center" />
        <Table.Column title={t(Translations.Common.Title)} dataIndex="value" align="center" />
        <Table.Column
          dataIndex="value"
          render={(value: string) => (
            <Space>
              <Button type="primary" size="small" icon={<DeleteRowOutlined />} danger onClick={() => handleDynamicItemDeleteClicked(value)} />
            </Space>
          )}
          width="5%"
          align="center"
        />
      </Table>
    </Card>
  );
});

const RequestDocumentsCard = React.memo(
  (props: {
    parentState: [
      { requestTargetId: number; title: string; description: string }[],
      Dispatch<SetStateAction<{ requestTargetId: number; title: string; description: string }[]>>
    ];
    excludeRequestTargets?: number[];
  }) => {
    const { t } = useTranslation();

    const [requestTarget, setRequestTarget] = useState<RequestSearchRequestTargetRs[0]>();
    const [description, setDescription] = useState<string>();
    const [requestTargets, setRequestTargets] = useState<RequestSearchRequestTargetRs>([]);

    const excludeRequestTargetIdsRef = useRef<number[]>([]);

    const searchRequestTargetApi = useHttpCall(requestSearchRequestTargetHttp);

    const handleDynamicItemDeleteClicked = (requestTargetId: number) => {
      const data = [...props.parentState[0]];
      remove(data, (element) => element.requestTargetId === requestTargetId);
      props.parentState[1](data);
      remove(excludeRequestTargetIdsRef.current, (element) => element === requestTargetId);
    };

    const handleDescriptionInputPressEnter = (event: React.KeyboardEvent<HTMLTextAreaElement>) => {
      event.preventDefault();
      addDynamicInputsValues();
    };

    const handleDynamicItemAddClicked = () => {
      addDynamicInputsValues();
    };

    const addDynamicInputsValues = () => {
      if (requestTarget && description) {
        const data = [...props.parentState[0]];
        props.parentState[1]([...data, { requestTargetId: requestTarget.id, title: requestTarget.title, description: description }]);
        excludeRequestTargetIdsRef.current.push(requestTarget.id);
        setRequestTarget(undefined);
        setDescription('');
      }
    };

    const array = props.parentState[0];

    const dataSource = useMemo(() => {
      return array.map((item, index) => ({ index: index + 1, requestTargetId: item.requestTargetId, title: item.title, description: item.description }));
    }, [array]);

    const handleRequestTargetSearched = (value: string) => {
      if (value && value.length > 2) {
        searchRequestTargetApi.call<RequestSearchRequestTargetRs>({ title: value, excludeRequestTargetIds: excludeRequestTargetIdsRef.current }).then((response) => {
          if (successfulResponse(response)) {
            setRequestTargets(response!.data);
          }
        });
      }
    };

    const handleRequestTargetSelected = (value: RequestSearchRequestTargetRs[0] | undefined) => {
      setRequestTarget(value);
      setRequestTargets([]);
    };

    return (
      <Card title={t(Translations.Core.RequestDocuments)} size="small" className="mb-3">
        <Row justify="center" className="mb-2">
          <Col span={24}>
            <ImprovedAutoComplete
              value={requestTarget}
              onChange={handleRequestTargetSelected}
              placeholder={t(Translations.Common.SearchAVar, { item: t(Translations.Core.RequestDocument) })}
              textPropertyName="title"
              options={requestTargets}
              onInputChanged={handleRequestTargetSearched}
              renderOption={(option) => <span>{option.title}</span>}
            />
          </Col>
        </Row>
        <Row justify="center" className="mb-3">
          <Col span={24}>
            <Input.TextArea
              rows={3}
              value={description}
              onChange={(event) => setDescription(event.currentTarget.value)}
              onPressEnter={handleDescriptionInputPressEnter}
              placeholder={t(Translations.Common.InputVarDescription, { item: t(Translations.Core.RequestDocument) })}
            />
          </Col>
        </Row>
        <Row>
          <Col>
            <Button type="primary" icon={<PlusOutlined />} onClick={handleDynamicItemAddClicked} />
          </Col>
        </Row>
        <Table key="tempId" size="small" dataSource={dataSource} pagination={false} className="mt-2 small-margin-empty-table antd-no-border-tr">
          <Table.Column title={t(Translations.Common.Row)} dataIndex="index" width="5%" align="center" />
          <Table.Column title={t(Translations.Common.Title)} dataIndex="title" align="center" />
          <Table.Column title={t(Translations.Common.Description2)} dataIndex="description" align="center" />
          <Table.Column
            dataIndex="requestTargetId"
            render={(value: number) => (
              <Space>
                <Button type="primary" size="small" icon={<DeleteRowOutlined />} danger onClick={() => handleDynamicItemDeleteClicked(value)} />
              </Space>
            )}
            width="5%"
            align="center"
          />
        </Table>
      </Card>
    );
  }
);
