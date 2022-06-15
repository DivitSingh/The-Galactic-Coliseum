using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatusAnimationEvents : MonoBehaviour {
    private bool stunCounter;

    public void StatusDisappear()
    {
        if (!BattleManager.turnCounter)
        {
            BattleManager.battleManager.playerDamageText.SetActive(false);
            BattleManager.battleManager.playerDamageText.GetComponent<Outline>().effectColor = Color.white;
            BattleManager.battleManager.opponentDamageText.GetComponent<Outline>().effectColor = Color.white;
        }
        else
        {
            BattleManager.battleManager.opponentDamageText.SetActive(false);
            BattleManager.battleManager.playerDamageText.GetComponent<Outline>().effectColor = Color.white;
            BattleManager.battleManager.opponentDamageText.GetComponent<Outline>().effectColor = Color.white;
        }
    }

    public void AttackCalculation()
    {
        if (!BattleManager.turnCounter)
        {
            if (Opponent.isParalyzed)
            {
                if (!stunCounter)
                {
                    BattleManager.battleManager.opponentDamageText.SetActive(true);
                    BattleManager.battleManager.opponentDamageText.GetComponent<Text>().text = "Paralyzed";
                    BattleManager.battleManager.opponentDamageText.GetComponent<Text>().color = Color.magenta;
                    BattleManager.battleManager.opponentDamageText.GetComponent<Outline>().effectColor = Color.yellow;
                    BattleManager.battleManager.opponentDamageText.GetComponent<Animator>().SetTrigger("Hover");
                    stunCounter = true;
                }
                else
                {
                    BattleManager.battleManager.opponentDamageText.SetActive(false);
                    Player.skill.TertiaryEffect();
                    stunCounter = false;
                }
            }
            else
            {
                BattleManager.battleManager.PlayerTurnEnd();
            }
        }
        else if (BattleManager.turnCounter)
        {
            if (Player.isParalyzed)
            {
                if (!stunCounter)
                {
                    BattleManager.battleManager.playerDamageText.SetActive(true);
                    BattleManager.battleManager.playerDamageText.GetComponent<Text>().text = "Paralyzed";
                    BattleManager.battleManager.playerDamageText.GetComponent<Text>().color = Color.magenta;
                    BattleManager.battleManager.playerDamageText.GetComponent<Outline>().effectColor = Color.yellow;
                    BattleManager.battleManager.playerDamageText.GetComponent<Animator>().SetTrigger("Hover");
                    stunCounter = true;
                }
                else
                {
                    BattleManager.battleManager.playerDamageText.SetActive(false);
                    Opponent.skill.TertiaryEffect();
                    stunCounter = false;
                }
            }
            else
            {
                BattleManager.battleManager.OpponentTurnEnd();
            }
        }
    }
}
