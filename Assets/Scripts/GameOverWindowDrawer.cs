using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class GameOverWindowDrawer : MonoBehaviour
{
    [SerializeField] private TMP_Text wonText;

    public void ReDraw(bool isWon, float mark)
    {
        var textForWon = isWon ? "Вы прошли уровень" : "Вы не прошли уровень";
        wonText.text = textForWon;
    }
}
