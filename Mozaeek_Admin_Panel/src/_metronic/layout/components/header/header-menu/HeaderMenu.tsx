/* eslint-disable no-script-url,jsx-a11y/anchor-is-valid,jsx-a11y/role-supports-aria-props */
import React, { useMemo } from 'react';
import { useTranslation } from 'react-i18next';
import { useLocation } from 'react-router';
import { NavLink } from 'react-router-dom';

import { AdminPath } from '../../../../../app/modules/admin/AdminRoutes';
import { CorePaths } from '../../../../../app/modules/core/CoreRoutes';
import { usePermissions } from '../../../../../features/hooks';
import { Translations } from '../../../../../features/localization';
import { isProduction } from '../../../../../utils/helpers';
import { checkIsActive } from '../../../../_helpers';

type MenuItem = {
  hide?: boolean;
  relativePosition: boolean;
  title: string;
  url: string;
  iconKeyFontawesome?: string;
  permissions?: string[];
};
export const HeaderMenu = React.memo(({ layoutProps }: any) => {
  const { t } = useTranslation();
  const location = useLocation();

  const { hasPermissions } = usePermissions();

  const getMenuItemActive = (url: string) => {
    return checkIsActive(location, url) ? 'menu-item-active' : '';
  };

  const menuItems = useMemo<{ [key: string]: MenuItem[] }>(() => {
    return {
      users: [
        {
          relativePosition: false,
          title: t(Translations.Common.Users),
          url: AdminPath.Users.Users,
          iconKeyFontawesome: 'users',
        },
      ],
      announcement: [
        {
          relativePosition: false,
          title: t(Translations.Core.RSS),
          url: CorePaths.Rss,
          iconKeyFontawesome: 'rss',
        },
        {
          relativePosition: false,
          title: t(Translations.Core.News),
          url: CorePaths.News,
          iconKeyFontawesome: 'newspaper',
        },
      ],
      basic: [
        {
          relativePosition: false,
          title: t(Translations.Core.Label),
          url: CorePaths.Labels,
          iconKeyFontawesome: 'tag',
        },
        {
          relativePosition: false,
          title: t(Translations.Core.RequestAct),
          url: CorePaths.RequestAct,
          iconKeyFontawesome: 'font',
        },
        {
          relativePosition: false,
          title: t(Translations.Core.Point),
          url: CorePaths.Points,
          iconKeyFontawesome: 'globe',
        },
        {
          relativePosition: false,
          title: t(Translations.Core.RequestOrganization),
          url: CorePaths.RequestOrganizations,
          iconKeyFontawesome: 'building',
        },
        {
          relativePosition: false,
          title: t(Translations.Core.Subjects),
          url: CorePaths.Subjects,
          iconKeyFontawesome: 'sticky-note',
        },
      ],
      request: [
        {
          relativePosition: false,
          title: t(Translations.Core.RequestTarget),
          url: CorePaths.RequestTargets,
          iconKeyFontawesome: 'bullseye',
        },
        {
          relativePosition: false,
          title: t(Translations.Core.Request),
          url: CorePaths.Requests,
          iconKeyFontawesome: 'bullseye',
        },
        {
          relativePosition: false,
          title: t(Translations.Core.PreRequest),
          url: CorePaths.PreRequest,
          iconKeyFontawesome: 'bullseye',
          hide: isProduction(),
        },
      ],
      pricing: [
        {
          relativePosition: false,
          title: t(Translations.Core.Request),
          url: CorePaths.RequestPricing.List,
          iconKeyFontawesome: 'money-bill',
        },
        {
          relativePosition: false,
          title: t(Translations.Core.Subject),
          url: CorePaths.SubjectPricing.List,
          iconKeyFontawesome: 'money-bill',
        },
      ],
    };
  }, [t]);

  const menuItem = (menuItem: MenuItem) => {
    let pass = !menuItem.hide;
    if (menuItem.permissions) {
      pass = hasPermissions(menuItem.permissions);
    }

    if (pass) {
      const relativePositionClassName = menuItem.relativePosition ? 'menu-item-rel' : undefined;
      return (
        <li key={menuItem.title} className={`menu-item ${relativePositionClassName} ${getMenuItemActive(menuItem.url)}`}>
          <NavLink className="menu-link" to={menuItem.url}>
            {menuItem.iconKeyFontawesome && (
              <span className="menu-icon">
                <i className={`fas fa-${menuItem.iconKeyFontawesome}`} />
              </span>
            )}
            <span className="menu-text">{menuItem.title}</span>
          </NavLink>
        </li>
      );
    }
    return undefined;
  };

  return (
    <div id="kt_header_menu" className={`header-menu header-menu-mobile ${layoutProps.ktMenuClasses}`} {...layoutProps.headerMenuAttributes}>
      {/*begin::Header Nav*/}
      <ul className={`menu-nav ${layoutProps.ulClasses}`}>
        {/*begin::1 Level*/}
        <li className={`menu-item menu-item-rel ${getMenuItemActive('/home')}`}>
          <NavLink className="menu-link" to="/">
            <span className="menu-text">{t(Translations.Menu.Home)}</span>
          </NavLink>
        </li>
        {/*end::1 Level*/}

        {/*begin::1 Level*/}
        <li data-menu-toggle={layoutProps.menuDesktopToggle} aria-haspopup="true" className={`menu-item menu-item-submenu menu-item-rel`}>
          <div className="menu-link menu-toggle">
            <span className="menu-text">{t(Translations.Admin.Admin)}</span>
            <i className="menu-arrow" />
          </div>
          <div className="menu-submenu menu-submenu-classic menu-submenu-left">
            <ul className="menu-subnav">{menuItems.users.map(menuItem)}</ul>
          </div>
        </li>

        {/*begin::1 Level*/}
        <li data-menu-toggle={layoutProps.menuDesktopToggle} aria-haspopup="true" className={`menu-item menu-item-submenu menu-item-rel`}>
          <div className="menu-link menu-toggle">
            <span className="menu-text">{t(Translations.Core.BasicData)}</span>
            <i className="menu-arrow" />
          </div>
          <div className="menu-submenu menu-submenu-classic menu-submenu-left">
            <ul className="menu-subnav">{menuItems.basic.map(menuItem)}</ul>
          </div>
        </li>
        {/*end::1 Level*/}

        {/*begin::1 Level*/}
        <li data-menu-toggle={layoutProps.menuDesktopToggle} aria-haspopup="true" className={`menu-item menu-item-submenu menu-item-rel`}>
          <div className="menu-link menu-toggle">
            <span className="menu-text">{t(Translations.Core.Request)}</span>
            <i className="menu-arrow" />
          </div>
          <div className="menu-submenu menu-submenu-classic menu-submenu-left">
            <ul className="menu-subnav">{menuItems.request.map(menuItem)}</ul>
          </div>
        </li>
        {/*end::1 Level*/}

        {/*begin::1 Level*/}
        <li data-menu-toggle={layoutProps.menuDesktopToggle} aria-haspopup="true" className={`menu-item menu-item-submenu menu-item-rel`}>
          <div className="menu-link menu-toggle">
            <span className="menu-text">{t(Translations.Core.Announcements)}</span>
            <i className="menu-arrow" />
          </div>
          <div className="menu-submenu menu-submenu-classic menu-submenu-left">
            <ul className="menu-subnav">{menuItems.announcement.map(menuItem)}</ul>
          </div>
        </li>
        {/*end::1 Level*/}

        {/*begin::1 Level*/}
        <li data-menu-toggle={layoutProps.menuDesktopToggle} aria-haspopup="true" className={`menu-item menu-item-submenu menu-item-rel`}>
          <div className="menu-link menu-toggle">
            <span className="menu-text">{t(Translations.Core.Pricing)}</span>
            <i className="menu-arrow" />
          </div>
          <div className="menu-submenu menu-submenu-classic menu-submenu-left">
            <ul className="menu-subnav">{menuItems.pricing.map(menuItem)}</ul>
          </div>
        </li>
        {/*end::1 Level*/}
      </ul>

      {/*end::Header Nav*/}
    </div>
  );
});
