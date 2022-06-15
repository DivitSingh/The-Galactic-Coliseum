using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Attack {
    public string name;
    public string description;
    public int accuracy;
    public int damageMod;
    public int buffValue;

    public Attack(string name, string description, int accuracy, int damageValue, int buffValue)
    {
        this.name = name;
        this.description = description;
        this.accuracy = accuracy;
        this.damageMod = damageValue;
        this.buffValue = buffValue;
    }

    // Effect of attacks
    public virtual void Effect()
    {

    }

    // Secondary effect of some attacks
    public virtual void SecondaryEffect()
    {

    }
}
