# BeEmote

### Needed Packages (Nugget install)

There are several packages that you need for this project to work properly.

To install everything without getting any headache :-), go to the nuget package console
In the menu bar: Tools > NuGet Package Manager > Package Manager Console
Once in the console, type:

    Update-Package -reinstall

If asked wheter to override an existing files, simply **type A** (Override ALL)

List of packages installed:
- Newtonsoft.Json
- Dapper
- Fody
- PropertyChanged.Fody
- MSTest.TestAdapter
- MSTest.TestFramework
- Castle.Core
- Moq

### Local Database

We use MySql.

> I use the root user here but you may use another one depending on your mysql usage.

Prepare to type the password corresponding to your root login, after entering each of the following commands.

First step is creating a user `beemote` with adequat privileges.
In the console (while in the solution folder):

    mysql -uroot -p < beemote_user.sql

Then, you need to have a localdatabase named `beemote` as well.
    
    mysql -uroot -p < beemote_db.sql


### Connection strings

Make a copy of the Example files in the BeEmote.Services project, in the Security folder.
Then rename them so that you end up with the following files.
- Example.ConnectionStrings.cs
- Example.Credentials.cs
- ConnectionStrings.cs
- Credentials.cs

In the 2 last files do:
- Remove ".Example" from the namespace.
- Replace the Snake_case_example_strings with proper values.
    - replace Host_Name with localhost
    - replace Database_Name with beemote
    - etc...

Should be good to go now.
