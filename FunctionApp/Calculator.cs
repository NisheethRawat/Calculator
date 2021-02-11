using System;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Collections;
using System.IO;

namespace FunctionApp
{
    public static class Calculator
    {
        [FunctionName("Calculator")]
        public static long Run(
            [HttpTrigger(AuthorizationLevel.Function, "get", Route = null)] HttpRequest req,
            ILogger log)
        {
            log.LogInformation("C# HTTP trigger function processed a request.");

            try
            {
                long response = 0;
                Queue queOperator = new Queue();
                Queue queOperand = new Queue();
                string body = new StreamReader(req.Body).ReadToEndAsync().Result;
                string[] result = FileOperations.FileOperations.ReadFileLines(body);

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

                return response;
            }
            catch (Exception ex)
            {
                log.LogError(ex.Message, ex);
                throw;
            }
        }
    }
}
