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
- [Authorization Mechanism](#authorization-mechanism)
- [Project Structure](#project-structure)

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

## Authorization Mechanism

In our Auth Server, users are authorized based on the policies assigned to their member profiles. The scope of this project includes creating, fetching, updating, and deleting members, as well as generating JWT access tokens through the login (authentication) process. Access is granted through the authorization of policies associated with user roles.

Every member has specific roles, and each role has defined policies that determine whether the user is authorized or not. You can decorate any method or class using our custom authorization attribute by passing the relevant policy.

## Project Structure

This project consists of three main components:

1. **Auth.Domain:** The domain project containing the core logic and authorization attributes.
2. **Auth.Gateway (optional but required according to current configurations):** The gateway project for handling external communications or integrations.
3. **Auth.Server:** The main authentication server responsible for user authentication, authorization, and token generation.
4. **Auth.Server.Test:** The test project for ensuring the functionality and security of the Auth Server.


### Auth.Domain

The `Auth.Domain` project is responsible for domain-specific tasks, providing the type of `Authorize` attribute, defining parameters in the `Authorize` attribute, and housing the AuthorizationMiddleware.

- **Authorize Attribute:** Defines the custom `AuthorizeAttribute` used for specifying authorization policies. This attribute includes parameters to set the policy name.

- **AuthorizationMiddleware:** This middleware is responsible for calling the Authorization API. It plays a crucial role in the authorization process by interacting with the Auth Server to validate user access based on policies.

- **RegisterClaims API:** The project includes the `RegisterClaims` API, allowing the registration of claims to the Auth Server. Claims are essential for user authorization and are used to determine access rights.

- **URL Configuration:** The URLs for any API calls within this project are retrieved from the `appsettings.json` file of the consumer project. Ensure that the configuration is set correctly to establish communication with the Auth Server.

- **Utility Classes:** Contains utility classes to facilitate various tasks within the domain.

- **Model Classes:** Includes model classes that define the structure of data used within the `Auth.Domain` project.

**Note:** It's crucial to register the `RegisterClaims` middleware before the `AuthorizationMiddleware` in the pipeline to ensure proper execution of claim registration before authorization checks.


### Auth.Gateway

The `Auth.Gateway` project serves as the gateway to communicate with microservices, including the Auth Server. It utilizes the Ocelot nuget package (https://github.com/ThreeMammals/Ocelot) as a gateway solution.

- **Ocelot NuGet Package:** The project integrates the Ocelot nuget package (https://www.nuget.org/packages/Ocelot/22.0.1/ReportAbuse) to function as the gateway. Ocelot provides features for routing, request aggregation, and response aggregation.

- **CORS Policy:** Initially, the gateway is configured to allow any domain to hit it with a CORS (Cross-Origin Resource Sharing) policy. This ensures flexibility in accepting requests from different origins.

- **Ocelot.json Configuration:** The project includes an `Ocelot.json` file that configures the gateway routes with upstreams and downstreams. This configuration is crucial for defining how requests are routed to and from the Auth Server and other microservices.

**Note:** Ensure that the `Ocelot.json` file is appropriately configured to reflect the desired routing behavior and to establish seamless communication between the Auth Gateway and the microservices.



The `Auth.Server` project serves as the main authentication server, managing user-related tasks and external logins with services like Google and Facebook.

- **User Management:**
  - **User Creation:** Provides functionality to create new users with proper validation mechanisms.
  - **User Update:** Allows for updating user information as needed.

- **Authentication:**
  - **Login & JWT Token Generation (Authentication):** Handles user authentication, including the login process and the generation of JWT (JSON Web Token) tokens for authorized users.

- **Authorization:**
  - **External Logins:** Supports external logins, such as Google, Facebook, etc., to provide additional authentication options for users.

- **Claims and Roles Management:**
  - **Registration of Claims:** Manages the registration of claims, essential for user authorization.
  - **Claim in Roles and Roles to Members:** Adds claims to roles and associates roles with members for fine-grained access control.

- **API Exposure:**
  - **Direct API Consumption:** Provides the option to directly consume all the APIs exposed by `Auth.Server` for user management, authentication, and authorization.
  - **Gateway Integration:** Alternatively, the APIs can be used through the `Auth.Gateway` for more controlled and flexible communication.

**Note:** Ensure proper configuration and security measures are in place for external logins. Consider using HTTPS to secure communication for sensitive operations.

### Auth.Server.Test

The `Auth.Server.Test` project serves as the client project to consume the functionalities of `Auth.Server` with minimal configurations.

- **Client Configuration:**
  - **Reference to Auth.Domain Library:** Add a reference to the `Auth.Domain` library in the client project.

  - **AuthorizationMiddleware Registration:** Register the `AuthorizationMiddleware` middleware in the client project's pipeline. This middleware is crucial for handling authorization checks.

  - **API Domain Exposure:** Expose the API domains in the `appsettings.json` file. If using the gateway, specify the `Auth.Gateway` domain; otherwise, use the `Auth.Server` domain. The domain is added with the "AuthServer" key in the current project's `appsettings.json` file.

- **Usage of AuthorizationAttribute:**
  - After completing the above steps, the client project can seamlessly use the `AuthorizationAttribute` in its controllers. This allows for easy integration of authentication and authorization functionalities in the client application.

- **Policy Registration Automation:**
  - To automate the registration of policies without manual intervention, the client project can create an API dedicated to registering policies. In this case, the `AuthServerConfigurationsController` in the `Auth.Server.Test` project serves this purpose.

  - Ensure the API is exposed in the `appsettings.json` file so that it is consumed on the first API hit. This approach helps retain consistency in policies between code, the database, and the client project.

  - By default, all policies used in the code for authorization will be registered in the `Auth.Server`, eliminating the need for manual registration in the client project.

  - This practice ensures policy consistency and simplifies the process of adding policies to appropriate roles.

- **Simplified Identity Server:**
  - The `Auth.Server` project is intentionally designed as an early-stage identity server with a focus on easy integration.

  - Minimal Configurations: It is tailored for a simplified setup, allowing users to register the middleware without additional complexities.

  - Lightweight Identity Management: While the server offers basic identity management features like user creation, update, login, and token generation, it avoids unnecessary complexity to ensure a lightweight and user-friendly experience.


- **Enhancing Functionality through Contributions:**
  - Contributors can enhance the functionality by creating forks of the repository and proposing changes through pull requests.

  - Issues and suggestions for improvements can be raised through GitHub issues.

  - The community is encouraged to contribute to the project by creating new features, fixing bugs, or suggesting optimizations.

  - To contribute, follow the standard GitHub workflow by creating branches, making changes, and submitting pull requests.

**Note:** Ensure to review the documentation and code for any updates and changes made by contributors.

Feel free to explore each component for a better understanding of the project's structure and functionality.


# Important Note: Gateway Configuration

## Overview

Currently, the Gateway is available in the project structure, but it is not actively in use. While the Gateway is functional, additional configurations are required to enable its usage.

## Gateway Configuration Steps

To utilize the Gateway functionality, follow these steps:

1. **Configure Upstream and Downstream:**
   - In the `Ocelot.json` configuration file within the `Auth.Gateway` project, ensure that appropriate upstream and downstream configurations are added. These configurations define how requests are routed between the Gateway and microservices.

2. **Expose Gateway Domain:**
   - In the client project, specifically the `Auth.Server.Test` project, update the `appsettings.json` file to expose the domain of the Gateway. If the Gateway is intended to be used, specify the Gateway domain instead of the `Auth.Server` domain.

## Note

As of now, the Gateway is not in active use, and the client project is configured to consume the `Auth.Server` directly. If you wish to leverage the Gateway, additional configurations are necessary.

Please be aware that the provided Gateway configurations are for reference, and adjustments may be required based on your specific project requirements.

Feel free to explore the `Ocelot.json` file and `appsettings.json` in the respective projects for more details on configuration options.

If you encounter any issues or have questions regarding the Gateway setup, feel free to reach out through GitHub issues.
