import { createSlice } from "@reduxjs/toolkit";
import { loginUser } from "../thunk/jwtVerify";
import { encrypt } from "../../utils/encryptionUtils";



export interface  UserData {
    Email: string;
    DisplayName: string;
    UserName: string;
    CreatedAt: string;
}

export interface DecodedToken {
    exp: number;
    UserData: UserData;
    IssuedAt: number;
    jti: string
    "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier": string;
}

interface LoginState {
    jwtPayload: DecodedToken | null;
    loading : boolean;
    error: string | null;
    jwt: string | null;
}

const initialState : LoginState = {
    jwtPayload: null,
    loading: false,
    error: null,
    jwt: null
}

const loginSlice = createSlice ({
    name: 'loginUser',
    initialState,
    reducers: {
        resetJwt: (state) => {
            state.jwt = null;
        },
        setJwtPayload: (state, action) => {
            state.jwtPayload = action.payload
        }
    },
    extraReducers: (builder) => {
        builder
            .addCase(loginUser.pending, (state) => {
                state.loading = true;
                state.error = null;
            })
            .addCase(loginUser.fulfilled, (state, action) => {
                state.loading = false;
                state.error = null;
                state.jwt = encrypt(action.payload);
                localStorage.setItem('jwtToken', action.payload);
            })
            .addCase(loginUser.rejected, (state, action) => {
                state.loading = false;
                state.error= action.payload as string;
            });
    },
});

export const {resetJwt, setJwtPayload} = loginSlice.actions;
export default loginSlice.reducer;