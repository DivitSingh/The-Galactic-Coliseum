using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalvingDroplets : Item
{
    private const int ID = 4;
    private const string NAME = "Salving Droplets";
    private const string DESCRIPTION = "Waters from the crater-springs of the planet of the Galactic Embassy. \n\nThe item may recover 30 health.";
    private const string STATUS_TEXT = "30 Rejuvenate";
    private const string TYPE = "Single-Turn";
    private const int ACCURACY = 3;
    private const int BUFF_VALUE = 30;
    private const int TURNS_ACTIVE = 0;

    public SalvingDroplets() : base(ID, NAME, DESCRIPTION, STATUS_TEXT, TYPE,  ACCURACY, BUFF_VALUE, TURNS_ACTIVE)
    {
        this.id = ID;
        this.name = NAME;
        this.description = DESCRIPTION;
        this.statusText = STATUS_TEXT;
        this.type = TYPE;
        this.accuracy = ACCURACY;
        this.buffValue = BUFF_VALUE;
        this.turnsActive = TURNS_ACTIVE;
    }

    public override void Effect()
    {
        AudioManager.audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/Sounds/Afflictions/Rejuvenating"));

        if (!BattleManager.turnCounter)
        {
            Player.currentHealth += buffValue;

            if (Player.currentHealth > Player.baseHealth)
            {
                Player.currentHealth = Player.baseHealth;
            }
        }
        else
        {
            Opponent.currentHealth += buffValue;

            if (Opponent.currentHealth > Opponent.baseHealth)
            {
                Opponent.currentHealth = Opponent.baseHealth;
            }
        }
    }
}

