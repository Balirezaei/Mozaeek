import React from 'react';
import { Col, Row } from 'react-bootstrap';
import Button from 'react-bootstrap/Button';
import { useTranslation } from 'react-i18next';
import { useDispatch } from 'react-redux';
import { Link } from 'react-router-dom';

import imageError from '../../../../../assets/Error.svg';
import { useMount } from '../../../../../features/hooks';
import { Translations } from '../../../../../features/localization';
import { sharedSlice } from '../../index';

const RenderError: React.VFC = () => {
  const { t } = useTranslation();
  const dispatch = useDispatch();

  useMount(() => {
    dispatch(sharedSlice.actions.setDisplayPath(null));
  });

  return (
    <Row className="justify-content-md-center main-height-full">
      <Col md={{ span: 5 }} className="text-center my-auto">
        <img src={imageError} alt="unauthorized" className="mb-2" />
        <p className="mb-9 text-purple">{t(Translations.Common.WeAreWorkingToSolveTheProblemSorryForTheInconvenience)}</p>
        <Link to="/">
          <Button variant="primary" className="min-width-md">
            <span className="fas fa-home mx-2" />
            <span className="mx-2">{t(Translations.Common.BackToHome)}</span>
          </Button>
        </Link>
      </Col>
    </Row>
  );
};
export default RenderError;
