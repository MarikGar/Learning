using System;
using System.Threading;
using Learning;

namespace Snake
{
	public enum UserKey { None, Left, Up, Wright, Down };

	class Program
	{
		const char Wall = '■';

		
		// глобальные переменные
		// чтобы не передавать их во все функции, где нужен размер поля
		// мы их сделаем видимыми для всех функций
		static int Width, Height;

		// прочитай про параметры программы в 3ей главе
		// не надо спрашивать у юзера больше ничо
		public static void Main( string[] args )
		{
			Width = int.Parse( args != null && args.Length > 1 ? args[ 0 ] : "40" );
			Height = int.Parse( args != null && args.Length > 2 ? args[ 1 ] : "20" );

			// поскольку Width & Height глоабльные и видимые, мы можем и не передавать
			// т.е. функция Game( int, int ) переписать в Game()
			// на щас не стану этого делать
			Game( Width, Height );

			// поскольку юзер все равно проиграет, то выведем
			Console.SetCursorPosition( 0, Height + 1 );      // выставляет курсор в нужную позицию для выведения слова
			Utils.Println( "Вы проиграли", ConsoleColor.Red );
			Console.ReadKey();
		}

		// Frame per Second
		const int FPS = 3; // 3 раза в секунду обрабатывать змею

		static void Game( int width, int height )
		{
			Console.Clear();	  			
			Console.CursorVisible = false;	 // мигающий курсор нам в игре не нужен

			DrawBox( width, height );

			// Game-loop
			// это игровой цикл, в котором будут такты
			// каждый такт нам надо обрабатать клавиши, перерисовать змею
			// и [может быть] появиться зайцам и яду
			// потому что зайцы появляются не каждый такт, а 1 раз в 5 секунд, например

			
			while (true)
			{
				ProceedMoves(); //Как правильно указать в параметрах получаемое значения нажатой кнопки ?????				   
			}
			// поскольку сюда надо передать миллисекунды
			// и у нас есть заданный FPS, то разделим 100мс на FPS
			// Thread.Sleep( 1000 / FPS );	  		
		}


		#region Game logic

		static void ProceedMoves()
		{
			GetCurrentKey();
		}

		static void GetCurrentKey() // Как правильно указать в параметрах получаемое значения нажатой кнопки?????
		{
			var ButtomControl =  Enum.UserKey // получается я после присвоения вместо наименования кнопок могу вписывать просто ButtomControl?
			ConsoleKeyInfo push;
			while (Console.KeyAvailable)
			{
				push = Console.ReadKey( true );
				switch (push.Key)
				{
					case ConsoleKey.UpArrow:
						Console.WriteLine( "Вы нажали  '{0}'", push.Key );
						break;
					case ConsoleKey.DownArrow:
						Console.WriteLine( "Вы нажали  '{0}'", push.Key );
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
			
		}
		#endregion

		#region Box drawing	
		static void DrawBox( int width, int height )
		{
			Console.ForegroundColor = ConsoleColor.Cyan;

			DrawHorizontal( 0, 0, width );
			DrawHorizontal( 0, height, width );

			DrawVertical( 0, 0, height );
			DrawVertical( width, 0, height );   // думаю, почему не нарисовался последний квадратик
		}

		static void DrawHorizontal( int x, int y, int len )    // начало в x,y длиной len
		{
			var line = new string( Wall, len );
			Console.SetCursorPosition( x, y );
			Console.Write( line );
		}

		static void DrawVertical( int x, int y, int len )  // принимает 3 параметра
		{
			for (int i = 0; i <= len; ++i)
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
				Console.Write( '☻' );
		}
		#endregion


		

	}
}

