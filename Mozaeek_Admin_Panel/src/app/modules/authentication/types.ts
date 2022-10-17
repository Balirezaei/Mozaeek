export type TokenAuthLoginResponse = {
  accessToken: string;
  refreshToken: string;
  encryptedAccessToken: string;
  expireInSeconds: number;
  user: {
    id: number;
    userName: string;
    roleNames: string[];
  };
};

export type LoginPayload = {
  time: Date;
};
