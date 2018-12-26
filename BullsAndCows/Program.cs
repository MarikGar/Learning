using System;
using System.Linq;

using Learning;

namespace BullsAndCows
{
	class Program
	{
		static void Main( string[] args )
		{
			Game();
			Con();
			Console.WriteLine( "\nDone." );
			Console.ReadLine();
		}

		static void Game()
		{
			// комп загадал 4хзначное число
			var guess = "5005"; // Utils.GetRandom( 4 );

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

				if (!CheckUserTry( userTry, guess.Length ))
					continue;

				// подсчитаем количество быков и коров
				var calcs = CalcBullsAndCows( guess, userTry );

				// покажем юзеру его результаты
				Utils.Println( $"{calcs.bulls} быков, {calcs.cows} коров", ConsoleColor.White );
			}

			// если мы оказались здесь (юзерне отгадал за 12 попыток, иначе бы мы вышли из функции по return)
			// то юзер не отгадал. покажем ему наше загаданное число
			Utils.Println( $"К сожалению Вы не отгадали мой число: {guess}", ConsoleColor.Red );
		}

		static bool CheckUserTry( string userTry, int needLength )
		{
			if (userTry.Length != needLength)
			{
				Utils.Println( $"должно быть {needLength} символов", ConsoleColor.Red );
				return false;
			}

			if (!int.TryParse( userTry, out var num ))
			{
				Utils.Println( $"должно быть число", ConsoleColor.Red );
				return false;
			}

			return true;
		}

		static (int bulls, int cows) CalcBullsAndCows( string guess, string userTry )
		{
			var sums = new int[ 10 ];
			int bulls = 0, cows = 0;
			for (int i= 0 ; i < guess.Length; i++)
			{
				var uc = userTry[ i ];
				if (uc == guess[ i ])
					bulls++;
				else
					sums[ uc - '0' ] = guess.Count( c => c == uc );
			}
			cows = sums.Sum();
			return (bulls, cows);
		}
		static void Con()
		{
			Found :
			Console.WriteLine( "Введите Y чтобы продолжить или N чтобы завершить " );
			string ot = Console.ReadLine();
			if ((ot == "y") || (ot == "Y"))
			{
				Game();
				goto Found;
			}
			else
			{

			}
			

		}
	}
}
