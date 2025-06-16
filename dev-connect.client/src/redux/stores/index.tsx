import { combineReducers, configureStore } from "@reduxjs/toolkit";
import { persistSlice } from "../persistenceUtils";
import persistStore from "redux-persist/es/persistStore";
import { encrypt } from "../../utils/encryptionUtils";
import loginUserSlice from "../slices/loginUser";
import userSlice from "../slices/users";


const rootReducer = combineReducers({
    loginUser: persistSlice(loginUserSlice, {sliceKey: "loginUser"}),
    users: persistSlice(userSlice, {sliceKey: "user"})
})



export const store = configureStore({
    reducer: rootReducer,
    middleware: (getDefaultMiddleWare) =>
        getDefaultMiddleWare({
            serializableCheck: {
                ignoredActions: ["persist/PERSIST"],
                ignoredPaths: ["login.jwt"],
            },
        }).concat(() => (next) => (action: any) => {
            const result = next(action);
            if(action.type === "loginUser/fulfilled") {
                const jwt = action.payload;
                localStorage.setItem("jwtToken", encrypt(jwt))
            }
            return result;
        })
})

export type RootState = ReturnType<typeof rootReducer>;
export type AppDispatch = typeof store.dispatch;
export const persistor = persistStore(store);