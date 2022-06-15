using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Item
{
    public int id;
    public string name;
    public string type;
    public string description;
    public string statusText;
    public int accuracy;
    public int buffValue;
    public int turnsActive;
    public int turnsCompleted;
    public GameObject statusSlot;

    public Item(int id, string name, string description, string statusText, string type, int accuracy, int buffValue, int turnsActive)
    {
        this.id = id;
        this.name = name;
        this.type = type;
        this.description = description;
        this.statusText = statusText;
        this.accuracy = accuracy;
        this.buffValue = buffValue;
        this.turnsActive = turnsActive;
    }

    // Effect of most items
    public virtual void Effect()
    {

    }

    // Affliction effect of certain items
    public virtual void Afflict()
    {

    }
}

