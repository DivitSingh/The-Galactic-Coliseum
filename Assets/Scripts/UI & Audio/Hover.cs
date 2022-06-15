using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Hover : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler{
    public GameObject hoverText;

    private void Awake()
    {
        hoverText.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        hoverText.SetActive(true);
        hoverText.transform.position = gameObject.transform.position + new Vector3(125.00f, 125.00f, 0.00f);

        if (gameObject.name == "LightAttackButton")
        {
            int damageValue;
            
            if ((Player.currentAttack + Player.lightAttack.damageMod) < 5)
            {
                damageValue = 5;    
            }
            else
            {
                damageValue = Player.currentAttack + Player.lightAttack.damageMod;
            }

            hoverText.GetComponentInChildren<Text>().text = Player.lightAttack.description +
                                                            "\n\nAccuracy - " + Player.lightAttack.accuracy + " / 6" +
                                                            "\nDamage - " + damageValue.ToString();
        }
        else if (gameObject.name == "HeavyAttackButton")
        {
            hoverText.GetComponentInChildren<Text>().text = Player.heavyAttack.description +
                                                            "\n\nAccuracy - " + Player.heavyAttack.accuracy + " / 6" +
                                                            "\nDamage - " + (Player.currentAttack + Player.heavyAttack.damageMod).ToString();
        }
        else if (gameObject.name == "SkillButton")
        {
            int damageValue;

            if ((Player.currentAttack + Player.skill.damageMod) < 5)
            {
                damageValue = 5;
            }
            else
            {
                damageValue = Player.currentAttack + Player.skill.damageMod;
            }

            hoverText.GetComponentInChildren<Text>().text = Player.skill.description +
                                                            "\n\nAccuracy - " + Player.skill.accuracy + " / 6" +
                                                            "\nDamage - " + damageValue.ToString();
        }
        else if (gameObject.name == "TraitButton")
        {
            hoverText.GetComponentInChildren<Text>().text = Player.trait.name + " - " + Player.trait.description;
        }
        else if (gameObject.name == "ItemButton")
        {
            hoverText.GetComponentInChildren<Text>().text = Player.item.name + " - " + Player.item.description;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        hoverText.SetActive(false);
    }
}
