using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Valiant : Trait {
    private const string NAME = "Valiant";
    private const string DESCRIPTION = "When she is first injured severely, Dolores is able to regain proportions of health that have been lost.";
    private const int BUFF_VALUE = 20;

    private bool effectComplete;

    public Valiant() : base(NAME, DESCRIPTION, BUFF_VALUE)
    {
        this.name = NAME;
        this.description = DESCRIPTION;
        this.buffValue = BUFF_VALUE;
    }

    public override void Effect()
    {
        if (!BattleManager.turnCounter && Player.currentHealth <= 50 && !effectComplete)
        {
            AudioManager.audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/Sounds/Fighters/Vanguardian Dolores/Trait"));
            BattleManager.battleManager.PlayerTraitDisplay(buffValue.ToString() + "\nRejuvenate");
            Player.currentHealth += buffValue;
            effectComplete = true;
        }
        else if (BattleManager.turnCounter && Opponent.currentHealth <= 50 && !effectComplete)
        {
            AudioManager.audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/Sounds/Fighters/Vanguardian Dolores/Trait"));
            BattleManager.battleManager.OpponentTraitDisplay(buffValue.ToString() + "\nRejuvenate");
            Opponent.currentHealth += buffValue;
            effectComplete = true;
        }
    }
}
