using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FunctionApp
{
    public class ValidateFile : IValidateFile
    {
        public void ValidateFileContent(ref string[] fileLines)
        {
            fileLines = fileLines.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            this.CheckOperatorAndOperand(ref fileLines);
            this.CheckForApply(ref fileLines);
        }

        private void CheckOperatorAndOperand(ref string[] fileLines)
        {
            decimal parsedId;
            List<string> operatorsList = new List<string>() { "add", "divide", "multiply", "subtract", "apply" };
            foreach (var line in fileLines)
            {
                if (!operatorsList.Contains(line.Split(" ")[0].ToLower()))
                    throw new Exception("Invalid File Format");

                if (!decimal.TryParse(line.Split(" ")[1], out parsedId))
                    throw new Exception("Invalid File Format");
            }
        }

        private void CheckForApply(ref string[] fileLines)
        {
            if (fileLines.Where(x => x.ToLower().Contains("apply")).ToList().Count <= 0)
                throw new Exception("Invalid File Format");
        }
    }
}
