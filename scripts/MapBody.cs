using Godot;
using System.Collections.Generic;

public partial class MapBody : StaticBody2D
{
	Vector2 mousePos;
	Vector2 cornerA;
	Vector2 cornerB;
	bool drawing = false;
	List<CollisionShape2D> kids = new List<CollisionShape2D>();

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
		
		// for (int i = 0; i < GetChildCount(); i++)
		// {
		// 	if(GetChild(i) is CollisionShape2D){
		// 		kids.Add((CollisionShape2D)GetChild(i));
		// 	}
		// }
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		mousePos = GetViewport().GetMousePosition();
		Vector2 snappedMousePos = new Vector2(mousePos.X - mousePos.X % 64, mousePos.Y - mousePos.Y % 64);

		if(Input.IsActionJustPressed("Mouse1")){
			if(!drawing){
				cornerA = snappedMousePos;
				drawing = true;
			}
			else{
				cornerB = snappedMousePos;
				drawing = false;

				CollisionShape2D newColl = new CollisionShape2D();
				RectangleShape2D rect = new RectangleShape2D();
				rect.Size = cornerB - cornerA;
				newColl.Shape = rect;
				newColl.Position = cornerA;
				AddChild(newColl);

				QueueRedraw();
			}
		}
	}

    public override void _Draw()
    {
		//works but maybe not good that it does? doesnt do what i want really..
		Color white = Colors.White;
		Rect2 rect = new Rect2(cornerA, cornerB-cornerA);
		DrawRect(rect, white);
    }
}
