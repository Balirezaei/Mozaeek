import {
  ArrowDownOutlined,
  DeleteOutlined,
  EditOutlined,
  LinkOutlined,
  MinusSquareOutlined,
  MoreOutlined,
  OrderedListOutlined,
  PlusOutlined,
  PlusSquareOutlined,
  ReloadOutlined,
  TableOutlined,
  UploadOutlined,
} from '@ant-design/icons';
import { Button, Card, Form, Input, Modal, Popconfirm, Select, Space, Table, Tabs } from 'antd';
import { ColumnsType } from 'antd/lib/table/interface';
import CloneDeep from 'lodash/cloneDeep';
import Last from 'lodash/last';
import Range from 'lodash/range';
import remove from 'lodash/remove';
import React, { useEffect, useRef, useState } from 'react';
import { useTranslation } from 'react-i18next';
import { useDispatch } from 'react-redux';
import { useImmer } from 'use-immer';

import { useAntdValidation, useHttpCall, useManualRerender } from '../../../../../features/hooks';
import { Translations } from '../../../../../features/localization';
import { ApiResponse, CreaDitMode, GetAllRequestBase } from '../../../../../types';
import { findDeepParentChildrenArray } from '../../../../../utils/helpers';
import { CreateSynonymRq, SynonymItem } from '../../../../http/core/core-apiTypes';
import { AppListBaseResponse } from '../../../../mosaik';
import { ApiModule } from '../../../apiModule';
import { AppErrorAlert, FormItemIdHidden, PopconfirmDelete, getCreaditTitle, sharedSlice, successfulResponse, useAntdTable } from '../../../shared';
import SimpleParentChildTree from '../../components/SimpleParentChild/SimpleParentChildTree/SimpleParentChildTree';
import SynonymsSection from '../../components/SynonymsSection/SynonymsSection';

type FormValues = {
  id: number;
  title: string[];
};

type SimpleParentChildItem = {
  id: number;
  title: string;
  parentId?: number;
  hasChild?: boolean;
  children?: SimpleParentChildItem[];
};

