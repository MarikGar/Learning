using System;

namespace Learning
{
	public struct Coord
	{
		// в нашем 2хмерном случае у координаты 2 поля: х и y
		public int X, Y;

		// заведем конструкторы (он конструируют новый объект)
		// один по 2 координатам сразу
		public Coord( int x, int y ) { X = x; Y = y; }
		// другой по 1ому одинаковому значению
		// и ссылаемся на другой конструктор, чтобы не копировать код, потому что КопиПаста=Зло
		public Coord( int one ) : this( one, one ) { }

		// координата (0,0) имеет уникальное значение. ну и обзавем ее явно
		// if (someCoord == Coord.Zero) сделатьЧтоТо();
		// сразу понятно с чем сраниваем и не ошибемся в написании Coord( 0, 9 ) случайно
		public static readonly Coord Zero = new Coord();

		// добавим Кооррдинаты осей
		public static readonly Coord UnitX = new Coord( 1, 0 ); // x=1, y=0. Ось OX
		public static readonly Coord UnitY = new Coord( 0, 1 ); // x=0, y=1. Ось OY

		// мы собираемся складывать координаты, поэтому добавим оператор сложения
		// он же будет использоваться и для операции +=
		// УХТЫ (мыВышлиИзБухты) 
		// мы теперь можем складывать Координаты также как всякие встроенные int-ы
		// a += b + c;
		public static Coord operator +( Coord a, Coord b )
		  // сложение коррдинаты = просто сложение по каждой оси
		  => new Coord { X = a.X + b.X, Y = a.Y + b.Y };

		// мы собираемся сравнивать координаты (тело со стеной и т.д.)
		// поэтому добавим операторы сранения
		// if (a == b) сделатьЧтоТоКогдаКоординатыРавны()
		public static bool operator ==( Coord a, Coord b )
		  => a.X == b.X && a.Y == b.Y;
		// if (a != b) сделатьЧтоТоКогдаКоординаты_НЕ_Равны()
		public static bool operator !=( Coord a, Coord b )
		  => a.X != b.X || a.Y != b.Y;

		// добавим уж заодно и вычитание
		// Coord a = b - c;
		public static Coord operator -( Coord a, Coord b )
		  => new Coord { X = a.X - b.X, Y = a.Y - b.Y };

		// добавим и противоположную по знаку (а чтобы было)
		// a = -b; // круто
		public static Coord operator -( Coord v )
		  => new Coord { X = -v.X, Y = -v.Y };

		// добавим оператор умножения на int: Coord * Scalar
		public static Coord operator *( Coord c, int s ) => new Coord( c.X * s, c.Y * s );
		// но чаще мы будем писать Scalar * Coord
		public static Coord operator *( int s, Coord c ) => new Coord( c.X * s, c.Y * s );

		// ненавижу эти тупые варнинги
		public override int GetHashCode()
			=> base.GetHashCode();
		public override bool Equals( object obj )
			=> base.Equals( obj );

		// чтобы в отладчике или при пчати можно было видеть нормальное занчение, а не {Snake.Coord}
		// перепишем(override) метод ToString, который есть у всех
		public override string ToString() => $"( {X}, {Y} )";
	}
} // почитай, что такое неймспейсы.. хаватит уже тупить на мелочах
