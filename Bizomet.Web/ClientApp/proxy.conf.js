const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:53087';

const PROXY_CONFIG = [
  {
    context: [
      "/api/account/register",
      "/api/account/login",
      "/api/account/logout",
      "/api/account/emailconfirmation",
      "/api/account/forgotpassword",
      "/api/account/resetpassword",
      "/api/userprofile/profile",
      "/api/userprofile/userportfolio",
      "/api/userprofile/update",
      "/api/token/refresh",
      "/api/token/revoke"
   ],
    target: target,
    secure: false
  }
]

module.exports = PROXY_CONFIG;
