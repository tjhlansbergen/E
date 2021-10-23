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

			//new Boolean equal;
			//equal = Number:AreEqual(count, 10);
			
			if(Number:AreEqual(count, 10))
			{
				keepLooping = false;
			}		
		}

		Console:WriteText("Hello World!");
		
		return true;
	}
}
