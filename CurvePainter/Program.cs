using System;

namespace CurvePainter
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new CurvePainter())
                game.Run();
        }
    }
}
