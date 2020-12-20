import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';
const apiUrl = 'https://localhost:44333';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'SRC Thor - Member Portal',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: apiUrl,
    redirectUri: baseUrl,
    clientId: 'SSO_App',
    responseType: 'code',
    scope: 'offline_access SSO',
  },
  apis: {
    default: {
      url: apiUrl,
      rootNamespace: 'Thor.SSO',
    },
  },
} as Environment;
