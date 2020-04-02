# data-mask
Mask/Anonymize (Hash) data in different file

# Usage

Download precompiled version from release tab in Github for specific OS 

Get help information by running

```
> datamask.exe --help
  -d, --delimiter    Provide Delimiter. Default is comma (,)

  -i, --input        Required. Path to input file.

  -o, --output       Required. Path to output file.

  -c, --column       Required. Comma separated columns number to mask. Starts with 0.

  --help             Display this help screen.

  --version          Display version information.

```


To mask a csv file run below code. `-c` parameter takes column index to mask. Columns are 0 indexed.

```
datamask.exe -i .\inputfile.csv -o .\output.csv -c 1,3
```

---

# Build

To build the application locally follow below instruction


## Prerequisite
 - dotnet core sdk 3.1
 
win-x64 Self Contained
```
dotnet publish -r win-x64 -o .\out -p:PublishSingleFile=true -p:PublishTrimmed=true -c Release
```

linux-x64 Self Contained
```
dotnet publish -r linux-x64 -o .\out -p:PublishSingleFile=true -p:PublishTrimmed=true -c Release
```

osx-x64 Self Contained
win-x64
```
dotnet publish -r osx-x64 -o .\out -p:PublishSingleFile=true -p:PublishTrimmed=true -c Release
```
