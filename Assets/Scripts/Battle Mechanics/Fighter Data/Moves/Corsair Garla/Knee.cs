using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knee : Attack
{
    private const string NAME = "Knee";
    private const string DESCRIPTION = "Knee - An unreliable blow with great potency.";
    private const int ACCURACY = 4;
    private const int DAMAGE_MOD = 10;
    private const int BUFFVALUE = 0;

    public Knee() : base(NAME, DESCRIPTION, ACCURACY, DAMAGE_MOD, BUFFVALUE)
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
}
