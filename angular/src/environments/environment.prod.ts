import { Environment } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: true,
  application: {
    baseUrl,
    name: 'SSO',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44325',
    redirectUri: baseUrl,
    clientId: 'SSO_App',
    responseType: 'code',
    scope: 'offline_access SSO',
  },
  apis: {
    default: {
      url: 'https://localhost:44325',
      rootNamespace: 'Thor.SSO',
    },
  },
} as Environment;
