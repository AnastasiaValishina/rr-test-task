using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

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

    void Start()
    {
        artImage.sprite = artArray[Random.Range(0, artArray.Length)];
        attackText.text = attack.ToString();
        healthText.text = health.ToString();
        manaText.text = mana.ToString();
    }

    public void UpdateAttack()
    {
        attackText.text = attack.ToString();
        AnimateText(attackText);
    }

    public void UpdateHealth()
    {
        healthText.text = health.ToString();
        AnimateText(healthText);
    }

    public void UpdateMana()
    {
        manaText.text = mana.ToString();
        AnimateText(manaText);
    }

    private void AnimateText(Text text)
    {
        Sequence mySequence = DOTween.Sequence();
        mySequence.Append(text.transform.DOScale(2f, 0.5f))
            .Append(text.transform.DOScale(1f, 0.5f));
    }
}
