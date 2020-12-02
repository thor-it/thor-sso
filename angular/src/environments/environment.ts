import { Config } from '@abp/ng.core';

const baseUrl = 'http://localhost:4200';

export const environment = {
  production: false,
  application: {
    baseUrl,
    name: 'SRC Thor - Member Portal',
    logoUrl: '',
  },
  oAuthConfig: {
    issuer: 'https://localhost:44333',
    redirectUri: baseUrl,
    clientId: 'SSO_App',
    responseType: 'code',
    scope: 'offline_access SSO',
  },
  apis: {
    default: {
      url: 'https://localhost:44333',
      rootNamespace: 'Thor.SSO',
    },
  },
} as Config.Environment;
