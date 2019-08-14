using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AvailableSpells
{
    SPELL_FIREBALL,

}

public abstract class Spell : MonoBehaviour
{
    // Awake is called on creation
    protected virtual void Awake()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (IsCooldown())
        {
            RemainingCooldown -= Time.deltaTime;
        }
    }

    protected void Cast()
    {
        if (IsCooldown())
        {
            // Cannot cast the spell because in cooldown
            return;
        }

        RemainingCooldown = Cooldown;
    }

    protected abstract void OnCast();

    protected void SetName(string name)
    {
        Name = name;
    }

    public string GetName()
    {
        return Name;
    }

    protected void SetDescription(string description)
    {
        Description = description;
    }

    public string GetDescription()
    {
        return Description;
    }

    protected void SetIcon(Sprite icon)
    {
        Icon = icon;
    }

    public Sprite GetIcon()
    {
        return Icon;
    }

    protected void SetSpellSprite(Sprite sprite)
    {
        SpellSprite = sprite;
    }

    public Sprite GetSpellSprite()
    {
        return SpellSprite;
    }

    protected void SetCooldown(float value)
    {
        if (value < 0.0f)
        {
            value = 0.0f;
        }

        Cooldown = value;
    }

    public float GetCooldown()
    {
        return Cooldown;
    }

    protected bool IsCooldown()
    {
        return RemainingCooldown <= 0.0f;
    }

    protected void SetRemainingCooldown(float value)
    {
        if (value < 0.0f)
        {
            value = 0.0f;
        }

        RemainingCooldown = value;
    }

    public float GetRemainingCooldown()
    {
        return RemainingCooldown;
    }

    private string Name;
    private string Description;
    private Sprite Icon;
    private Sprite SpellSprite;

    private float Cooldown;
    private float RemainingCooldown;
}
