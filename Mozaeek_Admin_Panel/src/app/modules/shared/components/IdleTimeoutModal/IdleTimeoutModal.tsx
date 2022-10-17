import { SyncOutlined } from '@ant-design/icons';
import { Button, Modal } from 'antd';
import React, { useEffect, useRef, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { useIdleTimer } from 'react-idle-timer';

import Clock from '../../../../../assets/timer.svg';
import { Translations } from '../../../../../features/localization';

type Props = {
  //Interaction : moving mouse or pressing keyboard buttons
  //DurationOnly : modal will show as soon as duration time passed
  mode?: 'Interaction' | 'DurationOnly';
  durationMin?: number;
  onRefresh?: () => void;
  resetDateNumber?: number;
};
const IdleTimeoutModal: React.VFC<Props> = React.memo((props) => {
  const { t } = useTranslation();

  const timeoutFn = useRef<ReturnType<typeof setTimeout>>();

  const timeout = 1000 * 60 * props.durationMin!;
  useIdleTimer({
    timeout,
    onIdle: () => {
      setVisible(true);
    },
    debounce: 500,
  });

  useEffect(() => {
    if (props.mode === 'DurationOnly') {
      timeoutFn.current = setTimeout(() => {
        setVisible(true);
      }, timeout);
    }
    return () => {
      if (timeoutFn.current) {
        clearTimeout(timeoutFn.current);
      }
    };
  }, [props.mode, props.resetDateNumber, timeout]);

  const [visible, setVisible] = useState(false);

  const handleRefresh = () => {
    if (props.onRefresh) {
      props.onRefresh();
      setVisible(false);
    } else {
      window.location.reload();
    }
  };

  return (
    <Modal visible={visible} closable={false} footer={null} className="text-center" centered>
      <div className="py-4">
        <img src={Clock} alt="timer" width={90} height={90} className="mb-6" />
        <h3 className="title-lg">{t(Translations.Common.StillAround)}</h3>
        <p className="mb-4">{t(Translations.Common.IdleTimeoutModalContent)}</p>
        <Button type="primary" htmlType="button" icon={<SyncOutlined />} size="large" onClick={handleRefresh} className="mt-3">
          {t(Translations.Common.Refresh)}
        </Button>
      </div>
    </Modal>
  );
});

IdleTimeoutModal.defaultProps = {
  durationMin: 10,
  mode: 'Interaction',
};

export default IdleTimeoutModal;
