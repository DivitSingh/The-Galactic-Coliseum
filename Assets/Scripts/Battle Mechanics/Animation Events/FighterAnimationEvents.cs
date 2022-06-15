using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FighterAnimationEvents : MonoBehaviour {
    public void NextRunState()
    {
        if (SceneManager.GetActiveScene().name == "GameMenu")
        {
            this.gameObject.GetComponent<Animator>().SetTrigger("Taunt");
        }
        else
        {
            if (this.gameObject == BattleManager.battleManager.playerObject)
            {
                this.gameObject.transform.position = BattleManager.battleManager.playerPosition;
            }
            else if (this.gameObject == BattleManager.battleManager.opponentObject)
            {
                this.gameObject.transform.position = BattleManager.battleManager.opponentPosition;
            }

            this.gameObject.GetComponent<Animator>().Play("Idle");
        }
    }

    public void TriggerHurt()
    {
        BattleManager.battleManager.AttackProcessing();
    }

    public void TriggerBuff()
    {
        if (!BattleManager.turnCounter)
        {
            Player.skill.SecondaryEffect();
        }
        else
        {
            Opponent.skill.SecondaryEffect();
        }
    }

    public void CheckNextState()
    {
        StartCoroutine(TransitionDelay());
    }

    private IEnumerator TransitionDelay()
    {
        yield return new WaitForSeconds(1.00f);

        int battlesLost = BattleManager.roundNumber - BattleManager.battlesWon;

        if (BattleManager.battlesWon > 1)
        {
            BattleManager.battleManager.Victory();
        }
        else if (battlesLost > 1)
        {
            BattleManager.battleManager.Defeat();
        }
        else
        {
            BattleManager.roundNumber++;
            Player.EnableStats();
            Opponent.EnableStats();
            SceneManager.LoadScene("Arena");
        }
    }
}
