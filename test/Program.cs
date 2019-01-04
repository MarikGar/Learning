using System;
using System.Threading;

class Sample
{
	public static void Main()
	{
		ConsoleKeyInfo but;

		do
		{
			Console.WriteLine( "Нажмите кнопку Escape, что бы выйти " );

			

			while (Console.KeyAvailable == false)
				Thread.Sleep( 250 ); // Loop until input is entered.

			but = Console.ReadKey( true );
			Console.WriteLine( "Вы нажали  '{0}'", but.Key );
		} while (but.Key != ConsoleKey.Escape);
	}
}