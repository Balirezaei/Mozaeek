import moment from 'moment-jalaali';
import { useMemo } from 'react';

import { cultureSelector } from '../../app/modules/account';
import { LocalizationHelpers } from '../../utils/helpers';
import { CalendarType, CultureName, HtmlDirection, SupportedCultures } from '../localization/cultures';
import { useAppSelector } from './index';

// const reverseMarginOrPaddingMap = new Map<string, string>([
//   ['ml', 'mr'],
//   ['mr', 'ml'],
//   ['pr', 'pl'],
//   ['pl', 'pr'],
// ]);
const useGlobalization = () => {
  const currentCulture = useAppSelector(cultureSelector);

  const datePickerFormat = useMemo(() => {
    switch (currentCulture.Calendar) {
      case CalendarType.Gregorian:
        return currentCulture.ShortDateFormat;
      case CalendarType.Jalali:
        return SupportedCultures.find((f) => f.Name === CultureName.EnUs)!.ShortDateFormat;
    }
  }, [currentCulture]);

  const dateTimePickerFormat = useMemo(() => {
    switch (currentCulture.Calendar) {
      case CalendarType.Gregorian:
        return currentCulture.ShortDateTimeFormat;
      case CalendarType.Jalali:
        return SupportedCultures.find((f) => f.Name === CultureName.EnUs)!.ShortDateTimeFormat;
    }
  }, [currentCulture]);

  // const formattedDate = (date: string, format: string, separator?: string | undefined, excludeJCharacters?: string[] | undefined) => {
  //   let formatToUse = format;
  //   if (culture.Calendar === CalendarType.Jalali && separator) {
  //     const parts = format.split(separator);
  //     formatToUse = parts
  //       .map((item) => {
  //         if (excludeJCharacters && excludeJCharacters.includes(item)) {
  //           return item;
  //         }
  //         return `j${item}`;
  //       })
  //       .join(separator);
  //   }
  //   return moment(date).utc(true).format(formatToUse);
  // };

  const getCultureOrDefault = (cultureName: CultureName | undefined | CultureName.EnUs | CultureName.FaIr | CultureName.NnNo) => {
    if (cultureName) {
      return LocalizationHelpers.getCultureFromCultureName(cultureName) ?? currentCulture;
    } else {
      return currentCulture;
    }
  };

  const formatHourMinute = (date: string, cultureName?: CultureName) => {
    const culture = getCultureOrDefault(cultureName);

    return moment(date).locale(culture.MomentLocal).utc(true).format('HH:mm');
  };

  const formatShortDate = (date: string, cultureName?: CultureName) => {
    const culture = getCultureOrDefault(cultureName);

    return moment(date).locale(culture.MomentLocal).utc(true).format(culture.ShortDateFormat);
  };

  const formatWeekDay = (date: string, cultureName?: CultureName) => {
    const culture = getCultureOrDefault(cultureName);

    return moment(date).locale(culture.MomentLocal).utc(true).format('dddd');
  };

  const formatShortMonthWeekDay = (date: string, cultureName?: CultureName) => {
    const culture = getCultureOrDefault(cultureName);

    return moment(date).locale(culture.MomentLocal).utc(true).format(culture.ShortMonthWeekDayFormat);
  };

  const formatFullMonthWeekDay = (date: string, cultureName?: CultureName) => {
    const culture = getCultureOrDefault(cultureName);

    return moment(date).locale(culture.MomentLocal).utc(true).format(culture.FullMonthWeekDayFormat);
  };

  const formatDateTime = (dateTime: string, cultureName?: CultureName, includeSeconds: boolean = false) => {
    const culture = getCultureOrDefault(cultureName);

    return moment(dateTime)
      .locale(culture.MomentLocal)
      .utc(true)
      .format(`${culture.ShortDateFormat} HH:mm${includeSeconds ? ':ss' : ''}`);
  };

  const formatCompleteDateTime = (dateTime: string, cultureName?: CultureName, includeSeconds: boolean = false) => {
    const culture = getCultureOrDefault(cultureName);

    return moment(dateTime)
      .locale(culture.MomentLocal)
      .utc(true)
      .format(`${culture.CompleteDateFormat}${includeSeconds ? ':ss' : ''}`);
  };

  const reversePositionOnRtl = (ltrPosition: 'right' | 'left') => {
    if (currentCulture.HtmlDirection === HtmlDirection.LeftToRight) {
      return ltrPosition;
    } else {
      return ltrPosition === 'left' ? 'right' : 'left';
    }
  };

  // const reverseMarginOrPaddingOnRtl = (className: string) => {
  //   if (culture.HtmlDirection === HtmlDirection.RightToLeft && className) {
  //     const value = className.split('-')[0];
  //
  //     const revereClassName = reverseMarginOrPaddingMap.get(value);
  //     if (revereClassName) {
  //       return className.replace(value, revereClassName);
  //     }
  //   }
  //   return className;
  // };

  return {
    culture: currentCulture,
    formatShortDate,
    formatShortMonthWeekDay,
    formatFullMonthWeekDay,
    formatWeekDay,
    formatDateTime,
    formatHourMinute,
    formatCompleteDateTime,
    datePickerFormat,
    dateTimePickerFormat,
    reversePositionOnRtl,
  };
};

export default useGlobalization;
