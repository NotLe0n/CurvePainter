using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace CurvePainter
{
    public static class Input
    {
        public static Vector2 MousePosition => Mouse.Position.ToVector2();
        public static bool MouseLeftDown => Mouse.LeftButton == ButtonState.Pressed;
        public static bool MouseRightDown => Mouse.RightButton == ButtonState.Pressed;
        public static bool MouseLeftClick => !(LastMouse.LeftButton == ButtonState.Pressed) && MouseLeftDown;
        public static bool MouseRightClick => !(LastMouse.RightButton == ButtonState.Pressed) && MouseRightDown;
        public static bool MouseLeftReleased => !MouseLeftDown && LastMouse.LeftButton == ButtonState.Pressed;
        public static bool MouseRightReleased => !MouseRightDown && LastMouse.RightButton == ButtonState.Pressed;
        public static MouseState Mouse => Microsoft.Xna.Framework.Input.Mouse.GetState();
        public static MouseState LastMouse => _lastMouse;

        public static KeyboardState Keyboard => Microsoft.Xna.Framework.Input.Keyboard.GetState();
        public static KeyboardState LastKeyboard => _lastKeyboard;
        public static bool JustPressed(Keys key) => !LastKeyboard.IsKeyDown(key) && Keyboard.IsKeyDown(key);


        private static KeyboardState _lastKeyboard;
        private static MouseState _lastMouse;
        public static void UpdateLastVariables()
        {
            _lastKeyboard = Microsoft.Xna.Framework.Input.Keyboard.GetState();
            _lastMouse = Microsoft.Xna.Framework.Input.Mouse.GetState();
        }
    }
}
