# A small Book Management in Library Management System
It's an small project on Book Management. Build With ASP.NET 5, JWT authentication and some other great features. 

<strong>Features implemented :</strong>

-- Written API endpoints to Get, Create, Update and Delete entries.</br> 
-- Authentication is done by JWT Authentication. No authorization added.</br>
-- Logged each API request and responses. Request, Responses data are shown in the console window.</br>
-- Handled all known and unhandled exceptions and showed proper messages to the calling client.</br> 
-- Followed by n-tier Architecture.</br>
-- In Memory DB is used with EF Core. No external DB connection is needed.</br>
-- GET/READ requests do not need any authentication, they are publicly accessible, only Create, Update and Delete endpoints require authentication.</br>
-- CRUD operations on Book object.</br>


# To setup the project in your local download the repo, clear the build files, rebuild and run. 
-- Athenticate with "/api/Books/authenticate" endpoint with Username = "test", Password = "test"
-- Copy the response token and paste it on Swagger Authorise field.
-- Use the endpoint.
