using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Neutral : Entity
{
    // Start is called before the first frame update
    void Start()
    {
        MyCollider.isTrigger = true;

        CurrentQuote = 0;
    }

    public override void OnMessageRead()
    {
        if (!IsClose)
        {
            return;
        }

        CurrentQuote++;
        if (CurrentQuote < Quotes.Length)
        {
            SendChatMessage(Quotes[CurrentQuote]);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (!collidedObject.tag.Equals("Player"))
        {
            Debug.Log("Not a player");
            return;
        }

        if (Quotes.Length <= 0)
        {
            return;
        }

        IsClose = true;
        SendChatMessage(Quotes[CurrentQuote]);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        GameObject collidedObject = collision.gameObject;
        if (!collidedObject.tag.Equals("Player"))
        {
            Debug.Log("Not a player");
            return;
        }

        CurrentQuote = 0;
        IsClose = false;
        PlayerUI.RemoveCurrentChatMessage();
    }

    public string[] Quotes;
    int CurrentQuote;
    bool IsClose;
}
