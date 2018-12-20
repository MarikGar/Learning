using System;

namespace Sravnenie
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
	}
}
