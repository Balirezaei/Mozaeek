import { Card, Form, Input, InputNumber, Select, Space, Spin, TreeSelect } from 'antd';
import React, { useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { useDispatch } from 'react-redux';
import { useLocation } from 'react-router';
import { useHistory } from 'react-router-dom';

import { useAntdValidation, useHttpCall } from '../../../../../features/hooks';
import { Translations } from '../../../../../features/localization';
import { ApiResponse, CreaDitMode } from '../../../../../types';
import { filterOptionByString, filterTreeNodeByString, fromQueryString, getSelectOptions, getTreeData } from '../../../../../utils/helpers';
import {
  PricingCreateRq,
  PricingGetByIdRs,
  RequestPriceCreateRq,
  RequestPriceGetByIdRs,
  RequestPriceGetInitDtoRs,
  RequestPriceUpdateRq,
  SubjectPriceCreateRq,
  SubjectPriceGetByIdRs,
  SubjectPriceGetInitDtoRs,
  SubjectPriceUpdateRq,
} from '../../../../http/core/core-apiTypes';
import { ApiModule } from '../../../apiModule';
import { AppErrorAlert, BackToListButton, DatePicker, FormItemActions, sharedSlice, successfulResponse } from '../../../shared';
import { SubjectItem } from '../../apiTypes';

type FormValues = {
  title: string;
  startDate: string;
  endDate: string;
  priceUnit: number;
  priceAmount: number;
  systemShare: number;
  requests: number[];
  subjects: number[];
};
type Props<TCreateRq, TGetByIdRs> = {
  apiModule: ApiModule;
  http: {
    getById: (data: { id: number }) => Promise<ApiResponse<TGetByIdRs>>;
    getInitDto: () => Promise<ApiResponse<SubjectPriceGetInitDtoRs>>;
    create: (data: TCreateRq) => Promise<ApiResponse<any>>;
    update: (data: TCreateRq & { id: number }) => Promise<ApiResponse<any>>;
  };
  listPath: string;
  icon: string;
};
function PricingCreadit<TCreateRq extends SubjectPriceCreateRq & RequestPriceCreateRq, TGetByIdRs extends PricingGetByIdRs>(
  props: Props<TCreateRq, TGetByIdRs>
) {
  const { t } = useTranslation();
  const dispatch = useDispatch();
  const location = useLocation();
  const history = useHistory();

  const queryStringObj = fromQueryString<{ id: number }>(location.search);
  const creaditMode: CreaDitMode = queryStringObj.id === undefined ? 'Create' : 'Edit';

  const [initDto, setInitDto] = useState<SubjectPriceGetInitDtoRs | RequestPriceGetInitDtoRs>();

  const getByIdApi = useHttpCall(props.http.getById);
  const getInitDtoApi = useHttpCall(props.http.getInitDto);
  const createApi = useHttpCall(props.http.create);
  const updateApi = useHttpCall(props.http.update);

  const submitPending = createApi.pending || updateApi.pending;

  const [form] = Form.useForm<FormValues>();
  const { labelWithRules } = useAntdValidation(form);

  useEffect(() => {
    getInitDtoApi.call<SubjectPriceGetInitDtoRs | RequestPriceGetInitDtoRs>(undefined).then((response) => {
      if (successfulResponse(response)) {
        setInitDto(response!.data);
      }
    });

    let moduleTranslateKey: string;
    switch (props.apiModule) {
      case 'RequestPricing':
        moduleTranslateKey = Translations.Core.RequestPricing;
        break;
      case 'SubjectPricing':
        moduleTranslateKey = Translations.Core.SubjectPricing;
        break;
    }

    if (creaditMode === 'Create') {
      dispatch(
        sharedSlice.actions.setDisplayPath({
          title: t(Translations.Common.Creation),
          breadcrumbs: [{ title: t(Translations.Core.Pricing) }, { title: t(moduleTranslateKey!), path: props.listPath }],
          fontawesomeIcon: props.icon,
        })
      );
    } else {
      dispatch(sharedSlice.actions.setDisplayPath('Skeleton'));

      getByIdApi.call<TGetByIdRs>({ id: queryStringObj.id }).then((response) => {
        if (successfulResponse(response)) {
          const result: TGetByIdRs = response!.data;
          form.setFieldsValue({
            title: result.title,
            startDate: result.startDate,
            endDate: result.endDate,
            priceUnit: result.priceUnitId,
            priceAmount: result.priceAmount,
            systemShare: result.systemShare,
            subjects: (result as unknown as SubjectPriceGetByIdRs).priceDetails.map((item) => item.subjectId),
            requests: (result as unknown as RequestPriceGetByIdRs).priceDetails.map((item) => item.requestId),
          });

          dispatch(
            sharedSlice.actions.setDisplayPath({
              title: t(Translations.Common.Edition),
              breadcrumbs: [
                { title: t(Translations.Core.Pricing) },
                { title: t(moduleTranslateKey!), path: props.listPath },
                { title: t(Translations.Common.Edition) },
                { title: result.title },
              ],
              fontawesomeIcon: props.icon,
              disableAutoLastBreadcrumb: true,
            })
          );
        }
      });
    }

    return () => {
      dispatch(sharedSlice.actions.setDisplayPath(null));
    };

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [queryStringObj.id]);

  useEffect(() => {
    if (initDto && initDto.unitPrices && creaditMode === 'Create') {
      form.setFieldsValue({
        priceUnit: initDto.unitPrices[0]?.id,
      });
    }
  }, [creaditMode, form, initDto, initDto?.unitPrices]);

  const handleSubmit = (values: FormValues) => {
    const commonData: PricingCreateRq = {
      title: values.title,
      startDate: values.startDate,
      endDate: values.endDate,
      priceAmount: +values.priceAmount,
      priceUnitId: +values.priceUnit,
      systemShare: +values.systemShare,
    };
    let data: RequestPriceCreateRq | SubjectPriceCreateRq;
    switch (props.apiModule) {
      case 'RequestPricing':
        data = { ...commonData, requestIds: values.requests } as RequestPriceCreateRq;
        break;
      case 'SubjectPricing':
        data = { ...commonData, subjectIds: values.subjects } as SubjectPriceCreateRq;
        break;
      default:
        return;
    }

    if (creaditMode === 'Create') {
      //@ts-ignore
      createApi.call<undefined>(data).then((response) => {
        if (successfulResponse(response)) {
          history.push(props.listPath);
        }
      });
    } else {
      data = { ...data, id: +queryStringObj.id } as SubjectPriceUpdateRq | RequestPriceUpdateRq;
      //@ts-ignore
      updateApi.call<undefined>(data).then((response) => {
        if (successfulResponse(response)) {
          history.push(props.listPath);
        }
      });
    }
  };

  const idListComponent = () => {
    switch (props.apiModule) {
      case 'SubjectPricing':
        return (
          <Form.Item name="subjects" {...labelWithRules({ label: t(Translations.Core.Subjects), rules: [{ type: 'Required' }] })}>
            <TreeSelect
              multiple
              treeData={getTreeData<SubjectItem>((initDto as SubjectPriceGetInitDtoRs)?.subjectList, 'title', 'id')}
              filterTreeNode={filterTreeNodeByString}
              allowClear
              disabled={submitPending}
            />
          </Form.Item>
        );
      case 'RequestPricing':
        return (
          <Form.Item name="requests" {...labelWithRules({ label: t(Translations.Core.Requests), rules: [{ type: 'Required' }] })}>
            <Select
              options={getSelectOptions((initDto as RequestPriceGetInitDtoRs)?.requestList, 'title', 'id')}
              filterOption={filterOptionByString}
              showSearch
              allowClear
              mode="multiple"
              disabled={submitPending}
            />
          </Form.Item>
        );
    }
  };

  return (
    <Card
      extra={
        <Space>
          <BackToListButton url={props.listPath} />
        </Space>
      }>
      {getInitDtoApi.error || getByIdApi.error ? (
        <AppErrorAlert error={getInitDtoApi.error || getByIdApi.error} />
      ) : (
        <>
          <AppErrorAlert error={createApi.error || updateApi.error} />
          <Spin spinning={getByIdApi.pending || getInitDtoApi.pending}>
            <Form form={form} onFinish={handleSubmit} labelCol={{ span: 3 }}>
              <Form.Item name="title" {...labelWithRules({ label: t(Translations.Common.Title), rules: [{ type: 'Required' }] })}>
                <Input disabled={submitPending} allowClear />
              </Form.Item>
              <Form.Item name="startDate" {...labelWithRules({ label: t(Translations.Common.StartDate), rules: [{ type: 'Required' }] })}>
                <DatePicker color="green" disabled={submitPending} />
              </Form.Item>
              <Form.Item name="endDate" label={t(Translations.Common.EndDate)}>
                <DatePicker color="red" disabled={submitPending} />
              </Form.Item>
              {/*<Row>*/}
              {/*  <Col span={12}>*/}
              {/*    */}
              {/*  </Col>*/}
              {/*  <Col span={12}>*/}
              {/*    */}
              {/*  </Col>*/}
              {/*</Row>*/}
              {idListComponent()}
              <Form.Item
                name="priceAmount"
                {...labelWithRules({ label: t(Translations.Common.AmountPrice), rules: [{ type: 'Required' }] })}
                wrapperCol={{ span: 6 }}>
                <Input
                  addonBefore={
                    <Form.Item name="priceUnit" noStyle>
                      <Select disabled={submitPending} style={{ width: '70px' }}>
                        {initDto?.unitPrices.map((item) => (
                          <Select.Option key={item.id} value={item.id}>
                            {item.title}
                          </Select.Option>
                        ))}
                      </Select>
                    </Form.Item>
                  }
                  disabled={submitPending}
                />
              </Form.Item>
              <Form.Item
                name="systemShare"
                {...labelWithRules({ label: t(Translations.Core.MosaikShare), rules: [{ type: 'Required' }] })}
                wrapperCol={{ span: 6 }}>
                <InputNumber disabled={submitPending} style={{ width: '100%' }} />
              </Form.Item>
              <FormItemActions formInstance={form} creaditMode={creaditMode} submitPending={submitPending} />
            </Form>
          </Spin>
        </>
      )}
    </Card>
  );
}

export default React.memo(PricingCreadit);
