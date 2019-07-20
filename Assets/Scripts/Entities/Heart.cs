using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Entity
{
    // Start is called before the first frame update
    void Start()
    {
        MyCollider.isTrigger = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (!player)
        {
            return;
        }

        // If player is full health, add 1 max health
        if (player.GetHealth() == player.GetMaxHealth())
        {
            player.SetMaxHealth(player.GetMaxHealth() + 1);
            player.SetHealth(player.GetMaxHealth());
        }
        else
        {
            player.SetHealth(player.GetHealth() + 1);
        }

        Destroy(this.gameObject);
    }
}
