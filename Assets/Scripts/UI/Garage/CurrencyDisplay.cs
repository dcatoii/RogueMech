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
        Text.text = "Funds:     $" + String.Format("{0:n0}", PlayerData.Currency);
    }
}
