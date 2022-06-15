using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsManager : MonoBehaviour {
    public Slider playerHealthBar;
    public Text playerNameText;
    public Text playerStatText;

    public Slider opponentHealthBar;
    public Text opponentNameText;
    public Text opponentStatsText;
	
	// Update is called once per frame
	void Update () {
        playerNameText.text = Player.fighterName;
        playerStatText.text = "Attack - " + Player.currentAttack +
                                "\nAccuracy - " + Player.accuracy +
                                "\nSpeed - " + Player.baseSpeed;
        playerHealthBar.GetComponentInChildren<Text>().text = Player.currentHealth + " / " + Player.baseHealth;
        playerHealthBar.maxValue = Player.baseHealth;
        playerHealthBar.value = Player.currentHealth;

        opponentNameText.text = Opponent.fighterName;
        opponentStatsText.text = "Attack - " + Opponent.currentAttack +
                                    "\nAccuracy - " + Opponent.accuracy +
                                    "\nSpeed - " + Opponent.baseSpeed;
        opponentHealthBar.GetComponentInChildren<Text>().text = Opponent.currentHealth + " / " + Opponent.baseHealth;
        opponentHealthBar.maxValue = Opponent.baseHealth;
        opponentHealthBar.value = Opponent.currentHealth;
    }
}
