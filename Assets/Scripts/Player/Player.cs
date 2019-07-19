using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player spawned");

        // Try to access PlayerUI
        MyUI = GetComponentInChildren<PlayerUI>();
        if (!MyUI)
        {
            Debug.LogError("Could not access PlayerUI");
            return;
        }

        // Try to access Rigidbody2D component
        Me = GetComponent<Rigidbody2D>();
        if (!Me)
        {
            Debug.LogError("Could not access Player Rigidbody2D");
            return;
        }

        SetMaxHealth(3);
        SetHealth(3);
        SetPlayerName("Albert");
        SetMovementSpeed(0.08f);
        SetJumpSpeed(8.0f);
    }

    void FixedUpdate()
    {
        CheckFalling();
    }

    public void Move(bool right)
    {
        if (right)
        {
            Me.transform.position = new Vector2(Me.transform.position.x + GetMovementSpeed(), Me.transform.position.y);
        }
        else
        {
            Me.transform.position = new Vector2(Me.transform.position.x - GetMovementSpeed(), Me.transform.position.y);
        }
    }

    public void Jump()
    {
        if (IsJumping || IsFalling)
        {
            return;
        }

        Me.velocity = new Vector2(Me.velocity.x, GetJumpSpeed());
    }

    public void TakeDamage(int Damage)
    {
        SetHealth(GetHealth() - Damage);
    }

    public void SetHealth(int value)
    {
        if (value > MaxHealth)
        {
            value = MaxHealth;
        }
        else if (value < 0)
        {
            value = 0;
        }

        Health = value;
        
        MyUI.UpdateHealth();

        if (Health <= 0)
        {
            Game.GameOver();
        }
    }

    public int GetHealth()
    {
        return Health;
    }

    public void SetMaxHealth(int value)
    {
        if (value < 1)
        {
            value = 1;
        }

        MaxHealth = value;

        MyUI.UpdateMaxHealth();
    }

    public int GetMaxHealth()
    {
        return MaxHealth;
    }

    public void SetPlayerName(string name)
    {
        PlayerName = name;
    }
    
    public string GetPlayerName()
    {
        return PlayerName;
    }

    public void SetMovementSpeed(float value)
    {
        MovementSpeed = value;
    }

    public float GetMovementSpeed()
    {
        return MovementSpeed;
    }

    public void SetJumpSpeed(float value)
    {
        JumpSpeed = value;
    }

    public float GetJumpSpeed()
    {
        return JumpSpeed;
    }

    public static void SetScore(int value)
    {
        Score = value;

        MyUI.UpdateScore(Score);
    }

    public static void AddScore(int value)
    {
        Score += value;

        MyUI.UpdateScore(Score);
    }

    public static int GetScore()
    {
        return Score;
    }

    public static void SetInvencible(bool enabled)
    {
        Invencible = enabled;
    }

    public static bool IsImmune()
    {
        return Invencible;
    }

    public static void SetCanMove(bool enabled)
    {
        AbleToMove = enabled;
    }

    public static bool CanMove()
    {
        return AbleToMove;
    }

    void CheckFalling()
    {
        if (Me.velocity.y > 0.01f)
        {
            IsJumping = true;
            IsFalling = false;
        }
        else if (Me.velocity.y < -0.01f)
        {
            IsJumping = false;
            IsFalling = true;
        }
        else
        {
            IsJumping = false;
            IsFalling = false;
        }
    }
    
    Rigidbody2D Me;
    static PlayerUI MyUI;

    public int Health;
    public int MaxHealth;

    public string PlayerName;
    public float MovementSpeed;
    public float JumpSpeed;

    static int Score;

    static bool Invencible;
    static bool AbleToMove;

    public bool IsJumping;
    public bool IsFalling;
}
