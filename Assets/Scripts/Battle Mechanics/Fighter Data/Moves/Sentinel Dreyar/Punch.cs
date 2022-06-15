using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Punch : Attack
{
    private const string NAME = "Punch";
    private const string DESCRIPTION = "Punch - A steady blow.";
    private const int ACCURACY = 5;
    private const int DAMAGE_MOD = 0;
    private const int BUFFVALUE = 0;

    public Punch() : base(NAME, DESCRIPTION, ACCURACY, DAMAGE_MOD, BUFFVALUE)
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
