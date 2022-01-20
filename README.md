# Demo-BlazorApp-WebAssembly

## Scaffolding Database

To perform the scaffolding it is necessary to open the package management console and use the command Add-Migration NOME-MIGRATION

![image](https://user-images.githubusercontent.com/49655304/149158032-b3f035cd-f743-482a-b422-070042122162.png)

Paying attention to the default project (drop-down menu) which must be the one defined as shared.
Subsequently with the Update-Database command it will be possible to apply the migration created previously to the database.

![image](https://user-images.githubusercontent.com/49655304/149159161-9ddf9e21-76d9-4a35-83a2-63fa2c5dbdfe.png)

At the end of the scaffolding operations in your console you should have a result similar to this

![image](https://user-images.githubusercontent.com/49655304/149158998-c77c1101-952f-4db3-aba9-b0006fde4445.png)

## Note

This project was created using the Visual Studio 2022 Community Edition wizard, choosing the "App WebAssembly Blazor" project model, then choosing the .NET 5.0 framework, ticked the 3 flags below: 
Configure for HTTPS, Hosted ASP.NET Core and Progressive Web Application)
