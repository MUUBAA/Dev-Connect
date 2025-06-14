import { createAsyncThunk } from "@reduxjs/toolkit";
import axios from 'axios';



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