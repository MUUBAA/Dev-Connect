import { decrypt } from "./encryptionUtils";











export const getDecryptJwt = () => {
    const encryptedJwt = localStorage.getItem('jwtToken');
    return encryptedJwt ? decrypt(encryptedJwt) : null;
}