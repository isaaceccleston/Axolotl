using System.IO.Pipes;
using Godot;

public partial class Player : CharacterBody2D
{
	[Export] float moveSpeed;
	[Export] float gravity;

	// Called when the node enters the scene tree for the first time.
	public override void _Ready()
	{
	}

	// Called every frame. 'delta' is the elapsed time since the previous frame.
	public override void _Process(double delta)
	{
		Velocity = new Vector2(moveSpeed*100, gravity*100);


		MoveAndSlide();
	}
}
