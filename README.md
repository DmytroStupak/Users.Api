How to Run the API
1.	Restore dependencies and build:
```
dotnet restore
dotnet build
```

2.	Run the API:
```
dotnet run --project Users.Api
```
The API will be available at http://localhost:5039

3.	Swagger UI:
Visit http://localhost:5039/swagger/index.html for interactive API documentation.


How to Run the Tests
•	Unit Tests:
```
dotnet test
```
This will run all tests in the User.Api.Tests project.


API Test Scenarios (curl)
1. Creating a Valid User (POST /users)

```
curl -X POST http://localhost:5039/Users \
  -H "Content-Type: application/json" \
  -d '{
    "fullName": "Alice Smith",
    "email": "alice@example.com",
    "dateOfBirth": "1990-05-15T00:00:00Z"
  }'
```
Expected Response:
•	Status: 201 Created
•	Body: JSON with the created user (including id).


2. Getting the List of Users (GET /users)

```
curl -X GET http://localhost:5039/Users
```
Expected Response:
•	Status: 200 OK
•	Body: JSON array of users.


3. Creating a User with Invalid Email (POST /users)
```
curl -X POST http://localhost:5039/Users \
  -H "Content-Type: application/json" \
  -d '{
    "fullName": "Bob Brown",
    "email": "not-an-email",
    "dateOfBirth": "1985-03-10T00:00:00Z"
  }'
```
Expected Response:
•	Status: 400 Bad Request
•	Body: "Valid Email is required."


4. Creating a User with Future Date of Birth (POST /users)
```
curl -X POST https://localhost:5001/Users \
  -H "Content-Type: application/json" \
  -d '{
    "fullName": "Charlie Green",
    "email": "charlie@example.com",
    "dateOfBirth": "2999-01-01T00:00:00Z"
  }'
```
Expected Response:
•	Status: 400 Bad Request
•	Body: "DateOfBirth cannot be in the future."


Java Tests :
```
•	Place UsersApiTest.java file in src/test/java/ in your Java project.
•	Add dependencies for rest-assured and junit-jupiter (JUnit 5) in your pom.xml or build.gradle.
•	Run with mvn test or your preferred method.

```


