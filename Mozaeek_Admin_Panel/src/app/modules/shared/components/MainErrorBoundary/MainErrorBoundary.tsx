import React from 'react';

import RenderError from '../../pages/RenderError/RenderError';

class MainErrorBoundary extends React.Component<any, any> {
  constructor(props: any) {
    super(props);
    this.state = { hasError: false };
  }

  static getDerivedStateFromError(error: any) {
    // Update state so the next render will show the fallback UI.
    return { hasError: true };
  }

  componentDidCatch(error: Error, errorInfo: React.ErrorInfo): void {
    // You can also log the error to an error reporting service
    //logErrorToMyService(error, errorInfo);
  }
  render() {
    if (this.state.hasError) {
      return <RenderError />;
    }

    return this.props.children;
  }
}

export default MainErrorBoundary;
