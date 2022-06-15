using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spacedust : Item
{
    private const int ID = 1;
    private const string NAME = "Spacedust";
    private const string DESCRIPTION = "Dust from outside the Galactic Embassy with unique properties. \n\nThe item temporarily increases attack by 15.";
    private const string STATUS_TEXT = "Strengthened";
    private const string TYPE = "Affliction";
    private const int ACCURACY = 6;
    private const int BUFF_VALUE = 15;
    private const int TURNS_ACTIVE = 1;

    private string buffTarget;

    public Spacedust() : base(ID, NAME, DESCRIPTION, STATUS_TEXT, TYPE, ACCURACY, BUFF_VALUE, TURNS_ACTIVE)
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
        AudioManager.audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/Sounds/Afflictions/Strengthened"));

        if (!BattleManager.turnCounter)
        {
            buffTarget = "Player";

            Player.currentAttack = Player.baseAttack + buffValue;
            Player.activeItemAfflictions.Add(this);
            statusSlot = MonoBehaviour.Instantiate(BattleManager.battleManager.statusSlotObject, GameObject.Find("PlayerAfflictionList").transform);

            Image[] afflicitionSlots = GameObject.Find("PlayerAfflictionList").transform.GetComponentsInChildren<Image>();

            foreach (Image image in afflicitionSlots)
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

            Opponent.currentAttack = Opponent.baseAttack + buffValue;
            Opponent.activeItemAfflictions.Add(this);
            statusSlot = MonoBehaviour.Instantiate(BattleManager.battleManager.statusSlotObject, GameObject.Find("OpponentAfflictionList").transform);

            Image[] afflicitionSlots = GameObject.Find("OpponentAfflictionList").transform.GetComponents<Image>();

            foreach (Image image in afflicitionSlots)
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
            statusSlot.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Afflictions/Strengthened");
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
