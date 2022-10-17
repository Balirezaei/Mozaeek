import { Tooltip } from 'antd';
import { TooltipPlacement } from 'antd/es/tooltip';
import clsx from 'clsx';
import React from 'react';

import { useGlobalization, useNumberFormat } from '../../../../../features/hooks';
import { CurrencyDisplay } from '../../../../../types';
import { LocalizationHelpers } from '../../../../../utils/helpers';
import classes from './FormattedCurrency.module.scss';

type Props = {
  number: number;
  currency?: string;
  currencyDisplay?: CurrencyDisplay;
  mathSymbol?: boolean;
  useDefaultHtmlDirection?: boolean;
  tooltip?: {
    show: boolean;
    position?: TooltipPlacement;
    details?: React.ReactElement;
  };
  className?: string;
};

const FormattedCurrency: React.VFC<Props> = (props) => {
  const numberFormat = useNumberFormat();
  const { reversePositionOnRtl } = useGlobalization();

  let number = props.number;
  let symbolElement;
  if (props.mathSymbol) {
    if (props.number > 0) {
      symbolElement = <span className="green7">+</span>;
    } else if (props.number < 0) {
      symbolElement = <span className="red7">-</span>;
      number = parseInt(number.toString().substr(1));
    }
  }

  const tooltipTitle = () => {
    return (
      <>
        <div className={clsx({ 'text-center': true, [classes.HasBorder]: props.tooltip!.details })}>
          {numberFormat.currency(number, props.currency, 'name')}
        </div>
        {props.tooltip!.details}
      </>
    );
  };

  const withTooltip = (children: React.ReactElement) => {
    return props.tooltip?.show ? (
      <Tooltip title={tooltipTitle()} placement={props.tooltip.position ? props.tooltip.position : reversePositionOnRtl('left')}>
        {children}
      </Tooltip>
    ) : (
      children
    );
  };

  return withTooltip(
    <span className={props.className} dir={props.useDefaultHtmlDirection ? LocalizationHelpers.getHtmlDirection() : 'ltr'}>
      {props.mathSymbol && symbolElement} <span>{numberFormat.currency(number, props.currency, props.currencyDisplay)}</span>
    </span>
  );
};

FormattedCurrency.defaultProps = {
  tooltip: { show: true },
};

export default FormattedCurrency;
