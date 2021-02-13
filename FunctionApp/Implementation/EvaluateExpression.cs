using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace FunctionApp
{
    public class EvaluateExpression: IEvaluateExpression
    {
        public void Evaluate(ref string[] fileLines, ref decimal response) {

            Queue queOperator = new Queue();
            Queue queOperand = new Queue();

            foreach (var syntax in fileLines)
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
        }
    }
}
