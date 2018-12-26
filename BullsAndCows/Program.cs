using System;
using System.Linq;

using Learning;

namespace BullsAndCows
{
	class Program
	{
		const int TryCount = 12; // количестов попыток
		const int DigitCount = 4; // количество цифр в числе
		static void Main( string[] args )
		{			
			MultiGame();

			Utils.Print ( "Спасибо за игру", ConsoleColor.Magenta );
			Console.ReadLine();
		}
			
		#region Game methods
		static void Game()
		{
			Console.Clear();
			// комп загадал 4хзначное число
			var guess = Utils.GetRandom( 4 );
			Utils.Println($"Я загадл {DigitCount}-х значное число.У Вас есть {TryCount} попыток, чтобы отгадать его", ConsoleColor.Magenta );
			// у юзера есть 12 попыток отгадать число
			for (int k = 1; k <= TryCount; ++k)
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

			// если мы оказались здесь, то юзер не отгадал (иначе бы мы вышли из функции по return)
			// покажем ему наше загаданное число
			Utils.Println( $"К сожалению Вы не отгадали мое число: {guess}", ConsoleColor.Red );
			Console.Clear();
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
		#endregion

		static void MultiGame()
		{
			string yes = "";
			do
			{
				Game();
				yes = Utils.AskUserForString( "Хотите сыграть еще раз? (Y/N)" );
			}
			while (yes.ToLower() == "y");
			

		}

		static void MultiGame2()
		{
			// а можно и так через обычный while с true - означает выполнять всегда
			// но в цикле есть проверка, которая выходит из цикла по break
			while (true)
			{
				Game();
				var yes = Utils.AskUserForString( "Хотите сыграть еще раз? (Y/N)" );
				// если не равно Y, то выходим из цикла (больше не играем)
				if (yes.ToLower() != "y") break;
			}
		}
	}
}
