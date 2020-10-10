using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrencyDisplay : MonoBehaviour {

    TMPro.TMP_Text Text;

    private void Start()
    {
        Text = GetComponent<TMPro.TMP_Text>();
    }

    private void FixedUpdate()
    {
        int numSpaces = 5;
        string CashString = "$" + String.Format("{0:n0}", PlayerData.Currency);
        numSpaces -= (CashString.Length - 7);
        string fundsText = "Funds:";
        for (int i = 0; i < numSpaces; i++)
            fundsText += " ";

        fundsText += CashString;
        Text.text = fundsText;
    }
}
