using System;
using Learning;

namespace Snake
{
	class Program
	{
		const char Wall = '■';

		// прочитай про параметры программы в 3ей главе
		// не надо спрашивать у юзера больше ничо
		public static void Main( string[] args ) 
		{
			var Width = int.Parse( args != null && args.Length > 1 ? args[ 0 ] : "40" );
			var Height = int.Parse( args != null && args.Length > 2 ? args[ 1 ] : "20" );

			Game( Width, Height );

			// поскольку юзер все равно проиграет, то выведем
			Console.SetCursorPosition( 0, Height + 1 );
			Utils.Println( "Вы проиграли", ConsoleColor.Red );
			Console.ReadKey();
		}

		static void Game( int width, int height )
		{
			Console.Clear();
			DrawBox( width, height );
		}


		#region Box drawing									 
		static void DrawBox( int width, int height )
		{
			Console.ForegroundColor = ConsoleColor.Cyan;

			DrawHorizontal( 0, 0, width ); 
			DrawHorizontal( 0, height, width );

			DrawVertical( 0, 0, height );
			DrawVertical( width, 0, height );	// думаю, почему не нарисовался последний квадратик
		}


		static void DrawHorizontal( int x, int y, int len )    // начало в x,y длиной len
		{
			var line = new string( Wall, len );
			Console.SetCursorPosition( x, y );
			Console.Write( line );
		}

		static void DrawVertical( int x, int y, int len )  // принимает 3 параметра
		{
			for (int i = 0; i < len; ++i)
			{
				Console.SetCursorPosition( x, i );
				Console.Write( Wall );
			}
		}
		#endregion

		#region Snake drawing
		static void SnakeBody( int x, int y )   // функция тела змеи   // это мнен для ориентира
		{
			Console.SetCursorPosition( (x + 3), (y + 3) );
			Console.Write( "*" );
		}
		#endregion
	}
}
