import { DeleteRowOutlined } from '@ant-design/icons';
import { Dropdown, Menu, Modal, Skeleton, Tree } from 'antd';
import Range from 'lodash/range';
import { DataNode, EventDataNode } from 'rc-tree/lib/interface';
import React, { useCallback, useEffect, useState } from 'react';
import { useTranslation } from 'react-i18next';

import { useHttpCall } from '../../../../../../features/hooks';
import { Translations } from '../../../../../../features/localization';
import { ApiResponse, GetAllRequestBase } from '../../../../../../types';
import { AppListBaseResponse } from '../../../../../mosaik';
import { AppErrorAlert, successfulResponse } from '../../../../shared';

// eslint-disable-next-line @typescript-eslint/no-unused-vars
type Props<TItem, TGetAllRs> = {
  http: {
    getAllParents: (data: GetAllRequestBase) => Promise<ApiResponse<TGetAllRs>>;
    getAllChildren: (data: { id: number }) => Promise<ApiResponse<TGetAllRs>>;
    delete: (data: { id: number }) => Promise<ApiResponse<any>>;
  };
  translationKeys: {
    item: string;
    items: string;
  };
  refreshTime: number;
};
function SimpleParentChildTree<
  TItem extends { id: number; title: string; parentId?: number; hasChild?: boolean },
  TGetAllRs extends AppListBaseResponse<TItem>
>(props: Props<TItem, TGetAllRs>) {
  const { t } = useTranslation();

  const [treeData, setTreeData] = useState<DataNode[]>();
  const [treeDataPending, setTreeDataPending] = useState<boolean>();
  const [deleteModalData, setDeleteModalData] = useState<{ visible: boolean; itemId: number; itemName: string }>();
  const [refreshTime, setRefreshTime] = useState<number>(props.refreshTime);

  const getAllParentsApi = useHttpCall(props.http.getAllParents);
  const getAllChildrenApi = useHttpCall(props.http.getAllChildren);
  const deleteApi = useHttpCall(props.http.delete);

  const handleDeleteClicked = (itemId: number, itemName: string) => {
    setDeleteModalData({ visible: true, itemId: itemId, itemName: itemName });
  };

  const treeContextMenu = (item: TItem) => {
    return (
      <Menu>
        {/*<Menu.Item key="1" icon={<EditOutlined />} className="text-warning">*/}
        {/*  {t(Translations.Common.Edit)}*/}
        {/*</Menu.Item>*/}
        <Menu.Item key="2" icon={<DeleteRowOutlined />} danger onClick={() => handleDeleteClicked(item.id, item.title)}>
          {t(Translations.Common.Delete)}
        </Menu.Item>
        {/*<Menu.Item key="3">{t(Translations.Common.AddSubsidiary)}</Menu.Item>*/}
      </Menu>
    );
  };

  const updateTreeData = (list: DataNode[], key: React.Key, children: DataNode[]): DataNode[] =>
    list.map((node) => {
      if (node.key === key) {
        return {
          ...node,
          children,
        };
      } else if (node.children) {
        return {
          ...node,
          children: updateTreeData(node.children, key, children),
        };
      }
      return node;
    });

  const handleTreeLoadData = (treeNode: EventDataNode) => {
    return new Promise<void>((resolve, reject) => {
      if (treeNode.children) {
        resolve();
        return;
      }
      getAllChildrenApi.call<TGetAllRs>({ id: treeNode.key as number }).then((response) => {
        if (successfulResponse(response)) {
          const dataNodes = response!.data.list.map(
            (item) =>
              ({
                key: item.id,
                title: (
                  <Dropdown overlay={() => treeContextMenu(item)} trigger={['contextMenu']}>
                    <span>{item.title}</span>
                  </Dropdown>
                ),
                isLeaf: !item.hasChild,
              } as DataNode)
          );
          setTreeData((state) => updateTreeData(state!, treeNode.key, dataNodes));
          resolve();
        } else {
          reject();
        }
      });
    });
  };

  const handleCancelDeleteModal = () => {
    setDeleteModalData(undefined);
    deleteApi.reset();
  };

  const handleDeleteItem = useCallback(() => {
    deleteApi.call({ id: deleteModalData!.itemId }).then((response) => {
      if (successfulResponse(response)) {
        setDeleteModalData(undefined);
      }
    });

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [deleteModalData?.itemId, deleteModalData?.visible, deleteModalData?.itemName]);

  useEffect(() => {
    setRefreshTime(props.refreshTime);
  }, [props.refreshTime]);

  useEffect(() => {
    setTreeDataPending(true);

    getAllParentsApi.call<TGetAllRs>({ PageNumber: 1, PageSize: 10000000 }).then((response) => {
      if (successfulResponse(response)) {
        const dataNodes = response!.data.list.map(
          (item) =>
            ({
              key: item.id,
              title: (
                <Dropdown overlay={() => treeContextMenu(item)} trigger={['contextMenu']}>
                  <span>{item.title}</span>
                </Dropdown>
              ),
              isLeaf: !item.hasChild,
            } as DataNode)
        );
        setTreeData(dataNodes);
        setTreeDataPending(false);
      }
    });

    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [refreshTime]);

  return (
    <>
      <Modal
        title={t(Translations.Common.AreYouSureDeleteThisItemVar, { item: t(props.translationKeys.item) })}
        visible={deleteModalData?.visible}
        okText={t(Translations.Common.Yes)}
        okButtonProps={{ danger: true, className: 'mx-2', style: { width: 80 } }}
        onOk={handleDeleteItem}
        cancelText={t(Translations.Common.No)}
        onCancel={handleCancelDeleteModal}
        closable={false}>
        <AppErrorAlert error={deleteApi.error} disableAutoHide />
        {t(Translations.Common.Name)} : <span className="font-weight-bold">{deleteModalData?.itemName}</span>
        <br />
        {t(Translations.Common.Code)} : <span className="font-weight-bold">{deleteModalData?.itemId}</span>
      </Modal>
      <div className="min-h-50px">
        {treeDataPending ? (
          Range(15).map((item) => <Skeleton.Input active size="small" className="row my-1" style={{ width: 200 }} key={item} />)
        ) : (
          <Tree treeData={treeData} showIcon loadData={handleTreeLoadData} />
        )}
      </div>
    </>
  );
}

export default React.memo(SimpleParentChildTree) as typeof SimpleParentChildTree;
