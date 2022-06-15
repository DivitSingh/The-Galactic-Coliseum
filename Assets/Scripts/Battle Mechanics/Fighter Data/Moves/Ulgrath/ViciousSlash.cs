using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViciousSlash : Skill
{
    private const string NAME = "Vicious Slash";
    private const string TYPE = "Affliction";
    private const string DESCRIPTION = "Vicious Slash - A strike that gravely injures the opponent.";
    private const int ACCURACY = 3;
    private const int DAMAGE_MOD = 10;
    private const int BUFF_VALUE = 10;
    private const int TURNS_ACTIVE = 4;

    private string buffTarget;

    public ViciousSlash() : base(NAME, TYPE, DESCRIPTION, ACCURACY, DAMAGE_MOD, BUFF_VALUE, TURNS_ACTIVE)
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
            buffTarget = "Opponent";
            BattleManager.damageAmount = Player.currentAttack + damageMod;
        }
        else
        {
            buffTarget = "Player";
            BattleManager.damageAmount = Opponent.currentAttack + damageMod;
        }
    }

    public override void SecondaryEffect()
    {
        if (buffTarget == "Player")
        {
            Player.activeSkillAfflictions.Add(this);
            statusSlot = MonoBehaviour.Instantiate(BattleManager.battleManager.statusSlotObject, GameObject.Find("PlayerAfflictionList").transform);

            Image[] afflicitonSlots = GameObject.Find("PlayerAfflictionList").transform.GetComponentsInChildren<Image>();

            foreach (Image image in afflicitonSlots)
            {
                if (image.gameObject != statusSlot.gameObject && image.sprite != null &&
                    (image.sprite.name == "Injured" || image.sprite.name == "Rejuvenating"))
                {
                    for (int i = 0; i < Player.activeSkillAfflictions.Count; i++)
                    {
                        if (Player.activeSkillAfflictions[i].statusSlot == image.gameObject)
                        {
                            Player.activeSkillAfflictions.Remove(Player.activeSkillAfflictions[i]);
                        }
                    }

                    for (int i = 0; i < Player.activeItemAfflictions.Count; i++)
                    {
                        if (Player.activeItemAfflictions[i].statusSlot == image.gameObject)
                        {
                            Player.activeItemAfflictions.Remove(Player.activeItemAfflictions[i]);
                        }
                    }

                    if (image.sprite.name == "Rejuvenating")
                    {
                        Player.activeSkillAfflictions.Remove(this);
                        MonoBehaviour.Destroy(statusSlot);
                    }

                    MonoBehaviour.Destroy(image.gameObject);
                    break;
                }
            }
        }
        else if (buffTarget == "Opponent")
        {
            Opponent.activeSkillAfflictions.Add(this);
            statusSlot = MonoBehaviour.Instantiate(BattleManager.battleManager.statusSlotObject, GameObject.Find("OpponentAfflictionList").transform);

            Image[] afflicitionSlots = GameObject.Find("OpponentAfflictionList").transform.GetComponentsInChildren<Image>();

            foreach (Image image in afflicitionSlots)
            {
                if (image.gameObject != statusSlot.gameObject && image.sprite != null &&
                    (image.sprite.name == "Injured" || image.sprite.name == "Rejuvenating"))
                {
                    for (int i = 0; i < Opponent.activeSkillAfflictions.Count; i++)
                    {
                        if (Opponent.activeSkillAfflictions[i].statusSlot == image.gameObject)
                        {
                            Opponent.activeSkillAfflictions.Remove(Opponent.activeSkillAfflictions[i]);
                        }
                    }

                    for (int i = 0; i < Opponent.activeItemAfflictions.Count; i++)
                    {
                        if (Opponent.activeItemAfflictions[i].statusSlot == image.gameObject)
                        {
                            Opponent.activeItemAfflictions.Remove(Opponent.activeItemAfflictions[i]);
                        }
                    }

                    if (image.sprite.name == "Rejuvenating")
                    {
                        Opponent.activeSkillAfflictions.Remove(this);
                        MonoBehaviour.Destroy(statusSlot);
                    }

                    MonoBehaviour.Destroy(image.gameObject);
                    break;
                }
            }
        }

        if (statusSlot != null)
        {
            AudioManager.audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/Sounds/Afflictions/Injured"));
            statusSlot.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Afflictions/Injured");
            statusSlot.GetComponentInChildren<Text>().text = (turnsActive - turnsCompleted).ToString();
        }
    }

    public override void Afflict()
    {
        if (buffTarget == "Player")
        {
            BattleManager.battleManager.PlayerStatusDisplay("Injured\n" + buffValue.ToString(), Color.red);
            Player.currentHealth -= buffValue;
        }
        else if (buffTarget == "Opponent")
        {
            BattleManager.battleManager.OpponentStatusDisplay("Injured\n" + buffValue.ToString(), Color.red);
            Opponent.currentHealth -= buffValue;
        }

        turnsCompleted++;
        statusSlot.GetComponentInChildren<Text>().text = (turnsActive - turnsCompleted).ToString();

        if (turnsCompleted >= turnsActive)
        {
            if (buffTarget == "Player")
            {
                Player.activeSkillAfflictions.Remove(this);
            }
            else if (buffTarget == "Opponent")
            {
                Opponent.activeSkillAfflictions.Remove(this);
            }

            MonoBehaviour.Destroy(statusSlot);
            buffTarget = "";
            turnsCompleted = 0;
        }
    }
}