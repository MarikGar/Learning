using System;

namespace Snake
{
	class Program
	{
		public static void Main()
		{
			Console.Clear();
			Console.SetCursorPosition( 0, 0 );
			int posx = Console.CursorTop;      // курсор в точке с координатой х=0
			int posy = Console.CursorLeft;     // курсор в точке с координатой у=0
			Console.WriteLine( "Введите длину сторон поля:  " );
			int len = int.Parse( Console.ReadLine() );
			Console.Clear();
			DrawHorizontal( posx, posy, len );		
			DrawVertical( posx, posy, len );
			SnakeBody( posx, posy );
			Console.WriteLine();
			Console.ReadKey();

			// вот теперь делай что хочешь, я пока посмотрю
			//читай еще раз 4 главу сначала  внимательно: как задаются методы, и как они вызываются
		}


		// 3) какого хрена здесь нет пустых строк? эта другая функция, их надо разделять визуально
		static void DrawHorizontal( int x, int y, int len )    // начало в x,y длиной len
		{
			for (int i = 0; i < len; ++i)				
			{
				Console.SetCursorPosition( i, y );
				Console.Write( "s" );
			}
			for (int p = 0; p < len; ++p)
			{
				Console.SetCursorPosition( p, (y + len) );
				Console.Write( "s" );
			}	  	  			
		}


		// то же самое
		static void DrawVertical( int x, int y, int len )  // принимает 3 параметра
		{
			for (int i = 0; i < len; ++i)
			{
				Console.SetCursorPosition( x, i );
				Console.Write( "s" );
			}
			for (int k = 0; k <= len; ++k)
			{
				Console.SetCursorPosition( (x + len), k );
				Console.Write( "s" );
			}
		}



		static void SnakeBody(int x , int y)	// функция тела змеи
		{
			Console.SetCursorPosition( (x+1), (y+1) );
			Console.Write( "*" );
		}
																								

	}
}
