using System;
using System.IO;
using System.IO.Compression;

namespace ZipUtility
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0 || Array.Exists(args, arg => arg.Equals("--help", StringComparison.OrdinalIgnoreCase)))
            {
                DisplayHelp();
                return;
            }

            string fromDirectory = null;
            string toDirectory = null;

            for (int i = 0; i < args.Length; i++)
            {
                if (args[i].Equals("--from", StringComparison.OrdinalIgnoreCase) && i + 1 < args.Length)
                {
                    fromDirectory = args[++i];
                }
                else if (args[i].Equals("--to", StringComparison.OrdinalIgnoreCase) && i + 1 < args.Length)
                {
                    toDirectory = args[++i];
                }
            }

            if (string.IsNullOrEmpty(fromDirectory) || string.IsNullOrEmpty(toDirectory))
            {
                Console.WriteLine("Error: Missing --from or --to argument");
                DisplayHelp();
                return;
            }

            if (!Directory.Exists(fromDirectory) || !Directory.Exists(toDirectory))
            {
                Console.WriteLine("Error: Either the source or the destination directory does not exist.");
                return;
            }

            string[] zipFiles = Directory.GetFiles(fromDirectory, "*.zip");
            if (zipFiles.Length == 0)
            {
                Console.WriteLine("No ZIP files found in the source directory.");
                return;
            }

            foreach (string zipFile in zipFiles)
            {
                try
                {
                    Console.WriteLine("Extracting {0} to {1}...", zipFile, toDirectory);
                    using (ZipArchive archive = ZipFile.OpenRead(zipFile))
                    {
                        foreach (ZipArchiveEntry entry in archive.Entries)
                        {
                            // Combine the destination directory with the archive entry's full name
                            string fullPath = Path.Combine(toDirectory, entry.FullName);

                            // Ensure the directory structure is retained for entries within folders in the ZIP file
                            string directoryName = Path.GetDirectoryName(fullPath);
                            if (!Directory.Exists(directoryName))
                            {
                                Directory.CreateDirectory(directoryName);
                            }

                            if (!string.IsNullOrEmpty(Path.GetExtension(entry.FullName)))  // Avoids trying to create a directory as a file
                            {
                                entry.ExtractToFile(fullPath, true);  // The 'true' argument overwrites existing files
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error extracting {0}: {1}", zipFile, ex.Message);
                }
            }
            Console.WriteLine("Extraction completed.");
        }

        static void DisplayHelp()
        {
            Console.WriteLine("ZipUtility Help:");
            Console.WriteLine("Usage: UtilityName.exe --from \"path_to_zip_files\" --to \"destination_path\"");
            Console.WriteLine("Arguments:");
            Console.WriteLine("  --from\tPath to the directory containing zip files.");
            Console.WriteLine("  --to\t\tPath to the destination directory where files will be extracted.");
            Console.WriteLine("  to compile, run the following command csc zip_extractor.cs /r:System.IO.Compression.FileSystem.dll /r:System.IO.Compression.dll");
        }
    }
}