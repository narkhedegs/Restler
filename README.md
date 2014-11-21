# Restler

Restler is a command-line collection runner for applications like DevHttpClient and Postman. It allows you to effortlessly run a DevHttpClient repository directly from the command-line. It is built with extensibility in mind so that you can easily create AddIns to fit your needs.

## Table of Contents

 - [Installation](#installation)
 - [AddIns](#addins)
 - [Available AddIns](#available-addins)

## Installation

Restler is built on .NET platform using C#. To run Restler, make sure that you have .NET framework version 4.0 or higher installed on your computer.

### Install from nuget

The easiest way to install Restler is through nuget. You can install nuget from [here](https://www.nuget.org/). Once the nuget is installed you can type following command at the commnd prompt to install Restler.
```cmd
nuget install restler
```
![alt text](http://i.imgur.com/Rerw0ZK.png "restler install from nuget")
Restler is a Tool Only nuget package.

### Install from a zip file

You can also install Restler by downloading a zip file from here and extracting it at the desired location on your computer.

### What's Included

Within the download you'll find the following directories and files. You'll see something like this:
![alt text](http://i.imgur.com/vhxkVsn.png "restler whats included")
 - Restler.exe is the Restler executable file.
 - AddIns is the directory to install all the AddIns. By default there are no AddIns installed and the AddIns directory is empty. It is very easy to install AddIns. Please refer the AddIns section in this documentation.
 - All other dll files are Restler dependencies.

## AddIns

Most of the Restler functionality is developed as AddIns. Restler AddIns are nuget packages. 

### How to Install an AddIn

By default there are no AddIns installed. The Command Line Interface of Restler can detect any installed AddIns and can display a list of different AddIns. Run the help command from command prompt to see a list of installed AddIns. 
```cmd
restler -h
```
![alt text](http://i.imgur.com/eLyqQzg.png "restler help")

As you can see there are no Parsers or AddIns installed. Only a default RestRequestCollectionRunner is installed which comes with Restler (You can also write your own if you want). Lets install a DevHttpClientRepositoryParser. DevHttpClientRepositoryParser parses the repository JSON file produced by [Dev Http Client Chrome Extension](https://chrome.google.com/webstore/detail/dhc-resthttp-api-client/aejoelaoggembcahagimdiliamlcdmfm?hl=en). You can write your own parsers. To install any AddIn cd in to the AddIns directory and type follwing command from command prompt.
```cmd
nuget install <AddIn Name>
```
For Example
```cmd
nuget install RestApiTester.Parsers.DevHttpClientRepositoryParser
```
![alt text](http://i.imgur.com/cSQRjoS.png "restler install addins")

Run the help command again and you will see that the Restler command line interface now shows DevHttpClientRepositoryParser under Available Parsers.
![alt text](http://i.imgur.com/df3Gz5x.png "restler help showing addins list")

Check out the list of all the available AddIns [here](#available-addins).


## Available AddIns

 - [RestApiTester.Parsers.DevHttpClientRepositoryParser ](https://www.nuget.org/packages/RestApiTester.Parsers.DevHttpClientRepositoryParser/)
 - [RestApiTester.AddIns.BasicAuthenticationAddIn](https://www.nuget.org/packages/RestApiTester.AddIns.BasicAuthenticationAddIn/)