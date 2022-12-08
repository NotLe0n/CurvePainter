using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CurvePainter.Curves;

public abstract class Curve
{
	public readonly Vector2[] controls;
	protected Vector2[]? points;
	protected int clickedControl = -1;

	private bool _isHovering;
	public bool IsHovering => _isHovering;

	private bool _selected;
	public bool Selected => _selected;

	protected const int NumSteps = (int)(1.0f / Factor);
	protected const float Factor = 0.02f;

	protected Curve(Vector2 start, Vector2 end)
	{
		// add control points at start, 30%, 70% and end
		controls = new[]
		{
			start,
			0.7f * start + 0.3f * end,
			0.3f * start + 0.7f * end,
			end
		};
	}

	public void Update(GameTime gameTime)
	{
		// if the curve is clicked select it
		if (Input.MouseLeftDown) {
			_selected = IsHovering;
		}

		for (int i = 0; i < controls.Length; i++) {
			var rect = new Rectangle((int)controls[i].X - 5, (int)controls[i].Y - 5, 10, 10);

			// if the mouse is hovering over a control point...
			if (rect.Contains(Input.MousePosition)) {
				// ... and it is clicked ...
				if (Input.MouseLeftDown) {
					// ... flag the control point
					// we don't move the control point here because you can lose it if you move your mouse too fast
					clickedControl = i;
				}
			}

			// move flagged control point
			if (clickedControl == i) {
				controls[i] = Input.MousePosition;
				_selected = true;

				PopulatePoints();
			}
		}

		// if the mouse is released no control point should be moved
		if (!Input.MouseLeftDown) {
			clickedControl = -1;
		}

		var min = Vector2.Min(Vector2.Min(controls[0], controls[1]), Vector2.Min(controls[2], controls[3]));
		var max = Vector2.Max(Vector2.Max(controls[0], controls[1]), Vector2.Max(controls[2], controls[3]));
		bool inside = Input.MousePosition.X >= min.X &&
		              Input.MousePosition.X <= min.X + max.X &&
		              Input.MousePosition.Y >= min.Y &&
		              Input.MousePosition.Y <= min.Y + max.Y;

		if (!inside) {
			_isHovering = false;
			return;
		}

		// detect if mouse is hovering over curve
		_isHovering = false;
		Vector2 closestPoint = default;
		float bestDistance = float.MaxValue;
		foreach (var point in points) {
			// find closest point on the curve to the mouse
			float currentDistance = Vector2.DistanceSquared(point, Input.MousePosition);
			if (currentDistance < bestDistance) {
				bestDistance = currentDistance;
				closestPoint = point;
			}
		}

		float distance = Vector2.Distance(closestPoint, Input.MousePosition);
		const float detectionRange = 10;
		if (distance > -detectionRange && distance < detectionRange) {
			_isHovering = true;
		}
	}

	public virtual void Draw(SpriteBatch spriteBatch)
	{
		// draw control points
		if (IsHovering || Selected) {
			foreach (Vector2 control in controls) {
				spriteBatch.Draw(CurvePainter.solid, new Rectangle((int)control.X - 5, (int)control.Y - 5, 10, 10),
					Color.Red);
			}
		}

		// debug draw
		//foreach (var pt in points)
		//{
		//    spriteBatch.Draw(Game1.solid, new Rectangle((int)pt.X, (int)pt.Y, 10, 10), Color.Green * .3f);
		//}

		// draw points
		for (int i = 0; i + 1 < points.Length; i++) {
			Vector2 pt = points[i];
			Vector2 npt = points[i + 1];

			// draw line
			spriteBatch.DrawLine(pt, npt, 2,
				IsHovering || Selected
					? Color.Orange
					: Color.White); // if the curve is hovered or selected draw the points in orange, otherwise in white
		}
	}

	public abstract void PopulatePoints();
}