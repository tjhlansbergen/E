// Hello World

Utility Program
{
	Function Boolean Start(Text arguments)
	{
		Text lineToWrite;

		lineToWrite = "Hello World";
		Console:WriteText(lineToWrite);

		
		Text:Append(lineToWrite, "!");
		Console:WriteText(lineToWrite);

		return true;
	}
}
