using Godot;

public partial class CharacterScene : CharacterBody2D
{
	[Export]
	public int Speed { get; set; } = 250;
	public AnimatedSprite2D anim;
	public Timer cd;
	public bool Attacking;
	
		public override void _Ready()
	{
		anim = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
		cd = GetNode<Timer>("CD");
	}

	public void GetInput()
	{

		Vector2 inputDirection = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		Velocity = inputDirection * Speed;
		
		if (inputDirection.Y > 0 && Attacking == false)
		{
			anim.Play("RunningForward");
		}
		else if (inputDirection.Y < 0 && Attacking == false)
		{
			anim.Play("RunningBackward");
		}
		else if (inputDirection.X > 0 && inputDirection.Y == 0 && Attacking == false)
		{
			anim.Play("WalkingSide");
			anim.FlipH = false;
		}
		else if (inputDirection.X < 0 && inputDirection.Y == 0 && Attacking == false)
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
		else if (Input.IsActionJustPressed("LeftClick") && anim.Animation == "IdleForward" && cd.WaitTime == 0.1)
		{
			Speed = 0;
			
			Attacking = true;
			if (Global.combo % 2 == 0)
			{
				anim.Play("ForwardAttack1");
				Global.combo +=1;
				
				
			}
			else if (Global.combo % 2 != 0)
			{
				anim.Play("ForwardAttack2");
				Global.combo +=1;
			}
			
		}
		else if (Input.IsActionJustPressed("LeftClick") && anim.Animation == "IdleBackward" && cd.WaitTime == 0.1)
		{
			Speed = 0;
			
			Attacking = true;
			if (Global.combo % 2 == 0)
			{
				anim.Play("BackwardAttack1");
				Global.combo +=1;
				
				
			}
			else if (Global.combo % 2 != 0)
			{
				anim.Play("BackwardAttack2");
				Global.combo +=1;
			}
		}
		
		else if (Input.IsActionJustPressed("LeftClick") && anim.Animation == "IdleBackward" && cd.WaitTime == 0.1)
		{
			Speed = 0;
			
			Attacking = true;
			if (Global.combo % 2 == 0)
			{
				anim.Play("BackwardAttack1");
				Global.combo +=1;
				
				
			}
			else if (Global.combo % 2 != 0)
			{
				anim.Play("BackwardAttack2");
				Global.combo +=1;
			}
		}
		
		else if (Input.IsActionJustPressed("LeftClick") && anim.Animation == "IdleSide" && cd.WaitTime == 0.1)
		{
			Speed = 0;
			
			Attacking = true;
			if (Global.combo % 2 == 0)
			{
				anim.Play("SideAttack1");
				Global.combo +=1;
				
				
			}
			else if (Global.combo % 2 != 0)
			{
				anim.Play("SideAttack2");
				Global.combo +=1;
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
		Speed = 250;
		Attacking = false;
		GD.Print(Global.combo);
		}
		else if (anim.Animation == "BackwardAttack1" || anim.Animation == "BackwardAttack2")
		{
			anim.Play("IdleBackward");
			Speed = 250;
			Attacking = false;
			GD.Print(Global.combo);
		}
		else if (anim.Animation == "SideAttack1" || anim.Animation == "SideAttack2")
		{
			anim.Play("IdleSide");
			Speed = 250;
			Attacking = false;
			GD.Print(Global.combo);
		}
	}
}



