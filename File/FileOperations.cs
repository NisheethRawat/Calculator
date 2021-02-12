using System;
using System.IO;

namespace FileOperation
{
    public class FileOperations : IFileOperations
    {
        public string[] ReadFileLines() {
            string[] fileLines = File.ReadAllLines(string.Concat(Environment.CurrentDirectory, "\\Instructions\\Calculator.txt"));
            return fileLines;
        }
        
    }
}
