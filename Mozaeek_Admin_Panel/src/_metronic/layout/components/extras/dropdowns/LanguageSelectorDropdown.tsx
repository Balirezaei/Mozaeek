import { Tooltip } from 'antd';
/* eslint-disable no-script-url,jsx-a11y/anchor-is-valid */
import clsx from 'clsx';
import React, { useMemo } from 'react';
import { Tooltip as BootstrapTooltip, Dropdown, OverlayTrigger } from 'react-bootstrap';
import { useTranslation } from 'react-i18next';

import { changeLanguageHttp } from '../../../../../app/modules/account/http/account-http';
import { LocalStorageKey } from '../../../../../features/constants';
import { useGlobalization, useHttpCall } from '../../../../../features/hooks';
import { Translations } from '../../../../../features/localization';
import { CultureName } from '../../../../../features/localization/cultures';
import { setStorage } from '../../../../../utils/helpers';
import { toAbsoluteUrl } from '../../../../_helpers';
import { DropdownTopbarItemToggler } from '../../../../_partials/dropdowns';

type CultureSelectData = {
  cultureName: CultureName;
  displayName?: string;
  text: string;
  flag: string;
};

export function LanguageSelectorDropdown() {
  const { t } = useTranslation();

  const culturesData = useMemo<CultureSelectData[]>(() => {
    const buildCultureDisplayName = (culture: CultureSelectData) => {
      const parts = culture.cultureName.split('-');
      culture.displayName = `${parts[0]}-${parts[1].toUpperCase()}`;
    };

    const items = [
      {
        cultureName: CultureName.EnUs,
        text: 'English',
        flag: toAbsoluteUrl('/media/svg/flags/226-united-states.svg'),
      },
      {
        cultureName: CultureName.NnNo,
        text: 'Norsk',
        flag: toAbsoluteUrl('/media/svg/flags/143-norway.svg'),
      },
    ];

    items.forEach(buildCultureDisplayName);

    return items;
  }, []);

  const { culture } = useGlobalization();
  const currentCultureData = culturesData.find((x) => x.cultureName === culture.Name)!;

  const changeLanguageApi = useHttpCall(changeLanguageHttp, { notificationOnError: true });

  const changeCulture = async (cultureName: CultureName) => {
    if (culturesData.find((f) => f.cultureName === cultureName)) {
      const response = await changeLanguageApi.call({ languageName: cultureName });
      if (response && response.data) {
        setStorage(LocalStorageKey.Culture, cultureName);
        window.location.reload();
      }
    } else {
      console.error(`Culture ${cultureName} is not supported.`);
    }
  };

  return (
    <Dropdown drop="down" alignRight>
      <Dropdown.Toggle as={DropdownTopbarItemToggler} id="dropdown-toggle-my-cart">
        <OverlayTrigger placement="bottom" overlay={<BootstrapTooltip id="language-panel-tooltip">{t(Translations.Tooltips.SelectLanguage)}</BootstrapTooltip>}>
          <div className="btn btn-icon btn-clean btn-dropdown btn-lg mr-1">
            <img className="h-25px w-25px rounded" src={currentCultureData.flag} alt={currentCultureData.text} />
          </div>
        </OverlayTrigger>
      </Dropdown.Toggle>
      <Dropdown.Menu className="p-0 m-0 dropdown-menu-right dropdown-menu-anim dropdown-menu-top-unround">
        <ul className="navi navi-hover py-4">
          {culturesData.map((culture) => (
            <li
              key={culture.cultureName}
              className={clsx('navi-item', {
                active: culture.cultureName === currentCultureData.cultureName,
              })}>
              <Tooltip title={culture.displayName} placement={'right'}>
                <a onClick={() => changeCulture(culture.cultureName)} className="navi-link">
                  <span className="symbol symbol-20 mr-3">
                    <img src={culture.flag} alt={culture.text} />
                  </span>
                  <span className="navi-text">{culture.text}</span>
                </a>
              </Tooltip>
            </li>
          ))}
        </ul>
      </Dropdown.Menu>
    </Dropdown>
  );
}
