using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ThickHide : Trait
{
    private const string NAME = "Thick Hide";
    private const string DESCRIPTION = "The skin of Ulgrath hardens on grave inury, mutating to a dense carapace. Attacks from the opponent reduce by 5.";
    private const int BUFF_VALUE = 5;

    public ThickHide() : base(NAME, DESCRIPTION, BUFF_VALUE)
    {
        this.name = NAME;
        this.description = DESCRIPTION;
        this.buffValue = BUFF_VALUE;
    }

    public override void Effect()
    {
        if (!BattleManager.turnCounter && Player.currentHealth <= 50 && !Player.isReinforced)
        {
            AudioManager.audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/Sounds/Fighters/Ulgrath/Trait"));
            BattleManager.battleManager.PlayerTraitDisplay("Reinforced");
            statusSlot = MonoBehaviour.Instantiate(BattleManager.battleManager.statusSlotObject, GameObject.Find("PlayerAfflictionList").transform);
            Player.isReinforced = true;
            BattleManager.damageMod = buffValue;
        }
        else if (BattleManager.turnCounter && Opponent.currentHealth <= 50 && !Opponent.isReinforced)
        {
            AudioManager.audioSource.PlayOneShot(Resources.Load<AudioClip>("Audio/Sounds/Fighters/Ulgrath/Trait"));
            BattleManager.battleManager.OpponentTraitDisplay("Reinforced");
            statusSlot = MonoBehaviour.Instantiate(BattleManager.battleManager.statusSlotObject, GameObject.Find("OpponentAfflictionList").transform);
            Opponent.isReinforced = true;
            BattleManager.damageMod = buffValue;
        }

        if (statusSlot != null)
        {
            statusSlot.GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Afflictions/Reinforced");
        }
    }
}
