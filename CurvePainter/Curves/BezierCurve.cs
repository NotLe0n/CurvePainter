﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CurvePainter.Curves;

public sealed class BezierCurve : Curve
{
	public BezierCurve(Vector2 start, Vector2 end) : base(start, end)
	{
		points = new Vector2[NumSteps + 1];
		PopulatePoints();
	}

	public override void PopulatePoints()
	{
		for (int i = 0; i <= NumSteps; i++) {
			float t = i * Factor;

			// calculate points and add them to the list
			points[i] = CalculateBezierCurve(t, controls[0], controls[1], controls[2], controls[3]);
		}
	}

	public override void Draw(SpriteBatch spriteBatch)
	{
		base.Draw(spriteBatch);

		// draw gray lines from control points 0-1 and 2-3
		if (IsHovering || Selected) {
			spriteBatch.DrawLine(controls[0], controls[1], 1, Color.LightGray);
			spriteBatch.DrawLine(controls[2], controls[3], 1, Color.LightGray);
		}
	}

	private Vector2 CalculateBezierCurve(float t, Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3)
	{
		return MathF.Pow(1 - t, 3) * p0 +
		       3 * MathF.Pow(1 - t, 2) * t * p1 +
		       3 * (1 - t) * (t * t) * p2 +
		       (t * t * t) * p3;
	}
}