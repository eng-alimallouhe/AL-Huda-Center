🚀 ProConnect: Professional Networking and Collaboration Platform

A full-stack web application designed for professional networking, social interaction, and project collaboration, built on the principles of Clean Architecture.

🌟 Table of Contents

About

Features

Technology Stack

Architecture & Patterns

Getting Started (Running the Application)

Project Structure

Usage

License

💡 About

ProConnect is a robust networking platform offering personalized profiles, real-time social engagement, a specialized job board, and a powerful system for professional collaboration.

The platform is engineered using the .NET Platform and Angular, strictly adhering to Clean Architecture principles to ensure maintainability, scalability, and independent testing across all layers. Elasticsearch is utilized to power high-speed searching and content ranking.

✨ Features

👤 Profile & Resume System

Customizable User Profile: Users can define personal info, skills, and professional experience.

Multi-Resume Builder: Users can create up to 5-6 fully customizable Resumes, select different styles and languages, and export them as PDF.

Job Application Integration: Built-in Resumes are used directly for job applications.

📢 Social & Community

Content Sharing: Users can post their ideas, opinions, and life issues.

Problem-Solving Forum: Users can post problems for the community, and the poster can mark the Best Solution.

Real-time Interaction: Powered by SignalR for live updates and notifications.

Community Management: Users can join interest-based communities and invite followers, based on defined community policies.

💼 Jobs Section

Detailed Job Posting: Job owners can set crucial details like duration, min/max salary, and other important information.

Applicant Management: Other users can apply for jobs by submitting a request that includes their selected platform-built Resume.

💻 Project Collaboration (New Feature)

Project Sharing: Users can share their personal or professional projects.

Collaboration: Users can invite other users to collaborate on projects.

Documentation: Projects include a dedicated space for writing a Project README file to display documentation.

Metadata: Projects are linked with skills and tags for easy discoverability.

Tracking: Ability to add Milestones for progress tracking.

Links & Settings: Users can share Source Code Link and Publish Link, and set the Project Type and License.

🛠️ Technology Stack

Category

Technology

Role in Project

Backend

.NET Platform (C#)

Core API development and business logic.

Frontend

Angular / TypeScript

UI Layer and client-side logic.

Real-time

SignalR

Enables real-time, two-way communication.

Database

SQL Server

Primary relational data store.

ORM

EF Core

Data access implementation for SQL Server.

Search/Ranking

Elasticsearch

High-speed searching and Feed Ranker engine.

.NET Libraries

AutoMapper, EmailKit, etc.

Utility and external service integrations.

🏗️ Architecture & Patterns

The platform utilizes a Monolithic Architecture structured around Clean Architecture principles for a highly maintainable and testable codebase.

Project Layers

Domain Layer: Core business rules, entities, and interfaces.

Infrastructure Layer: Implementation of external resources (EF Core, SQL Server, Elasticsearch).

Application Layer: Application logic, coordinating data flow (Commands, Queries, DTOs).

API Layer: The RESTful interface and entry point for the backend.

Common Layer: Shared utilities and helpers.

UI Layer (Angular Layer): Presentation layer (client-side).

Design Patterns Used

Repositories Pattern: Abstracts data access from the Application/Domain layers.

Specifications Pattern: Encapsulates and reuses complex query logic.

⚙️ Getting Started (Running the Application)

Follow these steps to set up and run ProConnect locally.

Prerequisites

You must have the following installed:

1. .NET SDK (Required Version)

2. Node.js & npm (LTS Version)

3. SQL Server Instance

4. Elasticsearch Instance (Must be running and accessible for search features to work)

🚀 Step 1: Clone the Repository

git clone [repository-url]
cd [project-root-folder]


🗄️ Step 2: Configure and Run the Backend (.NET API)

Database Configuration:

Navigate to the API Layer folder.

Open appsettings.json and update the ConnectionStrings to point to your local SQL Server instance.

Apply Migrations:

In the terminal, execute the EF Core migration command:

dotnet ef database update --project InfrastructureLayer


Run the API:

Start the backend service:

dotnet run --project APILayer


The API should start running (e.g., at https://localhost:5001).

🌐 Step 3: Configure and Run the Frontend (Angular UI)

Install Dependencies:

Navigate to the UI Layer (Angular) folder.

Install the required Node packages:

npm install


Configure API Endpoint:

Verify the API endpoint configuration in the Angular environment file to ensure it points to the running backend (e.g., https://localhost:5001).

Start the UI:

Run the Angular development server:

ng serve -o


The application will automatically open in your browser (typically http://localhost:4200).

💻 Usage

Once running, you can:

Register or Log in.

Navigate to your Profile to start building Resumes and adding skills.

Explore the Projects section to share your work and find collaborators.

Use the main feed to post and engage in community problem-solving.

📄 License

[Specify License Type here, e.g., MIT, Apache 2.0]
