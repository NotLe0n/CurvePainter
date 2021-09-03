using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace CurvePainter
{
    class CurveDrawArea
    {
        public readonly List<Curve> curves = new List<Curve>();
        private Line _drawingCurve;
        private bool drawingMode;
        private CurveType curveType;

        public enum CurveType
        {
            Bezier, Spline
        }

        public void Update(GameTime gameTime)
        {
            UpdateInput();

            if (drawingMode)
            {
                var lastCurveEnd = curves.Count == 0 ? Input.MousePosition : curves[^1].controls[^1];

                if (Input.MouseLeftDown && _drawingCurve == null)
                {
                    _drawingCurve = new Line(lastCurveEnd, Input.MousePosition, Color.Orange);
                }

                if (!Input.MouseLeftDown && _drawingCurve != null)
                {
                    AddCurve(_drawingCurve.startPoint, Input.MousePosition, curveType);
                }
            }

            FixSplineEndings();
            _drawingCurve?.Update();
            foreach (var curve in curves)
            {
                curve.Update(gameTime);
            }
        }

        private void UpdateInput()
        {
            if (Input.JustPressed(Keys.F))
            {
                drawingMode = !drawingMode;
            }
            if (Input.JustPressed(Keys.R))
            {
                curves.RemoveAll(x => x.Selected);
            }
            if (Input.JustPressed(Keys.E))
            {
                curveType = (CurveType)(((uint)curveType + 1) % 2);
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(CurvePainter.font, "Curve Count: " + curves.Count, new Vector2(10, 20), Color.White);
            spriteBatch.DrawString(CurvePainter.font, "Mode: " + (drawingMode ? "Draw" : "Select"), new Vector2(10, 50), Color.White);
            spriteBatch.DrawString(CurvePainter.font, "Curve Type: " + curveType, new Vector2(10, 80), Color.White);

            _drawingCurve?.Draw(spriteBatch);
            foreach (var curve in curves)
            {
                curve.Draw(spriteBatch);
            }
        }

        private void AddCurve(Vector2 start, Vector2 end, CurveType type)
        {
            switch (type)
            {
                case CurveType.Bezier:
                    curves.Add(new BezierCurve(start, end));
                    break;
                case CurveType.Spline:
                    curves.Add(new SplineCurve(start, end));
                    break;
                default:
                    break;
            }

            _drawingCurve = null;
        }

        private void FixSplineEndings()
        {
            for (int i = 0; i < curves.Count; i++)
            {
                if (curves[i] is SplineCurve curve)
                {
                    if (i - 1 > 0)
                        curve.prevPoint = curves[i - 1].controls[2];
                    if (i + 1 < curves.Count)
                        curve.nextPoint = curves[i + 1].controls[1];

                    curve.PopulatePoints();
                }
            }
        }
    }
}
