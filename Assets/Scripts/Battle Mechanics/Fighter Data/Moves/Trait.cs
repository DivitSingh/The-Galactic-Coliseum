using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Trait
{
    public string name;
    public string description;
    public int buffValue;
    public GameObject statusSlot;

    public Trait(string name, string description, int buffValue)
    {
        this.name = name;
        this.description = description;
        this.buffValue = buffValue;
    }

    // Effect of most single-turn skills
    public virtual void Effect()
    {

    }

}
