using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Smash : Attack
{
    private const string NAME = "Smash";
    private const string DESCRIPTION = "Smash - A swift attack that damages foes.";
    private const int ACCURACY = 3;
    private const int DAMAGE_MOD = 0;
    private const int BUFFVALUE = 0;

    public Smash() : base(NAME, DESCRIPTION, ACCURACY, DAMAGE_MOD, BUFFVALUE)
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
