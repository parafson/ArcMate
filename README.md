# ArcMate

ArcMate is a .NET 8 console application for creating, extracting, and inspecting ZIP archives.
It provides a simple interactive menu and uses the `SharpCompress` library for archive operations.

## Features

- Create a ZIP archive from a folder
- Create a ZIP archive while preserving the root folder in the archive
- Extract ZIP archives to a destination folder
- Extract archives in verbose mode with a progress bar
- List archive contents with per-file size, compressed size, and compression ratio

## Tech Stack

- C#
- .NET 8
- SharpCompress (`0.45.0`)

## Project Structure

```text
ArcMate/
├── ArcMateSolution.slnx
├── ArcMate/
│   ├── Program.cs
│   ├── ArchiveService.cs
│   └── ArcMate.csproj
└── README.MD
```

## Prerequisites

- .NET SDK 8.0 or later

## Getting Started

### 1. Clone the repository

```bash
git clone <your-repo-url>
cd ArcMate
```

### 2. Restore dependencies

```bash
dotnet restore ArcMate/ArcMate.csproj
```

### 3. Build

```bash
dotnet build ArcMate/ArcMate.csproj
```

### 4. Run

```bash
dotnet run --project ArcMate/ArcMate.csproj
```

## Menu Options

When the app starts, choose one of the following:

1. Create Archive
2. Create Archive with Root Folder
3. Extract Archive
4. Extract Archive (Verbose)
5. List Archive Contents
6. Exit

## Usage Notes

- Output ZIP files are created as `<folderPath>.zip`.
- Extraction output is written to a folder matching the archive name (without extension) in the same directory as the archive.
- Invalid paths are handled with user-facing error messages.

## License

This project is licensed under the MIT License. See `LICENSE.txt` for details.
