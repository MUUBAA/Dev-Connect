import { createSlice } from "@reduxjs/toolkit"
import { EmailVerification, registerUser } from "../thunk/jwtVerify"




export interface userData {
    Email: string,
    UserName: string,
    Name: string,
    Password: string,
    CreatedAt: string
}

export interface RegistrationState {
    loading: boolean,
    error: string | null,
    success : boolean | null
}

const initialState : RegistrationState  = {
    loading: false,
    error: null,
    success: false
}

const RegistrationSlice = createSlice({
    name: "registration",
    initialState,
    reducers: {},
    extraReducers :(builder) => {
        builder
        .addCase(registerUser.pending, (state) => {
            state.loading = true
            state.error = null
        })
        .addCase(registerUser.fulfilled, (state) => {
            state.loading = false
            state.error = null
        })
        .addCase(registerUser.rejected, (state, action) => {
            state.loading = false
            state.error = action.payload as string
        })
        .addCase(EmailVerification.pending, (state) => {
            state.loading = true;
            state.error = null;
            state.success = false;
        })
        .addCase(EmailVerification.fulfilled, (state) => {
            state.loading = false;
            state.error = null;
            state.success = true;
        })
        .addCase(EmailVerification.rejected, (state, action) => {
            state.loading = false;
            state.success = false;
            state.error = action.payload as string;
        })
    }
})

export default RegistrationSlice.reducer;