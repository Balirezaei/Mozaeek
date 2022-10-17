export enum Language {
  English = 'English',
  Persian = 'Persian',
  Norwegian = 'Norwegian',
}

export enum LanguageCode {
  En = 'en',
  Fa = 'fa',
  Nb = 'nb',
}

export enum HtmlDirection {
  LeftToRight = 'ltr',
  RightToLeft = 'rtl',
}

export enum CultureName {
  EnUs = 'en-us',
  FaIr = 'fa-ir',
  NnNo = 'nb-no',
}

export enum CalendarType {
  Gregorian = 'Gregorian',
  Jalali = 'Jalali',
}

export enum MomentLocal {
  En = 'en',
  Fa = 'fa',
  Nb = 'nb',
}

export const SupportedLanguages: readonly Language[] = Object.values(Language);
export const SupportedLanguagesCodes: readonly LanguageCode[] = Object.values(LanguageCode);
export const SupportedCultureNames: readonly CultureName[] = Object.values(CultureName);
export const SupportedCalendars: readonly CalendarType[] = Object.values(CalendarType);

export type SupportedCulture = {
  Name: CultureName;
  Language: Language;
  LanguageCode: LanguageCode;
  MomentLocal: MomentLocal;
  HtmlDirection: HtmlDirection;
  Calendar: CalendarType;
  ShortDateFormat: string;
  ShortDateTimeFormat: string;
  DisplayShortDateFormat: string;
  DisplayShortDateTimeFormat?: string;
  FullMonthWeekDayFormat: string;
  ShortMonthWeekDayFormat: string;
  CompleteDateFormat: string;
};

export const SupportedCultures: readonly SupportedCulture[] = [
  {
    Name: CultureName.EnUs,
    Language: Language.English,
    LanguageCode: LanguageCode.En,
    MomentLocal: MomentLocal.En,
    HtmlDirection: HtmlDirection.LeftToRight,
    Calendar: CalendarType.Gregorian,
    ShortDateFormat: 'MM/DD/YYYY',
    ShortDateTimeFormat: 'MM/DD/YYYY HH:mm',
    DisplayShortDateFormat: 'Month/Day/Year',
    FullMonthWeekDayFormat: 'dddd, MMMM D',
    ShortMonthWeekDayFormat: 'ddd, MMM D',
    CompleteDateFormat: 'dddd, MMMM D YYYY - HH:mm',
  },
  {
    Name: CultureName.FaIr,
    Language: Language.Persian,
    LanguageCode: LanguageCode.Fa,
    MomentLocal: MomentLocal.Fa,
    HtmlDirection: HtmlDirection.RightToLeft,
    Calendar: CalendarType.Jalali,
    ShortDateFormat: 'jYYYY/jMM/jDD',
    ShortDateTimeFormat: 'jYYYY/jMM/jDD HH:mm',
    DisplayShortDateFormat: 'روز/ماه/سال',
    FullMonthWeekDayFormat: 'dddd، jD jMMMM',
    ShortMonthWeekDayFormat: 'ddd، jD jMMM',
    CompleteDateFormat: 'dddd، jMMMM jD jYYYY - HH:mm',
  },
  {
    Name: CultureName.NnNo,
    Language: Language.Norwegian,
    LanguageCode: LanguageCode.Nb,
    MomentLocal: MomentLocal.Nb,
    HtmlDirection: HtmlDirection.LeftToRight,
    Calendar: CalendarType.Gregorian,
    ShortDateFormat: 'DD/MM/YYYY',
    ShortDateTimeFormat: 'DD/MM/YYYY HH:mm',
    DisplayShortDateFormat: 'Day/Month/Year',
    FullMonthWeekDayFormat: 'dddd, MMMM D',
    ShortMonthWeekDayFormat: 'ddd, MMM D',
    CompleteDateFormat: 'dddd, MMMM D YYYY - HH:mm',
  },
];
