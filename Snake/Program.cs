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
			
			




			DrawHorizontal();
			DrawVertical();
			Console.WriteLine();
			Console.ReadKey();

		}
		void DrawHorizontal( int posx, int posy, int len )    // начало в x,y длиной len
		{
			for (int i = 0; i < len; ++i)
			{
				Console.SetCursorPosition( i, posy );
				Console.Write( "s" );
			}
			for (int p = 0; p < len; ++p)
			{
				Console.SetCursorPosition( p, (posy + len) );
				Console.Write( "s" );
			}	  	  			
		}
		void DrawVertical( int posx, int posy, int len )
		{
			for (int i = 0; i < len; ++i)
			{
				Console.SetCursorPosition( posx, i );
				Console.Write( "s" );
			}
			for (int k = 0; k <= len; ++k)
			{
				Console.SetCursorPosition( (posx + len), k );
				Console.Write( "s" );
			}
		}
			
	}
}
