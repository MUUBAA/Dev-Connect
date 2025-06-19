import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from "axios";


export interface UserCreateParams {
    username: string;
    email: string;
    password: string;
};

export const loginUser = createAsyncThunk<string, { userName: string; password: string }>('loginUser', async ({ userName, password }, { rejectWithValue }) => {
  try {
    const response = await axios.post(`/auth/login?userName=${userName}&password=${password}`, {
      headers: {
        'Content-Type': 'application/json',
      },

    });
    return response.data;
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
  } catch (error: any) {
    return rejectWithValue(error.response.data.message || 'Login failed');
  }
}
);
export const registerUser = createAsyncThunk<string, UserCreateParams>(
  'registerUser',
  async (userData, { rejectWithValue }) => {
    try {
      const response = await axios.post(
        '/auth/register',
        userData,
        {
          headers: {
            'Content-Type': 'application/json-patch+json',
            'accept': 'text/plain',
          },
        }
      );
      return response.data;
    } catch (error: any) {
      return rejectWithValue(error.response?.data?.message || 'Registration failed');
    }
  }
);

export interface EmailVerificationToken {
  token: string;
}

export const EmailVerification  = createAsyncThunk<string,  EmailVerificationToken>(
  "/auth/verifyEmail",
  async (userData, {rejectWithValue}) => {
    try {
      const response = await axios.get(
        `/auth/verify-email?token=${encodeURIComponent(userData.token)}`,
        {
          headers: {
             'Content-Type': 'application/json-patch+json',
             'accept': 'text/plain',
          }
        }
      );
      return response.data;
    } catch (error: any)
    {
      return rejectWithValue(error.response?.data?.message)
    }
  }
)

export interface ForgotPasswordMail {
  email : string
}

export const ForgotPassword = createAsyncThunk<any, ForgotPasswordMail>(
  '/auth/forgotPassword',
  async (userData, { rejectWithValue }) => {
    try {
      const response = await axios.post(
        `/auth/forgot-password?email=${encodeURIComponent(userData.email)}`,
        {},
        {
          headers: {
            'accept': '*/*',
          }
        }
      );
      return response.data;
    } catch (error: any) {
      return rejectWithValue(error.response?.data?.message);
    }
  }
);
