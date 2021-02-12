using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.IO;
using FileOperation;
using System.Net.Http;
using System.Linq;
using System.Collections.Generic;

namespace FunctionApp
{
    public class Calculator
    {
        private readonly IFileOperations _fileOperation;

        public Calculator(IFileOperations fileOperation)
        {
            _fileOperation = fileOperation;
        }

        [FunctionName("Calculator")]
        public long Run([HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation(string.Concat("C# Calculator function started at ", DateTime.UtcNow,"."));

            try
            {
                long response = 0;
                Queue queOperator = new Queue();
                Queue queOperand = new Queue();
                string[] result = _fileOperation.ReadFileLines();
                ValidateFile(ref result);

                foreach (var syntax in result)
                {
                    if (syntax.Split(" ")[0].ToLower() == "apply")
                    {
                        response = Convert.ToInt32(syntax.Split(" ")[1]);
                        break;
                    }
                    else
                    {
                        queOperator.Enqueue(syntax.Split(" ")[0]);
                        queOperand.Enqueue(syntax.Split(" ")[1]);
                    }
                }

                while (queOperand.Count > 0)
                {
                    switch (queOperator.Dequeue().ToString().ToLower())
                    {
                        case "add":
                            response = response + Convert.ToInt32(queOperand.Dequeue());
                            break;
                        case "subtract":
                            response = response - Convert.ToInt32(queOperand.Dequeue());
                            break;
                        case "multiply":
                            response = response * Convert.ToInt32(queOperand.Dequeue());
                            break;
                        case "divide":
                            response = response / Convert.ToInt32(queOperand.Dequeue());
                            break;
                    }

                }

                log.LogInformation(string.Concat("C# Calculator function completed at ", DateTime.UtcNow, "."));
                return response;
            }
            catch (Exception ex)
            {
                log.LogError(string.Concat("C# Calculator function received error at ", DateTime.UtcNow, "."));
                log.LogError(ex.Message, ex);
                throw;
            }
        }

        private void ValidateFile(ref string[] fileLines)
        {
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
