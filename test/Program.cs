using System;
using System.Threading;

class Sample
{
	public static void Main()
	{
		ConsoleKeyInfo push;
		while (Console.KeyAvailable == true)
		{
			push = Console.ReadKey( true );
			switch (push.Key)
			{
				case ConsoleKey.UpArrow:
					Console.WriteLine( "Вы нажали  '{0}'", push.Key );
					break;
				case ConsoleKey.DownArrow:
					Console.WriteLine( "Вы нажали  '{0}'", push.Key );
					Console.ReadKey();
					break;
				case ConsoleKey.LeftArrow:
					Console.WriteLine( "Вы нажали  '{0}'", push.Key );
					break;
				case ConsoleKey.RightArrow:
					Console.WriteLine( "Вы нажали  '{0}'", push.Key );
					break;
				default:
					Console.WriteLine( "Вы нажали не верную кнопку '{0}'", push.Key );
					break;
			}
		}
		Console.ReadKey();
	}
}