using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwingingBlow : Skill {
    private const string NAME = "Swinging Blow";
    private const string TYPE = "Single-Turn";
    private const string DESCRIPTION = "Swinging Blow - A precise strike that deals immense damage.";
    private const int ACCURACY = 4;
    private const int DAMAGE_MOD = 50;
    private const int BUFF_VALUE = 0;
    private const int TURNS_ACTIVE = 0;

    public SwingingBlow() : base(NAME, TYPE, DESCRIPTION, ACCURACY, DAMAGE_MOD, BUFF_VALUE, TURNS_ACTIVE)
    {
        this.name = NAME;
        this.type = TYPE;
        this.description = DESCRIPTION;
        this.accuracy = ACCURACY;
        this.damageMod = DAMAGE_MOD;
        this.buffValue = BUFF_VALUE;
        this.turnsActive = TURNS_ACTIVE;
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
