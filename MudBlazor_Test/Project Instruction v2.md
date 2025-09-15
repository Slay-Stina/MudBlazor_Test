\# Project Instruction



\### Introduction



We need a \*\*Settings\*\* page in our application \*\*OSST Lite\*\* where we can manage our lines. The application will be able to create, list, edit, and delete lines. The application will be developed with a \*\*mobile-first\*\* approach, but it will also be usable on tablets and desktops.



-----



\### Basic functions



&nbsp; \* \*\*Add a line\*\* with the following information: `Name`, `IpAddress`, `PortNumber`, `IsDefault`, `IsSelected`

&nbsp; \* \*\*List all lines\*\*, showing every attribute

&nbsp; \* \*\*Edit a line\*\* to update the following information: `Name`, `IpAddress`, and `PortNumber`

&nbsp; \* \*\*Change which line is the default line\*\* via a button

&nbsp; \* \*\*Change which line is selected\*\* via a button

&nbsp; \* \*\*Delete a line\*\*



-----



\### User Interface



Create a simple UI to display all lines. The UI should clearly indicate which line is the default and which line is the selected one. Each line should include buttons to \*\*Edit\*\*, \*\*Delete\*\*, \*\*Set as Default\*\*, and \*\*Set as Selected\*\*.



-----



\### Technical Demands



&nbsp; \* \*\*.NET 9\*\*

&nbsp; \* \*\*Blazor Web App\*\*

&nbsp;     \* Interactive render mode: `Server`

&nbsp;     \* Interactivity location: `Per page/component`

&nbsp; \* \*\*GitHub repository\*\*

&nbsp;     \* Repository set to `private`

&nbsp;     \* Pull Requests (PL) / Merge requests (MR) should be made per features

&nbsp;     \* Commits should reference tasks

&nbsp; \* \*\*UI styling\*\*

&nbsp;     \* Use \*\*Mudblazor\*\* or separate `.css` files for each component/page

&nbsp; \* \*\*Communications\*\* via `socket (TCP)`



-----



\### Naming Conventions



All variable names, method names, and comments should be written in \*\*English\*\*. Abbreviations should be avoided unless necessary (for example, if the full name would be excessively long). Names should be clear and descriptive to make the code easy to understand. Code should be structured and written in a readable way, with proper indentation, spacing, and logical organization, so that others can easily follow and maintain it.



-----



\### Documentation



\*\*Comment the code when necessary\*\*. For example, if a function name does not fully explain its purpose and the function cannot be broken down further, add comments to clarify its behavior.



-----



\### Architecture



\#### Flow



Example when creating a line:



1\.  Client sends request to API Controller using `Contracts CreateLineRequest`.

2\.  API Controller receives request, mapping `CreateLineRequest` to `LineModel` and validates the information.

3\.  API sends `LineModel` to Application by calling `LineService.CreateLine(LineModel)`.

4\.  Application maps `LineModel` to `Line entity (Domain)` and validates business logic, for example, if the line already exists in the database or if the user who wants to create the line is authorized to create a line.

5\.  Application saves the line in the database by calling `LineRepository.Add(Line)`.



\#### Client



\*\*Blazor Web App\*\* sends requests and receives responses by referencing `Contracts` project.



\#### API



Routing via \*\*Controllers\*\* and map the request to the correct entity model and validates that the information is as expected. When validated, API sends the entity model to Application Services.



\#### Contracts



Has request and response models for communication between clients and API.



\#### Application



Receives entity models from API, validates business logic requirements. If successful, map entity models to an entity and “sends” it to Repositories.



Repositories save the entity in the database. Repository is usually separated as an own \*\*Infrastructure\*\* project, but not necessary for this assignment.



\#### Database



This can be created by writing a fake database project that opens a socket. It validates the request and saves the line in a `.json` file or a text file written as JSON text.



```json

{

&nbsp; "DefaultLine": {

&nbsp;   "Name": "Default Line",

&nbsp;   "IpAddress": "123.456.789.10",

&nbsp;   "Portnumber": 1234,

&nbsp;   "IsDefault": true,

&nbsp;   "IsSelected": false

&nbsp; },

&nbsp; "Lines": \[

&nbsp;   {

&nbsp;   "Name": "Default Line",

&nbsp;   "IpAddress": "123.456.789.10",

&nbsp;   "Portnumber": 1234,

&nbsp;   "IsDefault": true,

&nbsp;   "IsSelected": false

&nbsp;   },

&nbsp;   {

&nbsp;     "Name": "Another Line",

&nbsp;     "IpAddress": "987.654.32.10",

&nbsp;     "Portnumber": 4567,

&nbsp;     "IsDefault": false,

&nbsp;     "IsSelected": true

&nbsp;   }

&nbsp; ]

}

```



-----



\### Way of Working



\#### Version management



There will be a `Master/Main` branch that will be seen as the “published” branch. This branch will only be updated through MRs approved by Jonna.



The students will have their own “mini” `Master/Main` branch. This branch will only be updated through MRs approved by both students.



\#### Meetings



Every \*\*Monday\*\*, the students will plan their week by choosing tasks they estimate can be completed within the week. They will present these tasks to Jonna in a meeting, taking turns leading it. Together, we will go through any questions or clarifications needed.



\#### Follow-ups



Throughout the week, we will stay in contact via \*\*Discord\*\*. On \*\*Thursday evenings\*\*, we will have a short check-in about how the week went: what problems we encountered and how we solved them, whether any tasks or working methods need to be adjusted, and so on.



Midway through the \*\*LIA period\*\*, each student will have an individual check-in to review expectations and progress toward goals. In the final week, we will hold an individual exit interview to provide and discuss final feedback.



-----



\### Problems



If any issues arise during the LIA period, please contact Jonna by phone, email, or via Discord.



-----



\### Miscellaneous



Working hours are flexible. We are generally available between \*\*9:00 and 16:00\*\*, but if you prefer to work 9:00–18:00 or 7:00–16:00, that is up to each individual. If you are sick or unable to work, or if you have an appointment, please inform Jonna.

