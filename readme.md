# ZipUtility

ZipUtility is a command-line tool developed in C# for extracting ZIP files from a source directory to a destination directory. This application was built on windows and for windows.

## Prerequisites

- .NET Framework (Version compatible with C# 5.0)

## Installation

1. Clone the repository or download the source code.
2. Compile the code using a C# compiler or an IDE like Visual Studio.
3. Obtain the executable (`ZipUtility.exe`) from the build output.

## Usage

Run the utility from the command line using the following format:

ZipUtility.exe --from "path_to_zip_files" --to "destination_path"

sql
Copy code

### Arguments:

- `--from`: Specifies the path to the directory containing ZIP files.
- `--to`: Specifies the path to the destination directory where the files will be extracted.

For help or to view the usage guidelines, use:

ZipUtility.exe --help

r
Copy code

### Example:

If you have a directory `C:\zippedFiles` containing ZIP files and you want to extract them to `C:\extractedFiles`, you would use:

ZipUtility.exe --from "C:\zippedFiles" --to "C:\extractedFiles"

markdown
Copy code

## Error Handling

The utility will provide feedback in the following scenarios:

- Missing `--from` or `--to` arguments.
- Either the source or the destination directory does not exist.
- No ZIP files found in the source directory.
- An error occurs during the extraction of a specific ZIP file.

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.
