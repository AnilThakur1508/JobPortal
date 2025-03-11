export interface RegisterModel {
    firstName: string;
    lastName: string;
    role: string;
    email: string;
    phoneNumber: string;
    profileImage: File | null;
    password: string;
    confirmPassword: string;
  }
  