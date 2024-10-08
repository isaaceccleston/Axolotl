using Godot;

public partial class MapBody : StaticBody2D
{
	Vector2 cornerA;
	Vector2 cornerB;
	Vector2 rectSize;
	bool drawing = false;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready(){}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{

		if(Input.IsActionJustPressed("Mouse1")){
			if(!drawing){
				cornerA = snapToInterval(GetViewport().GetMousePosition(), 32);
				drawing = true;
			}
			else{
				cornerB = snapToInterval(GetViewport().GetMousePosition(), 32);
				drawing = false;

				rectSize = cornerB - cornerA;

				CollisionShape2D newCollider = new CollisionShape2D();
				RectangleShape2D colliderRect = new RectangleShape2D();
				ColorRect newRect = new ColorRect();
				
				colliderRect.Size = cornerB - cornerA;	
				newCollider.Shape = colliderRect;
				Vector2 adjustedPosition = new Vector2(cornerA.X + rectSize.X / 2, cornerA.Y + rectSize.Y / 2);
				newCollider.Position = adjustedPosition;

				newRect.Size = colliderRect.Size;
				newRect.Color = Colors.White;
				newRect.Position = -rectSize / 2;
				
				AddChild(newCollider);
				newCollider.AddChild(newRect);
			}
		}
	}

	public static Vector2 snapToInterval(Vector2 coords, int interval){
		float x = coords.X - (coords.X % interval);
		float y = coords.Y - (coords.Y % interval);

		return new Vector2(x, y);
	}
}
