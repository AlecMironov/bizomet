export interface ContactUsRequestModel {
    userId: string;
    reason: string;
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    description: string;
    captcha: string;
}