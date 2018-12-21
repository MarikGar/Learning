using System;

using Learning;

// не давай имена проектам с русскими буквами или пробелами
// bulls and cows
// русские буквы плохо, потому что у китайца, который будет запускать твою прогу, не будет русского языка
// пробелы плохо, потому что прогу при запуске через скрипт надо будет засовывать в кавычки
// а еще аргументы в кавычки и получается ересь в кавычках
// называй проекты по-англицки в ПаскальСтиле - каждое слово с большой буквы
namespace BullsAndCows
{
	// не надо плодить кучу солюшенов.
	// достаточно один солюшен, он связан с твоим обучением
	// и добавлять в него новые проекты
	// правый клик на солюшен, и Add new project

	// файл Utils.cs я включил как ссылку на файл из другого проекта
	// если файл поменять там или тут, он автоматически изменится и в другом проекте
	// не надо таскать или копировать код туда-сюда, он один на всех, и это норм
	// поскольку файл Utils может дергаться из разных проектов, я засунул его
	// в namespace Learning. и выше ты можешь увидеть 
	// using Learning; 
	// чтобы Utils стал доступен из кода

	// эти комменты можно удалить

	class Program
	{
		static void Main( string[] args )
		{
			var snum = Utils.AskUserForString( "введите число от 2 до 9" );
			if (!int.TryParse( snum, out int num ))
			{
				// не одна строка, потому в { }
				Utils.Println( "вы ввели не число", ConsoleColor.Red );
				return; // выходим из Main, а значит и из програмы
			}

			var mnum = Math.Max( 2, Math.Min( 1_000_000_000, num ));
			Console.WriteLine( $"вы ввели число {num}. я его изменил до {mnum}" );
			var rand = Utils.GetRandom( mnum );
			Utils.Println( $"я загадал число с {mnum}цифр: {rand}", ConsoleColor.Magenta );

			Console.WriteLine( "Done." );
			Console.ReadLine();

		}

		const int TryCount = 10;
		const int DigitCount = 4;

		static void myCode()
		{
			// не надо изобретать колеса
			// измени на 
			Console.WriteLine( $"я загадал число с {DigitCount} цифрами. вам надо его отгадать за {TryCount} попыток" );
			// вводится загадываемое число
			string number = Utils.GetRandom( DigitCount );

			// NB в плохом коде возникает много WTF, в хорошем мало
			// number & number1 - ничего не говорящие имена
			// number - это строка
			// number.Length - это длина строки
			// как ты длину строки используешь как количество попыток? 
			// получается "hello" и "55555" дают тебе 5 попыток? 
			// WTF ?
			// чтобы строку преобразовать в число используй int.Parse(str)

			// назови строку, что загадал комп как guess
			// назови строку, которую вводит пользователь как input, userTry, option или proposed
			// как больше нравится

			int bull = 0;
			int cow = 0;
			for (int i = 0; i < number.Length; i++) // количество попыток
			{
				// измени на Utils.AskUserForString()
				string number1 = Utils.AskUserForString( "введите свой вариант" ); // вводится загадываемое число

				// введи проверку, что пользовательь не ошибся и не ввел слишком короткую или длиную строку
				// иначе нет смысла провреять быки и коровы если их длина не совпадает

				// остальное пока не смотрел
				for (int k = 0; k < number1.Length; k++) // проверка на быка
				{

					if (number1[ k ] == number[ k ])
					{
						bull = bull + 1;                    // счетчик сколько быков
					}
					else
					{
						for (int s = 0; s < number.Length; s++)
						{
							if (number1[ k ] == number[ s ])
							{
								cow = cow + 1;                 // счетчик на корову

							}

						}
					}


				}
				Console.WriteLine( "быков {0}", bull );
				Console.WriteLine( "коров {0}", cow );
			}

		}
	}
}
