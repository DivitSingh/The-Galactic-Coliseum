using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealingOil : Item {
    private const int ID = 0;
    private const string NAME = "Healing Oil";
    private const string DESCRIPTION = "An ingenious compound formed by Heraklios, aids in recovery of health. \n\nThe item recovers 20 health.";
    private const string STATUS_TEXT = "20 Rejuvenate";
    private const string TYPE = "Single-Turn";
    private const int ACCURACY = 6;
    private const int BUFF_VALUE = 20;
    private const int TURNS_ACTIVE = 0;

    public HealingOil() : base(ID, NAME, DESCRIPTION, STATUS_TEXT, TYPE,  ACCURACY, BUFF_VALUE, TURNS_ACTIVE)
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
