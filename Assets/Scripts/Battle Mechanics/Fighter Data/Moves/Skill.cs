using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Skill{
    public string name;
    public string type;
    public string description;
    public int accuracy;
    public int damageMod;
    public int buffValue;
    public int turnsActive;
    public int turnsCompleted;
    public GameObject statusSlot;

    public Skill(string name, string type, string description, int accuracy, int damageMod, int buffValue, int turnsActive)
    {
        this.name = name;
        this.type = type;
        this.description = description;
        this.accuracy = accuracy;
        this.damageMod = damageMod;
        this.buffValue = buffValue;
        this.turnsActive = turnsActive;
    }

    // Effect of most single-turn skills
    public virtual void Effect()
    {
  
    }

    // Affliction effect of certain skills
    public virtual void Afflict()
    {

    }

    // Secondary effect of certain skills
    public virtual void SecondaryEffect()
    {

    }

    // Tertiary effect of certain skills
    public virtual void TertiaryEffect()
    {

    }

}
