using Godot;
using System;
using System.Runtime.CompilerServices;
using System.Threading;

public partial class MonsterSlime : CharacterBody2D
{
	public const float Speed = 300.0f;
	[Export]
	public Marker2D endPoint;
	public Vector2 startPosition, endPosition;
	public int speed = 20;
	
	public float limit = 0.5f;
	
    public override void _Ready()
    {
        base._Ready();
		startPosition = Position;
		endPosition = endPoint.GlobalPosition;
    }
	public void changeDirection()
	{
		Vector2 tempEnd = endPosition;
		endPosition = startPosition;
		startPosition = tempEnd;
	}
	public void updateVelocity()
	{
			Vector2 moveDirection = endPosition - Position;
			if (moveDirection.Length() < limit)
			{
				changeDirection();
			}
			Velocity = moveDirection.Normalized()*speed;
	}
	public void updateAnimation()
	{
		String animationString = "goUp";
		if (Velocity.Y>0)
			animationString = "goDown";
		AnimatedSprite2D a = this.FindChild("AnimatedSprite2D") as AnimatedSprite2D;
		a.Play(animationString);

	}
	public override void _PhysicsProcess(double delta)
	{
		
		updateVelocity();
		MoveAndSlide();
		updateAnimation();
		
	}
}
