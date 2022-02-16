export interface AuthResponseModel {
  userName: string;
  email: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  roles: string[];
  token: string;
  refreshToken: string;
  picture: string;
}
