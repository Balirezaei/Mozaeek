import { FormInstance } from 'antd/es/form';
import { Rule } from 'antd/lib/form';
import { RuleObject, StoreValue } from 'rc-field-form/lib/interface';
import { useTranslation } from 'react-i18next';

import { Translations } from '../localization';

type LabelWithRules = {
  label?: string;
  fieldName?: string;
  rules?: {
    type:
      | 'MinLength'
      | 'MaxLength'
      | 'Required'
      | 'EnglishLetterOnly'
      | 'EnglishLetterAndNumbersOnly'
      | 'EnglishLetterAndWhitespaceOnly'
      | 'EnglishLetterAndNumbersAndWhitespaceOnly'
      | 'MatchField'
      | 'Pattern';
    arguments?: (string | number | { matchFieldName: string; rejectionMessage: string })[];
  }[];
};
const useAntdValidation = (formInstance: FormInstance) => {
  const { t } = useTranslation();

  // const requiredRule = (fieldNameKey: string): Rule => ({
  //   required: true,
  //   message: t(Translations.Validations.FieldRequired, { field: t(fieldNameKey) }),
  // });
  //
  // const minRule = (fieldName: string, min: number): Rule => ({
  //   min: min,
  //   message: t(Translations.Validations.FieldMin, { field: fieldName, count: min }),
  // });
  //
  // const maxRule = (fieldName: string, max: number): Rule => ({
  //   max: max,
  //   message: t(Translations.Validations.FieldMax, { field: fieldName, count: max }),
  // });
  //
  // const onlyEnglishLettersRule = (fieldName: string, allowWhitespace: boolean = false): Rule => ({
  //   pattern: new RegExp(allowWhitespace ? '^[a-zA-Z ]*$' : '^[a-zA-Z]*$'),
  //   message: t(Translations.Validations.FieldOnlyEnglishLetters, { field: fieldName }),
  // });
  //
  // const matchFieldRule = (matchFieldName: string, rejectionMessage: string): Rule => ({
  //   validator(rule: RuleObject, value: StoreValue) {
  //     if (!value || formInstance.getFieldValue(matchFieldName) === value) {
  //       return Promise.resolve();
  //     }
  //     return Promise.reject(rejectionMessage);
  //   },
  // });

  const labelWithRules = (data: LabelWithRules) => {
    // eslint-disable-next-line array-callback-return
    const rules = data.rules?.map<Rule>((rule) => {
      switch (rule.type) {
        case 'MinLength':
          let min;
          if (rule.arguments) {
            min = +rule.arguments[0];
          } else {
            min = 1;
          }
          return { min: min, message: t(Translations.Validations.FieldMin, { field: data.label ?? data.fieldName, count: min }) };
        case 'MaxLength':
          let max;
          if (rule.arguments) {
            max = +rule.arguments[0];
          } else {
            max = 1;
          }
          return { max: max, message: t(Translations.Validations.FieldMax, { field: data.label ?? data.fieldName, count: max }) };
        case 'Required':
          return { required: true, message: t(Translations.Validations.FieldRequired, { field: data.label ?? data.fieldName }) };
        case 'EnglishLetterOnly':
          return { pattern: new RegExp('^[a-zA-Z]*$'), message: t(Translations.Validations.FieldOnlyEnglishLetters, { field: data.label ?? data.fieldName }) };
        case 'EnglishLetterAndNumbersOnly':
          return {
            pattern: new RegExp('^[a-zA-Z0-9]*$'),
            message: t(Translations.Validations.FieldOnlyEnglishLettersOrNumbers, { field: data.label ?? data.fieldName }),
          };
        case 'EnglishLetterAndWhitespaceOnly':
          return { pattern: new RegExp('^[a-zA-Z ]*$'), message: t(Translations.Validations.FieldOnlyEnglishLetters, { field: data.label ?? data.fieldName }) };
        case 'EnglishLetterAndNumbersAndWhitespaceOnly':
          return {
            pattern: new RegExp('^[a-zA-Z0-9 ]*$'),
            message: t(Translations.Validations.FieldOnlyEnglishLettersOrNumbers, { field: data.label ?? data.fieldName }),
          };
        case 'Pattern':
          return {
            pattern: new RegExp(rule.arguments![0] as string),
            message: rule.arguments?.[1] as string,
          };
        case 'MatchField':
          return {
            validator: (r: RuleObject, value: StoreValue) => {
              if (rule.arguments) {
                const ruleArgument = rule.arguments[0] as { matchFieldName: string; rejectionMessage: string };
                if (!value || formInstance.getFieldValue(ruleArgument.matchFieldName) === value) {
                  return Promise.resolve();
                }
                return Promise.reject(ruleArgument.rejectionMessage);
              }
            },
          };
      }
    });
    return { label: data.label, rules: rules };
  };

  //return { requiredRule, minRule, maxRule, onlyEnglishLettersRule, matchFieldRule, labelWithRules };
  return { labelWithRules };
};

export default useAntdValidation;
