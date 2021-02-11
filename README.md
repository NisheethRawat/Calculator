# Calculator

To Test via Postman use the below Curl.
	curl --location --request GET 'http://localhost:7071/api/Calculator'


Main Project -  Function App
Testing Project - NunitTestProject

examples of the calculator lifecycle might be:

Example 1.
add 2
multiply 3
apply 3

API response - 15

Example 2.
multiply 9
apply 5

API response - 45

Example 3.
add 2
multiply 3
multiply 9
add 40
add 40
add 40
add 40
apply 5

API response - 349