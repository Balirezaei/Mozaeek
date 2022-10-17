import { Skeleton, Space, Tooltip } from 'antd';
import get from 'lodash/get';
/* eslint-disable no-script-url,jsx-a11y/anchor-is-valid */
import React, { useMemo } from 'react';

import { useAppSelector, useGlobalization } from '../../../../features/hooks';
import { getNowISO } from '../../../../utils/helpers';
import { useHtmlClassService } from '../../_core/MetronicLayout';
import { BreadCrumbs } from './components/BreadCrumbs';

export function SubHeader() {
  const { formatFullMonthWeekDay, formatShortDate } = useGlobalization();

  const displayPath = useAppSelector((state) => state.shared.displayPath);

  const uiService = useHtmlClassService();

  const layoutProps = useMemo(() => {
    return {
      config: uiService.config,
      subheaderMobileToggle: get(uiService.config, 'subheader.mobile-toggle'),
      subheaderCssClasses: uiService.getClasses('subheader', true),
      subheaderContainerCssClasses: uiService.getClasses('subheader_container', true),
    };
  }, [uiService]);

  return (
    <div id="kt_subheader" className={`subheader py-2 py-lg-4   ${layoutProps.subheaderCssClasses}`}>
      <div className={`${layoutProps.subheaderContainerCssClasses} d-flex align-items-center justify-content-between flex-wrap flex-sm-nowrap`}>
        {/* Info */}
        <div className="d-flex align-items-center flex-wrap mr-1">
          {layoutProps.subheaderMobileToggle && (
            <button className="burger-icon burger-icon-left mr-4 d-inline-block d-lg-none" id="kt_subheader_mobile_toggle">
              <span />
            </button>
          )}
          {displayPath === 'Skeleton' ? (
            <Skeleton.Input size="small" active style={{ width: 400 }} />
          ) : (
            <>
              <div className="d-flex align-items-baseline mr-5">
                {displayPath?.fontawesomeIcon && <i className={`fas fa-${displayPath?.fontawesomeIcon} mr-2 text-success`} />}
                <h5 className="text-dark font-weight-bold my-2 mr-5">{displayPath?.title}</h5>
              </div>

              <BreadCrumbs items={displayPath?.breadcrumbs} />
            </>
          )}
        </div>

        {/* Toolbar */}
        <div className="d-flex align-items-center">
          <Space>
            <Tooltip title={formatShortDate(getNowISO())} placement="bottom">
              <div className="btn btn-light btn-sm font-weight-bold default-cursor">
                <span className="text-primary font-weight-bold">{formatFullMonthWeekDay(getNowISO())}</span>
              </div>
            </Tooltip>
          </Space>
        </div>
      </div>
    </div>
  );
}
