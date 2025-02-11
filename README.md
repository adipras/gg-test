# gg-test API

A .NET Core Web API with MySQL database.

## Features

### Send email
- Send emails using Google SMTP
- Store email history (recipient, subject, body, sent timestamp)
- Retrieve sent email history

### Swagger API documentation

## Prerequisites
- .NET Core SDK (latest stable version)
- MySQL Server
- Node.js (optional, for frontend testing)
- Postman (optional, for API testing)

## Setup Instructions

### 1. Clone the Repository
```sh
git clone https://github.com/yourusername/gg-test.git
cd gg-test
```

### 2. Configure Environment Variables
Set credentials securely:

#### Linux/macOS
```sh
export SMTP_USERNAME="your-email@gmail.com"
export SMTP_PASSWORD="your-email-password"
export JWT_KEY="your-very-long-and-secure-secret-key-here"
```

#### Windows PowerShell
```powershell
$env:SMTP_USERNAME="your-email@gmail.com"
$env:SMTP_PASSWORD="your-email-password"
$env:JWT_KEY="your-very-long-and-secure-secret-key-here"
```

Alternatively, use `dotnet user-secrets`:

```sh
dotnet user-secrets init
dotnet user-secrets set "SmtpSettings:Username" "your-email@gmail.com"
dotnet user-secrets set "SmtpSettings:Password" "your-email-password"
dotnet user-secrets set "JwtSettings:JwtKey" "your-email-password"
```

### 3. Configure Database Connection
Update `appsettings.json` with your MySQL connection:

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=gg_test;User=root;Password=yourpassword;"
}
```

### 4. Install Dependencies
```sh
dotnet restore
```

### 5. Apply Migrations
```sh
dotnet ef database update
```

### 6. Run the API
```sh
dotnet run
```

API will be available at `http://localhost:5125/swagger`.

## API Endpoints

### Send Email
**POST** `/api/emails/send`
```json
{
  "recipient": "recipient@example.com",
  "subject": "Test Email",
  "body": "<h1>Hello, World!</h1><p>This is a test email.</p>"
}
```

### Get Email History
**GET** `/api/emails/history`

### User Login
**POST** `/api/auth/login`
```json
{
  "username": "your-username",
  "password": "your-password"
}
```

### Get Users
**GET** `/api/users`
- Requires authentication.
- Returns a list of users based on the role of the currently logged-in user.
  - **Admin**: Can see all user data.
  - **Manager**: Can only see data for users in their department.
  - **Employees**: Can only see their own data.

## Seeded Users for Testing

| Username           | Password     | Role     |
|--------------------|--------------|----------|
| admin@example.com  | admin123     | Admin    |
| manager@example.com| manager123   | Manager  |
| emp1@example.com   | emp123       | Employee |
| emp2@example.com   | emp123       | Employee |

## Security Considerations
- **Never store credentials in `appsettings.json`**.
- **Use environment variables or `dotnet user-secrets` for local development**.
- **Ensure `.gitignore` excludes sensitive files before pushing to GitHub**.

## License
MIT License

