# Project Convention

## Backend Team Convention

This document outlines the team agreements for the **Backend Development** of the project. These agreements are set to ensure code consistency, readability, and maintainability across the project.

### Naming Convention:

#### 1. Properties:
- All property names should start with an **uppercase letter**.
- This ensures consistency and readability in property names.

#### 2. Private Variables:
- Private variable names should begin with an **underscore (_) ** to distinguish them from public properties and methods.

#### 3. Interfaces:
- Interfaces should begin with a capital **I** followed by a descriptive name that represents the service or functionality.

#### 4. Method and Function Naming:
- Method names should be in **PascalCase**, starting with a capital letter.

#### 5. Class Naming:
- Class names should be in **PascalCase**, representing the entity or concept it models.

### Code Style and Practices:

#### 1. Consistent Indentation:
- Use **4 spaces** for indentation in all code files.
- Avoid mixing spaces and tabs.

#### 2. Braces Style:
- Always place braces on the next line after the control statement.

#### 3. Exception Handling:
- Use **try-catch** blocks to handle exceptions where necessary.
- For logging exceptions, use the **Logger** library.

---

## Frontend Team Convention

This document outlines the team agreements for the **Frontend Development** of the project using **Angular**. These agreements are designed to ensure code consistency, maintainability, and smooth collaboration within the team.

### Naming Conventions:

#### 1. Component Naming:
- **Component names** should be in **PascalCase** and describe the component’s functionality.
- Component file names should follow the pattern: `component-name.component.ts`.

#### 2. Service Naming:
- **Service names** should be in **PascalCase** and always end with `Service`.
- Service file names should follow the pattern: `service-name.service.ts`.

#### 3. Variable Naming:
- **Local variable names** should be in **camelCase**.
- **Constants** should be in **UPPER_SNAKE_CASE**.

#### 4. Interface Naming:
- **Interface names** should start with the letter **I** followed by a descriptive name in **PascalCase**.

#### 5. File and Folder Naming:
- Use **kebab-case** for file and folder names.
- Each component, service, or module should be in its own folder with the appropriate files.

#### 6. Observable Naming:
- Observable names should start with the **$** followed by a descriptive name in **camelCase**.

### Code Style and Practices:

#### 1. Indentation and Spacing:
- Use **2 spaces** for indentation, do not use tabs.
- Ensure consistent spacing around operators and inside parentheses.

#### 2. Braces Style:
- Always use braces for all control structures, even if the body is a single line.

---

## General Convention:

### Branching Strategy:
- Use **feature branches** for each new feature or bug fix.

#### Naming conventions for branches:
- **Feature branches**: `feature/feature-name`
- **Bug fix branches**: `bugfix/bug-name`
- **Hotfix branches**: `hotfix/hotfix-name`
- **Build a new Part**: `build/part-name`

#### 3. Commenting Code:
- Use **//** for any comment (one line or multi-line).
- Always document public methods with **XML comments**.
