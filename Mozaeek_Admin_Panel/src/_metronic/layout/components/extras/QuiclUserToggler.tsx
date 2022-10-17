import get from 'lodash/get';
/* eslint-disable no-restricted-imports */
/* eslint-disable no-script-url,jsx-a11y/anchor-is-valid */
import React, { useMemo } from 'react';
import { OverlayTrigger, Tooltip } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';

import { profileSelector } from '../../../../app/modules/account';
import { useAppSelector } from '../../../../features/hooks';
import { Translations } from '../../../../features/localization';
import { useHtmlClassService } from '../../_core/MetronicLayout';
import { UserProfileDropdown } from './dropdowns/UserProfileDropdown';

export function QuickUserToggler() {
  const { t } = useTranslation();

  const uiService = useHtmlClassService();
  const layoutProps = useMemo(() => {
    return {
      offcanvas: get(uiService.config, 'extras.user.layout') === 'offcanvas',
    };
  }, [uiService]);

  const profile = useAppSelector(profileSelector);

  return (
    <>
      {layoutProps.offcanvas && (
        <OverlayTrigger placement="bottom" overlay={<Tooltip id="quick-user-tooltip">{t(Translations.Tooltips.ViewUser)}</Tooltip>}>
          <div className="topbar-item">
            <div className="btn btn-icon w-auto btn-clean d-flex align-items-center btn-lg px-2" id="kt_quick_user_toggle">
              <>
                <span className="text-muted font-weight-bold font-size-base d-none d-md-inline mr-1">
                  {t(Translations.Common.Hi)}
                  {t(Translations.Common.Comma)}
                </span>
                <span className="text-dark-50 font-weight-bolder font-size-base d-none d-md-inline mr-3">{profile.firstname}</span>
                <span className="symbol symbol-35 symbol-light-success">
                  <span className="symbol-label font-size-h5 font-weight-bold">{profile.firstname!.toUpperCase().charAt(0)}</span>
                </span>
              </>
            </div>
          </div>
        </OverlayTrigger>
      )}

      {!layoutProps.offcanvas && <UserProfileDropdown />}
    </>
  );
}
