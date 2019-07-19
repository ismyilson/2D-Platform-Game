using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : Entity
{
    // Start is called before the first frame update
    void Start()
    {
        Me = GetComponent<Rigidbody2D>();
        if (!Me)
        {
            Debug.LogError("FallingBlock has no Rigidbody2D, destroying");
            Destroy(this);
            return;
        }

        OriginalY = transform.position.y;

        SetFallingSpeed(5.0f);
        SetDamage(1);
    }

    void FixedUpdate()
    {   
        if (HasFallen)
        {
            RecoverTimer += Time.deltaTime;
            if (RecoverTimer >= RecoverTime)
            {
                RecoverTimer = 0.0f;
                HasFallen = false;
                GoUp();
            }
        }
        else
        {
            if (transform.position.y >= OriginalY)
            {
                if (!Falling)
                {
                    Fall();
                }
            }
            else if (Me.velocity.y > -0.01f)
            {
                HasFallen = true;
                Falling = false;
            }
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (!player)
        {
            return;
        }

        player.TakeDamage(Damage);
    }

    void Fall()
    {
        Me.gravityScale = GetFallingSpeed();
        Me.velocity = new Vector2(0.0f, 0.0f);

        Falling = true;
        HasFallen = false;
    }

    void GoUp()
    {
        Me.gravityScale = 0;
        Me.velocity = new Vector2(0.0f, 1.0f);
    }

    public void SetFallingSpeed(float value)
    {
        FallingSpeed = value;
    }

    public float GetFallingSpeed()
    {
        return FallingSpeed;
    }

    public void SetDamage(int value)
    {
        Damage = value;
    }

    public int GetDamage()
    {
        return Damage;
    }

    Rigidbody2D Me;
    public float FallingSpeed;
    public int Damage;

    float OriginalY;
    float RecoverTimer;
    const float RecoverTime = 1.0f;

    bool HasFallen;
    bool Falling;
}
