import { fileURLToPath, URL } from 'node:url';
import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';
import basicSsl from '@vitejs/plugin-basic-ssl';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [plugin(), basicSsl()],
    resolve: {
        alias: {
            '@': fileURLToPath(new URL('./src', import.meta.url))
        }
    },
    server: {
        proxy: {
            "^swagger": {
                target: "http://localhost:5069/",
                secure: false,
                changeOrigin: true,
            },
            "^/auth/.*": {
                target: "http://localhost:5069/"
            }
        },
        port: 5015
        }
    }
)
