# Assecor Assessment Test (DE)

## Implementation Notes

<details>
<summary><strong>Deutsche Version</strong></summary>

### Inhaltsverzeichnis

- [Datenquelle](#datenquelle)
- [Projektstruktur](#projektstruktur)
- [Architektur](#architektur)
- [Tests](#tests)

### ▶ Datenquelle <a id="datenquelle"></a>
Die Anwendung verwendet zwei unterschiedliche Datenquellen:
* CSV Datei, die im Rahmen der Aufgabe bereitgestellte war
* EF Core  

Die CSV Datei wird nur einmal beim Start der Anwendung eingelesen und in den Speicher geladen, um wiederholte Dateizugriffe bei jeder Anfrage zu vermeiden.  

Was POST Operationen angeht, sie sind für die CSV Datenquelle nicht implementiert, da die CSV-Datei gemäß den Anforderungen nicht verändert werden darf. Für EF Core hingegen ist eine POST Operation zur Erstellung einer „Person“ implementiert.

### ▶ Projektstruktur <a id="projektstruktur"></a>
Die Lösung ist in drei Schichten mit klar definierten Verantwortlichkeiten unterteilt:

- **Presentation**  
  Enthält die REST API Endpunkte und ist verantwortlich für Request Handling, Routing und die Erzeugung von HTTP Antworten.

- **Core**  
  Enthält die Domänenlogik, Entitäten, DTOs, Service Interfaces und die Mapping Logik.  
  Diese Schicht ist unabhängig von Infrastruktur-, API- oder Presentation-spezifischen Aspekten.

- **Infrastructure**  
  Enthält den CSV-Reader und die Repository Implementierungen und ist für den Datenzugriff zuständig.

### ▶ Architektur <a id="architektur"></a>
DTOs werden eingesetzt, um den API Vertrag von den internen Domänenentitäten zu entkoppeln.  
Das Mapping zwischen Entitäten und DTOs erfolgt zentral, um klare und konsistente Verantwortlichkeiten sicherzustellen.

<details>
<summary><strong>Projektstruktur anzeigen</strong></summary>

```
├── src
│   ├── API
│   │   ├── API.csproj
│   │   ├── Endpoints
│   │   │   └── PersonEndpoints.cs
│   │   └── Program.cs
│   ├── Core
│   │   ├── Core.csproj
│   │   ├── Dtos
│   │   │   ├── PersonCreateDto.cs
│   │   │   ├── PersonMapper.cs
│   │   │   └── PersonReadDto.cs
│   │   ├── Entities
│   │   │   └── Person.cs
│   │   ├── Enums
│   │   │   └── Color.cs
│   │   ├── Exceptions
│   │   │   ├── DataSourceUnavailableException.cs
│   │   │   └── UnknownColorException.cs
│   │   ├── Interfaces
│   │   │   ├── IPersonRepository.cs
│   │   │   └── IPersonService.cs
│   │   └── Services
│   │       └── PersonService.cs
│   └── Infrastructure
│       ├── Data
│       │   ├── Csv
│       │   │   ├── CsvPersonRepository.cs
│       │   │   └── PersonCsvReader.cs
│       │   └── Ef
│       │       ├── EFPersonRepository.cs
│       │       ├── Migrations
│       │       │   ├── 20260131165830_InitialCreate.Designer.cs
│       │       │   ├── 20260131165830_InitialCreate.cs
│       │       │   └── PersonDbModelSnapshot.cs
│       │       └── PersonDb.cs
│       ├── DependencyInjection
│       │   └── ServiceCollectionExtensions.cs
│       ├── Infrastructure.csproj
│       └── Options
│           ├── CsvOptions.cs
│           ├── DataSourceOptions.cs
│           └── EfOptions.cs
└── test
    ├── API.Tests
    │   ├── API.Tests.csproj
    │   └── EndpointTests.cs
    └── Infrastructure.Tests
        ├── Infrastructure.Tests.csproj
        └── PersonCsvReaderTest.cs
```
</details>

### ▶ Tests <a id="tests"></a>
Die Unit Tests konzentrieren sich auf die REST API, wie in der Aufgabenstellung gefordert. Der Ziel ist, korrektes HTTP Verhalten, passende Statuscodes und die zurückgegebenen Daten zu überprüfen. Zusätzlich wurden einige Unit Tests für die Infrastructure-Schicht implementiert, insbesondere für die Klasse "PersonCsvReader", um sicherzustellen, dass die CSV-Lese Logik und die Datenverarbeitung wie erwartet funktionieren.

Für manuelle Tests wurde außerdem die Swagger UI verwendet.
</details>


</details>

<details>
<summary><strong>English version</strong></summary>
  
## Table of Content 

- [Data Source](#data-source)
- [Project Structure](#project-structure)
- [Architecture](#architecture)
- [Testing](#testing)


### ▶ Data Source <a id="data-source"></a>
The application uses two types of data source:
* The CSV file provided by the assignment.  
* EF Core 

The CSV file is read only once during application startup and loaded into memor, as this avoids repeated disk access on each request. 

POST operations are not implemented for CSV data source. This is justified by the requirement that the CSV file shall not be modified.  However, for EF Core, a POST operation enabling the creation of a "Person" is implemented.

### ▶ Project Structure  <a id="project-structure"></a>
The solution is structured into three layers with clear responsibilities:

- **Presentation**  
  Contains the REST API endpoints and is responsible for request handling, routing, and HTTP response generation.

- **Core**  
  Contains the domain logic, entities, DTOs, service interfaces, and mapping logic.
  This layer is independent of any infrastructure/api/presentation concerns.

- **Infrastructure**  
  Contains the CSV reader and repository implementations. It is responsible for data access.

### ▶ Architecture <a id="architecture"></a>

DTOs are used to decouple the API contract from the internal domain entities.  
Mapping between entities and DTOs is handled centrally to keep responsibilities clear and consistent.

<details>
<summary><strong>Click to see the project tree</strong></summary>

```
├── src
│   ├── API
│   │   ├── API.csproj
│   │   ├── Endpoints
│   │   │   └── PersonEndpoints.cs
│   │   └── Program.cs
│   ├── Core
│   │   ├── Core.csproj
│   │   ├── Dtos
│   │   │   ├── PersonCreateDto.cs
│   │   │   ├── PersonMapper.cs
│   │   │   └── PersonReadDto.cs
│   │   ├── Entities
│   │   │   └── Person.cs
│   │   ├── Enums
│   │   │   └── Color.cs
│   │   ├── Exceptions
│   │   │   ├── DataSourceUnavailableException.cs
│   │   │   └── UnknownColorException.cs
│   │   ├── Interfaces
│   │   │   ├── IPersonRepository.cs
│   │   │   └── IPersonService.cs
│   │   └── Services
│   │       └── PersonService.cs
│   └── Infrastructure
│       ├── Data
│       │   ├── Csv
│       │   │   ├── CsvPersonRepository.cs
│       │   │   └── PersonCsvReader.cs
│       │   └── Ef
│       │       ├── EFPersonRepository.cs
│       │       ├── Migrations
│       │       │   ├── 20260131165830_InitialCreate.Designer.cs
│       │       │   ├── 20260131165830_InitialCreate.cs
│       │       │   └── PersonDbModelSnapshot.cs
│       │       └── PersonDb.cs
│       ├── DependencyInjection
│       │   └── ServiceCollectionExtensions.cs
│       ├── Infrastructure.csproj
│       └── Options
│           ├── CsvOptions.cs
│           ├── DataSourceOptions.cs
│           └── EfOptions.cs
└── test
    ├── API.Tests
    │   ├── API.Tests.csproj
    │   └── EndpointTests.cs
    └── Infrastructure.Tests
        ├── Infrastructure.Tests.csproj
        └── PersonCsvReaderTest.cs
```
</details>

### ▶ Testing <a id="testing"></a>
Unit tests focus on the REST interface, as required by the assignment. The goal is to verify correct HTTP behavior, response codes, and returned data. Besides, some unit tests were also implemented for the Infrastructure, especially for the PersonCsvReader class, to make sure the CSV reading logic and the data treatment behave as expected. 

Swagger UI was also used to perform manual testing steps.
</details>


## Zielsetzung

Das Ziel ist es ein REST – Interface zu implementieren. Bei den möglichen Frameworks stehen .NET(C#), Java oder Go zur Auswahl. Dabei sind die folgenden Anforderungen zu erfüllen:

* Es soll möglich sein, Personen und ihre Lieblingsfarbe über das Interface zu verwalten
* Die Daten sollen aus einer CSV Datei lesbar sein, ohne dass die CSV angepasst werden muss
* Alle Personen mit exakten Lieblingsfarben können über das Interface identifiziert werden

Einige Beispieldatensätze finden sich in `sample-input.csv`. Die Zahlen der ersten Spalte sollen den folgenden Farben entsprechen:

| ID | Farbe |
| --- | --- |
| 1 | blau |
| 2 | grün |
| 3 | violett |
| 4 | rot |
| 5 | gelb |
| 6 | türkis |
| 7 | weiß |

Das Ausgabeformat der Daten ist als `application/json` festgelegt. Die Schnittstelle soll folgende Endpunkte anbieten:

**GET** /persons
```json
[{
"id" : 1,
"name" : "Hans",
"lastname": "Müller",
"zipcode" : "67742",
"city" : "Lauterecken",
"color" : "blau"
},{
"id" : 2,
...
}]
```

**GET** /persons/{id}

*Hinweis*: als **ID** kann hier die Zeilennummer verwendet werden.
```json
{
"id" : 1,
"name" : "Hans",
"lastname": "Müller",
"zipcode" : "67742",
"city" : "Lauterecken",
"color" : "blau"
}
```

**GET** /persons/color/{color}
```json
[{
"id" : 1,
"name" : "Hans",
"lastname": "Müller",
"zipcode" : "67742",
"city" : "Lauterecken",
"color" : "blau"
},{
"id" : 2,
...
}]
```

## Akzeptanzkriterien

1. Die CSV Datei wurde eingelesen, und wird programmintern durch eine dem Schema entsprechende Modellklasse repräsentiert.
2. Der Zugriff auf die Datensätze so abstrahiert, dass eine andere Datenquelle angebunden werden kann, ohne den Aufruf anpassen zu müssen.
3. Die oben beschriebene REST-Schnittstelle wurde implementiert und liefert die korrekten Antworten.
4. Der Zugriff auf die Datensätze, bzw. auf die zugreifende Klasse wird über Dependency Injection gehandhabt.
5.  Die REST-Schnittstelle ist mit Unit-Tests getestet. 
6.  Die `sample-input.csv` wurde nicht verändert 

## Bonuspunkte
* Implementierung als MSBuild Projekt für kontinuierliche Integration auf TFS (C#/.NET) oder als Maven/Gradle Projekt (Java)
* Implementieren Sie eine zusätzliche Methode POST/ Personen, die eine zusätzliche Aufzeichnung zur Datenquelle hinzufügen
* Anbindung einer zweiten Datenquelle (z.B. Datenbank via Entity Framework)

Denk an deine zukünftigen Kollegen, und mach es ihnen nicht zu einfach, indem du deine Lösung öffentlich zur Schau stellst. Danke!

# Assecor Assessment Test (EN)

## goal

You are to implement a RESTful web interface. The choice of framework and stack is yours between .NET (C#), Java or Go. It has to fulfill the following criteria:

* You should be able to manage persons and their favourite colour using the interface
* The application should be able to read the date from the CSV source, without modifying the source file
* You can identify people with a common favourite colour using the interface

A set of sample data is contained within `sample-input.csv`. The number in the first column represents one of the following colours:

| ID | Farbe |
|---|---|
| 1 | blau |
| 2 | grün |
| 3 | violett |
| 4 | rot |
| 5 | gelb |
| 6 | türkis |
| 7 | weiß |

the return content type is `application/json`. The interface should offer the following endpoints:

**GET** /persons
```json
[{
"id" : 1,
"name" : "Hans",
"lastname": "Müller",
"zipcode" : "67742",
"city" : "Lauterecken",
"color" : "blau"
},{
"id" : 2,
...
}]
```

**GET** /persons/{id}

*HINT*: use the csv line number as your **ID**.
```json
{
"id" : 1,
"name" : "Hans",
"lastname": "Müller",
"zipcode" : "67742",
"city" : "Lauterecken",
"color" : "blau"
}
```

**GET** /persons/color/{color}
```json
[{
"id" : 1,
"name" : "Hans",
"lastname": "Müller",
"zipcode" : "67742",
"city" : "Lauterecken",
"color" : "blau"
},{
"id" : 2,
...
}]
```

## acceptance criteria

1. The csv file is read and represented internally by a suitable model class.
2. File access is done with an interface, so the implementation can be easily replaced for other data sources.
3. The REST interface is implemented according to the above specifications.
4. Data access is done using a dependency injection mechanism
5. Unit tests for the REST interface are available.
6. `sample-input.csv` has not been changed.

## bonus points are awarded for the following
* implement the project with MSBuild in mind for CI using TFS/DevOps when using .NET, or as a Maven/Gradle project in Java
* Implement an additional **POST** /persons to add new people to the dataset
* Add a secondary data source (e.g. database via EF or JPA)

Think about your potential future colleagues, and do not make it too easy for them by posting your solution publicly. Thank you!



