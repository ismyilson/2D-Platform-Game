using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitDoor : Entity
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

        Game.FinishLevel();
    }
}
