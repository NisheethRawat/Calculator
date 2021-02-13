using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionApp
{
    public interface IEvaluateExpression
    {
        void Evaluate(ref string[] fileLines, ref decimal response);
    }
}