type Props<TItem, TGetAllRs> = {
  apiModule: ApiModule;
  http: {
    getAllParents: (data: GetAllRequestBase) => Promise<ApiResponse<TGetAllRs>>;
    getAllChildren: (data: { id: number }) => Promise<ApiResponse<TGetAllRs>>;
    getById: (data: { id: number }) => Promise<ApiResponse<TItem>>;
    getInitDto: () => Promise<ApiResponse<any>>;
    create: (data: { title: string; parentId?: number }) => Promise<ApiResponse<any>>;
    update: (data: Exclude<TItem, 'hasChild'>) => Promise<ApiResponse<any>>;
    delete: (data: { id: number }) => Promise<ApiResponse<any>>;
    importExcel: (data: FormData) => Promise<ApiResponse<any>>;
    deleteAll?: () => Promise<ApiResponse<any>>;

    getAllSynonyms?: () => Promise<ApiResponse<SynonymItem[]>>;
    createSynonym?: (data: CreateSynonymRq) => Promise<ApiResponse<any>>;
    deleteSynonym?: (data: { id: number }) => Promise<ApiResponse<any>>;
  };
  translationKeys: {
    item: string;
    items: string;
  };
  icon: string;
  extra?: { [key: string]: any };
};
function SimpleParentChild<TItem extends SimpleParentChildItem, TGetAllRs extends AppListBaseResponse<TItem>>(props: Props<TItem, TGetAllRs>) {
  const { t } = useTranslation();
  const dispatch = useDispatch();

  const manualRerender = useManualRerender();
  const table = useAntdTable<TItem>(props.apiModule, { indexColumn: false, alignColumns: 'right' });

  const [tabActiveKey, setTabActiveKey] = useState<string>('table');
  const [request, setRequest] = useState<GetAllRequestBase>(table.requestData);
  const [currentItem, setCurrentItem] = useState<TItem>();
  const [items, updateItems] = useImmer<TGetAllRs | undefined>(undefined);
  const [creaditModalData, updateCreaditModalData] = useImmer<{ mode: CreaDitMode; visible: boolean; currentId?: number; inputNumbers: number }>({
    mode: 'Create',
    visible: false,
    inputNumbers: 1,
  });
  const [treeDataTime, setTreeDataTime] = useState<number>(Date.now());
  const [synonymItem, setSynonymItem] = useState<{ name: string; time: number }>();
  const [synonymsDataTime, setSynonymsDataTime] = useState<number>(Date.now());

  const getAllParentsApi = useHttpCall(props.http.getAllParents);
  const getAllChildrenApi = useHttpCall(props.http.getAllChildren);
  const getByIdApi = useHttpCall(props.http.getById);
  const getInitDtoApi = useHttpCall(props.http.getInitDto);
  const createApi = useHttpCall(props.http.create);
  const updateApi = useHttpCall(props.http.update);
  const deleteApi = useHttpCall(props.http.delete);
  const importExcelApi = useHttpCall(props.http.importExcel);
  const deleteAllApi = useHttpCall(props.http.deleteAll);

  const [form] = Form.useForm<FormValues>();
  const { labelWithRules } = useAntdValidation(form);

  const parentIdRef = useRef<number | undefined>();
  const parentIdToGetChildrenRef = useRef<number>();
  const inputFileExcelRef = useRef<HTMLInputElement>(null);
  const openItemsIdArrayRef = useRef<number[]>([]);

  table.setRequestDataChangeFn(setRequest);

  useEffect(() => {
    dispatch(
      sharedSlice.actions.setDisplayPath({
        title: t(props.translationKeys.items),
        breadcrumbs: [{ title: t(Translations.Core.BasicData) }],
        fontawesomeIcon: props.icon,
      })
    );
    return () => {
      dispatch(sharedSlice.actions.setDisplayPath(null));
    };
  }, [dispatch, props.icon, props.translationKeys.items, t]);

  useEffect(() => {
    getAllParents();

    openItemsIdArrayRef.current.forEach((item) => {
      getAllChildren(item);
    });

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [request, manualRerender.rerenderData]);

  useEffect(() => {
    const fieldInstance = form.getFieldInstance(['title', creaditModalData.inputNumbers - 1]);
    fieldInstance?.focus();
  }, [creaditModalData.inputNumbers, form]);

  useEffect(() => {
    if (creaditModalData.currentId) {
      getByIdApi.call<TItem>({ id: creaditModalData.currentId }).then((response) => {
        if (successfulResponse(response)) {
          setCurrentItem(response!.data);
          if (creaditModalData.mode === 'Edit') {
            form.setFieldsValue({
              id: response!.data.id,
              title: [response!.data.title],
            });
          } else if (creaditModalData.mode === 'AddSub') {
            parentIdRef.current = creaditModalData.currentId;
          }
        }
      });
    }

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [creaditModalData.currentId, creaditModalData.mode, form]);

  useEffect(() => {
    if (items) {
      table.setData(items);
    }

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [items]);

  const getAllParents = () => {
    deleteApi.reset();
    importExcelApi.reset();
    getAllChildrenApi.reset();

    getAllParentsApi.call<TGetAllRs>({ ...request }).then((response) => {
      if (successfulResponse(response)) {
        const result = CloneDeep(response!.data);
        // result.list.forEach((f) => {
        //   f.children = [];
        //   f.children.push({ id: f.id + 10000, title: 'AAA', parentId: f.parentId });
        // });
        updateItems(result);
      }
    });
  };

  const handleAddClicked = () => {
    updateCreaditModalData((draft) => {
      draft.mode = 'Create';
      draft.visible = true;
    });
  };

  const handleSubmit = (values: FormValues) => {
    switch (creaditModalData.mode) {
      case 'AddSub':
      case 'Create':
        createApi.call<number>({ parentId: parentIdRef.current, title: Last(values.title)! }).then((response) => {
          if (successfulResponse(response)) {
            parentIdRef.current = response!.data;
            updateCreaditModalData((draft) => {
              draft.inputNumbers = draft.inputNumbers + 1;
            });
            manualRerender.needRerenderRef.current = true;
          }
        });
        break;
      case 'Edit':
        //@ts-ignore
        updateApi.call<TItem>({ id: values.id, title: Last(values.title)!, parentId: currentItem!.parentId }).then((response) => {
          if (successfulResponse(response)) {
            manualRerender.needRerenderRef.current = true;
            handleCloseModal();
          }
        });
        break;
    }
  };

  const handleCloseModal = () => {
    manualRerender.rerenderOnNeed();

    updateCreaditModalData((draft) => {
      draft.visible = false;
      draft.currentId = undefined;
      draft.inputNumbers = 1;

      createApi.reset();
      updateApi.reset();
    });

    form.resetFields();
    parentIdRef.current = undefined;
  };

  const handleModalOkClicked = () => {
    if (creaditModalData.inputNumbers > 1) {
      handleCloseModal();
    } else {
      form.submit();
    }
  };

  const handleCreaditInputPressEnter = (e: React.KeyboardEvent<HTMLInputElement>) => {
    e.preventDefault();
    form.submit();
  };

  const handleDeleteClicked = (id: number) => {
    deleteApi.call({ id: id }).then((response) => {
      if (response && response.data) {
        getAllParents();
      }
    });
  };

  const handleAddSubClicked = (id: number) => {
    updateCreaditModalData((draft) => {
      draft.visible = true;
      draft.currentId = id;
      draft.mode = 'AddSub';
    });
  };

  const handleEditClicked = (id: number) => {
    updateCreaditModalData((draft) => {
      draft.visible = true;
      draft.currentId = id;
      draft.mode = 'Edit';
    });
  };

  const columns: ColumnsType<TItem> = [
    {
      title: t(Translations.Common.Title),
      dataIndex: 'title',
      key: 'title',
    },
    {
      key: 'actions',
      dataIndex: 'actions',
      width: '20%',
      render: (value, record) => {
        return (
          <Space>
            <PopconfirmDelete itemName={t(props.translationKeys.item)} pending={deleteApi.pending} data={record.id} onDelete={handleDeleteClicked} />
            <Button htmlType="button" size="small" type="primary" icon={<EditOutlined />} className="antd-gold6-btn" onClick={() => handleEditClicked(record.id)} />
            <Button htmlType="button" size="small" type="default" icon={<OrderedListOutlined />} onClick={() => handleAddSubClicked(record.id)} />
            <Button
              htmlType="button"
              size="small"
              type="default"
              icon={<LinkOutlined />}
              onClick={() => setSynonymItem({ name: record.title, time: Date.now() })}
              title={t(Translations.Core.Synonym)}
            />
            {props.apiModule === ApiModule.RequestOrganizations && (
              <Button
                htmlType="button"
                size="small"
                type="default"
                icon={<span className="fa fa-globe" />}
                onClick={() => props.extra!.openDefiniteRequestOrgModal(record.id)}
              />
            )}
          </Space>
        );
      },
    },
  ];
  table.configColumns(columns);

  const handleExpand = (expanded: boolean, record: TItem) => {
    if (expanded) {
      parentIdToGetChildrenRef.current = record.id;
      openItemsIdArrayRef.current.push(record.id);
      getAllChildren(record.id);
    } else {
      remove(openItemsIdArrayRef.current, (item) => item === record.id);
    }
  };

  const getAllChildren = (parentId: number) => {
    getAllChildrenApi.call<TGetAllRs>({ id: parentId }).then((response) => {
      if (successfulResponse(response)) {
        updateItems((draft) => {
          const item = findDeepParentChildrenArray<SimpleParentChildItem>(draft!.list, 'id', parentId);
          if (item) {
            item.children = [];
            for (const it of response!.data.list) {
              item.children.push({ ...it });
            }
          }
        });
      }
    });
  };

  const handleRefresh = () => {
    switch (tabActiveKey) {
      case 'table':
        manualRerender.rerender();
        break;
      case 'tree':
        setTreeDataTime(Date.now());
        break;
      case 'synonyms':
        setSynonymsDataTime(Date.now());
        break;
    }
  };

  const handleImportExcelClicked = () => {
    inputFileExcelRef.current?.click();
  };

  const handleExcelUpload = () => {
    if (inputFileExcelRef.current?.files && inputFileExcelRef.current?.files.length > 0) {
      const formData: FormData = new FormData();
      formData.append('file', inputFileExcelRef.current.files[0]);
      importExcelApi.call(formData).then((response) => {
        if (successfulResponse(response)) {
          manualRerender.rerender();
        }
      });
    }
  };

  const handleDeleteAllClicked = () => {
    deleteAllApi.call(undefined).then((response) => {
      if (successfulResponse(response)) {
        manualRerender.rerender();
      }
    });
  };

  return (
    <>
      {props.apiModule === 'RequestOrganizations' ? (
        <Modal>
          <Select />
        </Modal>
      ) : (
        <></>
      )}
      <input ref={inputFileExcelRef} type="file" style={{ display: 'none' }} onChange={handleExcelUpload} />
      <Modal
        forceRender
        footer={null}
        title={getCreaditTitle(t, creaditModalData.mode, t(props.translationKeys.item), currentItem?.title, getByIdApi.pending)}
        visible={creaditModalData.visible}
        onCancel={handleCloseModal}>
        <AppErrorAlert error={createApi.error || updateApi.error} />
        <Form form={form} labelCol={{ span: 4 }} onFinish={handleSubmit}>
          {creaditModalData.currentId && creaditModalData.mode === 'Edit' && <FormItemIdHidden id={creaditModalData.currentId} />}
          {Range(creaditModalData.inputNumbers).map((item, index) => (
            <React.Fragment key={index}>
              <Form.Item
                name={['title', item]}
                className="mb-1"
                {...labelWithRules({ label: t(Translations.Common.Title), rules: index === creaditModalData.inputNumbers - 1 ? [{ type: 'Required' }] : [] })}>
                <Input
                  disabled={getInitDtoApi.pending || getByIdApi.pending || index !== creaditModalData.inputNumbers - 1}
                  onPressEnter={handleCreaditInputPressEnter}
                />
              </Form.Item>
              {index !== creaditModalData.inputNumbers - 1 && (
                <div className="text-right mb-1">
                  <ArrowDownOutlined />
                </div>
              )}
            </React.Fragment>
          ))}
          <Button
            type="primary"
            htmlType="button"
            onClick={handleModalOkClicked}
            loading={updateApi.pending || createApi.pending}
            disabled={getByIdApi.pending}
            className="mt-2">
            {t(Translations.Common.Ok)}
          </Button>
        </Form>
      </Modal>

      <Card>
        <AppErrorAlert error={getAllParentsApi.error || getAllChildrenApi.error || importExcelApi.error} disableAutoHide />
        <AppErrorAlert error={deleteApi.error} />
        <Tabs
          tabBarExtraContent={
            <Space>
              <Button type="primary" className="antd-purple5-btn" icon={<UploadOutlined />} onClick={handleImportExcelClicked} loading={importExcelApi.pending}>
                {t(Translations.Common.ImportExcel)}
              </Button>
              {tabActiveKey === 'table' && (
                <Button type="primary" htmlType="button" onClick={handleAddClicked} icon={<PlusOutlined />}>
                  {t(Translations.Common.Create)}
                </Button>
              )}
              <Button type="default" htmlType="button" icon={<ReloadOutlined />} onClick={handleRefresh}>
                {t(Translations.Common.Refresh)}
              </Button>
              {props.http.deleteAll && (
                <Popconfirm
                  title={t(Translations.Common.AreYouSureDeleteTheEntireRecords)}
                  okType="primary"
                  okButtonProps={{ danger: true }}
                  onConfirm={handleDeleteAllClicked}>
                  <Button type="primary" danger icon={<DeleteOutlined />}>
                    {t(Translations.Common.DeleteAll)}
                  </Button>
                </Popconfirm>
              )}
            </Space>
          }
          activeKey={tabActiveKey}
          onChange={setTabActiveKey}>
          <Tabs.TabPane
            key="table"
            tab={
              <span>
                <TableOutlined />
                {t(Translations.Common.TableView)}
              </span>
            }>
            <Table
              {...table.tableProps}
              size="small"
              loading={getAllParentsApi.pending || getAllChildrenApi.pending || deleteAllApi.pending}
              columns={columns}
              expandable={{
                indentSize: 30,
                onExpand: handleExpand,
                expandIcon: ({ expanded, onExpand, record }) => {
                  if (record.hasChild === false) {
                    return <MinusSquareOutlined className="invisible pr-5" />;
                  }
                  return expanded ? (
                    <MinusSquareOutlined onClick={(e) => onExpand(record, e)} className="font-size-h6 pr-5" />
                  ) : (
                    <PlusSquareOutlined onClick={(e) => onExpand(record, e)} className="font-size-h6 pr-5" />
                  );
                },
              }}
            />
          </Tabs.TabPane>
          <Tabs.TabPane
            key="tree"
            tab={
              <span>
                <MoreOutlined />
                {t(Translations.Common.TreeView)}
              </span>
            }>
            <SimpleParentChildTree<TItem, TGetAllRs>
              http={{
                getAllParents: props.http.getAllParents,
                getAllChildren: props.http.getAllChildren,
                delete: props.http.delete,
              }}
              translationKeys={{
                item: Translations.Core.Label,
                items: Translations.Core.Labels,
              }}
              refreshTime={treeDataTime}
            />
          </Tabs.TabPane>
          {props.http.getAllSynonyms && (
            <Tabs.TabPane
              forceRender
              key="synonyms"
              tab={
                <span>
                  <LinkOutlined />
                  {t(Translations.Core.Synonym)}
                </span>
              }>
              <SynonymsSection
                moduleName={t(props.translationKeys.item)}
                item={synonymItem}
                refreshTime={synonymsDataTime}
                http={{
                  getAllSynonyms: props.http.getAllSynonyms!,
                  createSynonym: props.http.createSynonym!,
                  deleteSynonym: props.http.deleteSynonym!,
                }}
              />
            </Tabs.TabPane>
          )}
        </Tabs>
      </Card>
    </>
  );
}

export default React.memo(SimpleParentChild) as typeof SimpleParentChild;
