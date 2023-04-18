using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DiplomacyType
{
    Trade,
    Donate,
    Compliment
}

public class DiplomacyUpdateScript : MonoBehaviour
{
    GameObject gm;
    private const int TRADE_INCREASE = 25;
    private const int DONATE_INCREASE = 75;
    private const int COMPLIMENT_INCREASE = 100;
    private const int INSULT_DECREASE = 100;

    private GameObject coglingMarker;
    private GameObject sleemasiMarker;
    private GameObject graxxianMarker;

    private GameObject UI;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager");
        UI = GameObject.Find("UI");
        coglingMarker = UI.transform.Find("Diplomacy Window/Cogling/Marker").gameObject;
        sleemasiMarker = UI.transform.Find("Diplomacy Window/Sleemasi/Marker").gameObject;
        graxxianMarker = UI.transform.Find("Diplomacy Window/Graxxian/Marker").gameObject;
    }

    private void adjustMarker(GameObject marker, int increment)
    {
        float screenWidth = Screen.width;

        if (increment > 0)
        {
            if (marker.transform.position.x > 0.84f * screenWidth)
            {
                marker.transform.position = new Vector3(0.84f * screenWidth, marker.transform.position.y);
            }
            else
            {
                marker.transform.position = new Vector3(marker.transform.position.x + (screenWidth * (increment / screenWidth)), marker.transform.position.y);

                if (marker.transform.position.x > 0.84f * screenWidth)
                {
                    marker.transform.position = new Vector3(0.84f * screenWidth, marker.transform.position.y);
                }
            }
        }
        else if (increment < 0)
        {
            if (marker.transform.position.x < 0.16f * screenWidth)
            {
                marker.transform.position = new Vector3(0.16f * screenWidth, marker.transform.position.y);
            }
            else
            {
                marker.transform.position = new Vector3(marker.transform.position.x + increment, marker.transform.position.y);

                if (marker.transform.position.x < 0.16f * screenWidth)
                {
                    marker.transform.position = new Vector3(0.16f * screenWidth, marker.transform.position.y);
                }
            }
        }
    }

    private int calculateTradeIncrease(float tradeAmount)
    {
        return TRADE_INCREASE + (int)(2 * tradeAmount / TRADE_INCREASE);
    }

    private int calculateDonateIncrease(float donateAmount)
    {
        return DONATE_INCREASE + (int)(5 * donateAmount / DONATE_INCREASE);
    }

    public void coglingTrade(float tradeAmount)
    {
        if (tradeAmount > 0)
        {
            var increment = gm.GetComponent<CoglingsDiplomacyScript>().AddReputation(DiplomacyType.Trade, calculateTradeIncrease(tradeAmount));
            adjustMarker(coglingMarker, increment);
        }
    }

    public void coglingDonate(float donateAmount)
    {
        if (donateAmount > 0)
        {
            var increment = gm.GetComponent<CoglingsDiplomacyScript>().AddReputation(DiplomacyType.Donate, calculateDonateIncrease(donateAmount));
            adjustMarker(coglingMarker, increment);
        }
    }

    public void coglingCompliment()
    {
        var increment = gm.GetComponent<CoglingsDiplomacyScript>().AddReputation(DiplomacyType.Compliment, COMPLIMENT_INCREASE);
        adjustMarker(coglingMarker, increment);
    }

    public void coglingInsult()
    {
        var increment = gm.GetComponent<CoglingsDiplomacyScript>().SubtractReputation(INSULT_DECREASE);
        adjustMarker(coglingMarker, -increment);
    }

    public void sleemasiTrade(float tradeAmount)
    {
        if (tradeAmount > 0)
        {
            var increment = gm.GetComponent<SleemasiDiplomacyScript>().AddReputation(DiplomacyType.Trade, calculateTradeIncrease(tradeAmount));
            adjustMarker(sleemasiMarker, increment);
        }
    }

    public void sleemasiDonate(float donateAmount)
    {
        if (donateAmount > 0)
        {
            var increment = gm.GetComponent<SleemasiDiplomacyScript>().AddReputation(DiplomacyType.Donate, calculateDonateIncrease(donateAmount));
            adjustMarker(sleemasiMarker, increment);
        }
    }

    public void sleemasiCompliment()
    {
        var increment = gm.GetComponent<SleemasiDiplomacyScript>().AddReputation(DiplomacyType.Compliment, COMPLIMENT_INCREASE);
        adjustMarker(sleemasiMarker, increment);
    }

    public void sleemasiInsult()
    {
        var increment = gm.GetComponent<SleemasiDiplomacyScript>().SubtractReputation(INSULT_DECREASE);
        adjustMarker(sleemasiMarker, -increment);
    }

    public void graxxianTrade(float tradeAmount)
    {
        if (tradeAmount > 0)
        {
            var increment = gm.GetComponent<GraxxianDiplomacyScript>().AddReputation(DiplomacyType.Trade, calculateTradeIncrease(tradeAmount));
            adjustMarker(graxxianMarker, increment);
        }
    }

    public void graxxianDonate(float donateAmount)
    {
        if (donateAmount > 0)
        {
            var increment = gm.GetComponent<GraxxianDiplomacyScript>().AddReputation(DiplomacyType.Donate, calculateDonateIncrease(donateAmount));
            adjustMarker(graxxianMarker, increment);
        }
    }

    public void graxxianCompliment()
    {
        var increment = gm.GetComponent<GraxxianDiplomacyScript>().AddReputation(DiplomacyType.Compliment, COMPLIMENT_INCREASE);
        adjustMarker(graxxianMarker, increment);
    }

    public void graxxianInsult()
    {
        var increment = gm.GetComponent<GraxxianDiplomacyScript>().SubtractReputation(INSULT_DECREASE);
        adjustMarker(graxxianMarker, -increment);
    }
}
