# Store Management and User Authentication API

## User Story

### Background

As a developer, you have been tasked with creating a web application to manage store data and users, with a focus on security and authentication. The application should allow creating, reading, updating, and deleting store records and provide a secure way to create and manage users with different roles. For authentication, you have chosen to use JWT Tokens along with MongoDB for data storage and Microsoft Identity for credential management.

### User Story

**Title**: Store and User Management with Secure Authentication

**As** a system administrator,  
**I want** to manage store data and users through a secure interface,  
**So that** I can ensure data integrity and security, and control access to application functionalities.

### Details

1. **Store Management (Store.Api)**:
   - **As** a system administrator,
   - **I want** to create, read, update, and delete store data through RESTful endpoints,
   - **So that** I can keep store information updated and organized.

2. **User Registration and Authentication (Store2.Api)**:
   - **As** a system administrator,
   - **I want** to create users and assign different roles,
   - **So that** I can control access to application functionalities.
   - **I want** users to log in using their credentials,
   - **So that** they can securely access the application and receive a JWT Token for authentication.

3. **Protected Access**:
   - **As** an authenticated user,
   - **I want** to access protected endpoints that require authentication,
   - **So that** I can perform secure operations as allowed by my role.
   - **As** an unauthenticated user,
   - **I want** to access unprotected endpoints,
   - **So that** I can view public information without the need to log in.

### Features

- **Store.Api**:
  - Endpoint to **create** a new store.
  - Endpoint to **read** a specific store's data or all stores.
  - Endpoint to **update** an existing store's data.
  - Endpoint to **delete** a store.

- **Store2.Api**:
  - Endpoint to **create** a new user with a specific role.
  - Endpoint for user **login**, with JWT Token generation.
  - **Protected** endpoint for performing specific operations available only to authenticated users.
  - **Unprotected** endpoint to view public information.

### Acceptance Criteria

1. **Store CRUD**:
   - It should be possible to create, read, update, and delete stores via the Store.Api.
   - Each store should have a unique identifier and at least two other relevant fields (e.g., store name, address).

2. **User Management**:
   - It should be possible to create users with different roles (e.g., administrator, regular user) in the Store2.Api.
   - It should be possible to log in with a created user and receive a JWT Token.

3. **Authentication and Authorization**:
   - Protected endpoints should be accessible only to authenticated users with a valid JWT Token.
   - Unprotected endpoints should be accessible without authentication.

4. **Security**:
   - User passwords should be securely stored using hashing.
   - The application should validate user credentials during login and issue a JWT Token for authenticated sessions.

### Postman Collection

To help tests in \postman collection has samples, in each api folder has .http file to perform tests

With these features and acceptance criteria, the application will be able to manage store and user data securely and efficiently, ensuring data integrity and secure access.
