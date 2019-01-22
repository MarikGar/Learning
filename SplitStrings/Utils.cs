using System;			    
using System.Collections.Generic; // Generic - переводится как шаблон. в этом неймспейсе находится List<T>

namespace Learning
{
	// класс всяких утилиток/методов
	public static class  Utils
	{
		#region input & output
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
		#endregion

		#region randoms
		// возвращает случайную строку из digits цифр
		// 4: 8715  5047  5324  5481  4634
		// 5: 87599 73232 10478 31672 82475
		public static string GetRandom( int DigitCount )
		{
			var min = (int)Math.Pow( 10, DigitCount - 1 );
			var max = min * 10;
			return _rnd.Next( min, max ).ToString();
		}
		static readonly Random _rnd = new Random();
		#endregion

		#region Shifts
		public static void ArrayShiftLeft <T> ( T[] arr )
		{
			var last = arr[ 0 ];
			for (int i = 0; i < arr.Length - 1; i++)
				arr[ i ] = arr[ i + 1 ];
			arr[ arr.Length - 1 ] = last;
		}

		public static void ArrayShiftRight<T>( T[] arr )
		{
			// исправь чтобы сдвигал вправо
			var first = arr[ (arr.Length-1) ];
			for (int i = (arr.Length-1); i > 0; i--)
				arr[ i ] = arr[ i-1 ];
			arr[ 0 ] = first; ; 		}

		public static void ListShiftLeft<T>( List<T> list )
		{
			var last = list[ 0 ];
			for (int i = 0; i < list.Count - 1; i++)
				list[ i ] = list[ i + 1 ];
			list[ list.Count - 1 ] = last;
		}

		public static void ListShiftRight<T>( List<T> list ) // для переименования жмем Ctrl+R
		{
			// исправь чтобы сдвигал вправо
			var first = list[ list.Count-1 ];
			for (int i = (list.Count - 1); i > 0; i--)
				list[ i ] = list[ i - 1 ];
			list[ 0 ] = first;
		}
		#endregion

		#region collections
		// Extension метод отличается от обычного тока ключевым словом this в параметрах
		// и его можно будет дергать для объекта так, будто он сразу там и был
		// например, ниже мы определим метод последнего элемента для списка, 
		// чтобы не писать list[ list.Count - 1 ], где можно ошибиться
		public static T LastItem<T>( this List<T> lst ) => lst[ lst.Count - 1 ];
		// и для нулевого тоже
		public static T FirstItem<T>( this List<T> lst ) => lst[ 0 ];
		#endregion
	}
}
