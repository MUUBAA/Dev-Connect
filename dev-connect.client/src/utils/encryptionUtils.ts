import CryptoJS from 'crypto-js'


const secretKey = 'your-secret-key';

export const encrypt = (text: string): string => {
    return CryptoJS.AES.encrypt(text, secretKey).toString();
};

export const decrypt = (ciphertext: string) : string => {
    const bytes = CryptoJS.AES.decrypt(ciphertext, secretKey);
    return bytes.toString(CryptoJS.enc.Utf8);
};