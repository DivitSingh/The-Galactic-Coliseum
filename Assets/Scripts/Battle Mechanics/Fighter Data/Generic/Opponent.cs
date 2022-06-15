using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opponent : MonoBehaviour {
    public static string fighterName;

    public static int baseHealth;
    public static int currentHealth;
    public static int baseAttack;
    public static int currentAttack;
    public static string accuracy;
    public static int baseSpeed;
    public static int currentSpeed;

    public static Attack lightAttack;
    public static Attack heavyAttack;
    public static Skill skill;
    public static List<Skill> activeSkillAfflictions;
    public static Trait trait;
    public static Item item;
    public static List<Item> activeItemAfflictions;

    public static bool isParalyzed;
    public static bool isReinforced;
    public static bool isFocused;
    public static bool skillUsed;
    public static bool itemUsed;

    public static Sprite factionIcon;
    public static AudioClip tauntSound;
    public static AudioClip hurtSound;
    public static AudioClip dieSound;

    private void Start()
    {
        activeSkillAfflictions = new List<Skill>();
        activeItemAfflictions = new List<Item>();
    }

    public static void EnableStats()
    {
        if (fighterName == "Vanguardian Dolores")
        {
            baseHealth = 150;
            currentHealth = baseHealth;
            baseAttack = 20;
            currentAttack = baseAttack;
            accuracy = "Medium";
            baseSpeed = 5;
            currentSpeed = baseSpeed;
            lightAttack = new Hit();
            heavyAttack = new Punt();
            skill = new PoweringStrike();
            trait = new Valiant();
            factionIcon = Resources.Load<Sprite>("Images/Faction Icons/Galactic Patrol");
            tauntSound = (AudioClip)Resources.Load("Audio/Sounds/Fighters/Vanguardian Dolores/Taunt");
            hurtSound = (AudioClip)Resources.Load("Audio/Sounds/Fighters/Vanguardian Dolores/Hurt");
            dieSound = (AudioClip)Resources.Load("Audio/Sounds/Fighters/Vanguardian Dolores/Die");
        }
        else if (fighterName == "Sentinel Dreyar")
        {
            baseHealth = 145;
            currentHealth = baseHealth;
            baseAttack = 10;
            currentAttack = baseAttack;
            accuracy = "High";
            baseSpeed = 10;
            currentSpeed = baseSpeed;
            lightAttack = new Punch();
            heavyAttack = new Jab();
            skill = new SwingingBlow();
            trait = new Persistence();
            factionIcon = Resources.Load<Sprite>("Images/Faction Icons/Galactic Patrol");
            tauntSound = (AudioClip)Resources.Load("Audio/Sounds/Fighters/Sentinel Dreyar/Taunt");
            hurtSound = (AudioClip)Resources.Load("Audio/Sounds/Fighters/Sentinel Dreyar/Hurt");
            dieSound = (AudioClip)Resources.Load("Audio/Sounds/Fighters/Sentinel Dreyar/Die");
        }
        else if (fighterName == "Ulgrath")
        {
            baseHealth = 130;
            currentHealth = baseHealth;
            baseAttack = 25;
            currentAttack = baseAttack;
            accuracy = "Low";
            baseSpeed = 5;
            currentSpeed = baseSpeed;
            lightAttack = new Smash();
            heavyAttack = new Gash();
            skill = new ViciousSlash();
            trait = new ThickHide();
            factionIcon = Resources.Load<Sprite>("Images/Faction Icons/Space Buccaneers");
            tauntSound = (AudioClip)Resources.Load("Audio/Sounds/Fighters/Ulgrath/Taunt");
            hurtSound = (AudioClip)Resources.Load("Audio/Sounds/Fighters/Ulgrath/Hurt");
            dieSound = (AudioClip)Resources.Load("Audio/Sounds/Fighters/Ulgrath/Die");
        }
        else if (fighterName == "Corsair Garla")
        {
            baseHealth = 125;
            currentHealth = baseHealth;
            baseAttack = 10;
            currentAttack = baseAttack;
            accuracy = "High";
            baseSpeed = 20;
            currentSpeed = baseSpeed;
            lightAttack = new ReconstructiveSlash();
            heavyAttack = new Knee();
            skill = new Reconstruct();
            trait = new Cybernetics();
            factionIcon = Resources.Load<Sprite>("Images/Faction Icons/Space Buccaneers");
            tauntSound = (AudioClip)Resources.Load("Audio/Sounds/Fighters/Corsair Garla/Taunt");
            hurtSound = (AudioClip)Resources.Load("Audio/Sounds/Fighters/Corsair Garla/Hurt");
            dieSound = (AudioClip)Resources.Load("Audio/Sounds/Fighters/Corsair Garla/Die");
        }
        else if (fighterName == "The Bot")
        {
            baseHealth = 140;
            currentHealth = baseHealth;
            baseAttack = 15;
            currentAttack = baseAttack;
            accuracy = "Medium";
            baseSpeed = 15;
            currentSpeed = baseSpeed;
            lightAttack = new Clout();
            heavyAttack = new Knock();
            skill = new ElectrifyingHeadbutt();
            trait = new HigherIntelligence();
            factionIcon = Resources.Load<Sprite>("Images/Faction Icons/Unknown");
            tauntSound = (AudioClip)Resources.Load("Audio/Sounds/Fighters/The Bot/Taunt");
            hurtSound = (AudioClip)Resources.Load("Audio/Sounds/Fighters/The Bot/Hurt");
            dieSound = (AudioClip)Resources.Load("Audio/Sounds/Fighters/The Bot/Die");
        }

        isParalyzed = false;
        isReinforced = false;
        isFocused = false;
        skillUsed = false;
        itemUsed = false;
    }
}
