import 'react-multi-date-picker/styles/colors/green.css';
import 'react-multi-date-picker/styles/colors/red.css';

import { Button } from 'antd';
import moment from 'moment-jalaali';
import React, { useEffect, useRef, useState } from 'react';
import persian from 'react-date-object/calendars/persian';
import persian_fa from 'react-date-object/locales/persian_fa';
import { useTranslation } from 'react-i18next';
import MultiDatePicker, { DateObject } from 'react-multi-date-picker';

import { Translations } from '../../../../../features/localization';
import { momentToISOUtcDate } from '../../../../../utils/helpers';

type Props = {
  color?: 'green' | 'red';
  value?: string;
  onChange?: (value: string | undefined) => void;
  includeTimeInISO?: boolean;
  calendar?: 'persian' | 'gregorian';
  disabled?: boolean;
};
const DatePicker: React.VFC<Props> = React.memo((props: Props) => {
  const { t } = useTranslation();

  const [value, setValue] = useState<DateObject>();

  const datePickerRef = useRef<any>();

  const handleChanged = (selectedDates: DateObject) => {
    setValue(selectedDates);
    datePickerRef.current!.closeCalendar();
  };

  useEffect(() => {
    if (props.value) {
      setValue(new DateObject(props.value));
    }
  }, [props.value]);

  useEffect(() => {
    if (value) {
      props.onChange?.(momentToISOUtcDate(moment(value?.toDate())));
    }

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [value]);

  const handleClear = () => {
    setValue(undefined);
    props.onChange?.(undefined);
  };

  return (
    <MultiDatePicker
      value={value}
      onChange={handleChanged}
      calendar={persian}
      locale={persian_fa}
      inputClass="ant-input"
      disabled={props.disabled}
      className={props.color}
      ref={datePickerRef}
      style={{
        width: '100%',
        boxSizing: 'border-box',
      }}
      containerStyle={{
        width: '100%',
      }}
      calendarPosition="top-right">
      <Button type="dashed" style={{ margin: '5px' }} onClick={() => setValue(new DateObject())}>
        {t(Translations.Common.Today)}
      </Button>
      <Button style={{ margin: '5px' }} onClick={handleClear}>
        {t(Translations.Common.Clear)}
      </Button>
    </MultiDatePicker>
  );
});

export default DatePicker;
