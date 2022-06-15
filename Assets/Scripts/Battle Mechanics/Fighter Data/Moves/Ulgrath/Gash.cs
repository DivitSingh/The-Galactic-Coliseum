using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gash : Attack
{
    private const string NAME = "Gash";
    private const string DESCRIPTION = "Gash - A powerful attack.";
    private const int ACCURACY = 2;
    private const int DAMAGE_MOD = 10;
    private const int BUFFVALUE = 0;

    public Gash() : base(NAME, DESCRIPTION, ACCURACY, DAMAGE_MOD, BUFFVALUE)
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
