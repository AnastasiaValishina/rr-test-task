using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Card : MonoBehaviour
{
    [SerializeField] Image artImage;
    [SerializeField] Sprite[] artArray;
    [SerializeField] Text attackText;
    [SerializeField] Text healthText;
    [SerializeField] Text manaText;

    [SerializeField] int minValue = -2, maxValue = 9;
    public int attack = 5;
    public int health = 5;
    public int mana = 5;
    public GameObject target;

    void Start()
    {
        attackText.text = attack.ToString();
        healthText.text = health.ToString();
        manaText.text = mana.ToString();
    }

    private void Update()
    {
        attackText.text = attack.ToString();
        healthText.text = health.ToString();
        manaText.text = mana.ToString();
    }
}
