using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PoweringStrike : Skill {
    private const string NAME = "Powering Strike";
    private const string TYPE = "Affliction";
    private const string DESCRIPTION = "Powering Strike - A mighty strike that can deal great damage to foes, raising attack power temporarily.";
    private const int ACCURACY = 4;
    private const int DAMAGE_MOD = 10;
    private const int BUFF_VALUE = 5;
    private const int TURNS_ACTIVE = 3;

    private string buffTarget;

    public PoweringStrike() : base(NAME, TYPE, DESCRIPTION, ACCURACY, DAMAGE_MOD, BUFF_VALUE, TURNS_ACTIVE)
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
            buffTarget = "Player";
            BattleManager.damageAmount = Player.currentAttack + damageMod;
        }
        else
        {
            buffTarget = "Opponent";
            BattleManager.damageAmount = Opponent.currentAttack + damageMod;
        }
    }

    public override void SecondaryEffect()
    {
        AudioManager.audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/Sounds/Afflictions/Strengthened"));

        if (buffTarget == "Player")
        {
            Player.currentAttack = Player.baseAttack + buffValue;
            Player.activeSkillAfflictions.Add(this);
            BattleManager.battleManager.PlayerStatusDisplay("Strengthened", new Color(255.00f / 255.00f, 100.00f / 255.0f, 25.00f / 255.0f));
            statusSlot = MonoBehaviour.Instantiate(BattleManager.battleManager.statusSlotObject, GameObject.Find("PlayerAfflictionList").transform);

            Image[] afflictionSlots = GameObject.Find("PlayerAfflictionList").transform.GetComponentsInChildren<Image>();

            foreach (Image image in afflictionSlots)
            {
                if (image.gameObject != statusSlot.gameObject && image.sprite != null &&
                    (image.sprite.name == "Enfeebled" || image.sprite.name == "Strengthened"))
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

                    if (image.sprite.name == "Enfeebled")
                    {
                        Player.currentAttack = Player.baseAttack;
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
            Opponent.currentAttack = Opponent.baseAttack + buffValue;
            Opponent.activeSkillAfflictions.Add(this);
            BattleManager.battleManager.OpponentStatusDisplay("Strengthened", new Color(255.00f / 255.00f, 100.00f / 255.0f, 25.00f / 255.0f));
            statusSlot = MonoBehaviour.Instantiate(BattleManager.battleManager.statusSlotObject, GameObject.Find("OpponentAfflictionList").transform);

            Image[] afflictionSlots = GameObject.Find("OpponentAfflictionList").transform.GetComponents<Image>();

            foreach (Image image in afflictionSlots)
            {
                if (image.gameObject != statusSlot.gameObject && image.sprite != null &&
                    (image.sprite.name == "Enfeebled" || image.sprite.name == "Strengthened"))
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

                    if (image.sprite.name == "Enfeebled")
                    {
                        Opponent.currentAttack = Opponent.baseAttack;
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
            statusSlot.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Afflictions/Strengthened");
            statusSlot.GetComponentInChildren<Text>().text = (turnsActive - turnsCompleted).ToString();
        }
    }

    public override void Afflict()
    {
        if (turnsCompleted < turnsActive)
        {
            statusSlot.GetComponentInChildren<Text>().text = (turnsActive - turnsCompleted).ToString();
            turnsCompleted++;
        }
        else
        {
            if (buffTarget == "Player")
            {
                Player.currentAttack = Player.baseAttack;
                Player.activeSkillAfflictions.Remove(this);
            }
            else if (buffTarget == "Opponent")
            {
                Opponent.currentAttack = Opponent.baseAttack;
                Opponent.activeSkillAfflictions.Remove(this);
            }

            MonoBehaviour.Destroy(statusSlot);
            buffTarget = "";
            turnsCompleted = 0;
        }
    }
}
