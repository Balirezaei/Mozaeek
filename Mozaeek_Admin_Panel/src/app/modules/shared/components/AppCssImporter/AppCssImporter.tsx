import 'socicon/css/socicon.css';
import '@fortawesome/fontawesome-free/css/all.min.css';
import 'antd/lib/style/index.less';
import 'antd/lib/button/style/index.less';
import 'antd/lib/divider/style/index.less';
import 'antd/lib/grid/style/index.less';
import 'antd/lib/space/style/index.less';
import 'antd/lib/dropdown/style/index.less';
import 'antd/lib/menu/style/index.less';
import 'antd/lib/pagination/style/index.less';
import 'antd/lib/steps/style/index.less';
import 'antd/lib/checkbox/style/index.less';
import 'antd/lib/date-picker/style/index.less';
import 'antd/lib/form/style/index.less';
import 'antd/lib/input/style/index.less';
import 'antd/lib/input-number/style/index.less';
import 'antd/lib/radio/style/index.less';
import 'antd/lib/rate/style/index.less';
import 'antd/lib/select/style/index.less';
import 'antd/lib/slider/style/index.less';
import 'antd/lib/switch/style/index.less';
import 'antd/lib/card/style/index.less';
import 'antd/lib/empty/style/index.less';
import 'antd/lib/popover/style/index.less';
import 'antd/lib/table/style/index.less';
import 'antd/lib/tabs/style/index.less';
import 'antd/lib/tag/style/index.less';
import 'antd/lib/tooltip/style/index.less';
import 'antd/lib/alert/style/index.less';
import 'antd/lib/modal/style/index.less';
import 'antd/lib/notification/style/index.less';
import 'antd/lib/skeleton/style/index.less';
import 'antd/lib/spin/style/index.less';
import 'antd/lib/popconfirm/style/index.less';
import 'antd/lib/tree/style/index.less';
import 'antd/lib/tree-select/style/index.less';
import 'antd/lib/image/style/index.less';

import '../../../../../_metronic/_assets/plugins/keenthemes-icons/font/ki.css';
import '../../../../../styles/custom.less';

import React from 'react';

//import 'antd/dist/antd.less';

const LtrImport = React.lazy(() => import('./LtrImport'));
const RtlImport = React.lazy(() => import('./RtlImport'));

type Props = {
  direction: 'ltr' | 'rtl';
};
const AppCssImporter: React.FC<Props> = (props) => {
  if (props.direction === 'rtl') {
    document.body.dir = 'rtl';
  }
  const component = props.direction === 'ltr' ? <LtrImport /> : <RtlImport />;
  return (
    <>
      <React.Suspense fallback={<></>}>{component}</React.Suspense>
      {props.children}
    </>
  );
};

export default AppCssImporter;
