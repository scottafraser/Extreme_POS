# Extreme Point of Sale

#### Epicodus C# group project 07.26.18

#### By Scott Fraser, Thad Donaghue, David Ngo and Matt Smith

## Description

A .NET web app that allows a server to select tables in a restaurant and place orders

## User Stories

* As a user, I need to log in
* As a user, I need to choose a table to associate with the order
* As a user, I need to choose a person/seat at the table to order food/drinks for
* As a user, I need to order a variety of food/drinks for the person/seat at the table
* As a user, I need to be able to order food/drinks for other people at the same table
* As a user, I need to be able to exit a table where food/drinks have been ordered and select another table and start the process anew
* As a user, I need to have multiple active tables that can have active food/drink orders appended
* As a user, I need to be able to close a table, generate a ticket total and save that to the restaurant history.
* As a user, I need to be able to select a closed table and start an entirely new ticket

## Upcoming features

* implement user login
* implement food and drink modifiers
* add the ability to split checks by seat or by fractions
* add the ability to transfer tickets between servers
* add the ability to transfer ordered items between seats
* implement admin privileges to add users/servers, menu items and modifiers


## Setup on OSX

* Download and install .Net Core 1.1.4
* Download and install Mono
* Download and install MAMP 4.5
* Clone the repo
* Open MAMP and start servers
* Import database "POS" PHPMyAdmin
* Run `dotnet restore` from project directory and test directory to install packages
* Run `dotnet build` from project directory and fix any build errors
* Run `dotnet test` from the test directory to run the testing suite
* Run `dotnet run` to run application



## Contribution Requirements

1. Clone the repo
1. Make a new branch
1. Commit and push your changes
1. Create a PR

## Technologies Used

* .Net Core 1.1.4
* MAMP 4.5
* MySql
* Bootstrap 3.3.7

## Links

* https://github.com/scottafraser/Extreme_POS

## License

This software is licensed under the MIT license.

Copyright (c) 2018 **Scott Fraser, Thad Donaghue, David Ngo and Matt Smith**
