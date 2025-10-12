A Fullstack Real Estate application with a .NET API, MongoDB database, and a React frontend. This README documents how to run the database, backend, frontend, and tests locally.

---

### Prerequisites
- **MongoDB** installed and available on the host.  
- **.NET 9 SDK** installed.  
- **Node.js and npm** installed.  
- PowerShell available for running the provided commands.

---

### Database
- **Start MongoDB** using the system command line:  
```bash
mongod
```
**If mongod is not yet configured**
```bash
md C:\data\db
"C:\Program Files\MongoDB\Server\{MongoDB_VERSION}\bin\mongod.exe" --dbpath "C:\data\db"
```

- **MongoDB URL**: http://localhost:27017

### Mongo Database Import

1. Install mongoimport dependences (MongoShell and Mongo Database Tools)

https://www.mongodb.com/try/download/shell
https://www.mongodb.com/try/download/database-tools

2. Manual import with mongoimport:

```bash
mongoimport --uri="mongodb://localhost:27017/realestate" --collection=owners --file=db/fixtures/realestate.owners.json --jsonArray --drop
mongoimport --uri="mongodb://localhost:27017/realestate" --collection=properties --file=db/fixtures/realestate.properties.json --jsonArray --drop
mongoimport --uri="mongodb://localhost:27017/realestate" --collection=propertyImages --file=db/fixtures/realestate.propertyImages.json --jsonArray --drop
mongoimport --uri="mongodb://localhost:27017/realestate" --collection=propertyTraces --file=db/fixtures/realestate.propertyTraces.json --jsonArray --drop
```

- Helper Script:
./scripts/import-fixtures.sh

#### Backup and Fixtures
- **Backup folder**: ./backup — contains a JSON export of the four collections.  
- **Fixtures folder**: ./db/fixtures — contains JSON files to import as fixtures into MongoDB. (Required for Mongo Database Import)

---

### Run Backend (API)
From the repository root:

1. **Build the API** (PowerShell):  
```powershell
dotnet restore
dotnet build
```

2. **Run the API** (PowerShell):  
```powershell
dotnet run --project .\RealEstate.Api\RealEstate.Api.csproj
```

- **Default API URL**: http://localhost:5093
- **Properties list API URL**: GET http://localhost:5093/api/properties
- **Property Details API URL**: GET http://localhost:5093/api/properties/{IdProperty}

---

### Run Frontend (React)
From the repository root change into the view folder or run from root depending on setup:

1. Change directory to the frontend folder if needed:  
```bash
cd view
```

2. Install dependences:  
```bash
npm install
```

3. **Start development server**:  
```bash
npm run dev
```

- **Default Web UI URL**: http://localhost:5173

---

### Run Tests
#### Backend Unit Tests (NUnit)
From the repository root (PowerShell):  
```powershell
dotnet test ./tests/RealEstate.Api.Tests/RealEstate.Api.Tests.csproj
```

#### Frontend Tests (Vitest)
From the frontend folder (view):  
```bash
cd view
npm run test
```

---

### Important URLs
- **Frontend**: http://localhost:5173  
- **API**: http://localhost:5093  
- **MongoDB**: http://localhost:27017

---

### Useful Commands for Importing JSON Fixtures
- Import a collection JSON file to MongoDB using mongoimport (example):  
```bash
mongoimport --db <your-db-name> --collection <collection-name> --file ./db/fixtures/<file>.json --jsonArray
```

---

### Notes and Tips
- Ensure `mongod` is running before starting the API and before importing fixtures.  
- Confirm ports 27017, 5093 and 5173 are free or adjust configuration accordingly.  
- If the frontend or backend expect environment variables, check for `.env` or configuration files in the project and set them before running.  
- Use the contents of ./backup if you need a full database restore; use ./db/fixtures for targeted fixture imports.

---
