import PropTypes from 'prop-types';
import React from 'react';
const TestChild: React.VFC<Props> = () => {
  return <></>;
};

type Props = {
  isActive: boolean;
};

TestChild.propTypes = {
  isActive: PropTypes.bool.isRequired,
};

export default TestChild;
