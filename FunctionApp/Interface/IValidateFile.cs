using System;
using System.Collections.Generic;
using System.Text;

namespace FunctionApp
{
    public interface IValidateFile
    {
        void ValidateFileContent(ref string[] fileLines);
    }
}
