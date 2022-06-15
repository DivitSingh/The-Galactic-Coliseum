using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DescriptionController : MonoBehaviour {
    public static DescriptionController descriptionController;

    public Text nameText;
    public Image factionIcon;
    public Text descriptionText;
    public Text statText;
    public Text skillText;
    public Text traitText;
    public GameObject models;

    public Text itemNameText;
    public Text itemDescriptionText;

    public Text arenaNameText;
    public Text arenaDescriptionText;
    public GameObject arenas;

    private void Awake()
    {
        if (descriptionController == null)
        {
            descriptionController = this;
        }
        else
        {
            Destroy(this);
        }

        foreach (Transform child in models.transform)
        {
                child.gameObject.SetActive(false);
        }

        foreach (Transform child in arenas.transform)
        {
            child.gameObject.SetActive(false);
        }

        factionIcon.gameObject.SetActive(false);
    }

    public void SetFighterDescription()
    {
        foreach (Transform child in models.transform)
        {
            child.gameObject.SetActive(false);
        }

        factionIcon.gameObject.SetActive(true);

        if (GameObject.Find("FighterSlots").GetComponent<Image>().color == Color.green)
        {
            nameText.text = Player.fighterName;
            factionIcon.sprite = Player.factionIcon;

            if (Player.fighterName == "Vanguardian Dolores")
            {
                descriptionText.text = "Guardswoman of the Galactic Embassy, Dolores is a bulky fighter, capable of dealing immense blows despite being at an advanced age. Embued with abnormal strength upon being exposed to a radioactive accident at the Galactic Laboratory, she is forced to wear a hazmat suit to prevent others from being affected by her radiation.";
            }
            else if (Player.fighterName == "Sentinel Dreyar")
            {
                descriptionText.text = "Once regarded a fine warrior, his days of glory are long gone. Regardless, as leader of the Galactic Patrol, Sentinel Dreyar can land well-aimed blows upon his foes.";
            }
            else if (Player.fighterName == "Ulgrath")
            {
                descriptionText.text = "An abomination created in the laboratory of the Galactic Embassy. By an ironic twist of fate, the powers of the monstrosity have become a menace to the very organization responsible for its creation. Taking a liking to the war-mongering ways of the Space Buccaneers, he has joined them in their rapacious endeavours.";
            }
            else if (Player.fighterName == "Corsair Garla")
            {
                descriptionText.text = "Scourge of the galaxy, Corsair Garla is the bane of the peace-making endeavors of the Galactic Embassy. After losing motor functions in half of her body due to a brutal fall during a skirmish against the Galactic Patrol, she made use of mechanical parts to continue her raids. Utilizing the electronic constructs to recuperate in battle, the pirate lasts long in a duel.";
            }
            else if (Player.fighterName == "The Bot")
            {
                descriptionText.text = "Artificial intelligence at its finest. As the wisest of warriors suggest, often it is most fitting to go back to the basics!";
            }


            statText.text = "\nBase Health - " + Player.baseHealth +
            "\nAttack - " + Player.baseAttack +
            "\nAccuracy - " + Player.accuracy +
            "\nSpeed - " + Player.baseSpeed;

            skillText.text = "Skill - " + Player.skill.name;
            traitText.text = "Trait - " + Player.trait.name;

            foreach (Transform child in models.transform)
            {
                if (child.gameObject.name == Player.fighterName)
                {
                    child.gameObject.SetActive(true);
                    child.gameObject.GetComponent<Animator>().Play("Run");
                }
            }
        }
        else
        {
            nameText.text = Opponent.fighterName;
            factionIcon.sprite = Opponent.factionIcon;

            if (Opponent.fighterName == "Vanguardian Dolores")
            {
                descriptionText.text = "Guardswoman of the Galactic Embassy, Dolores is a bulky fighter, capable of dealing immense blows despite being at an advanced age. Embued with abnormal strength upon being exposed to a radioactive accident at the Galactic Laboratory, she is forced to wear a hazmat suit to prevent others from being affected by her radiation.";
            }
            else if (Opponent.fighterName == "Sentinel Dreyar")
            {
                descriptionText.text = "Once regarded a fine warrior, his days of glory are long gone. Regardless, as leader of the Galactic Patrol, Sentinel Dreyar can land well-aimed blows upon his foes.";
            }
            else if (Opponent.fighterName == "Ulgrath")
            {
                descriptionText.text = "An abomination created in the laboratory of the Galactic Embassy. By an ironic twist of fate, the powers of the monstrosity have become a menace to the very organization responsible for its creation. Taking a liking to the war-mongering ways of the Space Buccaneers, he has joined them in their rapacious endeavours.";
            }
            else if (Opponent.fighterName == "Corsair Garla")
            {
                descriptionText.text = "Scourge of the galaxy, Corsair Garla is the bane of the peace-making endeavors of the Galactic Embassy. After losing motor functions in half of her body due to a brutal fall during a skirmish against the Galactic Patrol, she made use of mechanical parts to continue her raids. Utilizing the electronic constructs to recuperate in battle, the pirate lasts long in a duel.";
            }
            else if (Opponent.fighterName == "The Bot")
            {
                descriptionText.text = "Artificial intelligence at its finest. As the wisest of warriors suggest, often it is most fitting to go back to the basics!";
            }

            statText.text = "\nBase Health - " + Opponent.baseHealth +
            "\nAttack - " + Opponent.baseAttack +
            "\nAccuracy - " + Opponent.accuracy +
            "\nSpeed - " + Opponent.baseSpeed;

            skillText.text = "Skill - " + Opponent.skill.name;
            traitText.text = "Trait - " + Opponent.trait.name;

            foreach (Transform child in models.transform)
            {
                if (child.gameObject.name == Opponent.fighterName)
                {
                    child.gameObject.SetActive(true);
                    child.gameObject.GetComponent<Animator>().Play("Run");
                }
            }
        }
    }

    public void SetItemDescription()
    {
        itemNameText.text = Player.item.name;
        itemDescriptionText.text = Player.item.description;
    }

    public void SetArenaDescription()
    {
        foreach (Transform child in arenas.transform)
        {
            child.gameObject.SetActive(false);
        }

        foreach (Transform child in arenas.transform)
        {
            if (child.gameObject.name == BattleManager.arenaName)
            {
                child.gameObject.SetActive(true);
            }
        }

        arenaNameText.text = BattleManager.arenaName;

        if (BattleManager.arenaName == "The Galactic Embassy")
        {
            arenaDescriptionText.text = "Home to Galactic Patrol, the Embassy is the sole place in the galaxy where any species is welcomed with open arms save for trouble-making pirates and brutal mutants!";
        }
        else if (BattleManager.arenaName == "The Laboratory")
        {
            arenaDescriptionText.text = "An abandoned building that once housed horrific abominations. A monument to the failings of the scientists of the Embassy, it is seldom visited even by the members of the Galactic Patrol.";
        }
        else if (BattleManager.arenaName == "The Asteroid")
        {
            arenaDescriptionText.text = "A massive asteroid part of an orbiting chain. Barren and mountainous, this secluded spot is considered the most apt location by space-farers for one to settle past scores.";
        }
    }
}
