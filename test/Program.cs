using System;

using Learning;

class Test
{
	static void PrintArray( int[] arr, string title )
	{
		Console.WriteLine( title );
		// чтобы распечатать массив, надо вывести каждый элемент с разделителем
		string dim = "";
		// пойми, что я тут делаю.. позапукай под отладчиком
		// если ты ничо не делаешь, то хз чо просиходитс настройками. пох
		// я здесь поставил точку останова (вот этот блять красынй кружок)
		//каждый раз проходя через этот цикл отладчик будет останавливаться и даст возможность посмотреть все перменные, если навести на них мышкой
		// когда отладишь, убери точку останова нажав на кружок еще раз
		foreach (var el in arr) { Console.Write( $"{dim}{el}" ); dim = ", "; }
		Console.WriteLine();
	}

	static void testShifts()
	{
		var arr = new int[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
		
		// чтобы не портить один массив на всех, создаем копию
		var tmp = ( int[] )arr.Clone();
		PrintArray( tmp, "BEFORE" );
		Utils.ArrayShiftLeft( tmp );
		PrintArray( tmp, "AFTER" );

		// оно работает я вижу
		// но послежний элемент 9, я делал что бы был превый
		// зациливание для нас не нужно, но можешь сделать чтобы он был первымпис

		// test ArrayShiftRight
		// test ListShiftLeft
		// test ListShiftRight
	}

	static void Main()
	{
		// чонить здеся

		testShifts();

		// finita la comedia
		Console.WriteLine( "Done." );
		Console.ReadLine();
	}
}