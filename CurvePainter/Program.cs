using System;

namespace CurvePainter;

public static class Program
{
	[STAThread]
	private static void Main()
	{
		using var game = new CurvePainter();
		game.Run();
	}
}