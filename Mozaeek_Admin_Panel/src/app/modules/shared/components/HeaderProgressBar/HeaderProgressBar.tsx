import React, { useEffect, useRef, useState } from 'react';
import { ProgressBar } from 'react-bootstrap';
import { useLocation } from 'react-router-dom';

const HeaderProgressBar: React.VFC = () => {
  const location = useLocation();

  const widthRef = useRef(0);
  const [width, setWidth] = useState(0);
  const animateTimeout = useRef<ReturnType<typeof setTimeout>>();
  const stopAnimateTimeout = useRef<ReturnType<typeof setTimeout>>();

  const animate = () => {
    animateTimeout.current = setTimeout(() => {
      if (widthRef.current <= 100) {
        widthRef.current = widthRef.current + 10;
        setWidth(widthRef.current);
        animate();
      } else {
        stopAnimate();
      }
    }, 30);
  };

  const stopAnimate = () => {
    clearTimeout(animateTimeout.current!);
    stopAnimateTimeout.current = setTimeout(() => {
      widthRef.current = 0;
      setWidth(widthRef.current);
    }, 300);
  };

  useEffect(() => {
    if (!location.pathname.startsWith('auth')) {
      animate();
    }
    return () => {
      clearTimeout(animateTimeout.current!);
    };
    // eslint-disable-next-line react-hooks/exhaustive-deps
  }, [location.pathname]);

  useEffect(() => {
    if (animateTimeout.current) {
      clearTimeout(animateTimeout.current);
    }
    if (stopAnimateTimeout.current) {
      clearTimeout(stopAnimateTimeout.current);
    }
  }, []);
  return (
    <div className="header-progress-bar" style={{ height: '3px', width: '100%' }}>
      {width > 0 && <ProgressBar variant="info" now={width} style={{ height: '3px' }} />}
    </div>
  );
};

export default HeaderProgressBar;
