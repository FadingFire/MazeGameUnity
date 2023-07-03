using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Text.RegularExpressions;

public class uiScript : MonoBehaviour
{
    public GameObject Stijl;
    public GameObject materiaal;
    public GameObject Lengte;
    public GameObject rush;
    public GameObject totaal;
    private int Totaal;
    public TMP_Text totaalText;
    public TMP_Text lengteText;

    private const int GrofMultiplier = 1;
    private const int MediumMultiplier = 3;
    private const int FijnMultiplier = 5;
    private const int PLA_Multiplier = 15 / 10;
    private const int KoperMultiplier = 3;
    private const int AluminiumMultiplier = 5;
    private const int RushMultiplier = 15 / 10;

    public void DisableLengte()
    {
        Lengte.SetActive(false);
        Stijl.SetActive(true);
    }

    public void DisableStijl()
    {
        Stijl.SetActive(false);
        materiaal.SetActive(true);
    }

    public void Disablemateriaal()
    {
        materiaal.SetActive(false);
        rush.SetActive(true);
    }

    public void Disablerush()
    {
        rush.SetActive(false);
        totaal.SetActive(true);
    }

    public void Retry()
    {
        Lengte.SetActive(true);
        totaal.SetActive(false);
    }

    public void CalculateTotaal(int multiplier)
    {
        if (!string.IsNullOrEmpty(lengteText.text))
        {
            string input = lengteText.text.Trim();
            string numericValue = Regex.Match(input, @"\d+").Value;

            if (int.TryParse(numericValue, out int length))
            {
                Totaal = length * multiplier;
            }
        }
    }

    public void grof()
    {
        CalculateTotaal(GrofMultiplier);
    }

    public void medium()
    {
        CalculateTotaal(MediumMultiplier);
    }

    public void fijn()
    {
        CalculateTotaal(FijnMultiplier);
    }

    public void ABS()
    {
        Totaal *= 1;
    }

    public void PLA()
    {
        Totaal *= PLA_Multiplier;
    }

    public void koper()
    {
        Totaal *= KoperMultiplier;
    }

    public void aluminium()
    {
        Totaal *= AluminiumMultiplier;
    }

    public void Rush()
    {
        Totaal *= RushMultiplier;
    }

    private string totaalString;

    public void rekenen()
    {
        totaalString = Totaal.ToString();
        totaalText.text = totaalString;
    }
}
