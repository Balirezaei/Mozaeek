/// <reference types="react-scripts" />
declare namespace NodeJS {
  export interface ProcessEnv {
    REACT_APP_Env: string;
    REACT_APP_AuthenticationApiBaseUrl: string;
    REACT_APP_CoreApiBaseUrl: string;
  }
}
