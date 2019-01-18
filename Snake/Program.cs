using System;
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
		const int FPS = 4; // 4 раза в секунду обрабатывать змею

		static void Game( int width, int height )
		{
			Console.Clear();
			Console.CursorVisible = false;   // мигающий курсор нам в игре не нужен

			// нарисуем границу
			DrawBox( width, height );

			// вот теперь понятно и кратко и пистато
			SetupSnake();

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

		// это связанные друг с другом переменные. держи их вместе
		static Coord _head; // координаты головы Змеи (Head)
		static Coord _ofs; // это текущее направление движения (Offset)

		static Coord _b1; // координаты 1 клетки тела 
		static Coord _b2;
		static Coord _old;

		// слишком много кода для настройки Змеи
		// потому выделим его в отдельный метод, чтобы не засорял наше и так мутное сознание
		// когда мы читаем код в методе Game
		static void SetupSnake()
		{
			// установим Змею в первоначальное положение по центру Границ
			// по горизонтали: голова Змеи в среднем столбце, тело тоже, идет вниз
			// юзаем конструктор по 2м координатам
			_head = new Coord( Width / 2, Height / 2 ); // по центру
			_ofs = Coord.Zero;
			// по вертикали: а тело на 1 клетку ниже
			_b1 = _head; // здесь присваиваем _head
			++_b1.Y; // а здесь делаем на 1летку ниже (увеличивае Y)
					 // 2ая клетка еще на 1 клутку ниже
			_b2 = _b1;
			++_b2.Y;
			// чтобы не затирала границу в (0,0)
			_old = _head;
		}

		static void ControlSnake( UserKey userKey )
		{	   			
			if (userKey == UserKey.None) return;
			_ofs = Coord.Zero;
			switch (userKey)
			{
				case UserKey.Left: _ofs = -Coord.UnitX; return; // красиво, но навен малопнятно для темных )
				case UserKey.Right: _ofs = Coord.UnitX; return;
				case UserKey.Up: _ofs = -Coord.UnitY; return; // юзается оператор -(Coord)
				case UserKey.Down: _ofs = Coord.UnitY; return;
			}
			// а вот здесь в зависимости от клавиши ты бедшь менять текущее направление движения
			// заметь, здесь не меняется позиция головы, она будет менять уже в другой функции

			// менять dx,dy в зависимости от клавиши
		}

		static bool MoveSnake()
		{
			// если змея не двигается, то ничего и не делаем
			if (_ofs == Coord.Zero) return true;

			// гораздо меньше кода и вроде как понятнее
			// если ты всюду будешь таскать x,y или двустрочные массивы, то мы тя первыми сдадим в дурку
			// а потому что ты сам от своих путаниц сойдешь с ума, а не по другйо причине
			_old = _b2;
			_b2 = _b1;
			_b1 = _head;
			_head += _ofs;

			// код длиньше, но из него поянятно, что сравниваем чото для головы _head
			if (_head.X == Width || _head.Y == Height || _head.X == 0 || _head.Y == 0) return false;

			// здесь условие изменится, потому что надо будет сранвивать не со 2ой клеткой
			// а ваще с любой из тела (но пока оставим почти как есть)
			if (_head == _b2) return false;

			// вот здесь будет двигаться Змея
			// менять x,y и проверить, заодно, чтобы не вышло за экраны

			// если Змея врежется в экран, то игра заканивается и вернем false
			return true;
		}

		static void DrawSnake()
		{
			// было Console.SetCursorPosition( _ox , _oy  );
			// стало
			SetCursor( _old );
			Console.Write( ' ' );

			Console.ForegroundColor = ConsoleColor.Yellow;
			SetCursor( _head );
			Console.Write( SnakeHead );

			SetCursor( _b1 );
			Console.Write( SnakeBody);

			SetCursor( _b2 );
			Console.Write( SnakeBody );
		}
		#endregion

		#region всяко-разно
		// добавим метод для позиционирования по Coord
		static void SetCursor( Coord v ) => Console.SetCursorPosition( v.X, v.Y );
		#endregion
	}
}

