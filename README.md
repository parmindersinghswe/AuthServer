# Auth Server

Welcome to the Auth Server repository! This repository contains the code for the authentication server responsible for handling user authentication and authorization in your application.

## Project Overview

The Auth Server is a crucial component of your application's security infrastructure. It manages user authentication, authorization, and token generation to ensure secure access to protected resources.

## Table of Contents

- [Features](#features)
- [Getting Started](#getting-started)
- [Configuration](#configuration)
- [Custom Authorization Attribute](#custom-authorization-attribute)
- [API Endpoints](#api-endpoints)
- [Security](#security)
- [Contributing](#contributing)

## Features

- **User Authentication:** Securely authenticate users with proper validation mechanisms.
- **Token Generation:** Generate and manage access tokens for authorized users.
- **Authorization:** Control access to resources based on user roles and permissions.

## Getting Started

To set up the Auth Server in your environment, follow these steps:

1. Clone this repository to your local machine:

    ```bash
    git clone https://github.com/parmindersinghswe/AuthServer.git
    ```

2. Navigate to the AuthServer.Domain project directory:

    ```bash
    cd AuthServer.Domain
    ```

3. Build the project:

    ```bash
    dotnet build
    ```

## Configuration

Configure the Auth Server by updating the configuration files. Key configuration points include:

- **Database Connection:** Specify the connection details for the database where user information is stored.
- **Token Configuration:** Set parameters such as token expiration times, secret keys, and algorithm.

Refer to the provided `appsettings.example.json` file for a template. Create a new configuration file named `appsettings.json` with your specific settings.

## Custom Authorization Attribute

The `AuthServer.Domain` project includes a custom `AuthorizeAttribute` that accepts a policy name in its constructor. To use it in your main project:

1. Build the `AuthServer.Domain` project.

    ```bash
    dotnet build AuthServer.Domain.csproj
    ```

2. Reference the built assembly in your main project.

3. Use the `AuthorizeAttribute` wherever authentication or authorization is required, providing the necessary policy name:

    ```csharp
    using AuthServer.Domain;

    // ...

    [Authorize("YourPolicyName")]
    public class YourController : ControllerBase
    {
        // Your authenticated and authorized controller actions
    }
    ```

Replace `"YourPolicyName"` with the actual policy name you want to enforce in your application.

```csharp
[Authorize("AdminPolicy")]
public class AdminController : ControllerBase
{
    // Actions accessible only to users with the "AdminPolicy"
}

[Authorize("UserPolicy")]
public class UserController : ControllerBase
{
    // Actions accessible only to users with the "UserPolicy"
}
```
This way, you can customize the policies based on your application's specific authorization requirements.

## API Endpoints

The Auth Server exposes the following API endpoints:

- **POST /auth/register:** Register a new user.
- **POST /auth/login:** Authenticate a user and receive an access token.
- **POST /auth/logout:** Invalidate the user's access token.
- **GET /auth/user:** Get information about the authenticated user.

For detailed API documentation, refer to [API Documentation](docs/api.md).

## Security

Security is a top priority for the Auth Server. Some key security considerations include:

- **Password Hashing:** User passwords are securely hashed using industry-standard algorithms.
- **Token Security:** Tokens are generated securely and include expiration times to mitigate unauthorized access.
- **SSL/TLS:** Ensure that the server uses HTTPS for secure communication.

## Contributing

We welcome contributions! If you find issues or have suggestions for improvements, please create a GitHub issue or submit a pull request.
