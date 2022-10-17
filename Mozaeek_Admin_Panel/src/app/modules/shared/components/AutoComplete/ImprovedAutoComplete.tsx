import { LoadingOutlined } from '@ant-design/icons';
import { Dropdown, Input, Menu, Spin, Tooltip } from 'antd';
import cuid from 'cuid';
import React, { KeyboardEvent, PropsWithChildren, Ref, useEffect, useState } from 'react';

type Props<T> = {
  value?: T;
  onChange?: (value: T | undefined) => void;
  onInputChanged: (value: string) => void;
  options: T[];
  renderOption: (option: T) => React.ReactElement;
  renderTooltip?: (option: T) => React.ReactElement | undefined;
  overlayStyle?: React.CSSProperties | undefined;
  textPropertyName: string;
  overlayClassName?: string;
  asyncPending?: boolean;
  additionVisibleCondition?: boolean;
  placeholder?: string;
  allowClear?: boolean;
  inputRef?: Ref<Input>;
  defaultItem?: any;
};
function ImprovedAutoComplete<T>(props: PropsWithChildren<Props<T>>) {
  const [selectedOption, setSelectedOption] = useState<T | undefined>(props.defaultItem);
  const [visible, setVisible] = useState(false);
  const [text, setText] = useState<string>();

  useEffect(() => {
    if (text && props.options.length > 0) {
      setVisible(true);
    }
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [props.options]);

  useEffect(() => {
    if (props.value) {
      setText((props.value as any)[props.textPropertyName]);
    } else {
      setText(undefined);
    }
  }, [props.textPropertyName, props.value]);

  const handleKeyDown = (event: KeyboardEvent<HTMLInputElement>) => {
    if (event.key === 'Backspace' && selectedOption) {
      props.onChange?.(undefined);
      setText(undefined);
      setSelectedOption(undefined);
      if (props.options.length) {
        setVisible(true);
      } else {
        setVisible(false);
      }
    }
  };

  const handleFocus = () => {
    if (!text && props.options.length > 0) {
      setVisible(true);
    }
  };

  const handleBlur = () => {
    setTimeout(() => setVisible(false), 350);
  };

  return (
    <>
      <Dropdown
        visible={props.additionVisibleCondition && visible}
        className={props.overlayClassName}
        overlay={
          <Menu className="pt-0 pb-0">
            {props.asyncPending
              ? null
              : props.options.map((item, index) => {
                  return (
                    <Menu.Item
                      className={index > 0 ? 'border-top' : ''}
                      key={cuid()}
                      onClick={() => {
                        setSelectedOption(item);
                        setText((item as any)[props.textPropertyName]);
                        props.onChange?.(item);
                        setVisible(false);
                      }}>
                      {props.renderOption(item)}
                    </Menu.Item>
                  );
                })}
          </Menu>
        }>
        <Tooltip title={selectedOption && props.renderTooltip ? props.renderTooltip(selectedOption) : undefined}>
          {/*<Input*/}
          {/*  ref={props.inputRef}*/}
          {/*  placeholder={props.placeholder}*/}
          {/*  value={text ?? (props.value as any)?.[props.textPropertyName]}*/}
          {/*  onChange={(event) => {*/}
          {/*    setText(event.target.value);*/}
          {/*    props.onInputChanged(event.target.value);*/}
          {/*  }}*/}
          {/*  allowClear={props.allowClear}*/}
          {/*  className="left-to-right"*/}
          {/*  onKeyDown={handleKeyDown}*/}
          {/*  onFocus={() => handleFocus()}*/}
          {/*  onBlur={() => handleBlur()}*/}
          {/*  //addonAfter={props.asyncPending && <LoadingOutlined spin />}*/}
          {/*/>*/}

          <div>
            <Input
              ref={props.inputRef}
              placeholder={props.placeholder}
              value={text ?? (props.value as any)?.[props.textPropertyName]}
              onChange={(event) => {
                setText(event.target.value);
                props.onInputChanged(event.target.value);
              }}
              allowClear={props.allowClear}
              onKeyDown={handleKeyDown}
              onFocus={() => handleFocus()}
              onBlur={() => handleBlur()}
            />
            <Spin spinning={props.asyncPending === true} indicator={<LoadingOutlined style={{ fontSize: 20 }} spin />} className="autocomplete-loading ">
              {props.children}
            </Spin>
          </div>
        </Tooltip>
      </Dropdown>
    </>
  );
}

ImprovedAutoComplete.defaultProps = {
  additionVisibleCondition: true,
};

export default ImprovedAutoComplete;
