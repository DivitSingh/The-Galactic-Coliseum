using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clout : Attack
{
    private const string NAME = "Clout";
    private const string DESCRIPTION = "Clout - A well-aimed blow from the fist.";
    private const int ACCURACY = 4;
    private const int DAMAGE_MOD = 0;
    private const int BUFFVALUE = 0;

    public Clout() : base(NAME, DESCRIPTION, ACCURACY, DAMAGE_MOD, BUFFVALUE)
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

