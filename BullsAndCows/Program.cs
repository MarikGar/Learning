using System;
using System.Linq;

using Learning;

namespace BullsAndCows
{
	class Program
	{
		static void Main( string[] args )
		{
			// сначала мы играем, потом чтото делаем еще - непонятно
			//Game();
			// по названию ваще них не понятно. 
			// даже нет предположений что это может быть
			// Может чистим Консоль-экран?.. хз
			// если это все таки серия игр, то надо сделать ток один метод
			// который сначала сыграет 1 раз, а потом спросит про продолжения
			// измени свой метод, чтобы он работал без Game() выше
			// т.е. первый Game() надо вызвать в самом методе
			//Con();

			// поскольку игра и следующие игры связаны, задавай их в одном методе
			MultiGame();

			Console.WriteLine( "\nDone." );
			Console.ReadLine();
		}

		// код можно прятать в такие сворачивающиеся области, чтобы не листамть 2-10 экранов, 
		// когда начнутся какието другие методы, не свзяанные с игрой
		// нажми [-] или [+] слева от #region
		#region Game methods
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

			// если мы оказались здесь, то юзер не отгадал (иначе бы мы вышли из функции по return)
			// покажем ему наше загаданное число
			Utils.Println( $"К сожалению Вы не отгадали мое число: {guess}", ConsoleColor.Red );
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

		// и так сойдет
		// давай понятные названия
		// когда будет дергаться метод Con() - них непонятно, что оно делает
		// ContinueGaming() - более менее понятное
		static void Con()
		{
			Found :
			// что продолжить? что завершить?
			// спроси по-русски "Хотите сыграть еще раз? (Y/N)"
			Console.WriteLine( "Введите Y чтобы продолжить или N чтобы завершить " );
			string ot = Console.ReadLine();
			if ((ot == "y") || (ot == "Y"))
			{
				Game();
				// goto - плохое слово
				// оно в основном используется в генерируемомо коде, потому компу так что проще
				// если goto встречается в человеческом коде, значит чтото не так 
				// или это должно быть спрятано на нижних уровнях кода
				goto Found;
			}
			// если else пустое, то его вообще не надо
			else
			{

			}
		}

		static void MultiGame()
		{
			string yes = "";
			// тело цикла do-while выполняется хотя бы раз - то, что нам и надо
			do
			{
				Game();
				yes = Utils.AskUserForString( "Хотите сыграть еще раз? (Y/N)" );
			}
			// выполнять цикл, пока юзер грит Y или y
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
