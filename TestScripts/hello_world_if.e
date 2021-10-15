// Hello World

Utility Program
{
	Function Boolean Start(Text arguments)
	{
		Console:WriteText("Before if");

		Boolean enterIf;

		enterIf = false;
		
		if(enterIf)
		{
			Console:WriteText("in if 1!");
			return true;
		}

		enterIf = true;

		if(enterIf)
		{
			Console:WriteText("in if 2!");
			return true;
		}
		
		Console:WriteText("after if!");
		
		return true;
	}
}
