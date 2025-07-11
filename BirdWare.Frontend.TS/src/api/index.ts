import axios from 'axios';
import { useAuthenticateStore } from '@/stores/authenticate.js';

const api = axios.create({
  baseURL: import.meta.env.VITE_VUE_API_BASE_URL
});

api.interceptors.request.use((config) => {
  const authenticate = useAuthenticateStore();

  if (authenticate.isLoggedIn) {
    config.headers.Authorization = "Bearer " + authenticate.jwtToken;
  }
  return config;
});

export default api;