export interface AuthResponseModel {
  isAuthSuccessful: boolean;
  errorMessage: string;
  id: string;
  userName: string;
  firstName: string;
  lastName: string;
  roles: string[];
  token: string;
  refreshToken: string;
}
