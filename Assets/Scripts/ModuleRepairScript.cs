using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModuleRepairScript : MonoBehaviour
{
    GameObject gm;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
    }

    public void RepairReactor()
    {
        if (gameManager.Ironite >= 5000 && gameManager.Aurarium >= 2500)
        {
            gameManager.Ironite -= 5000;
            gameManager.Aurarium -= 2500;
            gm.GetComponent<GameManager>().SetIsReactorRepaired();
        }
    }

    public void RepairComputer()
    {
        if (gameManager.Ironite >= 5000 && gameManager.Zorium >= 2500)
        {
            gameManager.Ironite -= 5000;
            gameManager.Zorium -= 2500;
            gm.GetComponent<GameManager>().SetIsComputerRepaired();
        }
    }

    public void RepairPlates()
    {
        if (gameManager.Ironite >= 5000 && gameManager.BloodStone >= 2500)
        {
            gameManager.Ironite -= 5000;
            gameManager.BloodStone -= 2500;
            gm.GetComponent<GameManager>().SetIsPlatesRepaired();
        }
    }
}
