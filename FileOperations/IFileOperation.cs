using System;
using System.Collections.Generic;
using System.Text;

namespace FileOperations
{
    public interface IFileOperation
    {
        string[] ReadFileLines();
    }
}
