ElmahEFLogger
=============
Global exceptions logger for Entity Framework 6.x using ELMAH.


Usage:
Add The following line to the `Application_Start` method of the `Global.asax.cs` file:
DbInterception.Add(new ElmahEfInterceptor());


Project Url:
https://github.com/VahidN/ElmahEFLogger