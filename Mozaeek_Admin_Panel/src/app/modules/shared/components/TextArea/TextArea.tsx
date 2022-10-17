import { Input } from 'antd';
import { TextAreaProps } from 'antd/lib/input/TextArea';
import clsx from 'clsx';
import React, { useState } from 'react';
import { useTranslation } from 'react-i18next';

import { Translations } from '../../../../../features/localization';

type Props = {
  showMaxLength?: boolean;
} & TextAreaProps;
const TextArea: React.VFC<Props> = React.memo((props) => {
  const { t } = useTranslation();

  const [remainingCharacters, setRemainingCharacters] = useState<number | undefined>(props.maxLength);

  const handleChange = (e: any) => {
    if (props.showMaxLength && props.maxLength) {
      setRemainingCharacters(props.maxLength - e.target.value.length);
    }
    props.onChange?.(e);
  };

  return (
    <div>
      <Input.TextArea value={props.value} onChange={handleChange} maxLength={props.maxLength} />
      {props.showMaxLength && props.maxLength && (
        <div
          className={clsx({
            'text-right': true,
            'text-danger': (100 * remainingCharacters!) / props.maxLength < 5,
            'text-warning': (100 * remainingCharacters!) / props.maxLength < 20,
            'font-weight-bolder': remainingCharacters === 0,
          })}>
          {t(Translations.Common.VarCharactersLeft, { count: remainingCharacters })}
        </div>
      )}
    </div>
  );
});

export default TextArea;
