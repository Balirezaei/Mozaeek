import { SelectProps } from 'antd';
import { ColumnsType } from 'antd/es/table';
import moment, { Moment } from 'moment';
import { DataNode } from 'rc-tree/lib/interface';

import { LanguageCode } from '../../features/localization/cultures';
import { LocalizationHelpers } from './LocalizationHelpers';

const today = moment().utc(true).startOf('day');

export const disableTodayAndBeforeAntdDatePicker = (current: any) => {
  return current && current < moment().utc(true).endOf('day');
};

export const disableTodayAndAfterAntdDatePicker = (current: any) => {
  return current && current > moment().utc(true).endOf('day');
};

export const disableYesterdayAndBeforeAntdDatePicker = (current: any) => {
  return current < moment().utc(true).subtract(1, 'day').endOf('day');
};

export const disableTomorrowAndAfterAntdDatePicker = (current: any) => {
  return current > moment().utc(true).endOf('day');
};

export const disableBeforeDateAntdDatePicker = (current: any, date: Moment) => {
  return moment(current).utc(true).isBefore(moment(date).utc(true), 'day');
};

export const disableAfterDateAntdDatePicker = (current: any, date: Moment) => {
  return moment(current).utc(true).isAfter(moment(date).utc(true), 'day');
};

export const disableOtherDatesAntdDatePicker = (
  current: Moment | any,
  from: Moment | undefined,
  to: Moment | undefined,
  options?: {
    disableYesterdayAndBefore?: boolean;
    disableToday?: boolean;
  }
) => {
  let beforeCondition = false;
  let afterCondition = false;
  let yesterdayAndBeforeCondition = false;
  let todayCondition = false;

  if (from) {
    beforeCondition = disableBeforeDateAntdDatePicker(current, from);
  }
  if (to) {
    afterCondition = disableAfterDateAntdDatePicker(current, to);
  }
  if (options?.disableYesterdayAndBefore) {
    yesterdayAndBeforeCondition = disableYesterdayAndBeforeAntdDatePicker(current);
  }
  if (options?.disableToday) {
    todayCondition = moment(current).utc(true).startOf('day').isSame(today);
  }

  return beforeCondition || afterCondition || yesterdayAndBeforeCondition || todayCondition;
};

export const antdDatePickerValue = (date: Date | undefined): Moment | undefined => {
  return date ? moment(date) : undefined;
};

export const getAntdLocale = () => {
  let antdLocale;
  switch (LocalizationHelpers.getCurrentCulture().LanguageCode) {
    case LanguageCode.En:
      antdLocale = require('antd/es/locale/en_US');
      break;
    case LanguageCode.Fa:
      antdLocale = require('antd/es/locale/fa_IR');
      break;
    case LanguageCode.Nb:
      antdLocale = require('antd/es/locale/nb_NO');
      break;
  }
  return antdLocale;
};

export const inputNumberCommaSeparatedProps = {
  formatter: (value: number | string | undefined) => value!.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ','),
  parser: (value: string | undefined) => value!.replace(/(,*)/g, ''),
};

export const alignTableColumns = (columns: ColumnsType<any>, align: ColumnsType[0]['align']) => {
  columns?.forEach((f) => {
    if (!f.align) {
      f.align = align;
    }
  });
};

export const getSelectOptions = <TItem>(items: TItem[] | undefined, labelProp: keyof TItem, valueProp: keyof TItem): SelectProps<any>['options'] => {
  if (items) {
    return items.map((item) => ({ label: item[labelProp] as unknown as string, value: item[valueProp] as unknown as string }));
  }
};

export const getTreeData = <TItem extends { id: number; parentId?: number }>(
  items: TItem[] | undefined,
  titleProp: keyof TItem,
  valueProp: keyof TItem
): DataNode[] | undefined => {
  const keys: number[] = [];

  if (items) {
    const array: (DataNode | undefined)[] = items.map((item) => BuildDataNodesFromItem(items, keys, titleProp, valueProp, item));
    return array.filter((f) => f !== undefined) as DataNode[];
  }
};

export const getTreeDataChildren = <TItem extends { id: number; parentId?: number }>(
  keys: number[],
  items: TItem[] | undefined,
  titleProp: keyof TItem,
  valueProp: keyof TItem,
  parentId: number | null = null
): DataNode[] | undefined => {
  if (items) {
    const children = items.filter((f) => f.parentId === parentId);
    const array: (DataNode | undefined)[] = children.map((item) => BuildDataNodesFromItem(items, keys, titleProp, valueProp, item));
    return array.filter((f) => f !== undefined) as DataNode[];
  }
};

const BuildDataNodesFromItem = <TItem extends { id: number; parentId?: number }>(
  items: TItem[] | undefined,
  keys: number[],
  titleProp: keyof TItem,
  valueProp: keyof TItem,
  item: TItem
) => {
  if (!keys.includes(item[valueProp] as unknown as number)) {
    keys.push(item[valueProp] as unknown as number);
    return {
      key: item[valueProp] as unknown as number,
      title: item[titleProp],
      children: getTreeDataChildren(keys, items, titleProp, valueProp, item.id),
    };
  }
  return undefined;
};

export const filterOptionByString = (input: string, option: any) => {
  return (option?.label as string).toLowerCase().indexOf(input.toLowerCase()) > -1;
};

export const filterTreeNodeByString = (inputValue: string, option: any) => {
  return (option?.title as string).toLowerCase().indexOf(inputValue.toLowerCase()) > -1;
};
