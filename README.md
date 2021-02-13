# Calculator

	1. This is Function App project for calculting expression
	2. To Test via Postman use the below Curl.
		curl --location --request GET 'http://localhost:7071/api/Calculator'
	3. It's a two layer application cosnsisting of Function App and File operation project.
	4. File operation project will handle all the file operation using System.IO namespace.
	5. Calculator\FileOperations\Instructions\Calculator.txt contains the input instructions to be executed.
	6. Test cases are present in NUnitTestProject, Test cases are included for each class of each layer.
	7. Testing of each layer is separated using moq object of MOQ library.
	8. All the files to be excluded for check in are placed under the gitignore file.

# Regarding Function App project

	1. Startup.cs will instantiate the File operation to be used in any function App.
	2. Complexity of file operations is hidden under File Interface for calling read and write operations.
	3. ValidateFile class is created to achieve validation on the input file.
	4. EvaluateExpression class is created to evaluate the input instructions.
	5. Both the classes are exposed using the interface and used in Calculator.cs.
	6. Dependent File layer is called using the interface object which is instantiated on the constructor of the function app.