using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReconstructiveSlash : Attack
{
    private const string NAME = "Reconstructive Slash";
    private const string DESCRIPTION = "Reconstructive Slash - An attack that recovers slight proportions of health.";
    private const int ACCURACY = 5;
    private const int DAMAGE_MOD = 0;
    private const int BUFFVALUE = 5;

    public ReconstructiveSlash() : base(NAME, DESCRIPTION, ACCURACY, DAMAGE_MOD, BUFFVALUE)
    {
        this.name = NAME;
        this.description = DESCRIPTION;
        this.accuracy = ACCURACY;
        this.damageMod = DAMAGE_MOD;
        this.buffValue = BUFFVALUE;
    }

    public override void Effect()
    {
        BattleManager.resultantAccuracy = accuracy;

        if (!BattleManager.turnCounter)
        {
            BattleManager.damageAmount = Player.currentAttack + damageMod;
        }
        else
        {
            BattleManager.damageAmount = Opponent.currentAttack + damageMod;
        }
    }

    public override void SecondaryEffect()
    {
        if (!BattleManager.turnCounter)
        {
            Image[] afflicitonSlots = GameObject.Find("PlayerAfflictionList").transform.GetComponentsInChildren<Image>();

            foreach (Image image in afflicitonSlots)
            {
                if (image.sprite.name == "Rejuvenating" || image.sprite.name == "Injured")
                {
                    return;
                }
            }

            AudioManager.audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/Sounds/Afflictions/Rejuvenating"));
            BattleManager.battleManager.PlayerStatusDisplay(buffValue.ToString() + " Rejuvenate", Color.green);
            Player.currentHealth += buffValue;

            if (Player.currentHealth > Player.baseHealth)
            {
                Player.currentHealth = Player.baseHealth;
            }
        }
        else
        {
            Image[] afflicitonSlots = GameObject.Find("OpponentAfflictionList").transform.GetComponentsInChildren<Image>();

            foreach (Image image in afflicitonSlots)
            {
                if (image.sprite.name == "Rejuvenating" || image.sprite.name == "Injured")
                {
                    return;
                }
            }

            AudioManager.audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/Sounds/Afflictions/Rejuvenating"));
            BattleManager.battleManager.OpponentStatusDisplay(buffValue.ToString() + " Rejuvenate", Color.green);
            Opponent.currentHealth += buffValue;

            if (Opponent.currentHealth > Opponent.baseHealth)
            {
                Opponent.currentHealth = Opponent.baseHealth;
            }
        }
    }
}


