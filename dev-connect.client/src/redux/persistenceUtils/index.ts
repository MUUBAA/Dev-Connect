import type { Reducer } from "@reduxjs/toolkit";
import { persistReducer } from 'redux-persist';
import type { PersistConfig } from 'redux-persist';
import storage from 'redux-persist/lib/storage';
import { FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER } from 'redux-persist';



type persistPartial<State> = {
    _persist? : {
        version : number;
        rehydrated: boolean;
    };
}& Partial<State>

interface PersistSliceOptions<State> {
    sliceKey: string;
    storageKey?: string;
    blacklist?: (keyof State & string[]);
}

export function persistSlice<State>(
    reducer: Reducer<State>,
    options: PersistSliceOptions<State>,
): Reducer<State & persistPartial<State>> {
    const {sliceKey, storageKey = sliceKey, blacklist = [FLUSH, REHYDRATE, PAUSE, PERSIST, PURGE, REGISTER]} = options;

    const persistConfig: PersistConfig<State> = {
        key: storageKey,
        storage,
        blacklist: blacklist as string[],
    };

    return persistReducer(persistConfig, reducer) as Reducer<State & persistPartial<State>>  
}