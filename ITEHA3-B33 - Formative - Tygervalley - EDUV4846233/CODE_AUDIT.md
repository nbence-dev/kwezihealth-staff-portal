# Code Audit — KweziHealth ESOP

Audit against the Practical Assignment rubric (Deliverables 1–4). No fixes applied — flags only.

## Biggest thing to verify first — architecture mismatch

Deliverable 2 literally says: *"Create `Services/StaffService.cs` using an in-memory collection"* and the rubric gives 4 marks specifically for "In-memory staff collection implemented." Deliverable 1 also asks for models "suitable for future in-memory integration."

What's actually built is `StaffService` → `StaffRepository` → `KweziHealthDbContext` (EF Core) → `UseInMemoryDatabase(...)`. That's a full EF Core data-access layer using EF's in-memory *provider*, not a simple `List<StaffMember>` held inside the service. It's arguably more "database-ready" than what's asked for, but it's not literally what the task text describes, and a marker grading strictly against "in-memory collection implemented" could read this either way. Worth confirming with the lecturer or re-reading the rubric wording carefully before assuming it's fine — it affects how Deliverable 1 and 2 are marked together.

## Deliverable 1 — Models & structure

- `StaffMember.Unit` is typed `int` — a comment in the file itself questions this ("could argue a unit could be something like A12"). Rubric explicitly awards marks for "correct data types used," so decide and commit rather than leaving the doubt in a comment.
- `csproj` has `<Folder Include="Abstractions\" />` but no `Abstractions` folder is used anywhere — dead reference, remove it.
- Several non-nullable string properties (`FullName`, `Email`, `Position`, `Username`, `Password`) produce CS8618 nullable warnings at build time. Not a functional bug, but for "good coding" polish, either add `required` or initialize them.

## Deliverable 2 — Service layer

- `StaffService` and `StaffRepository` are cleanly separated and don't reference controllers — good.
- `StaffMemberDto` and `LoginDto` have **no data annotations** (`[Required]`, etc.), while `StaffMember`/`SystemAdmin` do. Since `AddEditStaff` binds to `StaffMemberDto`, not `StaffMember`, the `ModelState.IsValid` check in the controller has nothing to actually invalidate — it will basically always pass regardless of what's submitted. Worth checking if that's intentional.

## Deliverable 3 — Controllers, auth, views

- **Validation bypass bug**: in `StaffController.AddEditStaff` (POST), the `ModelState.IsValid` check only happens in the "add" branch. The `if (id != null)` (edit) branch returns before ever reaching that check, so edits skip validation entirely — even the currently-empty validation that exists.
- **No 404 handling**: if `AddEditStaff(int? id)` gets an `id` that doesn't exist in the DB, `RetrieveStaffById` returns `null`, and the view silently renders as "Add" mode. Submitting that form then creates a brand-new staff member instead of telling the user the ID was invalid.
- **Views double-nest HTML**: `_ViewStart.cshtml` applies `_Layout` to every view by default, but `Login.cshtml`, `Index.cshtml`, and `AddEditStaff.cshtml` each contain their own `<!DOCTYPE html><html><head>...<body>` tags. That means a full `<html>/<body>` document renders *inside* `_Layout`'s already-open `<body>` — invalid/nested HTML. Content views under `_Layout` should just have the fragment (no doctype/html/head/body tags). This also means the Login page currently renders wrapped in the authenticated navbar (Add Staff/Logout links) even though nobody's logged in yet.
- **Login has no `@model` directive**: the controller returns `View(loginDto)` on failed login, but the view isn't strongly typed and has no `asp-validation-summary`, so a failed login currently gives the user no feedback at all — ties back to the `// Remember to validate and add errors` TODO in the code.
- **CSRF tokens generated but never validated**: the `asp-action` forms auto-emit an antiforgery token, but ASP.NET Core MVC (unlike Razor Pages) does **not** validate it automatically — this needs `[ValidateAntiForgeryToken]` on the POST actions (or a global filter in `Program.cs`). Right now the token is decorative.
- `Logout` is triggered via an `<a>` link (GET) in `_Layout.cshtml`, and the action itself has no `[HttpGet]`/`[HttpPost]` restriction, so it silently accepts any verb. State-changing actions like logout are conventionally POST-only.
- Dead/commented-out code in `StaffController.Search` — clean up before submission, since writing/code quality is also assessed.
- `AddEditStaff.cshtml`'s hidden `StaffId` input is never read by the controller (it reads the route `id`, not the posted form field) — unused markup.

## Deliverable 4 — Program.cs / build / run

- Services, DbContext, auth middleware, and routing are all registered correctly, and middleware ordering (`UseRouting` → `UseAuthentication` → `UseAuthorization`) is correct.
- `dotnet build` succeeds with 0 errors (11 nullable warnings, noted above).
- Not yet verified end-to-end in a running browser session — worth doing manually before submission, especially given the AddEditStaff/validation issues above.
