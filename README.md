# SimpleLoginRegisterProject

## Warnings:
**This project doesn't have any CSS.**

**This project isn't by any means secure.**

## Project goals:
- Build ASP.NET Core API from "empty" project ASP.NET Core
- Use SQL Server database with Entity Framework Core (Code first)
- Get database connection string from appsettings file
- Create API controller with login and register features
- Passwords should be at least hashed on backend
- Controller shouldn't have any access to database
- All interactions with database should be handled by service layer
- Services should be injected using Microsoft.Extension.DependencyInjection
- API calls should be authorized by global API key
- API calls should be made by external React.JS app using axios
- API should be tested in Postman Chrome extension

## Encountered problems and solutions:
- All API calls resulted with 404 - Registered controllers
- API calls from external apps was blocked - Configured CORS policy
- All API calls from React app refreshed it - Added prevention of default button onClick event
- Checkbox onChange.target.value was always on, therefore Register component state.permissions was always 0 - 
Added additional variable to store next state.permissions value

## What have I learned during work on this project:
- What is CORS policy
- Fact that you need to register controllers somewhere
- Basic understanding how to use API authorization based on API key
