# 🚀 Syrian Developers Network

## 💡 About the Project
This project is a **Full-Stack Web Application** designed as a professional networking platform for developers. It provides users with personalized profiles, idea-sharing spaces, technical Q&A, specialized communities, project showcases, and a complete job posting/application system with an integrated resume builder.

It leverages a modern technology stack and follows **Clean Architecture** principles to ensure maintainability, scalability, and separation of concerns.

---

## ✨ Features
1. **User & Profile Management**
    * **Customizable Profile:** Users can update personal information, BIO, skills, and professional details.
    * **Resume Builder:** Users can create multiple resumes (5–6), choose styling/language, then save or export them as `PDF`.

2. **📢 Social & Content Sharing**
    * **Ideas Space:** Users can share ideas or life issues and interact with others' posts.
    * **Q&A System:** Users can post coding issues/bugs and receive community solutions. A best solution can be selected.
    * **Project Exhibition:** Users can publish projects, receive ratings & suggestions, and invite collaborators.

---

## 🛠️ Technology Stack
This project uses the following core technologies:

| **Category**   | **Technology** | **Description** |
|----------------|----------------|-----------------|
| 🖥️ Backend      | .NET Platform  | Business logic & API layer |
| 🎨 Frontend     | Angular        | SPA UI framework |
| 💻 Language     | TypeScript / C# | Primary programming languages |
| 🗄️ Database     | SQL Server     | Relational DBMS |
| 🔗 ORM          | EF Core        | Data access abstraction |
| ⚡ Real-time     | SignalR        | Real-time communication |
| 🔍 Search       | Elasticsearch  | Search & ranking engine |

### 📦 Key .NET Libraries
- AutoMapper (object-to-object mapping)
- EmailKit / similar email provider
- EF Core packages (migrations & database interaction)
- SMS Sender library

---

## 🏗️ Architecture & Patterns

### Clean Architecture
Ensures independent, testable, and maintainable layers.

### Monolithic Architecture
The system is deployed as a unified structure for simplicity and performance.

### Design Patterns
* **Repository Pattern:** Abstracts data access to keep the application independent from the underlying data source.
* **Specification Pattern:** Encapsulates and composes query logic in a reusable manner.

---

## 📂 Project Structure
The solution is divided according to Clean Architecture principles:

- **Domain Layer**: Core business rules, entities, interfaces.
- **Infrastructure Layer**: EF Core, SQL Server implementation, Elasticsearch integration.
- **Application Layer**: Commands, queries, DTOs, business logic orchestration.
- **API Layer**: Receives HTTP requests and communicates with the Application layer.
- **Common Layer**: Shared utilities, helpers, exceptions.
- **UI Layer (Angular)**: Frontend presentation and user interaction.

---

## ⚙️ Getting Started
### Prerequisites
- .NET SDK
- Node.js & npm
- SQL Server
- Docker Desktop

### 1. Repository Installation
Clone the repository:

```bash
git clone "https://github.com/eng-alimallouhe/Syrian-Developers-Network-SPN-.git"
```

Move to any folder:
```bash
cd path/to/any/folder
```

---

### 2. Docker Setup
We use Docker Desktop to run **Elasticsearch, Kibana, and Redis**.

1. Install Docker Desktop.
2. Navigate to the `Docker` folder.
3. Create or locate `docker-compose.yml`.
4. Paste the following content:

```yaml
services:
  elasticsearch:
    image: elasticsearch:8.13.4
    container_name: elasticsearch
    environment:
      - discovery.type=single-node
      - ES_JAVA_OPTS=-Xms1g -Xmx1g
      - xpack.security.enabled=false
    ports:
      - "9200:9200"
    networks:
      - elk

  kibana:
    image: kibana:8.13.4
    container_name: kibana
    depends_on:
      - elasticsearch
    ports:
      - "5601:5601"
    environment:
      - ELASTICSEARCH_HOSTS=http://elasticsearch:9200
    networks:
      - elk

  redis:
    image: redis:7
    container_name: redis
    ports:
      - "6379:6379"
    networks:
      - backend

  redis-insight:
    image: redis/redis-stack:latest
    container_name: redis-insight
    ports:
      - "8001:8001"
    networks:
      - backend

networks:
  elk:
  backend:
```

Start Docker containers:
```bash
docker compose up -d
```

Stop containers:
```bash
docker compose down
```

---

### 3. Backend Setup
Install dependencies:
```bash
dotnet restore
```

Configure database inside `appsettings.json`, then run:
```bash
dotnet run
```
Backend will run at:
```
http://localhost:5000
```

---

### 4. Frontend Setup
Install Angular dependencies:
```bash
npm install
```

Run the Angular app:
```bash
ng serve
```
أو:
```bash
npm start
```
Frontend runs on:
```
http://localhost:4200
```

---

## ▶️ Usage
- **Registration/Login:** Access the platform.
- **Profile:** Build your professional identity and create multiple-format resumes.
- **Projects:** Upload, document (with READMEs), and collaborate on projects.
- **Jobs:** Browse or post job opportunities.
- **Community:** Engage in discussions & problem-solving.

---

## 🤝 Contributing
We welcome contributions!  
Please follow the project's **Clean Architecture** style and **Design Patterns**.  
If available, refer to `CONTRIBUTING.md` for detailed guidelines.

---

## 📄 License
Specify license here (e.g., **MIT License**).

