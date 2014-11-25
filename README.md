# Restler

Restler is a command-line collection runner for applications like DevHttpClient and Postman. It allows you to effortlessly run a DevHttpClient repository directly from the command-line. It is built with extensibility in mind so that you can easily create AddIns to fit your needs.

## Table of Contents

 - [Installation](#installation)
 - [AddIns](#addins)
 - [Running a Rest Request Collection](#running-a-rest-request-collection)
 - [Options](#options)
 - [Available AddIns](#available-addins)
 - [To Do](#to-do)

## Installation

Restler is built on .NET platform using C#. To run Restler, make sure that you have .NET framework version 4.0 or higher installed on your computer.

### Install from chocolatey

The easiest way to install Restler is through chocolatey. You can install chocolatey from [here](https://chocolatey.org/). Once the chocolatey is installed you can type following command at the commnd prompt to install Restler.
```cmd
choco install restler
```
![alt text](http://i.imgur.com/h0IDO9q.png "restler install from chocolatey")

### Install from a zip file

You can also install Restler by downloading a zip file from [here](https://drive.google.com/file/d/0B1en8SOjvkGZZHdCU0xaRWdyaFE/view?usp=sharing) and extracting it at the desired location on your computer.

### What's Included

Within the download you'll find the following directories and files. You'll see something like this:
![alt text](http://i.imgur.com/dmFoXbV.png "restler whats included")
 - Restler.exe is the Restler executable file.
 - All other dll files are Restler dependencies.

## AddIns

Most of the Restler functionality is developed as AddIns. Restler AddIns are nuget packages. 

### How to Install an AddIn

By default there are no AddIns installed. The Command Line Interface of Restler can detect any installed AddIns and can display a list of different AddIns. Run the help command from command prompt to see a list of installed AddIns. 
```cmd
restler -h
```
![alt text](http://i.imgur.com/Ic8hhii.png "restler help")

As you can see there are no Parsers or AddIns installed. Only a default RestRequestCollectionRunner is installed which comes with Restler (You can also write your own if you want). Lets install a DevHttpClientRepositoryParser. DevHttpClientRepositoryParser parses the repository JSON file produced by [Dev Http Client Chrome Extension](https://chrome.google.com/webstore/detail/dhc-resthttp-api-client/aejoelaoggembcahagimdiliamlcdmfm?hl=en). You can write your own parsers. To install any AddIn type follwing command from command prompt.
```cmd
restler --installAddIn <AddIn Name>
```
For Example
```cmd
restler --installAddIn RestApiTester.Parsers.DevHttpClientRepositoryParser
```
![alt text](http://i.imgur.com/LOmrkWw.png "restler install addins")

Run the help command again and you will see that the Restler command line interface now shows DevHttpClientRepositoryParser under Available Parsers.
![alt text](http://i.imgur.com/sGXxTeC.png "restler help showing addins list")

Check out the list of all the available AddIns [here](#available-addins).

## Running a Rest Request Collection

In this section we will see how to run a Rest Request Collection like Dev Http Client Repository JSON file. Procedure for running a Rest Request Collection should be similar for other collections like Postman or any other custom collection that you may have.

We have a simple Dev Http Client Repository to test Reddit API.

![alt text](http://i.imgur.com/jd4xQ2b.png "restler simple dhc rest request collection")

We can get follwoing JSON file after exporting this collection to JSON. Save this JSON file as reddit-collection.json.
```json
{
  "version": 3,
  "nodes": [
    {
      "id": "8CB601B3-4088-41E6-869D-DE31FEEB717B",
      "lastModified": "2014-11-21T16:51:57.860-05:00",
      "name": "Reddit",
      "type": "Project"
    },
    {
      "id": "AED2FBFA-9C76-4760-AFDF-E1029D0C7627",
      "lastModified": "2014-11-21T16:50:59.108-05:00",
      "name": "Get Hot ",
      "headers": [],
      "method": {
        "link": "http://www.w3.org/Protocols/rfc2616/rfc2616-sec9.html#sec9.3",
        "name": "GET"
      },
      "body": {
        "autoSetLength": true,
        "bodyType": "Text"
      },
      "headersType": "Form",
      "type": "Request",
      "uri": {
        "path": "www.reddit.com/r/hot.json",
        "scheme": {
          "name": "http",
          "version": "V11"
        }
      },
      "parentId": "8CB601B3-4088-41E6-869D-DE31FEEB717B"
    },
    {
      "id": "AFE055DF-5F7B-42D9-9B54-62A7F25029CA",
      "lastModified": "2014-11-21T16:51:57.861-05:00",
      "name": "Get Top for C#",
      "headers": [],
      "method": {
        "link": "http://www.w3.org/Protocols/rfc2616/rfc2616-sec9.html#sec9.3",
        "name": "GET"
      },
      "body": {
        "autoSetLength": true,
        "bodyType": "Text"
      },
      "headersType": "Form",
      "type": "Request",
      "uri": {
        "path": "www.reddit.com/r/csharp/top.json",
        "scheme": {
          "name": "http",
          "version": "V11"
        }
      },
      "parentId": "8CB601B3-4088-41E6-869D-DE31FEEB717B"
    }
  ]
}
```
Type following command in the command prompt to run our reddit collection using Restler. Please refer to the [Options](#options) section in this documentation to learn about all the command line options available for Restler.
```cmd
restler --collection reddit-collection.json --parser DevHttpClientRepositoryParser
```
Restler will run all the REST Requests in reddit-collection.json file. You should see an output similar to the following screenshot.

![alt text](http://i.imgur.com/PEvdhli.png "restler simple collection run output")

## Options

```text
Options:
  --collection       Specify path to REST Request Collection file.
  --parser           Specify parser for parsing collection file.
  --configuration    Specify configuration as a JSON file.
  --environment      Specify one of the environments in your configuration.
  --installAddIn     Specify name of the AddIn to be installed.
  --help             Displays this help screen.
```

## Available AddIns

 - [RestApiTester.Parsers.DevHttpClientRepositoryParser ](https://www.nuget.org/packages/RestApiTester.Parsers.DevHttpClientRepositoryParser/)
 - [RestApiTester.AddIns.BasicAuthenticationAddIn](https://www.nuget.org/packages/RestApiTester.AddIns.BasicAuthenticationAddIn/)

## To Do

 - Complete the documentation. Write documentation about the configuration file.
 - Remove some dependencies like FluentValidation, Ninject and Command Line Parser.
 - Use ILMerge to create a single executable file for Restler.
 - Create HTML Report Generator Add In.
 - Create TFS Integration Add In.
 - Write parser for Postman collection.