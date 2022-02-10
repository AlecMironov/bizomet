# Bizomet

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 12.0.2.

## Development server

Run `ng serve` for a dev server. Navigate to `http://localhost:4200/`. The app will automatically reload if you change any of the source files.

## Code scaffolding

Run `ng generate component component-name` to generate a new component. You can also use `ng generate directive|pipe|service|class|guard|interface|enum|module`.

## Build

Run `ng build` to build the project. The build artifacts will be stored in the `dist/` directory.

## Running unit tests

Run `ng test` to execute the unit tests via [Karma](https://karma-runner.github.io).

## Running end-to-end tests

Run `ng e2e` to execute the end-to-end tests via a platform of your choice. To use this command, you need to first add a package that implements end-to-end testing capabilities.

## Further help

To get more help on the Angular CLI use `ng help` or go check out the [Angular CLI README](https://github.com/angular/angular-cli/blob/master/README.md).



****************************************************
Update to the latest packages:
****************************************************
npm i -g npm-check-updates
ncu -u
npm install
****************************************************


****************************************************
Update css from scss:
****************************************************
1. first globally install saas
npm install node-sass 

2. Update theme and layout (add --watch to watch changes)  -w --watch                    Watch stylesheets and recompile when they change

--- for debug
sass src/assets/theme/purple/theme-dark.scss src/assets/theme/purple/theme-dark.css --no-source-map
sass src/assets/theme/purple/theme-light.scss src/assets/theme/purple/theme-light.css --no-source-map
sass src/assets/layout/css/purple/layout-dark.scss src/assets/layout/css/purple/layout-dark.css --no-source-map
sass src/assets/layout/css/purple/layout-light.scss src/assets/layout/css/purple/layout-light.css --no-source-map

--- for production
sass src/assets/theme/purple/theme-dark.scss src/assets/theme/purple/theme-dark.css --style=compressed --no-source-map
sass src/assets/theme/purple/theme-light.scss src/assets/theme/purple/theme-light.css --style=compressed --no-source-map
sass src/assets/layout/css/purple/layout-dark.scss src/assets/layout/css/purple/layout-dark.css --style=compressed --no-source-map
sass src/assets/layout/css/purple/layout-light.scss src/assets/layout/css/purple/layout-light.css --style=compressed --no-source-map

****************************************************
