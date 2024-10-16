using System;
using System.Drawing;
using Godot;

public partial class MapBody : StaticBody2D
{
	Vector2 positionOne = new Vector2();
	Vector2 positionTwo = new Vector2();
	bool drawing = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		CollisionShape2D collider = new CollisionShape2D();
		RectangleShape2D collisionRect = new RectangleShape2D();
		ColorRect visibleRect = new ColorRect();
		Vector2 rectSize;

		if(Input.IsActionJustPressed("Mouse1")){
			if(!drawing){
				positionOne = snapToInterval(GetViewport().GetMousePosition(), 32);
				drawing = true;
			}
			else{
				positionTwo = snapToInterval(GetViewport().GetMousePosition(), 32);
				drawing = false;

				rectSize = new Vector2(Math.Abs(positionTwo.X - positionOne.X), Math.Abs(positionTwo.Y - positionOne.Y));
				
				Vector2 topLeft = new Vector2(Math.Min(positionOne.X, positionTwo.X), Math.Min(positionOne.Y, positionTwo.Y));

				collisionRect.Size = rectSize;
				collider.Shape = collisionRect;
				collider.Position = new Vector2(topLeft.X + rectSize.X / 2, topLeft.Y + rectSize.Y / 2);

				visibleRect.Size = rectSize;
				visibleRect.Color = Colors.White;
				visibleRect.Position = -rectSize / 2;

				AddChild(collider);
				collider.AddChild(visibleRect);
			}
		}
	}

	public static Vector2 snapToInterval(Vector2 coords, int interval){
		float x = coords.X - (coords.X % interval);
		float y = coords.Y - (coords.Y % interval);

		return new Vector2(x, y);
	}
}
