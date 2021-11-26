## Table of Contents

* [Description](#-description-)
* [How to run](#-how-to-run-)
* [Custom connectionstring](#-custom-connectionstring-)

<h1> XML-Funtime </h1>

<h2> Description </h2>
A .NET solution to:
* write a boolean to XML files in C:\Orders
* read said files and write the boolean, filename and the entire XML to a SQL Database
* view that data in a Web App

<h2> How to run </h2>

<i>I used Visual Studio, I suggest you do the same</i>
* Check out this repo (duh)
* Build the solution, so you get the .exe's
* Run these .exe's in order:
  1. CreateDB.exe
  1. OrderGenerator.exe
  1. OrderReader.exe
* Open the OrderViewer WebApp
* Use the little input box at the top to filter on Date (exactly as displayed, it's a string)

<h2> Custom connectionstring </h2>

Want to use a custom connectionString instead of the default?

Just add a file `config.xml` to
* CreateDB\bin\Release\net6.0
* OrderReader\bin\Release\net6.0
* OrderViewer\bin\Release\net6.0

As I search for 'settings', then 'connectionString', it should be structured like this:
```xml
<settings>
	<connectionString>YourConnectionStringHere</connectionString>
</settings>
```
