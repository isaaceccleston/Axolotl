using Godot;
using System.Collections.Generic;
using System.Diagnostics;

public partial class MapBody : StaticBody2D
{
	Vector2 mousePos;
	Vector2 cornerA;
	Vector2 cornerB;
	bool drawing = false;
	bool rectExists = false;
	List<CollisionShape2D> kids = new List<CollisionShape2D>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		for (int i = 0; i < GetChildCount(); i++)
		{
			if(GetChild(i) is CollisionShape2D){
				kids.Add((CollisionShape2D)GetChild(i));
			}
		}
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		mousePos = GetViewport().GetMousePosition();

		if(Input.IsActionJustPressed("Mouse1")){
			if(!drawing){
				drawing = true;
				Debug.Print("draw mode entered");
				
				cornerA.X = mousePos.X - mousePos.X % 64;
				cornerA.Y = mousePos.Y - mousePos.Y % 64;
			}
			else{
				cornerB.X = mousePos.X - mousePos.X % 64;
				cornerB.Y = mousePos.Y - mousePos.Y % 64;
				drawing = false;
				Debug.Print("draw mode exited");

				RectangleShape2D drawnRect = new RectangleShape2D();
				drawnRect.Size = new Vector2(cornerB.X - cornerA.X, cornerB.Y - cornerA.Y);
				
				CollisionShape2D collider = new CollisionShape2D();
				collider.Shape = drawnRect;
				
				ColorRect rect = new ColorRect();
				rect.Color = Colors.White;
				rect._Draw();
				collider.AddChild(rect);

				AddChild(collider);
				
				Debug.Print($"")
			}
		}
	}
}
