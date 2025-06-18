import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from 'axios';


export interface UserCreateParams {
    username: string;
    email: string;
    password: string;
};

export const loginUser = createAsyncThunk<any, { userName: string; password: string }>('loginUser', async ({ userName, password }, { rejectWithValue }) => {
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
export const registerUser = createAsyncThunk<any, UserCreateParams>(
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

export const EmailVerification  = createAsyncThunk<any,  EmailVerificationToken>(
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