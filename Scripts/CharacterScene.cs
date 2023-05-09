using Godot;

public partial class CharacterScene : CharacterBody2D
{
	[Export]
	public int Speed { get; set; } = 500;
	public int DashSpeed = 1012300;
	public AnimatedSprite2D anim;
	public Timer cd;
	public bool Attacking;
	public bool Dashing; 
	
	public CollisionShape2D attack1;
	public CollisionShape2D attack2;
	public CollisionShape2D attack3;
	public CollisionShape2D attack4;
	public CollisionShape2D collider;
	
		public override void _Ready()
	{
		anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		cd = GetNode<Timer>("CD");
		
		attack1 = GetNode<CollisionShape2D>("attack1");
		attack2 = GetNode<CollisionShape2D>("attack2");
		attack3 = GetNode<CollisionShape2D>("attack3");
		attack4 = GetNode<CollisionShape2D>("attack4");
		
		collider = GetNode<CollisionShape2D>("Collider");
		
	}

	public void GetInput()
	{

		Vector2 inputDirection = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Velocity = inputDirection * Speed;
		
		
		// Movement Animations
		if (inputDirection.Y > 0 && Dashing == false && Attacking == false && !Input.IsActionJustPressed("LeftClick") && !Input.IsActionJustPressed("ui_accept"))
		{
			anim.Play("RunningForward");
		}
		else if (inputDirection.Y < 0 && Dashing == false && Attacking == false && !Input.IsActionJustPressed("LeftClick") && !Input.IsActionJustPressed("ui_accept"))
		{
			anim.Play("RunningBackward");
		}
		else if (inputDirection.X > 0 && inputDirection.Y == 0 && Dashing == false && Attacking == false && !Input.IsActionJustPressed("LeftClick") && !Input.IsActionJustPressed("ui_accept"))
		{
			anim.Play("WalkingSide");
			anim.FlipH = false;
		}
		else if (inputDirection.X < 0 && inputDirection.Y == 0 && Dashing == false && Attacking == false && !Input.IsActionJustPressed("LeftClick") && !Input.IsActionJustPressed("ui_accept"))
		{
			anim.Play("WalkingSide");
			anim.FlipH = true;
		}
		else if (inputDirection == new Vector2(0,0) && anim.Animation == "RunningForward")
		{
			anim.Play("IdleForward");
		}
		else if (inputDirection == new Vector2(0,0) && anim.Animation == "RunningBackward")
		{
			anim.Play("IdleBackward");
		}
		else if (inputDirection == new Vector2(0,0) && anim.Animation == "WalkingSide" && anim.FlipH == true)
		{
			anim.Play("IdleSide");
			anim.FlipH = true;
		}
		else if (inputDirection == new Vector2(0,0) && anim.Animation == "WalkingSide")
		{
			anim.Play("IdleSide");
		}
		else if (Input.IsActionPressed("ui_accept") && anim.Animation == "RunningForward")
		{
			Speed = 1500;
			anim.Play("DashForward");
			Dashing = true;
		}
		else if (Input.IsActionPressed("ui_accept") && anim.Animation == "WalkingSide")
		{
			Speed = 1500;
			anim.Play("DashSide");
			Dashing = true;
		}
		
		else if (Input.IsActionPressed("ui_accept") && anim.Animation == "RunningBackward")
		{
			Speed = 1500;
			anim.Play("DashBackward");
			Dashing = true;
		}
		
		// Combat Animations
		else if (Input.IsActionJustPressed("LeftClick") && cd.WaitTime == 0.1 && anim.Animation == "IdleForward" || anim.Animation == "RunningForward")
		{
			Speed = 0;
			
			Attacking = true;
			if (Global.combo % 2 == 0)
			{
				anim.Play("ForwardAttack1");
				Global.combo +=1;
				attack1.Disabled = false;
			}
			else if (Global.combo % 2 != 0)
			{
				anim.Play("ForwardAttack2");
				Global.combo +=1;
				attack1.Disabled = false;
			}
			
		}
		
		else if (Input.IsActionJustPressed("LeftClick") && cd.WaitTime == 0.1 && anim.Animation == "IdleBackward" || anim.Animation == "RunningBackward")
		{
			Speed = 0;
			
			Attacking = true;
			if (Global.combo % 2 == 0)
			{
				anim.Play("BackwardAttack1");
				Global.combo +=1;
				attack3.Disabled = false;
				
				
			}
			else if (Global.combo % 2 != 0)
			{
				anim.Play("BackwardAttack2");
				Global.combo +=1;
				attack3.Disabled = false;
			}
		}
		
		else if (Input.IsActionJustPressed("LeftClick") && cd.WaitTime == 0.1 && anim.Animation == "IdleSide" || anim.Animation == "WalkingSide")
		{
			Speed = 0;
			
			Attacking = true;
			if (Global.combo % 2 == 0)
			{
				anim.Play("SideAttack1");
				Global.combo +=1;
				attack2.Disabled = false;
				
				
			}
			else if (Global.combo % 2 != 0)
			{
				anim.Play("SideAttack2");
				Global.combo +=1;
				attack2.Disabled = false;
			}
		}
	}

	public override void _PhysicsProcess(double delta)
	{
		GetInput();
		MoveAndSlide();
	}
	
	public void _on_animated_sprite_2d_animation_finished()
	{
		if (anim.Animation == "ForwardAttack1" || anim.Animation == "ForwardAttack2")
		{

		anim.Play("IdleForward");
		Speed = 500;
		Attacking = false;
		GD.Print(Global.combo);
		attack1.Disabled = true;
		}
		else if (anim.Animation == "BackwardAttack1" || anim.Animation == "BackwardAttack2")
		{
			anim.Play("IdleBackward");
			Speed = 500;
			Attacking = false;
			GD.Print(Global.combo);
			attack3.Disabled = true;
		}
		else if (anim.Animation == "SideAttack1" || anim.Animation == "SideAttack2")
		{
			anim.Play("IdleSide");
			Speed = 500;
			Attacking = false;
			attack2.Disabled = true;
		}
		
		else if (anim.Animation == "DashForward")
		{
			anim.Play("RunningForward");
			Speed = 500;
			Dashing = false;
		}
		
		else if (anim.Animation == "DashSide")
		{
			anim.Play("WalkingSide");
			Speed = 500;
			Dashing = false;
		}
		
		else if (anim.Animation == "DashBackward")
		{
			anim.Play("RunningBackward");
			Speed = 500; 
			Dashing = false;
		}
	}
}



