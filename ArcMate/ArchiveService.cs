using SharpCompress.Common;
using SharpCompress.Readers;
using SharpCompress.Writers;
using System;
using System.Net.NetworkInformation;
using System.Reflection;

namespace ArcMate;

class ArchiveService
{
    public void Create(string folder)
    {
        try
        {
            if (!Directory.Exists(folder))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  [ERROR] Folder not found: {folder}");
                Console.ResetColor();
                return;
            }

            string output = $"{folder}.zip";
            using var stream = File.Create(output);
            using var writer = WriterFactory.OpenWriter(stream, ArchiveType.Zip,
                new WriterOptions(CompressionType.Deflate));

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n  Creating archive...");
            Console.ResetColor();

            int fileCount = 0;
            foreach (var file in Directory.GetFiles(folder, "*", SearchOption.AllDirectories))
            {
                writer.Write(Path.GetRelativePath(folder, file), file);
                fileCount++;
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  [SUCCESS] Archive created successfully!");
            Console.ResetColor();
            Console.WriteLine($"  Location: {output}");
            Console.WriteLine($"  Files archived: {fileCount}");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  [ERROR] {ex.Message}");
            Console.ResetColor();
        }
    }

    public void CreateWithRoot(string folder)
    {
        try
        {
            if (!Directory.Exists(folder))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  [ERROR] Folder not found: {folder}");
                Console.ResetColor();
                return;
            }

            string output = $"{folder}.zip";
            string rootFolderName = new DirectoryInfo(folder).Name;

            using var stream = File.Create(output);
            using var writer = WriterFactory.OpenWriter(stream, ArchiveType.Zip,
                new WriterOptions(CompressionType.Deflate)
                {
                    CompressionLevel = (int)SharpCompress.Compressors.Deflate.CompressionLevel.BestCompression,
                    LeaveStreamOpen = false
                });

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n  Creating archive with root folder...");
            Console.ResetColor();

            var allFiles = Directory.GetFiles(folder, "*", SearchOption.AllDirectories)
                .OrderBy(f => f)
                .ToList();

            int totalFiles = allFiles.Count;
            int processedFiles = 0;

            Console.WriteLine();

            foreach (var file in allFiles)
            {
                string relativePath = Path.GetRelativePath(folder, file);
                string archivePath = Path.Combine(rootFolderName, relativePath);

                writer.Write(archivePath, file);
                processedFiles++;

                DrawProgressBar(processedFiles, totalFiles, Path.GetFileName(file));
            }

            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  [SUCCESS] Archive created successfully!");
            Console.ResetColor();
            Console.WriteLine($"  Location: {output}");
            Console.WriteLine($"  Root folder: {rootFolderName}");
            Console.WriteLine($"  Files archived: {totalFiles}");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  [ERROR] {ex.Message}");
            Console.ResetColor();
        }
    }

    public void Extract(string archivePath)
    {
        try
        {
            if (!File.Exists(archivePath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  [ERROR] Archive not found: {archivePath}");
                Console.ResetColor();
                return;
            }

            using var stream = File.OpenRead(archivePath);
            using var reader = ReaderFactory.OpenReader(stream);
            string dest = Path.Combine(
                Path.GetDirectoryName(archivePath)!,
                Path.GetFileNameWithoutExtension(archivePath));

            Directory.CreateDirectory(dest);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n  Extracting archive...");
            Console.ResetColor();

            int fileCount = 0;
            while (reader.MoveToNextEntry())
            {
                if (!reader.Entry.IsDirectory)
                {
                    reader.WriteEntryToDirectory(dest);
                    fileCount++;
                }
            }

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"\n  [SUCCESS] Extraction completed!");
            Console.ResetColor();
            Console.WriteLine($"  Location: {dest}");
            Console.WriteLine($"  Files extracted: {fileCount}");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  [ERROR] {ex.Message}");
            Console.ResetColor();
        }
    }

    public void ExtractVerbose(string archivePath)
    {
        try
        {
            if (!File.Exists(archivePath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  [ERROR] Archive not found: {archivePath}");
                Console.ResetColor();
                return;
            }

            using var stream = File.OpenRead(archivePath);
            using var reader = ReaderFactory.OpenReader(stream);
            string dest = Path.Combine(
                Path.GetDirectoryName(archivePath)!,
                Path.GetFileNameWithoutExtension(archivePath));

            Directory.CreateDirectory(dest);

            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\n  Extracting archive...");
            Console.ResetColor();

            var entries = new List<string>();
            while (reader.MoveToNextEntry())
            {
                if (!reader.Entry.IsDirectory)
                {
                    entries.Add(reader.Entry.Key);
                }
            }

            int totalFiles = entries.Count;
            int processedFiles = 0;

            stream.Position = 0;
            reader.Dispose();

            using var stream2 = File.OpenRead(archivePath);
            using var reader2 = ReaderFactory.OpenReader(stream2);

            Console.WriteLine();

            while (reader2.MoveToNextEntry())
            {
                if (!reader2.Entry.IsDirectory)
                {
                    reader2.WriteEntryToDirectory(dest);
                    processedFiles++;

                    DrawProgressBar(processedFiles, totalFiles, Path.GetFileName(reader2.Entry.Key));
                }
            }

            Console.WriteLine("\n");
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"  [SUCCESS] Extraction completed!");
            Console.ResetColor();
            Console.WriteLine($"  Location: {dest}");
            Console.WriteLine($"  Files extracted: {totalFiles}");
        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  [ERROR] {ex.Message}");
            Console.ResetColor();
        }
    }

    public void ListContents(string archivePath)
    {
        try
        {
            if (!File.Exists(archivePath))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n  [ERROR] Archive not found: {archivePath}");
                Console.ResetColor();
                return;
            }

            using var stream = File.OpenRead(archivePath);
            using var reader = ReaderFactory.OpenReader(stream);

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n" + new string('=', 100));
            Console.WriteLine($"  Archive: {Path.GetFileName(archivePath)}");
            Console.WriteLine(new string('=', 100));
            Console.ResetColor();

            var entries = new List<(string path, bool isDirectory, long size, long compressedSize)>();

            while (reader.MoveToNextEntry())
            {
                entries.Add((
                    reader.Entry.Key,
                    reader.Entry.IsDirectory,
                    reader.Entry.Size,
                    reader.Entry.CompressedSize
                ));
            }

            var folders = new HashSet<string>();
            foreach (var entry in entries.Where(e => !e.isDirectory))
            {
                var dirParts = Path.GetDirectoryName(entry.path)?.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                if (dirParts != null && dirParts.Length > 0)
                {
                    string currentPath = "";
                    foreach (var part in dirParts)
                    {
                        currentPath = string.IsNullOrEmpty(currentPath) ? part : Path.Combine(currentPath, part);
                        folders.Add(currentPath);
                    }
                }
            }

            var sortedEntries = entries.Where(e => !e.isDirectory).OrderBy(e => e.path).ToList();

            long totalOriginal = 0;
            long totalCompressed = 0;
            int fileCount = 0;

            string lastFolder = "";

            foreach (var entry in sortedEntries)
            {
                string[] parts = entry.path.Split(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar);
                int depth = parts.Length - 1;

                string currentFolder = depth > 0 ? string.Join(Path.DirectorySeparatorChar.ToString(), parts.Take(depth)) : "";

                if (currentFolder != lastFolder)
                {
                    if (!string.IsNullOrEmpty(lastFolder))
                    {
                        Console.WriteLine();
                    }

                    if (!string.IsNullOrEmpty(currentFolder))
                    {
                        Console.ForegroundColor = ConsoleColor.Yellow;
                        Console.WriteLine($"  [{currentFolder}]");
                        Console.ResetColor();
                    }

                    lastFolder = currentFolder;
                }

                string fileName = parts[^1];

                double compressionRatio = entry.size > 0 ? (1 - (double)entry.compressedSize / entry.size) * 100 : 0;

                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"  [FILE] ");
                Console.ResetColor();
                Console.Write($"{fileName,-50}");

                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.Write($" | Original: {FormatSize(entry.size),-10}");
                Console.Write($" | Compressed: {FormatSize(entry.compressedSize),-10}");
                Console.Write($" | Ratio: {compressionRatio:F1}%");
                Console.ResetColor();
                Console.WriteLine();

                totalOriginal += entry.size;
                totalCompressed += entry.compressedSize;
                fileCount++;
            }

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("\n" + new string('=', 100));
            Console.ResetColor();

            double totalRatio = totalOriginal > 0 ? (1 - (double)totalCompressed / totalOriginal) * 100 : 0;

            Console.WriteLine($"  Total Files: {fileCount}");
            Console.WriteLine($"  Total Folders: {folders.Count}");
            Console.WriteLine($"  Original Size: {FormatSize(totalOriginal)}");
            Console.WriteLine($"  Compressed Size: {FormatSize(totalCompressed)}");
            Console.WriteLine($"  Space Saved: {FormatSize(totalOriginal - totalCompressed)} ({totalRatio:F1}%)");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine(new string('=', 100));
            Console.ResetColor();

        }
        catch (Exception ex)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\n  [ERROR] {ex.Message}");
            Console.ResetColor();
        }
    }

    private void DrawProgressBar(int current, int total, string currentFile)
    {
        const int barWidth = 50;
        double percentage = (double)current / total;
        int filledWidth = (int)(barWidth * percentage);

        Console.SetCursorPosition(0, Console.CursorTop);
        Console.Write("  [");

        Console.ForegroundColor = ConsoleColor.Green;
        Console.Write(new string('#', filledWidth));
        Console.ResetColor();

        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.Write(new string('-', barWidth - filledWidth));
        Console.ResetColor();

        Console.Write($"] {percentage:P0} ({current}/{total})");

        string displayName = currentFile.Length > 30 ? currentFile.Substring(0, 27) + "..." : currentFile;
        Console.Write($" - {displayName}");

        Console.Write(new string(' ', 10));
    }

    private string FormatSize(long bytes)
    {
        string[] sizes = { "B", "KB", "MB", "GB", "TB" };
        double len = bytes;
        int order = 0;
        while (len >= 1024 && order < sizes.Length - 1)
        {
            order++;
            len = len / 1024;
        }
        return $"{len:0.##} {sizes[order]}";
    }
}