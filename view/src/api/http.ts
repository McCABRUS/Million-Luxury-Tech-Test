import axios from 'axios';

const API_BASE = import.meta.env.VITE_API_URL ?? 'http://localhost:5093/api';

export const http = axios.create({
  baseURL: `${API_BASE}/properties`,
  headers: { 'Content-Type': 'application/json' },
  timeout: 10000
});