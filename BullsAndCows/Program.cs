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
			var guees = Utils.GetRandom( 4 );
			var userInput = Utils.AskUserForString( "Ведите свой вариант" ); 
			if (guees == userInput)
			{ 
				Console.WriteLine( "Вы ввели правильное значение" ); // здесь еще должно быть количество попыток
				
			}
			else
			{
				Console.WriteLine( "хуй" ); // здесь будет результат функции static (int bull, int cow) myCode( guees , userInput)

			}
			Console.ReadLine();

		}
	}
}
static (int bull, int cow) myCode( guees , userInput)
{
	int bull = 0;
	int cow = 0;
	for (int i = 0; i < guees.Length; i++) // количество попыток
	{
		for (int k = 0; k < userInput.Length; k++) // проверка на быка
		{

			if (userInput[ k ] == guees[ k ])
			{
				bull = bull + 1;                    // счетчик сколько быков
			}
			else
			{
				for (int s = 0; s < guees.Length; s++)
				{
					if (userInput[ k ] == guees[ s ])
					{
						cow = cow + 1;                 // счетчик на корову

					}

				}
			}


		}
		return (bull, cow);
	}

}
	}
}

		