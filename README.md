# Assignment 6

## Status Overview

[![Build And Test](https://github.com/cschadewitz/EWU-CSCD379-2021-Spring/actions/workflows/AssignmentBuild.yml/badge.svg)](https://github.com/cschadewitz/EWU-CSCD379-2021-Spring/actions/workflows/AssignmentBuild.yml)

[![Deploy](https://github.com/cschadewitz/EWU-CSCD379-2021-Spring/actions/workflows/AssignmentDeploy.yml/badge.svg)](https://github.com/cschadewitz/EWU-CSCD379-2021-Spring/actions/workflows/AssignmentDeploy.yml)

[![Website secretsantacasey.azurewebsites.net](https://img.shields.io/website-up-down-green-red/https/secretsantacasey.azurewebsites.net.svg)](https://secretsantacasey.azurewebsites.net)

[![Swagger Docs](https://img.shields.io/badge/Swagger-API-1abc9c.svg)](https://secretsantacasey-api.azurewebsites.net/swagger/index.html)

## Assignment

- Configure a CI Build against the SecretSanta solution
  - Compile all the source code ✔❌
  - Run all the user tests ✔❌
  - Add the Build Status to the Read.me file of your project.  (Be sure to use relative links so it works across branches and even when forked.) ✔❌
  - Place a URL to your fork and branch in your readme (so that we can easily navigate there to see the build)
- Configure a CD deployment of the project (https://docs.microsoft.com/en-us/azure/app-service/deploy-github-actions)
  - Configure an Azure Web App (using the Azure Portal) ✔❌
  - Configure a GitHub Action to deploy to an Azure Web App (https://github.com/Azure/webapps-deploy) ✔❌
  - Add the Deploy Status to the Read.me file of your project.  (Be sure to use relative links so it works across branches and even when forked.) ✔❌
  - Be sure to deploy the API project and verify that the UI that calls the API project works correctly. This will require you to update the URL/address in the Web project that points to the API project. ✔❌
- Place a hyperlink to the running website in both your Readme and your PR. (Consider using a badge that shows whether the site is up or not.) ✔❌

## Extra Credit

- Have the Web Tests run against the deployed website after the deployment completes.
- Configure the API site URL address into the Web Project during the deployment using a GitHub Action variable.
- Configure code coverage and generate a report (see https://github.com/dotnet/docs/blob/main/docs/core/testing/unit-testing-code-coverage.md) ✔❌
