import { Button } from 'antd';
import React from 'react';
import { useDispatch } from 'react-redux';

import { LocalStorageKey } from '../../../../../features/constants';
import { removeStorage } from '../../../../../utils/helpers';
import { authenticationSlice } from '../../../authentication';

// const columns = [
//   {
//     title: 'Name',
//     dataIndex: 'name',
//     key: 'name',
//   },
//   {
//     title: 'Age',
//     dataIndex: 'age',
//     key: 'age',
//     width: '12%',
//   },
//   {
//     title: 'Address',
//     dataIndex: 'address',
//     width: '30%',
//     key: 'address',
//   },
// ];
//
// const data = [
//   {
//     key: 1,
//     name: 'یک',
//     age: 60,
//     address: 'New York No. 1 Lake Park',
//     children: [
//       {
//         key: 11,
//         name: 'یک یک',
//         age: 42,
//         address: 'New York No. 2 Lake Park',
//       },
//       {
//         key: 12,
//         name: 'یک دو',
//         children: [
//           {
//             key: 121,
//             name: 'دو یک',
//             age: 16,
//             address: 'New York No. 3 Lake Park',
//           },
//         ],
//       },
//     ],
//   },
// ];
//
// console.log(toJson(data));

const TestC: React.VFC = () => {
  const dispatch = useDispatch();

  return (
    <>
      <Button
        onClick={() => {
          dispatch(authenticationSlice.actions.clearAuthToken());
        }}>
        Remove Auth Token
      </Button>
    </>
  );
};

export default TestC;
