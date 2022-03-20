import { UserRole } from "./user-role.model";

export class UserProfileModel {
    userName: string;
    firstName: string;
    lastName: string;
    userEmail: string;
    role: UserRole;
    roles: string[];
    nameTitle: string;
    addressLine1: string;
    addressLine2: string;
    city: string;
    stateProvince: string;
    country: string;
    zipPostal: string;
    userPhoneNumber: string;
    phoneNumberBusiness: string;
    phoneNumberHome: string;
    phoneNumberCell: string;
    phoneNumberFax: string;
    picture: string;
    locationCountry: string;
    locationState: string;
    locationCity: string;
    description: string;
    rolesInfo: string[];
    tags: string[];
}