using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Reconstruct : Skill
{
    private const string NAME = "Reconstruct";
    private const string TYPE = "Affliction";
    private const string DESCRIPTION = "Reconstruct - Commences a refragmentation of damaged parts, aiding in the reovery of health.";
    private const int ACCURACY = 6;
    private const int DAMAGE_MOD = 0;
    private const int BUFF_VALUE = 20;
    private const int TURNS_ACTIVE = 3;

    private string buffTarget;

    public Reconstruct() : base(NAME, TYPE, DESCRIPTION, ACCURACY, DAMAGE_MOD, BUFF_VALUE, TURNS_ACTIVE)
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
        BattleManager.damageAmount = 0;
    }

    public override void SecondaryEffect()
    {
        if (!BattleManager.turnCounter)
        {
            buffTarget = "Player";
            Player.activeSkillAfflictions.Add(this);
            statusSlot = MonoBehaviour.Instantiate(BattleManager.battleManager.statusSlotObject, GameObject.Find("PlayerAfflictionList").transform);

            Image[] afflicitionSlots = GameObject.Find("PlayerAfflictionList").transform.GetComponentsInChildren<Image>();

            foreach (Image image in afflicitionSlots)
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

                    if (image.sprite.name == "Injured")
                    {
                        Player.activeSkillAfflictions.Remove(this);
                        MonoBehaviour.Destroy(statusSlot);
                    }

                    MonoBehaviour.Destroy(image.gameObject);
                    break;
                }
            }

            BattleManager.battleManager.PlayerTurnEnd();
        }
        else if (BattleManager.turnCounter)
        {
            buffTarget = "Opponent";
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

                    if (image.sprite.name == "Injured")
                    {
                        Opponent.activeSkillAfflictions.Remove(this);
                        MonoBehaviour.Destroy(statusSlot);
                    }

                    MonoBehaviour.Destroy(image.gameObject);
                    break;
                }
            }

            BattleManager.battleManager.OpponentTurnEnd();
        }

        if (statusSlot != null)
        {
            AudioManager.audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/Sounds/Afflictions/Rejuvenating"));
            statusSlot.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Afflictions/Rejuvenating");
            statusSlot.GetComponentInChildren<Text>().text = (turnsActive - turnsCompleted).ToString();
        }
    }

    public override void Afflict()
    {
        if (buffTarget == "Player")
        {
            AudioManager.audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/Sounds/Afflictions/Rejuvenating"));
            BattleManager.battleManager.PlayerStatusDisplay(buffValue.ToString() + " Rejuvenate", Color.green);
            Player.currentHealth += buffValue;

            if (Player.currentHealth > Player.baseHealth)
            {
                Player.currentHealth = Player.baseHealth;
            }
        }
        else if (buffTarget == "Opponent")
        {
            AudioManager.audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/Sounds/Afflictions/Rejuvenating"));
            BattleManager.battleManager.OpponentStatusDisplay(buffValue.ToString() + " Rejuvenate", Color.green);
            Opponent.currentHealth += buffValue;

            if (Opponent.currentHealth > Opponent.baseHealth)
            {
                Opponent.currentHealth = Opponent.baseHealth;
            }
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
