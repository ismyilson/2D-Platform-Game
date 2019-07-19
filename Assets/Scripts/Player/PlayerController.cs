using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Player Controller started");

        // Try to access Player
        player = GetComponentInParent<Player>();
        if (!player)
        {
            Debug.LogError("Could not access Player");
            return;
        }

        IsPlayerCurrentTalking = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                if (IsCurrentlyTalking())
                {
                    PlayerUI.RemoveCurrentChatMessage();
                }
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                // ShowMenu();

                if (IsCurrentlyTalking())
                {
                    return;
                }

                if (Game.IsPaused())
                {
                    Game.UnpauseGame();
                }
                else
                {
                    Game.PauseGame();
                }
                
                return; // Return because pausing disables everything else
            }
        }

        if (Input.anyKey)
        {
            if (Input.GetKey(KeyCode.A))
            {
                player.Move(false);
            }
            else if (Input.GetKey(KeyCode.D))
            {
                player.Move(true);
            }

            if (Input.GetKey(KeyCode.W))
            {
                player.Jump();
            }
        }
    }

    public static void SetCurrentlyTalking(bool talking)
    {
        IsPlayerCurrentTalking = talking;
    }

    public static bool IsCurrentlyTalking()
    {
        return IsPlayerCurrentTalking;
    }

    Player player;
    static bool IsPlayerCurrentTalking;
}
