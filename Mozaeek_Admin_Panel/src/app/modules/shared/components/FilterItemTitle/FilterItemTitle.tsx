import { ClearOutlined } from '@ant-design/icons';
import { Button, Col, Row } from 'antd';
import React from 'react';
import { useTranslation } from 'react-i18next';

import { Translations } from '../../../../../features/localization';

type Props = {
  title: string | React.ReactNode;
  resetButtonVisibility: boolean | undefined;
  onResetClick: () => void;
};
const FilterItemTitle: React.VFC<Props> = React.memo((props) => {
  const { t } = useTranslation();

  return (
    <Row justify="space-between" align="middle" className="mb-5">
      <Col>{typeof props.title === 'string' ? <div className="title-sm mb-0">{props.title}</div> : props.title}</Col>
      <Col>
        <Button
          type="default"
          size="small"
          htmlType="button"
          onClick={props.onResetClick}
          icon={<ClearOutlined />}
          className={!props.resetButtonVisibility ? 'd-none' : undefined}>
          <span className="d-none d-xl-inline">{t(Translations.Common.ResetForm)}</span>
        </Button>
      </Col>
    </Row>
  );
});

export default FilterItemTitle;
