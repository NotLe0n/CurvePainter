using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace CurvePainter;

public class Line
{
	public Vector2 startPoint;
	public Vector2 endPoint;
	public readonly Color color;
	public float Angle => MathF.Atan2(endPoint.Y - startPoint.Y, endPoint.X - startPoint.X);

	private int clickedControl = -1;

	public Line(Vector2 start, Vector2 end, Color color)
	{
		startPoint = start;
		endPoint = end;
		this.color = color;
	}

	public void Update()
	{
		var startPointRect = new Rectangle(startPoint.ToPoint(), new Point(10, 10));
		var endPointRect = new Rectangle(endPoint.ToPoint(), new Point(10, 10));

		if (Input.MouseLeftDown) {
			if (startPointRect.Contains(Input.MousePosition)) {
				// we don't move the control point here because you can lose it if you move your mouse too fast
				clickedControl = 0;
			}

			if (endPointRect.Contains(Input.MousePosition)) {
				// we don't move the control point here because you can lose it if you move your mouse too fast
				clickedControl = 1;
			}

			// move flagged control point
			if (clickedControl == 0) {
				startPoint = Input.MousePosition;
			}

			if (clickedControl == 1) {
				endPoint = Input.MousePosition;
			}
		}
		else {
			// if the mouse is released no control point should be moved
			clickedControl = -1;
		}
	}

	public void Draw(SpriteBatch spriteBatch)
	{
		// draw line
		spriteBatch.DrawLine(startPoint, endPoint, 2, color);

		// draw control points
		spriteBatch.Draw(CurvePainter.solid, new Rectangle((int)startPoint.X - 5, (int)startPoint.Y - 5, 10, 10),
			Color.Red);
		spriteBatch.Draw(CurvePainter.solid, new Rectangle((int)endPoint.X - 5, (int)endPoint.Y - 5, 10, 10),
			Color.Red);
	}
}