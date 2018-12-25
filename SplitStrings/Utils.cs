using System;

namespace Learning
{
	// класс всяких утилиток/методов
	public static class Utils
	{
		// поскольку запрос строки у юзера не такая уж уникальная операция 
		// и может понадобиться везде
		// вынес ее в отдельный метод/процедуру
		// да еще и раскрасим прогу (запусти и посмотри)
		public static string AskUserForString( string text )
		{
			// поскольку мы не должны чтото менять глобально
			// то запомним старый цвет консоли

			// даем переменным нормальные названия, чтобы сразу было понятно, что это за хрень
			var oldColor = Console.ForegroundColor;
			
			// установим желтый цвет для приглашения
			Console.ForegroundColor = ConsoleColor.Yellow;
			// вводить будем на этой же строке, потому просто Write, не WriteLine
			Console.Write( text + ": " );
			
			// зеленый для юзерского ввода
			Console.ForegroundColor = ConsoleColor.Green;
			var res = Console.ReadLine();

			// а здесь восстановим старый цвет
			Console.ForegroundColor = oldColor;

			return res;
		}

		// печатает строку какимто цветом
		public static void Print( string text, ConsoleColor color )
		{
			var oldColor = Console.ForegroundColor;
			Console.ForegroundColor = color;
			Console.Write( text );
			Console.ForegroundColor = oldColor;
		}

		// печатает строку какимто цветом и переводит курсор на новую строку
		public static void Println( string text, ConsoleColor color )
		{
			Print( text, color );
			Console.WriteLine();
		}

		// возвращает случайную строку из digits цифр
			// 4: 8715  5047  5324  5481  4634
			// 5: 87599 73232 10478 31672 82475
			public static string GetRandom( int digits )
		{
			var min = ( int )Math.Pow( 10, digits - 1 );
			var max = min * 10;
			return _rnd.Next( min, max ).ToString();
		}
		static readonly Random _rnd = new Random();
	}
}
