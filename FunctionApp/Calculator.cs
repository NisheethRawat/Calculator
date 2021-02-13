using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.IO;
using FileOperations;
using System.Net.Http;
using System.Linq;
using System.Collections.Generic;

namespace FunctionApp
{
    public class Calculator
    {
        private readonly IFileOperation _fileOperation;

        public Calculator(IFileOperation fileOperation)
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
                decimal response = 0;
                string[] fileLines = _fileOperation.ReadFileLines();

                IValidateFile _validateFile = new ValidateFile();
                _validateFile.ValidateFileContent(ref fileLines);

                IEvaluateExpression _evaluateExpression = new EvaluateExpression();
                _evaluateExpression.Evaluate(ref fileLines, ref response);

                log.LogInformation(string.Concat("C# Calculator function completed at ", DateTime.UtcNow, "."));
                return Convert.ToInt64(response);
            }
            catch (Exception ex)
            {
                log.LogError(string.Concat("C# Calculator function received error at ", DateTime.UtcNow, "."));
                log.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
