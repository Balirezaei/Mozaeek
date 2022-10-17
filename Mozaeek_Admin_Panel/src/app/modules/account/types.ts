export type GetCurrentProfileResponse = Partial<{
  userId: number;
  firstname: string;
  lastname: string;
  userName: string;
  emailAddress: string;
  phoneNumber: string;
  isPhoneNumberConfirmed: boolean;
  isEmailConfirmed: boolean;
  timezone: string;
  isActive: boolean;
  gender: boolean;
  birthDay: Date;
  isNewsletter: boolean;
  nationalityId: string;
}>;

export type UpdateProfileSagaPayload = {
  firstname: string;
  lastname: string;
  gender: boolean;
  birthDay?: Date;
  phoneNumber: string;
  timezone: string;
  nationalityId: string;
};
