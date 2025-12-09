# 🚀Syrian Developers Network 


## 💡 About the project 
- this project is a Full-Stack Web Application designed as a professional newtwork platform. It provides users with personalized profiles, a space to share ideas and problems, specialized communities for various interests, and a dedicated job posting/application system with integrated resume building.
- It leverages a modern technology stack, employing Clean Architecture principles to ensure maintainability, scalability, and separation of concerns.

---

## ✨ Features
1. User and & his Own Profile

    * Customizable Profile: the user can customize his profile by filling his informations like name, BIO, his skills and professional details.

    * Built-in Resume Builder: user can create up to 5 or 6 customized Resumes withtin the platform, he can choose the style/language and save or export them as `PDF`. 

2. 📢 Social & Content Sharing

    * Ideas Space: users can share them ideas or life issues by posting the issue and thay can interact with another post to share them opinions.

    * Q&A: when you code you almost will see a bug so you can share this bug in Q&A space and the other users can suggest them solutions then you mark one solution as the best solution.

    * Project Exhibition: for each user there is a Exhibition for the user can share his projects and get rating and suggests and can invite a other user to collaborate the project.

## 🛠️ Technology Stack
- this project is built using the following core technologies: 

| **Category**   | **Technology** | **Description** |
|----------------|----------------|-----------------|
| 🖥️ Backend      | .NET Platform  | Business logic & API. |
| 🎨 Frontend     | Angular        | SPA UI framework. |
| 💻 Language     | TypeScript / C#| Main programming languages. |
| 🗄️ Database     | SQL Server     | Relational DBMS. |
| 🔗 ORM          | EF Core        | Data access abstraction. |
| ⚡ Real-time     | SignalR        | Real-time functionality. |
| 🔍 Search       | Elasticsearch  | Search + ranking. |

### 📦 Key .NET Libraries
- AutoMapper: Used for object-to-object mapping.
- EmailKit (or similar): Used for handling email functionality.
- EF packages: the libraries that used to set th EF Core in the environment and do migrations 
- SMS Sender Library

## 🏗️ Architecture & Patterns 

#### Clean Architecture
The project follows the principles of Clean Architecture to create independent, testable, and maintainable layers.

### Monolithic Architecture
The application is deployed as a single, unified codebase (Monolithic Architecture).

### Design Patterns
* Repositories Pattern: Abstracts the data access logic, making the application independent of the specific data source.

* Specifications Pattern: Used to encapsulate query logic and reusability, often in conjunction with the Repository Pattern.

## 📂 Project Structure
The solution is divided into the following layers, adhering to Clean Architecture principles:

    * **Domain Layer**: Contains enterprise-wide business rules, entities, and interfaces (the core of the application).

    * **Infrastructure Layer**: Contains implementation details for external dependencies (e.g., EF Core implementation, SQL Server connectivity, third-party service integration like Elasticsearch).

    * **Application Layer**: Contains application-specific business rules, command/query handlers, and DTOs. It orchestrates the flow between the Domain and Infrastructure layers.

    * **API Layer (Presentation)**: The entry point of the backend application, typically handling HTTP requests and utilizing the Application layer.

    * **Common Layer**: Contains shared utilities, exceptions, and helpers used across multiple layers.

    * **UI Layer (Angular Layer)**: The client-side application responsible for the presentation and user interaction.

## ⚙️ Getting Started
### Prerequisites
* .NET SDK (Specific Version)

* Node.js & npm (Specific Version)

* SQL Server Instance

* Docker Desktop


### 1. Repository Installation 

1. Clone the repository: 

```bash
cd path/to/any/folder
```
* then 

```bash
git clone "https://github.com/eng-alimallouhe/Syrian-Developers-Network-SPN-.git"
```

### 2. Docker Setup 
we use Docker Desktop to run Kibana, Elastic and Redis 
follow this steps to setup docker environment

    1. Install Docker Desktop Application 
    2. navigat to Docker Folder in the cloned repository, in this folder you will find file with name: **docker-compose.yml** if you could not find it create a new one then paste this code in it: 
    ```Json 
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

### 2. Backend Setup

#### Install Dependencies

- Navigate to the Backend folder and run the following command to install required dependencies:
- open the prject folder in CMD then write this command:
```bash
dotnet restore
```

#### Configure Database

- Update the appsettings.json file in the Backend project with your database connection string.
- Run the Application
```bash
dotnet run
```

- The backend API will be running at http://localhost:5000.

### 3. Frontend Setup

#### Install Dependencies

- Navigate to the Frontend folder and install the required Angular dependencies:
- open the UI folder in CMD then write this comman:
```bash
npm install
```

#### Run the Application

- To run the Angular application locally:
```bash
ng serve
```
or 
```bash
npm start
```

- The frontend will be running at http://localhost:4200
