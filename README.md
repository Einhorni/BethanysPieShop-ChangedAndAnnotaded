# Tutorial Project: Bethanys Pie Shop

This project is based on the Pluralsight course "ASP.NET Core Fundamentals" from Gill Cleeren: https://app.pluralsight.com/library/courses/asp-dot-net-core-6-fundamentals/table-of-contents

As of now (July 27th 2024) the application should work without any error.

## What I have learned:
- Basics of ASP.NET Core with MVC with Razor Page and Blazor functionality on the side
- Working with forms and model binding
- Writing tests
- Usage of Entity Framework Core for database integration
- Making a site interactive with Blazor, using ASP.NET Identity

## Where I do need more practice
- Practically all topics need to be tested out in an own project
- Deeper understanding in writing APIs (ASP.NET. Core Web API), use of EF Core
- Definitely writing tests
- Using interactivity and ASP.NET Identity
- Application und usage of Razor and Blazor

## Additions and changes:
- (With the help of a senior dev): Changed Blazor SearchPage to be just a component using _Layout and a RazorView. Original: BlazorSearch rendered in a separat Layout / Razor App
- (with the help of a senior dev): fixed the bug that 2 pies were added to the Shopping cart after modul 10 
- Added a second search Icon with link to the BlazorSearch to Display usage of Blazor functionality
- Fixed bug of the ShoppingCartSummary, that didn't add a number if you put more than 1 pie of a type in the Shopping cart
- created a link from the shoppingcartSummary to the Shopping cart
- fixed css
	  Buttons for Details and PieCard class
	  Style of Input in the BlazorSearch Page
- Added a second RazorPage Checkout in the Shopping cart to Display usage
- Changed reference link for bootstrap (the tip I found in the discussion part of the tutorial) - without it the Dropdown showing the different categories in the Header wouldn't work
    Changed the reference links for the jquery Validation alike
- Added the identity scaffolding manually as hinted in the Course discussion, therefore all files were creates rather than Account/Login and Account/Register.
- Added functionality for Razor pages Auth when using Razor page checkout button
