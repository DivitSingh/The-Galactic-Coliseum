using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Persistence : Trait
{
    private const string NAME = "Persistence";
    private const string DESCRIPTION = "When Dreyar misses an attempt to use a skill, his attack increases by 10.";
    private const int BUFF_VALUE = 10;

    public Persistence() : base(NAME, DESCRIPTION, BUFF_VALUE)
    {
        this.name = NAME;
        this.description = DESCRIPTION;
        this.buffValue = BUFF_VALUE;
    }

    public override void Effect()
    {
        if (!BattleManager.turnCounter && BattleManager.moveType == Player.skill.name && BattleManager.rollValue <= (6 - BattleManager.resultantAccuracy))
        {
            AudioManager.audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/Sounds/Fighters/Sentinel Dreyar/Trait"));
            BattleManager.battleManager.PlayerTraitDisplay("Strengthened");
            statusSlot = MonoBehaviour.Instantiate(BattleManager.battleManager.statusSlotObject, GameObject.Find("PlayerAfflictionList").transform);
            Player.currentAttack = Player.baseAttack + buffValue;

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

                    MonoBehaviour.Destroy(image.gameObject);
                    break;
                }
            }
        }
        else if (BattleManager.turnCounter && BattleManager.moveType == Opponent.skill.name && BattleManager.rollValue <= (6 - BattleManager.resultantAccuracy))
        {
            AudioManager.audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/Sounds/Fighters/Sentinel Dreyar/Trait"));
            BattleManager.battleManager.OpponentTraitDisplay("Strengthened");
            statusSlot = MonoBehaviour.Instantiate(BattleManager.battleManager.statusSlotObject, GameObject.Find("OpponentAfflictionList").transform);
            Opponent.currentAttack = Opponent.baseAttack + buffValue;

            Image[] afflictionSlots = GameObject.Find("OpponentAfflictionList").transform.GetComponentsInChildren<Image>();

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

                    MonoBehaviour.Destroy(image.gameObject);
                    break;
                }
            }
        }

        if (statusSlot != null)
        {
            statusSlot.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Afflictions/Strengthened");
        }
    }
}
