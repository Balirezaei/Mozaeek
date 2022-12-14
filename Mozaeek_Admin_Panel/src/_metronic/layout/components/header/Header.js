import objectPath from 'object-path';
import React, { useMemo } from 'react';

import { HeaderProgressBar } from '../../../../app/modules/shared';
import { useHtmlClassService } from '../../_core/MetronicLayout';
import { HeaderMenuWrapper } from './header-menu/HeaderMenuWrapper';
import { Topbar } from './Topbar';

export function Header() {
  const uiService = useHtmlClassService();

  const layoutProps = useMemo(() => {
    return {
      headerClasses: uiService.getClasses('header', true),
      headerAttributes: uiService.getAttributes('header'),
      headerContainerClasses: uiService.getClasses('header_container', true),
      menuHeaderDisplay: objectPath.get(uiService.config, 'header.menu.self.display'),
    };
  }, [uiService]);

  return (
    <>
      {/*begin::Header*/}
      <div className={`header ${layoutProps.headerClasses}`} id="kt_header" {...layoutProps.headerAttributes}>
        {/*begin::Container*/}
        <div className={` ${layoutProps.headerContainerClasses} d-flex align-items-stretch justify-content-between`}>
          <HeaderProgressBar />
          {/*begin::Header Menu Wrapper*/}
          {layoutProps.menuHeaderDisplay && <HeaderMenuWrapper />}
          {!layoutProps.menuHeaderDisplay && <div />}
          {/*end::Header Menu Wrapper*/}

          {/*begin::Topbar*/}
          <Topbar />
          {/*end::Topbar*/}
        </div>
        {/*end::Container*/}
      </div>
      {/*end::Header*/}
    </>
  );
}
