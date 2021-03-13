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
    [SerializeField] int attack = 5;
    [SerializeField] int health = 5;
    [SerializeField] int mana = 5;
    [SerializeField] int[] values = new int[3];
    void Start()
    {
        artImage.sprite = artArray[Random.Range(0, artArray.Length)];
        values[0] = attack;
        values[1] = health;
        values[2] = mana;
        attackText.text = values[0].ToString();
        healthText.text = values[1].ToString();
        manaText.text = values[2].ToString();
    }

    public void ChangeToRandomValue()
    {
        int randomValue = Random.Range(minValue, maxValue);
        int randomParameter = Random.Range(0, 2);
        values[randomParameter] = randomValue;

        if (values[1] < 1)
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        attackText.text = values[0].ToString();
        healthText.text = values[1].ToString();
        manaText.text = values[2].ToString();
    }
}
