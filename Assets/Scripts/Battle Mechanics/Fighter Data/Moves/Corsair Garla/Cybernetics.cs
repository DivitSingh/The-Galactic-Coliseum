using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cybernetics : Trait
{
    private const string NAME = "Cybernetics";
    private const string DESCRIPTION = "When damaged immensely, the mechanical body of Garla begins to augment its reconstructive properties, improving her healing attack.";
    private const int BUFF_VALUE = 5;

    private bool effectComplete;

    public Cybernetics() : base(NAME, DESCRIPTION, BUFF_VALUE)
    {
        this.name = NAME;
        this.description = DESCRIPTION;
        this.buffValue = BUFF_VALUE;
    }

    public override void Effect()
    {
        if (!BattleManager.turnCounter && Player.currentHealth <= 50 && !effectComplete)
        {
            AudioManager.audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/Sounds/Fighters/Corsair Garla/Trait"));
            BattleManager.battleManager.PlayerTraitDisplay("Enhanced");
            statusSlot = MonoBehaviour.Instantiate(BattleManager.battleManager.statusSlotObject, GameObject.Find("PlayerAfflictionList").transform);
            effectComplete = true;
            Player.lightAttack.buffValue += buffValue;
        }
        else if (BattleManager.turnCounter && Opponent.currentHealth <= 50 && !effectComplete)
        {
            AudioManager.audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/Sounds/Fighters/Corsair Garla/Trait"));
            BattleManager.battleManager.OpponentTraitDisplay("Enhanced");
            statusSlot = MonoBehaviour.Instantiate(BattleManager.battleManager.statusSlotObject, GameObject.Find("OpponentAfflictionList").transform);
            effectComplete = true;
            Opponent.lightAttack.buffValue += buffValue;
        }

        if (statusSlot != null)
        {
            statusSlot.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Afflictions/Enhanced");
        }
    }
}
