﻿using System;
using System.Diagnostics;
using System.Threading;

using Learning;

namespace Snake
{
	// право - это Right, не Wright
	public enum UserKey { None, Left, Up, Right, Down };

	class Program
	{
		const char Wall = '■';

		//const char SnakeHead = '☺';
		//const char SnakeBodyq = '○';
		//const char SnakeHead = '☻';
		//const char SnakeBody = '●';
		const char SnakeHead = '@';
		const char SnakeBody = 'o';

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
			Console.CursorVisible = false;   // мигающий курсор нам в игре не нужен

			_x = Width / 2;
			_y = Height / 2;
			_dx = _dy = 0;

			DrawBox( width, height );

			// Game-loop
			// это игровой цикл, в котором будут такты
			// каждый такт нам надо обрабатать клавиши, перерисовать змею
			// и [может быть] появиться зайцам и яду
			// потому что зайцы появляются не каждый такт, а 1 раз в 5 секунд, например

			while (ProceedMoves())
			{
				// поскольку сюда надо передать миллисекунды
				// и у нас есть заданный FPS, то разделим 100мс на FPS
				Thread.Sleep( 1000 / FPS );
			}
		}

		#region Game logic
		// возвращает true, если надо продолжать игру
		// иначе false, по которому завершаем Game-loop в Game()
		static bool ProceedMoves()
		{
			// функция возвращает реузльтат (так было задумано) Юзерскую кнопку
			// так присвой ее какойнить переменной
			var key = GetCurrentKey();

			// и выведи кнопку в консоль
			// код надо отлаживать и писать так, чтобы видеть результат выполнения
			//Console.SetCursorPosition( Width + 5, Height / 2 );
			//Console.Write( $"{key}   " );
			if (key != UserKey.None) Debug.Print( $"{DateTime.Now:HHmmss.fff} {key}\n" );

			ControlSnake( key );
			var snakeIsOk = MoveSnake();

			DrawSnake();

			return snakeIsOk;
		}

		// функция возвращает юзерскую клавишу. одну из списка констант UserKey
		// что тут непонятного?
		static UserKey GetCurrentKey()
		{
			// если ничо не нажато, то так и выдаем - None
			if (!Console.KeyAvailable) return UserKey.None;
			// воклицательный знак означает НЕ. читай про унарные операторы
			// и все условие звучит как "если клавиша НЕ доступна, то ..."

			// здесь какието клавиши лежат в буфере
			// их надо все забрать из буфера и по последней выдать результат
			ConsoleKeyInfo key; // было название "push" - еще раз тупое название для клавиши
			do key = Console.ReadKey( true );
			while (Console.KeyAvailable);
			// пока клавиши доступны в буфере консоли, вытаскиваем их по одной в key
			// ВСЁ. вот такой простой тупой цикл
			// озвучь по-русски, что надо сделать и сделай
			// почитай про метод Резиновой уточки в википедии

			// теперь key содержит последюю клавишу
			// по ней и вернем из функции результат
			// потому что функция была задумана для того, чтобы вернуть нажатую клавишу. НЕЕ?
			switch (key.Key)
			{
				case ConsoleKey.LeftArrow: return UserKey.Left;
				case ConsoleKey.RightArrow: return UserKey.Right;
				case ConsoleKey.UpArrow: return UserKey.Up;
				case ConsoleKey.DownArrow: return UserKey.Down;
			}

			// чо тут сложного? что ты так долго тупишь над простой функцией?
			// непонятно, читай про функции еще раз.. в других источниках.. 
			// не надо книжку до дыр зачитывать, если в ней тебе непонятна тема
			// есть дохуя где можно еще почитать про функции, ее параметры и возвращаемые значения
			return UserKey.None;
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


		#region Snake drawing & control		

		static int _x, _y; // координаты головы Змеи
		static int _bx, _by; // координаты 1 клетки тела 
		static int _dx, _dy; // это текущее направление движения

		static void ControlSnake( UserKey userKey )
		{
			Console.SetCursorPosition( _bx - _dx, _by - _dy ); ;
			Console.Write( ' ' );
			if (userKey == UserKey.None) return;
			_dx = _dy = 0;
			switch (userKey)
			{
				case UserKey.Left: _dx = -1; return;
				case UserKey.Right: _dx = 1; return;
				case UserKey.Up: _dy = -1; return;
				case UserKey.Down: _dy = 1; return;
			}
			// а вот здесь в зависимости от клавиши ты бедшь менять текущее направление движения
			// заметь, здесь не меняется позиция головы, она будет менять уже в другой функции

			// менять dx,dy в зависимости от клавиши
		}

		static bool MoveSnake()
		{
			Console.SetCursorPosition( _bx , _by ); 
			Console.Write( ' ' );
			_bx = _x;
			_by = _y;
			_x = _x + _dx;
			_y = _y + _dy;	  			
			 		
			// вот здесь будет двигаться Змея
			// менять x,y и проверить, заодно, чтобы не вышло за экраны

			// если Змея врежется в экран, то игра заканивается и вернем false
			return true;
		}

		static void DrawSnake()
		{	 				   
			Console.ForegroundColor = ConsoleColor.Yellow;
			Console.SetCursorPosition( _x, _y );
			Console.Write( SnakeHead );
			Console.SetCursorPosition( _bx, _by );
			Console.Write( SnakeBody );
		}
		#endregion
	}
}

