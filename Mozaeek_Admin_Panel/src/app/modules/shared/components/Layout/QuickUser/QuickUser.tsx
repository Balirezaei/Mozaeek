import React, { useMemo } from 'react';
import { useTranslation } from 'react-i18next';
import SVG from 'react-inlinesvg';
import { Link, useHistory } from 'react-router-dom';

import { useAppSelector, usePermissions } from '../../../../../../features/hooks';
import { Translations } from '../../../../../../features/localization';
import { toAbsoluteUrl } from '../../../../../../utils/helpers';
import { profileSelector } from '../../../../account';

type NavItem = {
  path: string;
  text: string;
  svgUrl: string;
  descriptions: string;
  permissions?: readonly string[];
};
const QuickUser = () => {
  const { t } = useTranslation();
  const history = useHistory();

  const { hasPermissions } = usePermissions();

  const profile = useAppSelector(profileSelector);

  const logoutClick = () => {
    const toggle = document.getElementById('kt_quick_user_toggle');
    if (toggle) {
      toggle.click();
    }
    history.push('/auth/logout');
  };

  const navItem = (data: NavItem) => {
    if (hasPermissions(data.permissions, true)) {
      return (
        <Link key={data.text} to={data.path} className="navi-item">
          <div className="navi-link">
            <div className="symbol symbol-40 bg-light mr-3">
              <div className="symbol-label">
                <span className="svg-icon svg-icon-md svg-icon-success">
                  <SVG src={toAbsoluteUrl(data.svgUrl)} />
                </span>
              </div>
            </div>
            <div className="navi-text">
              <div className="font-weight-bold">{data.text}</div>
              <div className="text-muted">{data.descriptions}</div>
            </div>
          </div>
        </Link>
      );
    }
  };

  const navItems = useMemo<readonly NavItem[]>(() => {
    return [
      {
        path: '/account/profile',
        svgUrl: '/media/svg/icons/General/Notification2.svg',
        text: t(Translations.Account.MyProfile),
        descriptions: t(Translations.Account.MyProfileDescription),
      },
    ];

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, []);

  return (
    <div id="kt_quick_user" className="offcanvas offcanvas-right offcanvas p-10">
      <div className="offcanvas-header d-flex align-items-center justify-content-between pb-5">
        <h3 className="font-weight-bold m-0">نمایه کاربر</h3>
        {/* eslint-disable-next-line jsx-a11y/anchor-is-valid */}
        <a href="#" className="btn btn-xs btn-icon btn-light btn-hover-primary" id="kt_quick_user_close">
          <i className="ki ki-close icon-xs text-muted" />
        </a>
      </div>

      <div className="offcanvas-content pr-5 mr-n5">
        <div className="d-flex align-items-center mt-5">
          <div className="symbol symbol-100 mr-5">
            <div
              className="symbol-label user-nav-avatar-symbol"
              style={{
                backgroundImage: `url(${toAbsoluteUrl('/media/svg/icons/user.svg')})`,
              }}
            />
            <i className="symbol-badge bg-success" />
          </div>
          <div className="d-flex flex-column">
            <span className="font-weight-bold font-size-h5 text-dark-75">
              {profile.firstname} {profile.lastname}
            </span>
            <div className="navi mt-2">
              <div className="navi-item">
                <span className="navi-link p-0 pb-2">
                  <span className="navi-icon mr-1">
                    <span className="svg-icon-lg svg-icon-primary">
                      <SVG src={toAbsoluteUrl('/media/svg/icons/Communication/Mail-notification.svg')} />
                    </span>
                  </span>
                  <span className="navi-text text-muted">{profile.userName}</span>
                </span>
              </div>
            </div>
            {/* <Link to="/logout" className="btn btn-light-primary btn-bold">
                Sign Out
              </Link> */}
            <button className="btn btn-light-primary btn-bold" onClick={logoutClick}>
              {t(Translations.Auth.SignOut)}
            </button>
          </div>
        </div>

        <div className="separator separator-dashed mt-8 mb-5" />

        <div className="navi navi-spacer-x-0 p-0">{navItems.map(navItem)}</div>

        <div className="separator separator-dashed my-7" />
      </div>
    </div>
  );
};

export default QuickUser;
