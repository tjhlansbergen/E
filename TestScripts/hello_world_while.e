// Hello World

Utility Program
{
	Function Boolean Start(Text arguments)
	{
		Console:WriteText("Before while");

		new Boolean keepLooping;
		new Number count;

		keepLooping = true;
		count = 0;		

		while(keepLooping)
		{
			Console:WriteNumber(count);
			Number:Add(count, 1);

			if(Number:AreEqual(count, 10))
			{
				keepLooping = false;
			}		
		}

		Console:WriteText("---------------");

		new Number count2;
		count2 = 5;

		while(Number:LessThen(count2, 15))
		{
			Console:WriteNumber(count2);
			Number:Add(count2, 2);
		}

		Console:WriteText("Hello World!");
		
		return true;
	}
}
