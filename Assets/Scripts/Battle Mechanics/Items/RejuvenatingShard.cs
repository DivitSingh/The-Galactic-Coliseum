using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RejuvenatingShard : Item
{
    private const int ID = 2;
    private const string NAME = "Rejuvenating Shard";
    private const string DESCRIPTION = "Shard from a distant moon that affects health and physical strength. \n\nThe item recovers 30 health at the cost of a decrease in attack by 10.";
    private const string STATUS_TEXT = "30 Rejuvenate \nEnfeebled";
    private const string TYPE = "Affliction";
    private const int ACCURACY = 6;
    private const int BUFF_VALUE = -10;
    private const int TURNS_ACTIVE = 1;

    private string buffTarget;

    public RejuvenatingShard() : base(ID, NAME, DESCRIPTION, STATUS_TEXT, TYPE, ACCURACY, BUFF_VALUE, TURNS_ACTIVE)
    {
        this.id = ID;
        this.name = NAME;
        this.description = DESCRIPTION;
        this.statusText = STATUS_TEXT;
        this.type = TYPE;
        this.accuracy = ACCURACY;
        this.buffValue = BUFF_VALUE;
        this.turnsActive = TURNS_ACTIVE;
    }

    public override void Effect()
    {
        AudioManager.audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/Sounds/Afflictions/Rejuvenating"), 1.00f);
        AudioManager.audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/Sounds/Afflictions/Enfeebled"), 0.50f);

        if (!BattleManager.turnCounter)
        {
            buffTarget = "Player";

            Player.currentHealth += 30;

            if (Player.currentHealth > Player.baseHealth)
            {
                Player.currentHealth = Player.baseHealth;
            }

            Player.currentAttack = Player.baseAttack + buffValue;

            if (Player.currentAttack < 5)
            {
                Player.currentAttack = 5;
            }

            Player.activeItemAfflictions.Add(this);
            statusSlot = MonoBehaviour.Instantiate(BattleManager.battleManager.statusSlotObject, GameObject.Find("PlayerAfflictionList").transform);

            Image[] afflicitonSlots = GameObject.Find("PlayerAfflictionList").transform.GetComponentsInChildren<Image>();

            foreach (Image image in afflicitonSlots)
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

                    if (image.sprite.name == "Strengthened")
                    {
                        Player.currentAttack = Player.baseAttack;
                        Player.activeItemAfflictions.Remove(this);
                        MonoBehaviour.Destroy(statusSlot);
                    }

                    MonoBehaviour.Destroy(image.gameObject);
                    break;
                }
            }
        }
        else
        {
            buffTarget = "Opponent";

            Opponent.currentHealth += 30;

            if (Player.currentHealth > Player.baseHealth)
            {
                Player.currentHealth = Player.baseHealth;
            }

            Opponent.currentAttack = Opponent.baseAttack + buffValue;

            if (Opponent.currentAttack < 5)
            {
                Opponent.currentAttack = 5;
            }

            Opponent.activeItemAfflictions.Add(this);
            statusSlot = MonoBehaviour.Instantiate(BattleManager.battleManager.statusSlotObject, GameObject.Find("OpponentAfflictionList").transform);

            Image[] afflicitonSlots = GameObject.Find("OpponentAfflictionList").transform.GetComponents<Image>();

            foreach (Image image in afflicitonSlots)
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

                    if (image.sprite.name == "Strengthened")
                    {
                        Opponent.currentAttack = Opponent.baseAttack;
                        Opponent.activeItemAfflictions.Remove(this);
                        MonoBehaviour.Destroy(statusSlot);
                    }

                    MonoBehaviour.Destroy(image.gameObject);
                    break;
                }
            }
        }

        if (statusSlot != null)
        {
            statusSlot.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Afflictions/Enfeebled");
            statusSlot.GetComponentInChildren<Text>().text = (turnsActive - turnsCompleted).ToString();
        }
    }

    public override void Afflict()
    {
        if (turnsCompleted < turnsActive)
        {
            turnsCompleted++;
        }
        else
        {
            if (buffTarget == "Player")
            {
                Player.currentAttack = Player.baseAttack;
                Player.activeItemAfflictions.Remove(this);
            }
            else if (buffTarget == "Opponent")
            {
                Opponent.currentAttack = Opponent.baseAttack;
                Opponent.activeItemAfflictions.Remove(this);
            }

            MonoBehaviour.Destroy(statusSlot);
            buffTarget = "";
            turnsCompleted = 0;
        }
    }
}
