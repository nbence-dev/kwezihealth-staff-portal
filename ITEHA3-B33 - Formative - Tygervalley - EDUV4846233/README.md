# KweziHealth ESOP

A layered ASP.NET Core MVC application for the KweziHealth Systems Enterprise Staff Operations Platform (ESOP) — a secure, admin-only staff management system built with C# and .NET.

## Overview

KweziHealth Systems needed to replace spreadsheet-based staff administration with a proper web application. This project implements Phase 1 of that initiative: administrator authentication and full CRUD staff management, built on a clean layered architecture (Models → Repositories → Services → Controllers → Views) that's ready for future database and cloud integration.

## Features

- **Administrator authentication** — cookie-based login/logout with session management
- **Staff management (CRUD)** — create, list, edit, delete, and search staff members
- **Access control** — staff management routes are restricted to authenticated administrators
- **Server-side validation** — data annotations on all input DTOs, with client- and server-side enforcement
- **User feedback** — success/error banners for all state-changing actions

## Tech Stack

- ASP.NET Core MVC (.NET 10)
- Entity Framework Core (in-memory provider)
- Bootstrap 5

## Project Structure

```
Controllers/    Access (auth) and Staff (CRUD) controllers
Services/       Business logic, decoupled from controllers
Repositories/   Data access via EF Core
Models/         Domain entities (StaffMember, SystemAdmin)
DTOs/           Input/validation models for forms
Data/           DbContext and seed data
Views/          Razor views (Login, Staff Index, Add/Edit Staff)
```

## Getting Started

**Prerequisites:** [.NET 10 SDK](https://dotnet.microsoft.com/download)

```bash
dotnet build
dotnet run
```

The app seeds a default administrator account on startup — see `Data/DataSeeder.cs` for credentials.

## Notes

This is a coursework project (Enterprise Programming in C#) built around a layered-architecture and secure-CRUD assignment brief.
