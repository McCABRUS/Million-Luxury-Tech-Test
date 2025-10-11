import { defineConfig } from 'vitest/config';
import react from '@vitejs/plugin-react-swc'

// https://vite.dev/config/
export default defineConfig({
  plugins: [react()],
  test: {
    environment: 'jsdom',
    globals: true,
    setupFiles: './src/tests/setupTests.ts',
    watch: false,
    coverage: {
      provider: 'v8',
      reporter: ['text', 'lcov'],
    },
  },
  server: {
    proxy: {
      '/assets': {
        target: 'http://localhost:5093',
        changeOrigin: true,
        secure: false
      }
    }
  }
})
