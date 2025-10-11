import { vi } from 'vitest';

const axios = {
  get: vi.fn(),
  post: vi.fn(),
  create: vi.fn(() => axios),
};

export default axios;