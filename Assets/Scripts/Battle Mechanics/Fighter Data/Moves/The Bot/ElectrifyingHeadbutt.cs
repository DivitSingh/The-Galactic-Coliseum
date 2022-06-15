using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ElectrifyingHeadbutt : Skill {
    private const string NAME = "Electrifying Headbutt";
    private const string TYPE = "Single-Turn";
    private const string DESCRIPTION = "Electrifying Headbutt - A sharp strike from the noggin. Paralyzes the opponent due to the impact and electrical shock it produces.";
    private const int ACCURACY = 6;
    private const int DAMAGE_MOD = 15;
    private const int BUFF_VALUE = 0;
    private const int TURNS_ACTIVE = 0;

    public ElectrifyingHeadbutt() : base(NAME, TYPE, DESCRIPTION, ACCURACY, DAMAGE_MOD, BUFF_VALUE, TURNS_ACTIVE)
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

    public override void SecondaryEffect()
    {
        AudioManager.audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/Sounds/Afflictions/Paralyzed"));

        if (!BattleManager.turnCounter)
        {
            Opponent.isParalyzed = true;
            statusSlot = MonoBehaviour.Instantiate(BattleManager.battleManager.statusSlotObject, GameObject.Find("OpponentAfflictionList").transform);
        }
        else
        {
            Player.isParalyzed = true;
            statusSlot = MonoBehaviour.Instantiate(BattleManager.battleManager.statusSlotObject, GameObject.Find("PlayerAfflictionList").transform);
        }

        statusSlot.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Afflictions/Paralyzed");
        statusSlot.GetComponentInChildren<Text>().text = 1.ToString();
    }

    public override void TertiaryEffect()
    {
        if (!BattleManager.turnCounter)
        {
            foreach (Button button in BattleManager.battleManager.moveButtons)
            {
                if ((button.gameObject.name == "SkillButton" && Player.skillUsed) ||
                    (button.gameObject.name == "ItemButton" && Player.itemUsed))
                {
                    button.interactable = false;
                }
                else
                {
                    button.interactable = true;
                }
            }

            BattleManager.battleManager.OpponentTurnEnd();
            Opponent.isParalyzed = false;
        }
        else
        {
            BattleManager.battleManager.PlayerTurnEnd();
            Player.isParalyzed = false;
        }

        MonoBehaviour.Destroy(statusSlot);
    }
}
