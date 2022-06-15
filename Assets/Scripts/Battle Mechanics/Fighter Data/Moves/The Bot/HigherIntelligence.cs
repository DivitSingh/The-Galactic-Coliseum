using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HigherIntelligence : Trait
{
    private const string NAME = "Higher Intelligence";
    private const string DESCRIPTION = "The Bot utilises the benefits of a near-perfect mind to strike its opponents carefully, granting it the possiblity of a critical strike.";
    private const int BUFF_VALUE = 0;

    private bool effectComplete;

    public HigherIntelligence() : base(NAME, DESCRIPTION, BUFF_VALUE)
    {
        this.name = NAME;
        this.description = DESCRIPTION;
        this.buffValue = BUFF_VALUE;
    }

    public override void Effect()
    {
        if (!BattleManager.turnCounter && !effectComplete)
        {
            AudioManager.audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/Sounds/Fighters/The Bot/Trait"));
            BattleManager.battleManager.PlayerTraitDisplay("Focused");
            statusSlot = MonoBehaviour.Instantiate(BattleManager.battleManager.statusSlotObject, GameObject.Find("PlayerAfflictionList").transform);
            effectComplete = true;
            Player.isFocused = true;
        }
        else if (BattleManager.turnCounter && !effectComplete)
        {
            AudioManager.audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/Sounds/Fighters/The Bot/Trait"));
            BattleManager.battleManager.OpponentTraitDisplay("Focused");
            statusSlot = MonoBehaviour.Instantiate(BattleManager.battleManager.statusSlotObject, GameObject.Find("OpponentAfflictionList").transform);
            effectComplete = true;
            Opponent.isFocused = true;
        }

        if (statusSlot != null)
        {
            statusSlot.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Afflictions/Focused");
        }
    }
}
