## Table of Contents

* [Description](#-description-)
* [How to run](#-how-to-run-)
* [Custom connectionstring](#-custom-connectionstring-)

<h1> XML-Funtime </h1>

<h2> Description </h2>

A solution to

* write a boolean to XML files in `C:\Orders`
* read said files and write the boolean, filename and the entire XML to an SQL Database
* view that data in a Web App

<h2> How to run </h2>

<i>I used Visual Studio, I suggest you do the same. Just run them in the same order.</i>
* Check out this repo (duh)
* Run these .exe's in order:
  1. CreateDB.exe
  1. OrderGenerator.exe
  1. OrderReader.exe (this is on a 10s timer, so wait a little before closing or just let it run in the background)
  1. OrderViewer.exe (this starts the server)
* Open the OrderViewer WebApp by visiting localhost on the port it told you it was hosting on
* Use the little input box at the top to filter on Date (exactly as displayed, it's a string)

<h2> Custom connectionstring </h2>

Want to use a custom connectionString instead of the default?

Just add a file `config.xml` to
* CreateDB\bin\Release\net6.0
* OrderReader\bin\Release\net6.0
* OrderViewer\bin\Release\net6.0

It should be structured like this:
```xml
<settings>
	<connectionString>YourConnectionStringHere</connectionString>
</settings>
```
