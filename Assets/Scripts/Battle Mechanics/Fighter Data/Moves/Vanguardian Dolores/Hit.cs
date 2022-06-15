using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hit : Attack
{
    private const string NAME = "Hit";
    private const string DESCRIPTION = "Hit - A quick, reliable physical attack.";
    private const int ACCURACY = 4;
    private const int DAMAGE_MOD = -5;
    private const int BUFFVALUE = 0;

    public Hit() : base(NAME, DESCRIPTION, ACCURACY, DAMAGE_MOD, BUFFVALUE)
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
