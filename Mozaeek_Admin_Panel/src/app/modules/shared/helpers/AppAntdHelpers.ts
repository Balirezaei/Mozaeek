import { TableProps } from 'antd';
import React from 'react';

import { AppListBaseResponse } from '../../../mosaik';
import { ShowTotalPagination } from '../index';

export const getPaginationFromAppListResponse = (
  data: AppListBaseResponse<any> | undefined,
  onChange: ((page: number, pageSize?: number) => void) | undefined,
  props?: TableProps<any>['pagination']
): TableProps<any>['pagination'] => {
  if (data) {
    return {
      ...props,
      current: data.pageNumber,
      onChange: onChange,
      total: data.totalCount,
      showQuickJumper: true,
      showTotal: (total: number, range: [number, number]) => React.createElement(ShowTotalPagination, { from: range[0], to: range[1], total: total }),
    };
  }
};

export const getPaginationFromAppListResponse1 = (
  data: AppListBaseResponse<any> | undefined,
  onChange: ((page: number, pageSize?: number) => void) | undefined,
  preferenceModuleName: string,
  props?: TableProps<any>['pagination']
): TableProps<any>['pagination'] => {
  if (data) {
    return {
      ...props,
      current: data.pageNumber,
      onChange: onChange,
      total: data.totalCount,
      showQuickJumper: true,
      showTotal: (total: number, range: [number, number]) => React.createElement(ShowTotalPagination, { from: range[0], to: range[1], total: total }),
    };
  }
};
