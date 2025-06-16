import { createSlice, type PayloadAction } from "@reduxjs/toolkit";
import type { UserData } from "./loginUser";
import { UserCreate, UserGetByUserName } from "../thunk/user";


interface User {
    id: number;
    displayName: string;
    email: string;
    passwordHash: string;
    status: string;
    createdAt: string;
    createdBy: string;
    updatedAt: string;
    updatedBy: string;
    isDeleted: boolean;
    deletedAt: string;
    deletedBy: string;
    image: string;
}

export interface UserResponse {
    id: number;
    name: string;
    email: string;
    username: string;
    passwordHash: string;
    emailVerifiedAt: string;
    status: number;
    createdAt: string;
    createdBy: string;
    image: string;
}

interface UserState {
    userProfile: UserData | null;
    userAccount: UserResponse | null;
    users: User[];
    loading: boolean;
    error: string | null;
}

const initialState : UserState = {
    userProfile: null,
    userAccount: null,
    users: [],
    loading: false,
    error: null
}

const userSlice = createSlice({
    name: 'user',
    initialState,
    reducers: {
        setUserProfile: (state, action: PayloadAction<UserData>) => {
            state.userProfile = action.payload;
        },
        resetUserProfile: (state) => {
            state.userProfile = null;
        },

    },
    extraReducers: (builder) => {
        builder
        .addCase(UserCreate.pending, (state) => {
            state.loading = true;
        })
        .addCase(UserCreate.fulfilled, (state) => {
            state.loading = false;
        })
        .addCase(UserCreate.rejected, (state, action) =>{
            state.loading = false;
            state.error = action.payload as string;
     })
     .addCase(UserGetByUserName.pending, (state) => {
        state.loading = true;
     })
     .addCase(UserGetByUserName.fulfilled, (state, action) => {
        state.loading = false;
        state.userAccount = action.payload.data
     })
     .addCase(UserGetByUserName.rejected, (state, action) => {
        state.loading = false;
        state.error = action.payload as string;
     })
    }
})

export const {setUserProfile, resetUserProfile} = userSlice.actions;
export default userSlice.reducer;