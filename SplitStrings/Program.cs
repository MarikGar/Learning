using System;

using Learning;

namespace SplitStrings
{
	class Program
	{
		static void Main( string[] args )
		{
			// методы/процедуры нужны, чтобы читая код сразу было понятно
			// что тут происходит. C Console.WriteLine( введите текст )
			// и Console.ReadLine() тоже понятно, но мы говорим про вообще
			string text = Utils.AskUserForString( "введите строку" );

			var res = Obrabotka( text );
			Console.WriteLine( "  ЧЕТ: {0} ", res.Evens );
			Console.WriteLine( "НЕЧЕТ: {0} ", res.Odds );

			// покажем юзеру, что прога остановилась
			// потому что иногда мы ничего не выводи, 
			// и непонятно, прога еще работает или уже нет
			Console.WriteLine( "\nDone." );
			// символ \n выше добавляет пустую строку
			Console.ReadLine();
		}

		static (string Evens, string Odds) Obrabotka( string text )
		{
			string se = null;
			string so = null;
			for (int i = 0; i < text.Length; i++)
			{
				if ((i % 2 == 0))
					// чтобы не писать s = s +
					// можно писать сразу += то же самое
					se += text[ i ];  
				else
					/* это многострочный коммент выделенный по-другому - слэш+звездочка
						если одна строка кода, то ее лучше не выделять скобками { }	*/
					so += text[ i ];
			}
			return (se, so);
		}
	}
}
