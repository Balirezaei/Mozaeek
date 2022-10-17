import { AppResponse } from '../../../../types';

export const successfulResponse = (response: AppResponse | undefined) => {
  //@ts-ignore
  return response && (response.error === undefined || response.error === null) && response.errors === undefined && response.traceId === undefined;
};
