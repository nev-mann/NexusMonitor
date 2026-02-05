# NexusMonitor
This is an educational Web API project built with .NET 10. The application utilizes a SQL Server database running within a Docker container.

## Password Configuration
To maintain security, this project does not store passwords in the Git repository. To run the application locally, you must configure secrets in two locations:

### 1. Docker
In the root directory (where docker-compose.yml is located), create a file named .env and enter your database password there.

### 2. .NET
In the appsettings.json file, provide the connection string password required to connect to the SQL Server instance running in the Docker container.