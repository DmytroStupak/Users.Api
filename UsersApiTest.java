import io.restassured.RestAssured;
import io.restassured.http.ContentType;
import org.junit.jupiter.api.BeforeAll;
import org.junit.jupiter.api.Test;

import static org.hamcrest.Matchers.*;

public class UsersApiTest {

    @BeforeAll
    public static void setup() {
        RestAssured.baseURI = "http://localhost";
        RestAssured.port = 5039;
        RestAssured.useRelaxedHTTPSValidation();
    }

    @Test
    public void createUser_ValidInput_ReturnsCreated() {
        String userJson = """
            {
                "fullName": "Test User",
                "email": "testuser@example.com",
                "dateOfBirth": "1995-01-01T00:00:00Z"
            }
            """;

        RestAssured.given()
            .contentType(ContentType.JSON)
            .body(userJson)
        .when()
            .post("/Users")
        .then()
            .statusCode(201)
            .body("id", notNullValue())
            .body("fullName", equalTo("Test User"))
            .body("email", equalTo("testuser@example.com"));
    }

    @Test
    public void createUser_InvalidEmail_ReturnsBadRequest() {
        String userJson = """
            {
                "fullName": "Bad Email",
                "email": "not-an-email",
                "dateOfBirth": "1995-01-01T00:00:00Z"
            }
            """;

        RestAssured.given()
            .contentType(ContentType.JSON)
            .body(userJson)
        .when()
            .post("/Users")
        .then()
            .statusCode(400);
    }
}