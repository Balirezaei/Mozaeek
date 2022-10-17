import { Alert, Button, DatePicker, Form, Input, Radio, Select, Space } from 'antd';
import { Moment } from 'moment';
import React from 'react';
import { useTranslation } from 'react-i18next';
import { useDispatch } from 'react-redux';

import { useAntdValidation, useAppSelector, useGlobalization } from '../../../../../features/hooks';
import { Translations } from '../../../../../features/localization';
import { antdDatePickerValue, disableTodayAndAfterAntdDatePicker } from '../../../../../utils/helpers';
import { AppErrorAlert, Countries } from '../../../shared';
import { profileSelector } from '../../index';
import { updateProfileSaga } from '../../redux/account-sagas';

type FormValues = Readonly<{
  firstname: string;
  lastname: string;
  gender: boolean;
  birthDay?: Moment;
  nationalityId: string;
  phoneNumber: string;
  timezone: string;
  emailAddress: string;
}>;
const EditAccount: React.VFC = () => {
  const { t } = useTranslation();
  const dispatch = useDispatch();
  const { datePickerFormat } = useGlobalization();

  const profile = useAppSelector(profileSelector)!;
  const apiState = useAppSelector((state) => state.account.updateProfileApi);

  const [formInstance] = Form.useForm();
  const { labelWithRules } = useAntdValidation(formInstance);

  // const initialValues: FormValues = {
  //   firstname: profile.firstname,
  //   lastname: profile.lastname,
  //   gender: profile.gender,
  //   birthDay: antdDatePickerValue(profile.birthDay),
  //   nationalityId: profile.nationalityId,
  //   phoneNumber: profile.phoneNumber ?? undefined,
  //   timezone: profile.timezone,
  //   emailAddress: profile.emailAddress,
  // };

  const handleSubmit = async (v: any) => {
    const values = v as FormValues;
    dispatch(
      updateProfileSaga({
        ...values,
        birthDay: values.birthDay?.toDate(),
      })
    );
  };

  return (
    <>
      {/*<AbpErrorAlert error={apiState.error} />*/}
      {/*<Form form={formInstance} initialValues={initialValues} labelCol={{ span: 4 }} onFinish={handleSubmit}>*/}
      {/*  <Form.Item name="emailAddress" label={t(Translations.Common.Email)}>*/}
      {/*    <Input type={'email'} disabled readOnly />*/}
      {/*  </Form.Item>*/}
      {/*  <Form.Item*/}
      {/*    name="firstname"*/}
      {/*    {...labelWithRules({ label: t(Translations.Common.FirstName), rules: [{ type: 'Required' }, { type: 'EnglishLetterAndWhitespaceOnly' }] })}>*/}
      {/*    <Input type={'text'} />*/}
      {/*  </Form.Item>*/}
      {/*  <Form.Item*/}
      {/*    name="lastname"*/}
      {/*    {...labelWithRules({ label: t(Translations.Common.LastName), rules: [{ type: 'Required' }, { type: 'EnglishLetterAndWhitespaceOnly' }] })}>*/}
      {/*    <Input type={'text'} />*/}
      {/*  </Form.Item>*/}
      {/*  <Form.Item name="nationalityId" {...labelWithRules({ label: t(Translations.Common.Nationality), rules: [{ type: 'Required' }] })}>*/}
      {/*    <Select showSearch optionFilterProp="children">*/}
      {/*      {Countries.map((country) => (*/}
      {/*        <Select.Option key={country.code} value={country.code}>*/}
      {/*          {country.name}*/}
      {/*        </Select.Option>*/}
      {/*      ))}*/}
      {/*    </Select>*/}
      {/*  </Form.Item>*/}
      {/*  <Form.Item name="birthDay" {...labelWithRules({ label: t(Translations.Common.Birthday), rules: [{ type: 'Required' }] })}>*/}
      {/*    <DatePicker format={datePickerFormat} disabledDate={disableTodayAndAfterAntdDatePicker} />*/}
      {/*  </Form.Item>*/}
      {/*  <Form.Item name="gender" label={t(Translations.Common.Gender)}>*/}
      {/*    <Radio.Group>*/}
      {/*      <Radio value>{t(Translations.Common.Male)}</Radio>*/}
      {/*      <Radio value={false}>{t(Translations.Common.Female)}</Radio>*/}
      {/*    </Radio.Group>*/}
      {/*  </Form.Item>*/}
      {/*  <Form.Item name="phoneNumber" label={t(Translations.Common.PhoneNumber)}>*/}
      {/*    <Input type="text" />*/}
      {/*  </Form.Item>*/}
      {/*  <Form.Item>*/}
      {/*    <Space>*/}
      {/*      <Button size={'large'} type="primary" htmlType="submit" loading={apiState.pending}>*/}
      {/*        {t(Translations.Common.Submit)}*/}
      {/*      </Button>*/}
      {/*      {apiState.success && <Alert type="success" message={t(Translations.Account.AccountInformationUpdatedSuccessfully)} />}*/}
      {/*    </Space>*/}
      {/*  </Form.Item>*/}
      {/*</Form>*/}
    </>
  );
};

export default EditAccount;
