import { defineConfig } from 'vite';
import plugin from '@vitejs/plugin-react';

// https://vitejs.dev/config/
export default defineConfig({
    plugins: [plugin()],
    server: {
        port: 5173,
        proxy: {
            '/api': {
                target: 'https://localhost:5210',
                changeOrigin: true,
                secure: false,
            }
        }
    }
})