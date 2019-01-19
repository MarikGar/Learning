using System;
using System.Collections.Generic;

using Learning;

class Test
{
	static void PrintArray<T>( T[] arr, string title )
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

	static void PrintList<T>( List<T> list, string title )
	{
		Console.WriteLine( title );
		string dim = "";
		foreach (var el in list) { Console.Write( $"{dim}{el}" ); dim = ", "; }
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

		// один шаблонный метод работает с любыми данными. КРУТО. не надо писать 20 дебилных одинаковых методов
		Console.WriteLine();
		var dbl = new double[] { 0.0, 0.1, 0.2, 0.3, 0.4, 0.5, 0.6, 0.7, 0.8, 0.9 };
		PrintArray( dbl, "BEFORE" );
		Utils.ArrayShiftLeft( dbl );
		PrintArray( dbl, "AFTER" );

		// проверим, что листо тож сдвигает
		Console.WriteLine();
		var list = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9 };
		PrintList( list, "BEFORE" );
		Utils.ListShiftLeft( list );
		PrintList( list, "AFTER" );

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