import { ColumnsType, TableProps } from 'antd/es/table';
import { TablePaginationConfig } from 'antd/lib/table/interface';
import React, { useEffect, useMemo, useRef, useState } from 'react';
import { useTranslation } from 'react-i18next';

import { Translations } from '../../../../features/localization';
import { alignTableColumns, isDevelopment, scrollTop } from '../../../../utils/helpers';
import { AppListBaseResponse } from '../../../mosaik';
import { ApiModule } from '../../apiModule';
import { ShowTotalPagination } from '../index';
import usePreferences from './usePreferences';

export type TableRequestData = {
  PageSize: number;
  PageNumber: number;
};
type AntdTableAsyncHookOptions = {
  pageSize?: number;
  rowKey?: string;
  scrollTopOnChange?: boolean;
  indexColumn?: boolean;
  alignColumns?: ColumnsType[0]['align'];
};
function useAntdTable<TRecordType>(apiModule: ApiModule, options: AntdTableAsyncHookOptions = {}) {
  const { t } = useTranslation();

  const preferences = usePreferences();
  const paginationData = preferences.getPreferenceApiPaginationOrDefault(apiModule);

  const internalOptions = useMemo<AntdTableAsyncHookOptions>(() => {
    return {
      ...options,
      pageSize: options.pageSize ?? paginationData.PageSize,
      rowKey: options.rowKey ?? 'id',
      alignColumns: options.alignColumns ?? 'center',
      indexColumn: options.indexColumn ?? true,
      scrollTopOnChange: options.scrollTopOnChange ?? true,
    };
  }, [options, paginationData.PageSize]);

  const [page, setPage] = useState<number>(1);
  const [total, setTotal] = useState<number>(0);
  const [records, setRecords] = useState<TRecordType[]>();
  const [pageSize, setPageSize] = useState<number>(internalOptions.pageSize!);

  const requestDataChangeFn = useRef<Function>();

  const setData = (data: AppListBaseResponse<TRecordType>) => {
    const list = internalOptions.indexColumn ? data.list.map((item, index) => ({ index: (page - 1) * pageSize + index + 1, ...item })) : data.list;
    setRecords(list);
    setTotal(data.totalCount);
  };

  const requestData = useMemo<TableRequestData>(() => {
    return { PageSize: pageSize, PageNumber: page };
  }, [page, pageSize]);

  useEffect(() => {
    if (requestDataChangeFn.current) {
      requestDataChangeFn.current(requestData);
    }
  }, [requestData]);

  const paginationConfig: TablePaginationConfig = {
    showSizeChanger: true,
    hideOnSinglePage: false,
    total: total,

    size: 'default',
    pageSize: pageSize,
    pageSizeOptions: isDevelopment() ? ['1', '2', '10', '20', '50', '100'] : undefined,
    current: page,
    onShowSizeChange: (current, size) => {
      setPageSize(size);
      preferences.setApiPagination(apiModule, { pageSize: size });
      if (!internalOptions.scrollTopOnChange) {
        scrollTop();
      }
    },
    onChange: (page) => {
      setPage(page);
      if (!internalOptions.scrollTopOnChange) {
        scrollTop();
      }
    },
    showTotal: (total: number, range: [number, number]) => <ShowTotalPagination from={range[0]} to={range[1]} total={total} />,
  };

  const setRequestDataChangeFn = (fn: Function) => {
    requestDataChangeFn.current = fn;
  };

  const configColumns = (columns: ColumnsType<TRecordType>) => {
    if (internalOptions.indexColumn) {
      columns.unshift({ title: t(Translations.Common.Row), width: '5%', dataIndex: 'index', align: 'center' });
    }
    alignTableColumns(columns, internalOptions.alignColumns);
  };

  const tableProps: TableProps<TRecordType> = {
    size: 'middle',
    pagination: paginationConfig,
    dataSource: records,
    rowKey: internalOptions.rowKey,
  };

  return { page, setPage, pageSize, total, records, tableProps, paginationConfig, requestData, setData, setRequestDataChangeFn, configColumns };
}

export default useAntdTable;
