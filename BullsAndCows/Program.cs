using System;

using Learning;

namespace BullsAndCows
{
	class Program
	{
		static void Main( string[] args )
		{
			Game();

			Console.WriteLine( "\nDone." );
			Console.ReadLine();
		}

		static void Game()
		{
			// комп загадал 4хзначное число
			var guess = "1234"; // Utils.GetRandom( 4 );

			// у юзера есть 12 попыток отгадать число
			for (int k = 1; k <= 12; ++k)
			{
				// запросим у юзреа очередную попытку
				var userTry = Utils.AskUserForString( $"попытка №{k}" );

				// проверим, не отгадал ли юзер наше число
				if (userTry == guess)
				{
					Utils.Println( "УРА! Вы отгадали! Поздравляю!", ConsoleColor.Cyan );
					// юзер выиграл игру. больше делать нечего. выходим из функции
					return;
				}

				// подсчитаем количество быков и коров
				var calcs = CalcBullsAndCows( guess, userTry );

				// покажем юзеру его результаты
				Utils.Println( $"{calcs.bulls} быков, {calcs.cows} коров", ConsoleColor.White );
			}

			// если мы оказались здесь (юзерне отгадал за 12 попыток, иначе бы мы вышли из функции по return)
			// то юзер не отгадал. покажем ему наше загаданное число
			Utils.Println( $"К сожалению Вы не отгадали мой число: {guess}", ConsoleColor.Red );
		}

		static (int bulls, int cows) CalcBullsAndCows( string guess, string userTry )
		{
			return (0, 0);
		}
	}
}
