# MaleFashion
---------------------------INTRODUCTION----------------------------------                                                                                           
Hello,My name is Zurab giorgadze,zuka for short and this is my solo
Full-Stack Ecommerce project.I am an aspiring lead developer who is
trying his best to gain as much experience and develop as many skills
as he can.this is a portfolio website,not intended for commercial use(as
of now,however it can be rewritten in such manner),website is wrritten
using .net core 3.0(this is an older version of .net core and i chose it
on purpose in order to display my proficiency in writing business code
in older versions)

---------------------------About This Project----------------------------
As i Have mentioned before,this project is a portfolio project,
I have been working on MaleFashion for combined duration
of 10 months.It started out as simple CRUD project and as of
now it has blossomed into something approximating a production-level
code.Website is using Colorib Free Template.
It has its own Blog,Custom written comment section,authorization and authentication
Shopping Cart,Admin Panel,
---------------------------Technologies Used-----------------------------
Many technologies(APIs,Libraries,Frameworks) have been used for this project,Including
HTML/CSS
Native Javascript
Vue.JS
C#
SQL (MSSQL)
Jquery 3.3.1(nice-select,nicescroll,magnific-popup,countdown,slicknav)
Bootstrap,Bootstrap Table Api
popper.js
trumbowyg.js
mixitup.js
Owl Carousel
Microsoft Asp.Net Core Identity
Google And Facebook Authentication API
SMTP
Stripe Payment Api
---------------------------Project Structure-----------------------------'
Coding pattern used is MVC
Project has Several Folders
Dependencies
wwwroot
Components
----This Folder Consists of ShoppingCartSummary ViewComponent used to get and populate ShoppingCartViewModel which consists of selected items

Controllers------------------------------------------
--this folder has 9 controllers
-AdministrationController takes care of managing roles and claims of specific accounts 
-AuthController-takes care of Authentication,Authorisation and related topics
-BlogController-Blog related functionality is defined here
-CheckOutController-this controller manages process of check out
-HomeController-manages access to different pages,populates them,also manages star rating system and custom product filter
-PanelController-manages Admin panel functionality
-PaymentApiController-is intended to manage payments however due to non-commercial nature of this project is feature is not
fully implemented
-ShoppingCartController-together with ShoppingCartSummary ViewComponent,this controller makes shopping cart work

Data--------------------------------------
Helper Classes,dbcontext and etc are defined under this folder
-AutoMapper-defines mapping between objects using AutoMapper.Extensions.Microsoft.DependencyInjection 5.0.1
-Claims-is a list of simple claims
-DbContext-Contains CRUDdbcontext which is IdentityDbContext that accepts ApplicationUser as an argument which inherits from IdentityUser------
-FileManager-Contains an interface IFileManager which manages images --------
-Helpers-this folder contains PageHelper,which is manages server-side pagination and is Blog related and RazorHelper which helps render Razor Views to String,used in Ajax popup modal when Managing users role
-Repository-Repository pattern which Handles Blog related Functionality
-SMTP-simple mail transfer service

Migrations----------------
Models-----------------
ViewModels------------
Views-----------
appsettings.json-----------
Program.cs------------
Startup.cs------------


