using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : Entity
{
    // Start is called before the first frame update
    void Start()
    {
        MyCollider.isTrigger = true;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (!collidedObject.tag.Equals("Player"))
        {
            Debug.Log("Not a player");
            return;
        }

        Player.AddScore(Value);
        Destroy(this.gameObject);
    }

    public string ItemName;
    public int Value;
}
