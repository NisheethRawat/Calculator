using System;
using System.IO;

namespace FileOperations
{
    public class FileOperation : IFileOperation
    {
        public string[] ReadFileLines() {
            string[] fileLines = File.ReadAllLines(string.Concat(Environment.CurrentDirectory, "\\Instructions\\Calculator.txt"));
            return fileLines;
        }
        
    }
}
