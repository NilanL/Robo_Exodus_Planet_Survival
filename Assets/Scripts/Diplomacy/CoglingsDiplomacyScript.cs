using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoglingsDiplomacyScript : MonoBehaviour
{
    public int reputation = 0;
    public bool isDestroyed;
    private bool isMaxRep = false;
    private int complimentCount = 0;
    private int tradeCount = 0;
    private int donateCount = 0;
    private GameManager gameManager;
    private const int WIN_REPUTATION = 600;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = gameObject.GetComponent<GameManager>();
    }

    public int AddReputation(DiplomacyType type, int rep)
    {
        int increment = 0;

        if (!isMaxRep)
        {
            switch (type)
            {
                case DiplomacyType.Compliment:
                    complimentCount++;
                    increment = (complimentCount >= 4 ? 0 : rep);
                    break;
                case DiplomacyType.Trade:
                    tradeCount++;
                    increment = (tradeCount >= rep ? 1 : rep / tradeCount);
                    break;
                case DiplomacyType.Donate:
                    donateCount++;
                    increment = (donateCount >= rep ? 1 : rep / donateCount);
                    break;
            }

            reputation += increment;

            if (reputation >= WIN_REPUTATION)
            {
                reputation = WIN_REPUTATION;
                gameManager.Ironite += 5000;
                gameManager.BloodStone += 2500;
                isMaxRep = true;
                ShowDiplomacyWinMessage();
            }
        }

        return increment;
    }

    public int SubtractReputation(int rep)
    {
        reputation -= rep;
        return rep;
    }

    public void ShowDiplomacyWinMessage()
    {
        var window = GameObject.Find("UI").transform.Find("Cogling Diplomacy Gift").gameObject;
        window.SetActive(true);
    }
}
