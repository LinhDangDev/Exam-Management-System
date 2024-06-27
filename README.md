# Online Examination System with Redis and Blazor

## Key Features

The system is packed with features designed to streamline the online examination process:

**1. Secure Authentication & Session Management:**
   - **Page Login:**
     - Secure user login and authentication via token-based system for both students and administrators.
     - Prevents simultaneous logins from different devices for a single student account, ensuring exam integrity.

**2. Real-time Information and Control:**
   - **Page Info:**
     - Real-time display of current date and time for students.
     - Exam access control: Students can only access the exam during an active exam session.

**3. Enhanced Exam Experience:**
   - **Page Exam:**
     - Support for diverse question formats, including:
       - Audio-based questions
       - Image-based questions
       - Mathematical equations using LaTeX
       - Fill-in-the-blank questions
     - Exam resumption: Allows students to seamlessly resume interrupted exams from their last saved point, even from a different device.
     - User-friendly interface: Provides students with clear visibility of answered and unanswered questions for better progress tracking.
     - Fast loading of exam content and answer keys through the efficient implementation of Redis Cache.

**4. Instant Results & Performance:**
   - **Page Result:**
     -  Leveraging [Dependency Injection (DI)](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection) and Redis, the system delivers instant exam score display without waiting for server-side processing.

**5. Comprehensive Administrative Tools:**
   - **Page Admin Manage:**
     - Provides administrators with comprehensive exam session management capabilities:
       - Activate and deactivate exam sessions.
       - Terminate exam sessions when required.
   - **Page Admin Monitor:**
     - Real-time monitoring of student activity:
       - Track student login status (logged in/out) and last login time.
     - Intervention tools for administrators:
       - Reset student logins in case of technical difficulties or system errors.
       - Grant additional exam time to students who experience disruptions.
     -  Detailed overview of student exam progress:
       -  Monitor students currently taking the exam.
       -  Identify students who have left the exam session.

## Technologies Used

This project leverages a robust technology stack:

- **Frontend:** HTML, CSS, JavaScript, [Blazor](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor)
- **Backend:** C# ([.NET Core](https://dotnet.microsoft.com/en-us/download))
- **Database:** [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- **Caching:** [Redis](https://redis.io/)

## Installation and Setup

1. **Clone the repository:**
    ```bash
    git clone https://github.com/LinhDangDev/Final_Project_Web.git
    ```
2. **Install .NET Core SDK:** Download and install from the official [Microsoft website](https://dotnet.microsoft.com/en-us/download).
3. **Install SQL Server:** Download and install from the official [Microsoft website](https://www.microsoft.com/en-us/sql-server/sql-server-downloads).
4. **Install Redis:** Download and install from  [Redis Windows](https://github.com/ServiceStack/redis-windows?tab=readme-ov-file). 
5. **Configure database connection:**
    * **`appsettings.json`:**
        ```json
        "ConnectionStrings": {
          "DefaultConnection": "Server=<Server>;Database=<Name_Database>;Trusted_Connection=True;MultipleActiveResultSets=true"
        }
        ```
    * **`ApplicationDbContext.cs`:**
        ```csharp
        public class ApplicationDbContext : DbContext
        {
           line 70 ...=> optionsBuilder.UseSqlServer("Server=<Server>;Database=<Name_Database>;Trusted_Connection=True;TrustServerCertificate=True;");
        }
        ```
    * Replace `<Server>` with your SQL Server name and `<Name_Database>` with your desired database name.
6. **Configure Redis:**
    * Open `appsettings.json` and update the Redis connection string (host, port, password) in the `ConnectionStrings` section.
    * Default host 127.0.0.1 port 6379
8. **Start the application:**
    * From the project root directory, run `dotnet run`.
9. **Access the application:**
    * Open a web browser and navigate to the address displayed in the console after starting the application (usually `https://localhost:7204`).
