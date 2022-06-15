using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jab : Attack
{
    private const string NAME = "Jab";
    private const string DESCRIPTION = "Jab - A powerful attack.";
    private const int ACCURACY = 4;
    private const int DAMAGE_MOD = 5;
    private const int BUFFVALUE = 0;

    public Jab() : base(NAME, DESCRIPTION, ACCURACY, DAMAGE_MOD, BUFFVALUE)
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
