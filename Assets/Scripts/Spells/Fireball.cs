using UnityEngine;

public class Fireball : Spell
{
    protected override void Awake()
    {
        base.Awake();

        SetName("Fireball");
        SetDescription("Shoots a fireball in the direction you're looking");
        SetCooldown(3.0f);

        Sprite sp = Resources.Load<Sprite>("fireball");
        if (sp)
        {
            SetIcon(sp);
        }

        // SetSpellSprite();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();

    }

    protected override void OnCast()
    {
        Debug.Log("Casted " + GetName());
    }
}
