
# BloggingPlatform
Web blogging platform using c# with ASP.NET Core MVC, SqlLite, Entity Framework, and Auth0.

## How to run the app 
1. Install Visual Studio Code - https://code.visualstudio.com/download
2. Install dotnet 6 - https://dotnet.microsoft.com/en-us/download/dotnet/6.0
3. Either fork or dowload the app
4. Open the folder `BloggingPlatform`
5. Build the application using `dotnet build` command
6. Start the web server using `dotnet run` command. The app will be served at https://localhost:7183
7. Go to https://localhost:7183 in your browser.

## How to write a post
1. Login or register
2. Go to the nav option `Write a post`
3. Type out some information there
4. Submit the post 

## How to import posts
1. Only `admin` user can import posts
2. Login as admin 
3. Go to the nav option `Import Posts`

## Future Features
- Manage high level of traffic using an in-memory data store
![Cache](https://user-images.githubusercontent.com/42942167/181866596-9c92e063-33a1-4116-9901-741094a62190.png)

## What the app looks like


# Test the application
1. Open the folder `BlogginPlatform.Test`
2. Run the `dotnet test` command
