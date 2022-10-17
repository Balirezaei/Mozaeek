export const paginateArray = (array: any[], page: number, pageSize: number) => {
  if (array) {
    return array.slice((page - 1) * pageSize, page * pageSize);
  }
  return undefined;
};

export const findDeepParentChildrenArray = <TItem extends { [key: string]: any; children?: TItem[] }>(
  array: TItem[],
  propertyName: keyof TItem,
  value: any
): TItem | undefined => {
  for (const item of array) {
    if (item[propertyName as string] === value) {
      return item;
    } else if (item.children && item.children.length > 0) {
      const deepItem = findDeepParentChildrenArray(item.children, propertyName, value);
      if (deepItem) {
        return deepItem;
      }
    }
  }
};
