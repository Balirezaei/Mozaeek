import { useState } from 'react';

export type ModalOperations = ReturnType<typeof useModalOperations>;

const useModalOperations = () => {
  const [visible, setVisible] = useState<boolean>(false);

  const showModal = () => {
    setVisible(true);
  };

  const closeModal = () => {
    setVisible(false);
  };

  return { visible, showModal, closeModal };
};

export default useModalOperations;
