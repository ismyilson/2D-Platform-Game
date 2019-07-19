using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    // Called before Start
    void Awake()
    {
        MyCollider = GetComponent<Collider2D>();
        if (!MyCollider)
        {
            Debug.LogError("Destroying Entity '" + name + "', all entities must have a Collider2D");
            Destroy(this.gameObject);
            return;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Game.IsPaused())
        {
            return;
        }
    }

    protected void SendChatMessage(string message)
    {
        if (PlayerController.IsCurrentlyTalking())
        {
            Debug.Log("Player is currently talking");
            return;
        }

        PlayerUI.SendChatMessage(this, message, Avatar);
    }

    public virtual void OnMessageRead()
    {

    }

    public Sprite Avatar;

    protected Collider2D MyCollider;
}
