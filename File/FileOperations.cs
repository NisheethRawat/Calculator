using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FileOperations
{
    public static class FileOperations
    {
        public static string[] ReadFileLines(string fileName) {
            string fullPath;

            if (string.IsNullOrEmpty(fileName))
                fullPath = string.Concat(Environment.CurrentDirectory, "\\Instructions\\Calculator.txt");
            else
                fullPath = string.Concat(Environment.CurrentDirectory, "\\Instructions\\", fileName,".txt");

            string[] fileLines = File.ReadAllLines(fullPath);
            ValidateFile(ref fileLines);
            return fileLines;
        }
        private static void ValidateFile(ref string[] fileLines) {
            fileLines = fileLines.Where(x => !string.IsNullOrEmpty(x)).ToArray();

            #region Check operator and operand list
            long parsedId;
            List<string> operatorsList = new List<string>() { "add", "divide", "multiply", "subtract", "apply" };
            foreach (var line in fileLines)
            {
                if (!operatorsList.Contains(line.Split(" ")[0].ToLower()))
                    throw new Exception("Invalid File Format");

                if (!long.TryParse(line.Split(" ")[1], out parsedId))
                    throw new Exception("Invalid File Format");
            }
            #endregion 

            #region Check apply is present
            if (fileLines.Where(x => x.ToLower().Contains("apply")).ToList().Count <= 0)
                throw new Exception("Invalid File Format");
            #endregion 
        }
    }
}
