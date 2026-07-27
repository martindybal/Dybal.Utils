# Releasing packages to NuGet

Packages are published to [nuget.org](https://www.nuget.org) by the
[`publish-nuget.yml`](.github/workflows/publish-nuget.yml) workflow, which runs when a
**GitHub Release is published**.

Each package is versioned and released **independently**. One GitHub Release publishes
**exactly one** package, chosen by the release tag.

Authentication uses **NuGet Trusted Publishing** (OIDC): the workflow exchanges a
short-lived GitHub OIDC token for a temporary nuget.org API key. There is no long-lived
API key stored in the repository.

## Tag format

The release tag selects the package and its version:

```
<PackageId>-<Version>
```

Examples:

- `Dybal.Utils.Guards-1.0.2`
- `Dybal.Utils.Extensions.DependencyInjection-1.1.0`

The workflow maps the package id to `src/Utils/<PackageId>/<PackageId>.csproj`, so the tag
prefix must match the project (and package) name exactly.

## One-time setup

### 1. Create the Trusted Publishing policy on nuget.org

Sign in to nuget.org → your username → **Trusted Publishing** → add a policy:

| Field            | Value            |
| ---------------- | ---------------- |
| Repository Owner | `martindybal`    |
| Repository       | `Dybal.Utils`    |
| Workflow File    | `publish-nuget.yml` |
| Environment      | *(leave empty)*  |

The policy owner must own the packages being published.

### 2. Add the `NUGET_USER` secret

The `NuGet/login` action needs your nuget.org **username** (the profile name, **not** your
email). Add it as a repository secret:

`Settings → Secrets and variables → Actions → New repository secret`

- Name: `NUGET_USER`
- Value: your nuget.org username

## Releasing a package

1. **Bump the version** in the package's `.csproj` (`<Version>`), and update its
   `CHANGELOG.md`. The `<Version>` in the project is the source of truth - the workflow
   verifies the release tag matches it and fails otherwise.
2. Merge to `main`.
3. Create a **GitHub Release** targeting `main`:
   - **Tag:** `<PackageId>-<Version>` (e.g. `Dybal.Utils.Guards-1.0.2`)
   - **Target:** `main`
   - **Title / notes:** use the changelog entry for that version.
4. **Publish** the release. The workflow packs that one project and pushes the `.nupkg`
   (and its `.snupkg` symbols) to nuget.org.

Re-publishing the same version is safe: the push uses `--skip-duplicate`.

> The tag must point to a commit that already contains this workflow. Create the release
> **after** the workflow is merged to `main`.
