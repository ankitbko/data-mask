# data-mask
Mask/Anonymize (Hash) data in different file

# Prerequisite
 - dotnet core sdk 3.1
 
# Usage

Build the solution by running - 

```ps
dotnet publish -o .\out
```

Go to `out` directory.

```
cd out
```

Get help information by running

```
> datamask.exe --help
  -d, --delimiter    Provide Delimiter. Default is comma (,)

  -i, --input        Required. Path to input file.

  -o, --output        Required. Path to output file.

  -c, --column       Required. Comma separated columns number to mask. Starts with 0.

  --help             Display this help screen.

  --version          Display version information.

```


To mask a csv file run below code. `-c` parameter takes column index to mask. Columns are 0 indexed.

```
datamask.exe -i .\inputfile.csv -o .\output.csv -c 1,3
```
