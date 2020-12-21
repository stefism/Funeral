## :eyeglasses: Project Introduction

**Funeral Website** is my defense project for **ASP.NET Core** course at [SoftUni](https://softuni.bg/ "SoftUni") (October-December 2020). The site allows the users to create and print an obituary on a pre-generated template, which can then be modified.

## :pencil2: Overview

The **Funeral Website** is entirely in Bulgarian with a focus on Bulgarian users.

I used the built-in **.NET** system for identity, as almost all pages of the identity were scaffolded and modified. The site has an administration section and when a user with administrative rights logs in to the site he gets access to the administration options. The administrator has the ability to add, edit and remove visual elements from the obituary template. It also has the ability to see information about users of the site such as their e-mails, the number of obituaries ordered, as well as to check the content uploaded by the user.

When writing the code I tried to follow the best practices for **Object Oriented design** and **high-quality code** for the Web application. I also tried to comply the **OOP principles** like data encapsulation, inheritance, abstraction and polymorphism and follow the principles of strong cohesion and loose coupling.
The **Funeral Website** is prevent from security vulnerabilities like SQL Injection, XSS, CSRF, parameter tampering, etc.

## :hammer: Built With
- ASP.NET [CORE 3.1](https://dotnet.microsoft.com/download/dotnet-core/3.1 "CORE 3.1") MVC
- Server-Side [Razor view engine](https://en.wikipedia.org/wiki/ASP.NET_Razor "Razor view engine")
- Razor pages for user identity system
- ASP.NET CORE view components
- ASP.NET Core areas
- MSSQL Server
- SendGrid
- Bootstrap
- Google reCaptcha

## :wrench: DB Diagram
![](https://i.ibb.co/YWVDssH/funeral-Db.jpg)

## :man_student: License
Copyright (c) 2020 Stefan Markov

This software is the property of the author mentioned above. You may download and view the software for informational or training purposes only.
It is forbidden to use the software or parts of it for commercial purposes without the express permission of the author.