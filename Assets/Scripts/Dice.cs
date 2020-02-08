using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Dice : MonoBehaviour
{

    public Sprite[] diceImages;
    public bool allowRoll;
    public int diceNumber = 0;

    public Action DiceRolled;

    SpriteRenderer diceRenderer;

    void Start()
    {
        diceRenderer = GetComponent<SpriteRenderer>();
        allowRoll = true;
    }

    private void OnMouseDown()
    {
        if(allowRoll)
        StartCoroutine("RollDice");
    }

    IEnumerator RollDice()
    {
        int dummyNumber = 1;
        allowRoll = false;

        for (int i = 1; i <= 10; i++)
        {
            diceRenderer.sprite = diceImages[dummyNumber];
            dummyNumber++;
            if (dummyNumber > 5) dummyNumber = 1;
            yield return new WaitForSeconds(0.1f);
        }

        diceNumber = (int)Mathf.Floor(UnityEngine.Random.Range(1, 7));

        DiceRolled();

        diceRenderer.sprite = diceImages[diceNumber];
        yield return null;
    }
}
