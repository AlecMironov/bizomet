const { env } = require('process');

const target = env.ASPNETCORE_HTTPS_PORT ? `https://localhost:${env.ASPNETCORE_HTTPS_PORT}` :
  env.ASPNETCORE_URLS ? env.ASPNETCORE_URLS.split(';')[0] : 'http://localhost:53087';

const PROXY_CONFIG = [
  {
    context: [
      "/api/common",
      "/api/token",
      "/api/account",
      "/api/userprofile",
      "/api/userportfolio",
      "/api/inquiry",
      "/api/project"
   ],
    target: target,
    secure: false
  }
]

module.exports = PROXY_CONFIG;
