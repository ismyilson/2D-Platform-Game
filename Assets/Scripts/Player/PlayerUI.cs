using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("PlayerUI started");

        // Try to access Player
        player = GetComponentInParent<Player>();
        if (!player)
        {
            Debug.LogError("Could not access Player");
            return;
        }

        // Setup the Game Timer
        GameObject UITimer = GameObject.Find("UI_Timer");
        if (!UITimer)
        {
            Debug.LogError("Could not access UI_Timer");
        }
        TimerText = UITimer.GetComponent<Text>();

        // Setup score counter
        GameObject UIScore = GameObject.Find("UI_Score");
        if (!UIScore)
        {
            Debug.LogError("Could not access UI_Score");
        }
        ScoreText = UIScore.GetComponent<Text>();

        // Setup player lives
        LivesContainer = GameObject.Find("UI_Lives");
        if (!LivesContainer)
        {
            Debug.LogError("Could not access UI_Lives");
        }
        LivesCount = 0;

        // Setup chat message avatar
        GameObject UIChatAvatar = GameObject.Find("UI_ChatAvatar");
        if (!UIChatAvatar)
        {
            Debug.LogError("Could not access UI_ChatAvatar");
        }
        ChatMessageAvatar = UIChatAvatar.GetComponent<Image>();

        // Setup chat message text
        GameObject UIChatText = GameObject.Find("UI_ChatText");
        if (!UIChatText)
        {
            Debug.LogError("Could not access UI_ChatText");
        }
        ChatMessageText = UIChatText.GetComponent<Text>();

        // Setup chat message panel
        ChatMessagePanel = GameObject.Find("UI_Chat");
        if (!ChatMessagePanel)
        {
            Debug.LogError("Could not access UI_Chat");
        }
        ChatMessagePanel.SetActive(false);

        // Setup spells
        GameObject UISpellPanel = GameObject.Find("UI_Spells");
        if (!UISpellPanel)
        {
            Debug.LogError("Could not access UI_Spells");
        }

        int MaxSpells = player.GetMaxSpells();
        Spells = new Image[MaxSpells];
        for (int i = 0; i < MaxSpells; ++i)
        {
            Image img = UISpellPanel.transform.GetChild(i).GetComponent<Image>();
            if (img)
            {
                Spells[i] = img;
            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void UpdateTimer()
    {
        string CurrentTime = TimerText.text;

        int seconds;
        if (!int.TryParse(CurrentTime.Substring(3), out seconds))
        {
            Debug.Log("Could not parse seconds");
            return;
        }

        seconds++;

        if (seconds >= 60)
        {
            int minutes;
            if (!int.TryParse(CurrentTime.Substring(0, 2), out minutes))
            {
                Debug.Log("Could not parse minutes");
                return;
            }

            minutes++;

            string minutesStr = minutes.ToString();
            if (minutesStr.Length <= 1)
            {
                minutesStr = minutesStr.Insert(0, "0");
            }

            TimerText.text = TimerText.text.Remove(0, 2).Insert(0, minutesStr);

            seconds = 0;
        }

        string secondsStr = seconds.ToString();
        if (secondsStr.Length <= 1)
        {
            secondsStr = secondsStr.Insert(0, "0");
        }

        TimerText.text = TimerText.text.Remove(3, 2).Insert(3, secondsStr);
    }

    public void UpdateHealth()
    {
        Image[] lives = LivesContainer.GetComponentsInChildren<Image>();
        int curPlayerHealth = player.GetHealth();

        int UIFullHealthCount = 0;
        for (int i = 0; i < lives.Length; ++i)
        {
            if (lives[i].sprite.Equals(FullHealthSprite))
            {
                UIFullHealthCount++;
            }
        }

        // If player health is more than we have set, we need to add
        if (curPlayerHealth > UIFullHealthCount)
        {
            for (int i = UIFullHealthCount; i < curPlayerHealth; ++i)
            {
                lives[i].sprite = FullHealthSprite;
            }
        }
        else // Else, we need to remove
        {
            for (int i = UIFullHealthCount; i > curPlayerHealth; --i)
            {
                lives[i - 1].sprite = EmptyHealthSprite;
            }
        }
    }

    public void UpdateMaxHealth()
    {
        Image[] lives = LivesContainer.GetComponentsInChildren<Image>();
        int maxPlayerHealth = player.GetMaxHealth();

        // If we have more children than max health, then we need to remove
        if (LivesCount > maxPlayerHealth)
        {
            int diff = LivesCount - maxPlayerHealth;
            for (int i = 0; i < LivesCount - maxPlayerHealth; ++i)
            {
                Destroy(lives[LivesCount - 1].gameObject);
                LivesCount--;
            }
        }
        else // Else, we need to add health
        {
            int diff = maxPlayerHealth - LivesCount;
            for (int i = 0; i < diff; ++i)
            {
                AddHealth(true);
                LivesCount++;
            }
        }
    }

    public void AddHealth(bool empty)
    {
        GameObject child = new GameObject("Health" + (LivesCount + 1));
        child.transform.parent = LivesContainer.transform;

        child.transform.position = new Vector2(50.0f + (80.0f * LivesCount), Screen.height - 50.0f);

        Image img = child.AddComponent<Image>() as Image;
        img.sprite = EmptyHealthSprite;
    }

    public void UpdateScore(int value)
    {
        ScoreText.text = value.ToString();
    }

    public static void SendChatMessage(Entity sender, string message, Sprite avatar)
    {
        Game.PauseGame();

        // Set current talking Entity
        CurrentTalkingEntity = sender;

        // Enable the chat message
        ChatMessagePanel.SetActive(true);

        // Set the text
        ChatMessageText.text = message;

        // Set the avatar
        ChatMessageAvatar.sprite = avatar;

        // Player is currently talking
        PlayerController.SetCurrentlyTalking(true);
    }

    public static void RemoveCurrentChatMessage()
    {
        if (!PlayerController.IsCurrentlyTalking())
        {
            return;
        }

        // Disable the chat panel
        ChatMessagePanel.SetActive(false);

        // Player is no longer talking
        PlayerController.SetCurrentlyTalking(false);

        // Unpause the game
        Game.UnpauseGame();

        if (CurrentTalkingEntity)
        {
            CurrentTalkingEntity.OnMessageRead();
            CurrentTalkingEntity = null;
        }
    }

    public void SetSpellIcon(int id, Sprite icon)
    {
        if (id >= player.GetMaxSpells())
        {
            id = 0;
        }
        
        Spells[id].sprite = icon;
    }

    Player player;

    public Sprite EmptyHealthSprite;
    public Sprite FullHealthSprite;

    static GameObject ChatMessagePanel;
    static Image ChatMessageAvatar;
    static Text ChatMessageText;
    static Entity CurrentTalkingEntity;

    static Text TimerText;
    static Text ScoreText;
    
    static Image[] Spells;

    GameObject LivesContainer;
    int LivesCount;
}