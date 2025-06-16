import { createAsyncThunk } from "@reduxjs/toolkit";
import { getDecryptJwt } from "../../utils/auth";
import axios from "axios";
import { error } from "console";


export interface UserCreateParams {
    name: string;
    email: string;
    password: string;
}

export const UserCreate = createAsyncThunk<any, UserCreateParams>(
    "User/Create",
    async (payload : UserCreateParams, { rejectWithValue}) => {
        try {
            const token = localStorage.getItem("jwtToken");

            if(!token)
            {
                return rejectWithValue("No token found");
            }

            const decodedToken = getDecryptJwt();
            if(!decodedToken)
            {
                return rejectWithValue("Invalid token");
            }

            const response = await axios.post("/user/create", payload, {
                headers: {
                    Authorization: `Bearer ${decodedToken}`,
                    "Content-Type": "application/json-patch-json",
                    Accept: "*/*",
                },
            });

            return response.data
        }catch (error: any)
        {
            return rejectWithValue(
                error.response?.data?.message || "User Creation failed"
            );
        }
    }
);



export interface UserGetByUserNameParams {
    username : string;
};
export const UserGetByUserName = createAsyncThunk<any, UserGetByUserNameParams>(
    "User/GetByUserName",
    async (payload: UserGetByUserNameParams, {rejectWithValue}) => {
        try {
            const token = localStorage.getItem("jwtToken");

            if(!token) {
                return rejectWithValue("No token found");
            }

            const decodedToken = getDecryptJwt();
            if (!decodedToken) {
                return rejectWithValue("Invalid token");
            }

            const response = await axios.get(
                `/user/getUserByUserName?username=${encodeURIComponent(payload.username)}`,
                {
                    headers: {
                        Authorization: `Bearer ${decodedToken}`,
                        Accept: "*/*",
                    },

                }
            );

        return response.data;
        } catch (error: any) {
            return rejectWithValue(
                error.response?.data?.message || "Failed to fetch user by username"
            )
        }
    }
)