import axios, { AxiosInstance } from 'axios';

const _axios = axios.create({
    baseURL: process.env.VUE_APP_API_BASE_URL || ''
});

declare global {
    interface Window { axios: AxiosInstance }
    const axios: AxiosInstance;
}

window["axios"] = _axios;