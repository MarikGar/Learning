using System;
using System.Collections.Generic;
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
		const char Food = '$';

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
			Utils.Println( "Нажмите <Escape> для выхода из программы ", ConsoleColor.Yellow );
			while (Console.ReadKey().Key != ConsoleKey.Escape) { }	 			
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

			SetupFood();

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
			DrawFood();

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

		static List<Coord> _snake = new List<Coord>();
		// это связанные друг с другом переменные. держи их вместе
		//static Coord _head; // координаты головы Змеи (Head)
		static Coord _ofs; // это текущее направление движения (Offset)

		//static Coord _b1; // координаты 1 клетки тела 
		//static Coord _b2;
		static Coord _old;

		// слишком много кода для настройки Змеи
		// потому выделим его в отдельный метод, чтобы не засорял наше и так мутное сознание
		// когда мы читаем код в методе Game
		static void SetupSnake()
		{
			// установим Змею в первоначальное положение по центру Границ
			// по горизонтали: голова Змеи в среднем столбце, тело тоже, идет вниз
			// юзаем конструктор по 2м координатам
			// голова будет по центру
			var head = new Coord( Width / 2, Height / 2 ); 
			// голова будет в [0] значит ее добавим первой
			_snake.Add( head );
			// по вертикали: а тело на 1 клетку ниже
			_snake.Add( head + Coord.UnitY );
			// хорошо бы заменить на + 2 * Coord.UnitY.. так и сделаем
			_snake.Add( head + 2 * Coord.UnitY );
			// хоть визуально у нас ничо не изменилось, но мы избавились от кучи x,y,bx,by,cx,cy
			// в принципе, не так уж много, но если бы у нас тело сразу было из 7 клеток, то уже запарило бы

			// смещение (0,0) потому что пока Змея не двигается
			_ofs = Coord.Zero;
			// очитску присвоим head, чтобы не затирала границу в (0,0)
			_old = head;
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

			// чтобы не писать каждый раз _snake[0] или _snake[_snake.Count - 1 ]
			// добавим в Utils Extension method LastItem
			_old = _snake.LastItem();
			// и выше мы вызвали наш метод так, будто он сразу был в List<T>, хотя его там не было, а мы сами тока что добавили

			// запомни текущую позицию головы
			var head = _snake.FirstItem();
			// сдвинем всю змею от головы[0] к хвосту - значит вправо
			Utils.ListShiftRight( _snake );
			// теперь голова ушла на [1] (а в [0] у нас последний элемент, ты так захотел, но он нам не ужен)
			// мы должны его заменить на новую позицю головы
			head += _ofs;
			_snake[ 0 ] = head;
			// здесь мы не можем заюзать .FirstItem() потому что он только на чтение (мы получаем из него значение), 
			// а не на запись, которой нам и надо изменить значение. а мы меняем

			// код длиньше, но из него поянятно, что сравниваем чото для головы _head
			if (head.X == Width || head.Y == Height || head.X == 0 || head.Y == 0) return false;

			// здесь условие изменится, потому что надо будет сранвивать не со 2ой клеткой
			// а ваще с любой из тела (но пока оставим почти как есть)
			// поскольку нас теперь змея длинная в списке, то надо проверить со всем списком (кроме самой головы)
			// если голова встречается в позиции отличной от головы, значит мы пересекли себя же
			if (_snake.IndexOf( head, 1 ) >= 0) return false;
			// почему 1, понимаю голова , а один?
			// потому что голова в [0], а проверить надо со всем остальным телом, которое начинается с 1

			// а теперь проверим с кроликом
			if (IsFoodEaten)
			{
				// старую еду надо убрать
				// установить новую
				SetupFood(); // этим мы сделали и убрали и установили сразу

				_inc += 3;
			}

			if (_inc > 0) // ты уже здесь вычел 1
			{
				_snake.Add( _old );
				_old.X = Width + 2; // вне поля
				_inc = _inc - 1; // пиши как тебе понятнее
				// --x это то же что и x-=1 а это то же что и x=x-1
			}

			return true;
		}

		static int _inc;

		static void DrawSnake()
		{
			// было Console.SetCursorPosition( _ox , _oy  );
			// стало
			SetCursor( _old );
			Console.Write( ' ' );

			Console.ForegroundColor = ConsoleColor.Yellow;

			// поскольку сначала у нас змея состоит из 3х клеток, то и будем рисовать 3 клетки
			// а здесь можно змею нарисовать оч просто
			// змея сместилась на 1 позицию
			// там, где была раньше голова ([1]) надо поставить тело
			SetCursor( _snake[ 1 ] );
			Console.Write( SnakeBody );
			SetCursor( _snake[ 2 ] );
			Console.Write( SnakeBody );
			// а вот голову уже нарисовать в новой [0]
			SetCursor( _snake.FirstItem() );
			Console.Write( SnakeHead );

			// выведи длину змеи здесь
			Console.SetCursorPosition( Width + 7, Height / 2 );
			// либо создание любого объекта проиходит через new и конструктор		
			SetCursor( new Coord( Width + 7, Height / 2 ) );
			// я сговорил в цвете используя Utils.? o
			Utils.Print( $"Длина={_snake.Count}", ConsoleColor.Magenta );
			//Utils.Print( _snake.Count.ToString(), ConsoleColor.Magenta );
			// изучи вопрос как преобразовывать любые значения в строку
		}
		#endregion

		#region food (кролик)
		// позиция кролика-еды
		static Coord _food;

		static void SetupFood()
		{
			// поскольку у нас уже есть Random в utils, его и заюзаем, а значит добавим туда новый метод
			// от 1 включительно до Width/Height исключительно - тем самым кролика ставим уже точн)
			for (_food = new Coord( Utils.NextRandom( 1, Width ), Utils.NextRandom( 1, Height )); 
				// если в Змее нашлась такая же точка, то результат будет Индекс, который >= 0
				// если не нашлоась, то Индекс будет -1
				// повторяем цикл, пока точка в змее
				_snake.IndexOf( _food ) >= 0;
				_food = new Coord( Utils.NextRandom( 1, Width ), Utils.NextRandom( 1, Height )))
			{
			}
		}

		static bool IsFoodEaten => _food == _snake.FirstItem();

		static void DrawFood()
		{
			Console.ForegroundColor = ConsoleColor.Green;
			SetCursor( _food );
			Console.Write( Food );
		}
		#endregion

		#region всяко-разно
		// добавим метод для позиционирования по Coord
		static void SetCursor( Coord v ) => Console.SetCursorPosition( v.X, v.Y );
		// этот метод принмает только Coord, он не принимает 2 инта
		#endregion
	}
}

