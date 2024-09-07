# How to Run the Application

Follow these steps to set up and run the application:

1. **Clone the repository:**
    ```sh
    git clone <repository_url>
    ```

2. **Start the Identity Server:**
    ```sh
    cd web-api/IdentityServer
    dotnet run
    ```

3. **Start the Hosted Service project:**
    ```sh
    cd web-api/HostedService
    dotnet run
    ```

4. **Start the WebApi project:**
    ```sh
    cd web-api/WebApi
    dotnet run --launch-profile https
    ```

5. **Open Postman and make a POST request to get the token:**
    - URL: `https://localhost:5001/connect/token`
    - Set up the authorization with the following details:
        - **Callback URL:** `https://localhost:5001/signin-oidc`
        - **Auth URL:** `https://localhost:5001/connect/authorize`
        - **Access Token URL:** `https://localhost:5001/connect/token`
        - **Client ID:** `web.client`
        - **Client Secret:** `secret`
        - **Scope:** `openid profile email libraryApi`
    - Click on **Get New Access Token** and authenticate:
        - For premium profile test, use:
            - **Username:** `alice`
            - **Password:** `alice`
        - For standard profile test, use:
            - **Username:** `bob`
            - **Password:** `bob`
    - Copy the received token.

6. **Use the token in API requests:**
    - In Postman, insert the token as a Bearer token in the authorization header of your API requests.

By following these steps, you should be able to successfully run and test the application.
