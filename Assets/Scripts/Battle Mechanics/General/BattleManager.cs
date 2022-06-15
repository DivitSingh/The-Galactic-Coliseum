using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class BattleManager : MonoBehaviour {
    public static BattleManager battleManager;

    public static string arenaName;
    public static int battlesWon;
    public static bool turnCounter;
    public static string moveType;
    public static int roundNumber;
    public static int rollValue;
    public static int resultantAccuracy;
    public static int damageAmount;
    public static int damageMod;

    public Vector3 playerPosition;
    public Vector3 opponentPosition;

    public List<AudioClip> arenaMusic;
    public AudioClip clockTick;
    public AudioClip fightSound;
    public AudioClip victory;
    public AudioClip defeat;
    public AudioClip winSound;
    public AudioClip loseSound;
    public AudioClip clickSound;
    public AudioClip attackSound;
    public AudioClip skillSound;
    public AudioClip itemSound;

    public GameObject playerObject;
    public GameObject opponentObject;

    public List<Button> moveButtons;
    public GameObject statusSlotObject;
    public GameObject playerDamageText;
    public GameObject playerStatusText;
    public GameObject playerTraitText;
    public GameObject opponentDamageText;
    public GameObject opponentStatusText;
    public GameObject opponentTraitText;

    public GameObject galacticEmbassy;
    public GameObject laboratory;
    public GameObject asteroid;

    public GameObject vanguardianDolores;
    public GameObject sentinelDreyar;
    public GameObject ulgrath;
    public GameObject corsairGarla;
    public GameObject theBot;

    private GameObject panel;

    private IEnumerator LateStart()
    {
        yield return new WaitForSeconds(0.00f);

        if (battleManager == null)
        {
            battleManager = this;
        }
        else
        {
            Destroy(this);
        }

        playerDamageText.SetActive(false);
        opponentDamageText.SetActive(false);

        playerPosition = new Vector3(403.00f, 0.00f, 50.00f);
        opponentPosition = new Vector3(401.80f, 0.00f, 50.00f);

        LoadArena();
    }

    private void Start()
    {
        StartCoroutine(LateStart()); 
    }

    public void LoadArena()
{
        if (arenaName == "The Galactic Embassy")
        {
            galacticEmbassy.SetActive(true);
            this.gameObject.GetComponent<AudioSource>().clip = arenaMusic[0];
        }
        else if (arenaName == "The Laboratory")
        {
            laboratory.SetActive(true);
            this.gameObject.GetComponent<AudioSource>().clip = arenaMusic[1];
        }
        else if (arenaName == "The Asteroid")
        {
            asteroid.SetActive(true);
            this.gameObject.GetComponent<AudioSource>().clip = arenaMusic[2];
        }

        this.gameObject.GetComponent<AudioSource>().Play();

        GameObject.Find("ItemButton").GetComponent<Image>().sprite = Resources.Load<Sprite>("Images/Items/" + Player.item.name);

        if (Player.fighterName == "Vanguardian Dolores")
        {
            playerObject = Instantiate(vanguardianDolores, playerPosition, Quaternion.Euler(0.00f, 270.00f, 0.00f));
        }
        else if (Player.fighterName == "Sentinel Dreyar")
        {
            playerObject = Instantiate(sentinelDreyar, playerPosition, Quaternion.Euler(0.00f, 270.00f, 0.00f));
        }
        else if (Player.fighterName == "Ulgrath")
        {
            playerObject = Instantiate(ulgrath, playerPosition, Quaternion.Euler(0.00f, 270.00f, 0.00f));
        }
        else if (Player.fighterName == "Corsair Garla")
        {
            playerObject = Instantiate(corsairGarla, playerPosition, Quaternion.Euler(0.00f, 270.00f, 0.00f));
        }
        else if (Player.fighterName == "The Bot")
        {
            playerObject = Instantiate(theBot, playerPosition, Quaternion.Euler(0.00f, 270.00f, 0.00f));
        }

        if (Opponent.fighterName == "Vanguardian Dolores")
        {
            opponentObject = Instantiate(vanguardianDolores, opponentPosition, Quaternion.Euler(0.00f, 90.00f, 0.00f));
        }
        else if (Opponent.fighterName == "Sentinel Dreyar")
        {
            opponentObject = Instantiate(sentinelDreyar, opponentPosition, Quaternion.Euler(0.00f, 90.00f, 0.00f));
        }
        else if (Opponent.fighterName == "Ulgrath")
        {
            opponentObject = Instantiate(ulgrath, opponentPosition, Quaternion.Euler(0.00f, 90.00f, 0.00f));
        }
        else if (Opponent.fighterName == "Corsair Garla")
        {
            opponentObject = Instantiate(corsairGarla, opponentPosition, Quaternion.Euler(0.00f, 90.00f, 0.00f));
        }
        else if (Opponent.fighterName == "The Bot")
        {
            opponentObject = Instantiate(theBot, opponentPosition, Quaternion.Euler(0.00f, 90.00f, 0.00f));
        }

        panel = GameObject.Find("Panel");
        StartCoroutine(FightIntro());
    }

    public void OnClick()
    {
        if (!turnCounter && Player.currentHealth > 0)
        {
            if (EventSystem.current.currentSelectedGameObject.name == "LightAttackButton")
            {
                AudioManager.audioSource.PlayOneShot(attackSound);
                moveType = "Light Attack";
                Player.lightAttack.Effect();

                if (damageAmount < 5)
                {
                    damageAmount = 5;
                }
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "HeavyAttackButton")
            {
                AudioManager.audioSource.PlayOneShot(attackSound);
                moveType = "Heavy Attack";
                Player.heavyAttack.Effect();
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "SkillButton" && !Player.skillUsed)
            {
                AudioManager.audioSource.PlayOneShot(skillSound);
                moveType = "Skill";
                Player.skill.Effect();
                Player.skillUsed = true;
            }
            else if (EventSystem.current.currentSelectedGameObject.name == "ItemButton" && !Player.itemUsed)
            {
                AudioManager.audioSource.PlayOneShot(itemSound, 1.00f);
                moveType = "Item";
                BattleManager.resultantAccuracy = Player.item.accuracy;
                Player.itemUsed = true;
                AttackProcessing();
            }

            if (moveType != "")
            {
                foreach (Button button in moveButtons)
                {
                    button.interactable = false;
                }

                if (moveType != "Item")
                {
                    PlayerAttackCommence();
                }
            }
        }
    }

    void PlayerAttackCommence()
    {
        playerObject.GetComponent<Animator>().SetTrigger(moveType);
    }

    public void PlayerStatusDisplay (string text, Color color)
    {
        playerStatusText.SetActive(true);
        playerStatusText.GetComponent<Text>().text = text;
        playerStatusText.GetComponent<Text>().color = color;
        playerStatusText.GetComponent<Outline>().effectColor = Color.yellow;
        playerStatusText.GetComponent<Animator>().SetTrigger("SpecialHover");
    }

    void PlayerHit()
    {
        if (moveType == "Item")
        {
            playerDamageText.SetActive(true);
            playerDamageText.GetComponent<Text>().text = Player.item.statusText;
            playerDamageText.GetComponent<Text>().color = Color.black;
            playerDamageText.GetComponent<Outline>().effectColor = Color.yellow;
            playerDamageText.GetComponent<Animator>().SetTrigger("Hover");
        }
        else
        {
            if (Opponent.isReinforced)
            {
                damageAmount -= damageMod;
            }

            if (Player.isFocused && rollValue == 6)
            {
                damageAmount += 15;
                opponentDamageText.GetComponent<Text>().text = moveType + "\nCritical\n" + damageAmount.ToString();
            }
            else
            {
                opponentDamageText.GetComponent<Text>().text = moveType + "\n" + damageAmount.ToString();
            }

            Opponent.currentHealth -= damageAmount;
            opponentDamageText.GetComponent<Text>().color = Color.red;
            opponentDamageText.SetActive(true);
            
            opponentDamageText.GetComponent<Animator>().SetTrigger("Hover");
        }
    }

    void PlayerMiss()
    {
        if (moveType == "Item")
        {
            playerDamageText.SetActive(true);
            playerDamageText.GetComponent<Text>().text = "Failure";
            playerDamageText.GetComponent<Text>().color = Color.blue;
            playerDamageText.GetComponent<Outline>().effectColor = Color.yellow;
            playerDamageText.GetComponent<Animator>().SetTrigger("Hover");
        }
        else
        {
            opponentDamageText.SetActive(true);
            opponentDamageText.GetComponent<Text>().text = moveType + "\nMiss";
            opponentDamageText.GetComponent<Text>().color = Color.red;
            opponentDamageText.GetComponent<Animator>().SetTrigger("Hover");
        }
    }

    public void PlayerTraitDisplay(string text)
    {
        playerTraitText.SetActive(true);
        playerTraitText.GetComponent<Text>().text = text;
        playerTraitText.GetComponent<Text>().color = Color.cyan;
        playerTraitText.GetComponent<Outline>().effectColor = Color.yellow;
        playerTraitText.GetComponent<Animator>().SetTrigger("SpecialHover");
    }

    public void PlayerTurnEnd()
    {
        playerDamageText.SetActive(false);
        playerDamageText.GetComponent<Outline>().effectColor = Color.white;
        opponentDamageText.SetActive(false);
        opponentDamageText.GetComponent<Outline>().effectColor = Color.white;

        Player.trait.Effect();

        moveType = "";
        rollValue = 0;
        resultantAccuracy = 0;
        damageAmount = 0;
        turnCounter = true;

        for (int i = 0; i < Player.activeSkillAfflictions.Count; i++)
        {
            Player.activeSkillAfflictions[i].Afflict();
        }

        for (int i = 0; i < Player.activeItemAfflictions.Count; i++)
        {
            Player.activeItemAfflictions[i].Afflict();
        }

        if (Opponent.currentHealth <= 0)
        {
            battlesWon++;
        }
        else if (Player.currentHealth <= 0)
        {
            AudioManager.audioSource.PlayOneShot(Player.dieSound);
            opponentObject.GetComponent<Animator>().SetTrigger("Victory");
            playerObject.GetComponent<Animator>().SetTrigger("Die");
            AudioManager.audioSource.PlayOneShot(loseSound, 1.00f);
        }
        else if (Opponent.currentHealth > 0)
        {
            OpponentAttackCommence();
        }

    }

    public void OpponentAttackCommence()
    {
        MoveSelection:
        int moveSelector = Random.Range(0, 3);

        if (moveSelector == 0)
        {
            AudioManager.audioSource.PlayOneShot(attackSound);
            moveType = "Light Attack";
            Opponent.lightAttack.Effect();

            if (damageAmount < 5)
            {
                damageAmount = 5;
            }
        }
        else if (moveSelector == 1)
        {
            AudioManager.audioSource.PlayOneShot(attackSound);
            moveType = "Heavy Attack";
            Opponent.heavyAttack.Effect();
        }
        else if (moveSelector == 2 && !Opponent.skillUsed)
        {
            AudioManager.audioSource.PlayOneShot(skillSound);
            moveType = "Skill";
            Opponent.skill.Effect();
            Opponent.skillUsed = true;
        }
        else
        {
            goto MoveSelection;
        }

        opponentObject.GetComponent<Animator>().SetTrigger(moveType);
    }

    public void OpponentStatusDisplay(string text, Color color)
    {
        opponentStatusText.SetActive(true);
        opponentStatusText.GetComponent<Text>().text = text;
        opponentStatusText.GetComponent<Text>().color = color;
        opponentStatusText.GetComponent<Outline>().effectColor = Color.yellow;
        opponentStatusText.GetComponent<Animator>().SetTrigger("SpecialHover");
    }

    void OpponentHit()
    {
        if (Player.isReinforced)
        {
            damageAmount -= damageMod;
        }

        if (Opponent.isFocused && rollValue == 6)
        {
            damageAmount += 15;
            playerDamageText.GetComponent<Text>().text = moveType + "\nCritical\n" + damageAmount.ToString();
        }
        else
        {
            playerDamageText.GetComponent<Text>().text = moveType + "\n" + damageAmount.ToString();
        }

        Player.currentHealth -= damageAmount;
        playerDamageText.GetComponent<Text>().color = Color.red;
        playerDamageText.SetActive(true);
        playerDamageText.GetComponent<Animator>().SetTrigger("Hover");
    }

    void OpponentMiss()
    {
        playerDamageText.SetActive(true);
        playerDamageText.GetComponent<Text>().text = moveType + "\nMiss";
        playerDamageText.GetComponent<Text>().color = Color.red;
        playerDamageText.GetComponent<Animator>().SetTrigger("Hover");
    }

    public void OpponentTraitDisplay(string text)
    {
        opponentTraitText.SetActive(true);
        opponentTraitText.GetComponent<Text>().text = text;
        opponentTraitText.GetComponent<Text>().color = Color.cyan;
        opponentTraitText.GetComponent<Outline>().effectColor = Color.yellow;
        opponentTraitText.GetComponent<Animator>().SetTrigger("SpecialHover");
    }

    public void OpponentTurnEnd()
    {
        playerDamageText.SetActive(false);
        playerDamageText.GetComponent<Outline>().effectColor = Color.white;
        opponentDamageText.SetActive(false);
        opponentDamageText.GetComponent<Outline>().effectColor = Color.white;

        Opponent.trait.Effect();

        moveType = "";
        rollValue = 0;
        resultantAccuracy = 0;
        damageAmount = 0;
        turnCounter = false;

        for (int i = 0; i < Opponent.activeSkillAfflictions.Count; i++)
        {
            Opponent.activeSkillAfflictions[i].Afflict();
        }

        if (Player.currentHealth <= 0)
        {

        }
        else if (Opponent.currentHealth <= 0)
        {
            AudioManager.audioSource.PlayOneShot(Opponent.dieSound);
            playerObject.GetComponent<Animator>().SetTrigger("Victory");
            opponentObject.GetComponent<Animator>().SetTrigger("Die");
            AudioManager.audioSource.PlayOneShot(winSound);
            battlesWon++;
        }
        else if (Player.currentHealth > 0)
        {
            foreach (Button button in moveButtons)
            {
                if ((button.gameObject.name == "SkillButton" && Player.skillUsed) ||
                    (button.gameObject.name == "ItemButton" && Player.itemUsed))
                {
                    button.interactable = false;
                }
                else
                {
                    button.interactable = true;
                }
            }
        }
    }

    public void AttackProcessing()
    {
        rollValue = Random.Range(1, 7);
        Debug.Log(rollValue);

        if (!turnCounter)
        {
            if (moveType == "Light Attack")
            {
                moveType = Player.lightAttack.name;
            }
            else if (moveType == "Heavy Attack")
            {
                moveType = Player.heavyAttack.name;
            }
            else if (moveType == "Skill")
            {
                moveType = Player.skill.name;
            }

            if (rollValue > (6 - resultantAccuracy))
            {
                if (moveType == Player.lightAttack.name)
                {
                    Player.lightAttack.SecondaryEffect();
                }
                else if (moveType == Player.heavyAttack.name)
                {
                    Player.heavyAttack.SecondaryEffect();
                }
                else if (moveType == Player.skill.name)
                {
                    Player.skill.SecondaryEffect();
                }
                else if (moveType == "Item")
                {
                    Player.item.Effect();
                }

                PlayerHit();

                if (Opponent.currentHealth > 0 && moveType != "Item")
                {
                    AudioManager.audioSource.PlayOneShot(Opponent.hurtSound);
                    opponentObject.GetComponent<Animator>().SetTrigger("Hurt");
                }
                else if (moveType != "Item" && !Opponent.isParalyzed)
                {
                    AudioManager.audioSource.PlayOneShot(Opponent.dieSound);
                    playerObject.GetComponent<Animator>().SetTrigger("Victory");
                    opponentObject.GetComponent<Animator>().SetTrigger("Die");
                    AudioManager.audioSource.PlayOneShot(winSound);
                }
            }
            else
            {
                PlayerMiss();
            }
        }
        else
        {
            if (moveType == "Light Attack")
            {
                moveType = Opponent.lightAttack.name;
            }
            else if (moveType == "Heavy Attack")
            {
                moveType = Opponent.heavyAttack.name;
            }
            else if (moveType == "Skill")
            {
                moveType = Opponent.skill.name;
            }

            if (rollValue > (6 - resultantAccuracy))
            {
                if (moveType == Opponent.lightAttack.name)
                {
                    Opponent.lightAttack.SecondaryEffect();
                }
                else if (moveType == Opponent.heavyAttack.name)
                {
                    Opponent.heavyAttack.SecondaryEffect();
                }
                else if (moveType == Opponent.skill.name)
                {
                    Opponent.skill.SecondaryEffect();
                }
                else if (moveType == "Item")
                {
                    Opponent.item.Effect();
                }

                OpponentHit();

                if (Player.currentHealth > 0 && moveType != "Item")
                {
                    AudioManager.audioSource.PlayOneShot(Player.hurtSound);
                    playerObject.GetComponent<Animator>().SetTrigger("Hurt");
                }
                else if (moveType != "Item" && !Player.isParalyzed)
                {
                    AudioManager.audioSource.PlayOneShot(Player.dieSound);
                    opponentObject.GetComponent<Animator>().SetTrigger("Victory");
                    playerObject.GetComponent<Animator>().SetTrigger("Die");
                    AudioManager.audioSource.PlayOneShot(loseSound, 1.00f);
                }
            }
            else
            {
                OpponentMiss();
            }
        }
    }

    public void OnReturnButonClick()
    {
        AudioManager.audioSource.PlayOneShot(clickSound);
        SceneManager.LoadScene("GameMenu");
    }

    public void Victory()
    {
        AudioManager.audioSource.Stop();
        AudioManager.audioSource.PlayOneShot(victory);
        panel.SetActive(true);
        panel.transform.Find("ReturnButton").gameObject.SetActive(true);
        panel.GetComponentInChildren<Text>().color = Color.yellow;
        panel.transform.Find("Text").GetComponent<Text>().text = "Victory";
        panel.transform.Find("NumberText").GetComponent<Text>().text = "";
    }

    public void Defeat()
    {
        AudioManager.audioSource.Stop();
        AudioManager.audioSource.PlayOneShot(defeat);
        panel.SetActive(true);
        panel.transform.Find("ReturnButton").gameObject.SetActive(true);
        panel.GetComponentInChildren<Text>().color = Color.red;
        panel.transform.Find("Text").GetComponent<Text>().text = "Defeat";
        panel.transform.Find("NumberText").GetComponent<Text>().text = "";
    }

    private IEnumerator FightIntro()
    {
        playerObject.transform.position -= new Vector3(0.00f, 0.00f, 3.75f);
        playerObject.transform.rotation = new Quaternion(0.00f, 0.00f, 0.00f, 0.00f);
        playerObject.GetComponent<Animator>().Play("Run");

        opponentObject.transform.position -= new Vector3(0.00f, 0.00f, 3.75f);
        opponentObject.transform.rotation = new Quaternion(0.00f, 0.00f, 0.00f, 0.00f);
        opponentObject.GetComponent<Animator>().Play("Run");

        panel.transform.Find("Text").GetComponent<Text>().text = "Round " + roundNumber;
        panel.transform.Find("ReturnButton").gameObject.SetActive(false);

        AudioManager.audioSource.PlayOneShot(clockTick);
        panel.transform.Find("NumberText").GetComponent<Text>().text = "3";

        yield return new WaitForSeconds(1.00f);

        AudioManager.audioSource.PlayOneShot(clockTick);
        panel.transform.Find("NumberText").GetComponent<Text>().text = "2";

        yield return new WaitForSeconds(1.00f);

        AudioManager.audioSource.PlayOneShot(clockTick);
        panel.transform.Find("NumberText").GetComponent<Text>().text = "1";

        yield return new WaitForSeconds(1.00f);

        AudioManager.audioSource.PlayOneShot(fightSound);
        panel.transform.Find("NumberText").GetComponent<Text>().text = "Fight!";

        yield return new WaitForSeconds(1.00f);

        panel.SetActive(false);

        if (Player.currentSpeed >= Opponent.currentSpeed)
        {
            turnCounter = false;

            foreach (Button button in moveButtons)
            {
                button.interactable = true;
            }
        }
        else
        {
            turnCounter = true;
            OpponentAttackCommence();

            foreach (Button button in moveButtons)
            {
                button.interactable = false;
            }
        }

        playerObject.transform.localRotation = Quaternion.Euler(0.00f, 270.00f, 0.00f);
        opponentObject.transform.localRotation = Quaternion.Euler(0.00f, 90.00f, 0.00f);
    }
}
