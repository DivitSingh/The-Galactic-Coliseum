using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {
    public AudioClip clickSound;

    public List<GameObject> allFighterSlots;

    private GameObject mainMenuCanvas;
    private GameObject instructionsMenuCanvas;

    private GameObject fighterMenu;
    private GameObject fighterPanel;
    private GameObject itemMenu;
    private GameObject arenaMenu;

    private GameObject continueButton;
    public GameObject returnButton;
    private GameObject itemButton;
    private GameObject arenaButton;
    private GameObject fightButton;

    private List<Item> allItems;

	// Use this for initialization
	void Start () {
        if (SceneManager.GetActiveScene().name == "Intro")
        {
            StartCoroutine(Begin());
        }
        else if (SceneManager.GetActiveScene().name == "Start")
        {
            mainMenuCanvas = GameObject.Find("MainMenu");
            instructionsMenuCanvas = GameObject.Find("InstructionsMenu");
            instructionsMenuCanvas.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().name == "GameMenu")
        {
            fighterMenu = GameObject.Find("FighterMenu");
            fighterPanel = GameObject.Find("FighterSlots");
            itemMenu = GameObject.Find("ItemMenu");
            arenaMenu = GameObject.Find("ArenaMenu");
            fighterPanel.GetComponent<Image>().color = Color.green;
            continueButton = GameObject.Find("ContinueButton");
            itemButton = GameObject.Find("ItemButton");
            arenaButton = GameObject.Find("ArenaButton");
            fightButton = GameObject.Find("FightButton");

            itemMenu.SetActive(false);
            arenaMenu.SetActive(false);
            continueButton.GetComponent<Button>().interactable = false;
            returnButton.SetActive(false);
            itemButton.SetActive(false);

            allItems = new List<Item>();
            allItems.Add(new HealingOil());
            allItems.Add(new Spacedust());
            allItems.Add(new RejuvenatingShard());
            allItems.Add(new PoweringFragment());
            allItems.Add(new SalvingDroplets());
            allItems.Add(new MeteorSand());
        }
    }

    public void OnReturnButtonClick()
    {
        if (SceneManager.GetActiveScene().name == "Start")
        {
            AudioManager.audioSource.PlayOneShot(clickSound);
            mainMenuCanvas.SetActive(true);
            instructionsMenuCanvas.SetActive(false);
        }
        else if (SceneManager.GetActiveScene().name == "GameMenu")
        {
            AudioManager.audioSource.PlayOneShot(clickSound);

            fighterMenu.SetActive(true);
            fighterPanel.GetComponent<Image>().color = Color.green;
            itemMenu.SetActive(false);
            arenaMenu.SetActive(false);

            continueButton.SetActive(true);
            continueButton.GetComponent<Button>().interactable = false;
            returnButton.SetActive(false);
            itemButton.SetActive(false);

            DescriptionController.descriptionController.nameText.text = "";
            DescriptionController.descriptionController.factionIcon.gameObject.SetActive(false);
            DescriptionController.descriptionController.descriptionText.text = "";
            DescriptionController.descriptionController.statText.text = "";
            DescriptionController.descriptionController.skillText.text = "";
            DescriptionController.descriptionController.traitText.text = "";
            DescriptionController.descriptionController.itemNameText.text = "";
            DescriptionController.descriptionController.itemDescriptionText.text = "";
            DescriptionController.descriptionController.arenaNameText.text = "";
            DescriptionController.descriptionController.arenaDescriptionText.text = "";

            foreach (Transform child in DescriptionController.descriptionController.models.transform)
            {
                child.gameObject.SetActive(false);
            }

            foreach (Transform child in DescriptionController.descriptionController.arenas.transform)
            {
                child.gameObject.SetActive(false);
            }
        }
    }

    #region Intro

    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    private IEnumerator Begin()
    {
        yield return new WaitForSeconds(2.00f);

        SceneManager.LoadScene("Start");
    }

    #endregion

    #region Main Menu
    public void OnStartButtonClick()
    {
        AudioManager.audioSource.PlayOneShot(clickSound);
        SceneManager.LoadScene("GameMenu");
    }

	public void OnInstructionsButtonClick()
    {
        AudioManager.audioSource.PlayOneShot(clickSound);
        mainMenuCanvas.SetActive(false);
        instructionsMenuCanvas.SetActive(true);
    }
    #endregion

    #region Character Menu
    public void OnCharacterButtonClick()
    {
        AudioManager.audioSource.PlayOneShot(clickSound);

        if (fighterPanel.GetComponent<Image>().color == Color.green)
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite.name == "Random")
            {
                ChooseRandomFighter("Player");
            }
            else
            {
                Player.fighterName = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite.name;
            }

            Player.EnableStats();
            DescriptionController.descriptionController.SetFighterDescription();
            AudioManager.audioSource.PlayOneShot(Player.tauntSound);
            continueButton.GetComponent<Button>().interactable = true;
        }
        else
        {
            if (EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite.name == "Random")
            {
                ChooseRandomFighter("Opponent");
            }
            else
            {
                Opponent.fighterName = EventSystem.current.currentSelectedGameObject.GetComponent<Image>().sprite.name;
            }

            Opponent.EnableStats();
            DescriptionController.descriptionController.SetFighterDescription();
            AudioManager.audioSource.PlayOneShot(Opponent.tauntSound);
            itemButton.GetComponent<Button>().interactable = true;
        }

        StartCoroutine(ClickDelay(4.25f));
    }

    public void OnContinueButtonClick()
    {
        AudioManager.audioSource.PlayOneShot(clickSound);
        fighterPanel.GetComponent<Image>().color = Color.red;
        continueButton.SetActive(false);
        returnButton.SetActive(true);
        itemButton.SetActive(true);
        returnButton.GetComponent<Button>().interactable = true;
        itemButton.GetComponent<Button>().interactable = false;
        DescriptionController.descriptionController.nameText.text = "";
        DescriptionController.descriptionController.factionIcon.gameObject.SetActive(false);
        DescriptionController.descriptionController.descriptionText.text = "";
        DescriptionController.descriptionController.statText.text = "";
        DescriptionController.descriptionController.skillText.text = "";
        DescriptionController.descriptionController.traitText.text = "";

        foreach (Transform child in DescriptionController.descriptionController.models.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    public void OnItemButtonClick()
    {
        AudioManager.audioSource.PlayOneShot(clickSound);
        fighterMenu.SetActive(false);
        itemMenu.SetActive(true);
        arenaButton.GetComponent<Button>().interactable = false;
    }
    #endregion

    #region Item Menu
    public void OnItemClick()
    {
        AudioManager.audioSource.PlayOneShot(clickSound);
        Player.item = allItems[int.Parse(EventSystem.current.currentSelectedGameObject.name)];
        DescriptionController.descriptionController.SetItemDescription();
        GameObject.Find("Heraklios").GetComponent<Animator>().Play("Respond");
        arenaButton.GetComponent<Button>().interactable = true;

        StartCoroutine(ClickDelay(1.50f));
    }

    public void OnArenaButtonClick()
    {
        AudioManager.audioSource.PlayOneShot(clickSound);
        itemMenu.SetActive(false);
        arenaMenu.SetActive(true);
        fightButton.GetComponent<Button>().interactable = false;

        foreach (Transform child in DescriptionController.descriptionController.arenas.transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    #endregion

    #region Arena Menu

    public void OnArenaClick()
    {
        AudioManager.audioSource.PlayOneShot(clickSound);
        BattleManager.arenaName = EventSystem.current.currentSelectedGameObject.name;
        DescriptionController.descriptionController.SetArenaDescription();
        fightButton.GetComponent<Button>().interactable = true;
    }

    public void OnFightButtonClick()
    {
        AudioManager.audioSource.PlayOneShot(clickSound);
        SceneManager.LoadScene("Arena");
        BattleManager.roundNumber = 1;
        BattleManager.battlesWon = 0;
    }

    #endregion

    private void ChooseRandomFighter(string target)
    {
        int numUnlockedFighters = 0;

        foreach(GameObject fighterSlot in allFighterSlots)
        {
            if (fighterSlot.activeSelf)
            {
                numUnlockedFighters++;
            }
        }

        int fighterSelector = Random.Range(0, numUnlockedFighters);

        if (target == "Player")
        {
            Player.fighterName = allFighterSlots[fighterSelector].GetComponent<Image>().sprite.name;
        }
        else if (target == "Opponent")
        {
            Opponent.fighterName = allFighterSlots[fighterSelector].GetComponent<Image>().sprite.name;
        }
    }

    private IEnumerator ClickDelay(float delayTime)
    {
        List<Button> buttons = new List<Button>();
        GameObject[] btn = GameObject.FindGameObjectsWithTag("Interact");

        for (int i = 0; i < btn.Length; i++)
        {
            buttons.Add(btn[i].GetComponent<Button>());
        }

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].enabled = false;
        }

        continueButton.GetComponent<Button>().interactable = false;
        returnButton.GetComponent<Button>().interactable = false;
        itemButton.GetComponent<Button>().interactable = false;
        arenaButton.GetComponent<Button>().interactable = false;
        fightButton.GetComponent<Button>().interactable = false;

        yield return new WaitForSeconds(delayTime);

        for (int i = 0; i < buttons.Count; i++)
        {
            buttons[i].enabled = true;
        }

        continueButton.GetComponent<Button>().interactable = true;
        returnButton.GetComponent<Button>().interactable = true;
        itemButton.GetComponent<Button>().interactable = true;
        arenaButton.GetComponent<Button>().interactable = true;
        fightButton.GetComponent<Button>().interactable = true;
    }
}
