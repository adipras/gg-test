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
Set SMTP credentials securely:

#### Linux/macOS
```sh
export SMTP_USERNAME="your-email@gmail.com"
export SMTP_PASSWORD="your-email-password"
```

#### Windows PowerShell
```powershell
$env:SMTP_USERNAME="your-email@gmail.com"
$env:SMTP_PASSWORD="your-email-password"
```

Alternatively, use `dotnet user-secrets`:

```sh
dotnet user-secrets init
dotnet user-secrets set "SmtpSettings:Username" "your-email@gmail.com"
dotnet user-secrets set "SmtpSettings:Password" "your-email-password"
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

API will be available at `http://localhost:5000/swagger`.

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

## Security Considerations
- **Never store credentials in `appsettings.json`**.
- **Use environment variables or `dotnet user-secrets` for local development**.
- **Ensure `.gitignore` excludes sensitive files before pushing to GitHub**.

## License
MIT License

