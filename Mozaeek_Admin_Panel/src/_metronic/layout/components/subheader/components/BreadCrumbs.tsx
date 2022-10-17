import { Tooltip } from 'antd';
import React from 'react';
import { Link } from 'react-router-dom';

import { BreadcrumbType } from '../../../../../app/modules/shared/types';

type Props = {
  items?: BreadcrumbType[];
};
export const BreadCrumbs: React.VFC<Props> = React.memo((props) => {
  if (!props.items || !props.items.length) {
    return null;
  }

  return (
    <ul className="breadcrumb breadcrumb-transparent breadcrumb-dot font-weight-bold p-0 my-2">
      <li className="breadcrumb-item">
        <Link to="/">
          <span className="fas fa-home text-muted" />
        </Link>
      </li>
      {props.items.map((item, index) => (
        <li key={`bc${index}`} className="breadcrumb-item">
          <Tooltip title={item.tooltip}>
            {item.path ? (
              <Link className="text-muted" to={item.path}>
                {item.title}
              </Link>
            ) : (
              <span className="text-muted">{item.title}</span>
            )}
          </Tooltip>
        </li>
      ))}
    </ul>
  );
});
