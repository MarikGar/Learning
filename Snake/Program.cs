using System;
using System.Threading;
using Learning;

namespace Snake
{
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
			Console.SetCursorPosition( 0, Height + 1 );
			Utils.Println( "Вы проиграли", ConsoleColor.Red );
			Console.ReadKey();
		}

		// Frame per Second
		const int FPS = 3; // 3 раза в секунду обрабатывать змею

		static void Game( int width, int height )
		{
			Console.Clear();
			// мигающий курсор нам в игре не нужен
			Console.CursorVisible = false;

			DrawBox( width, height );

			// Game-loop
			// это игровой цикл, в котором будут такты
			// каждый такт нам надо обрабатать клавиши, перерисовать змею
			// и [может быть] появиться зайцам и яду
			// потому что зайцы появляются не каждый такт, а 1 раз в 5 секунд, например
			while (true)
			{
				ProceedMoves();

				// поскольку сюда надо передать миллисекунды
				// и у нас есть заданный FPS, то разделим 100мс на FPS
				Thread.Sleep( 1000 / FPS );
			}
		}

		#region Game logic

		// это глобальная переменная
		// она помнит свое состояние всегда
		// мне нужна для текущего цвета, тебе не нужна и сможешь удалить
		static int CurrColor = 0;

		// рисует смайл за полем
		static void ChangeSmile()
		{
			Console.SetCursorPosition( Width + 5, Height / 2 );
			// убеждаюсь, что цвет есть
			var hasColor = Enum.IsDefined( typeof( ConsoleColor ), ++CurrColor );
			// если такого цвет нет, то снова сбрасываем на 0
			if (!hasColor) CurrColor = 0;
			// ставим существующий цвет
			Console.ForegroundColor = ( ConsoleColor )CurrColor;
			Console.Write( '☻' );
		}

		// функция вызывается каждый такт в Game-loop
		static void ProceedMoves()
		{
			// я просто делаю мигающий смайл гдето за полем
			ChangeSmile();

			// тебе же надо будет обрабатывать клавиши и двигать Змею
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
			Console.Write( "*" );
		}
		#endregion
	}
}
