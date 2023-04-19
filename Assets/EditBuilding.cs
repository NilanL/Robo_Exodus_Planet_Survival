using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EditBuilding : MonoBehaviour
{
    private GameObject gm;
    private GameManager gameManager;
    private GameObject UI;

    public GameObject melee;
    public GameObject ranged;

    private void Start()
    {
        gm = GameObject.Find("GameManager");
        gameManager = gm.GetComponent<GameManager>();
        UI = GameObject.Find("UI");
    }

    public void RepairTurret()
    {
        var turret = gm.GetComponent<GameManager>().ActiveTurret;
        if (turret)
        {
            var turretHealth = turret.GetComponent<Unit_Health>();
            if (gameManager.Ironite >= 100 && turretHealth.currentHealth != turretHealth.maxHealth)
            {
                gameManager.Ironite -= 100;

                int newHealth = turretHealth.currentHealth + 1000;
                turretHealth.currentHealth = (newHealth > turretHealth.maxHealth) ? turretHealth.maxHealth : newHealth;

                var window = UI.transform.Find("Building Windows/Turret Window").gameObject;
                var health = window.transform.Find("Turret Health").gameObject;

                health.GetComponent<Text>().text = "Health: (" + turretHealth.currentHealth + "/" + turretHealth.maxHealth + ")";
            }
        }
    }

    public void DestroyTurret()
    {
        var turret = gm.GetComponent<GameManager>().ActiveTurret;
        Destroy(turret);
    }

    public void RepairWall()
    {
        var wall = gm.GetComponent<GameManager>().ActiveWall;

        if (wall)
        {
            var wallHealth = wall.GetComponent<Unit_Health>();

            if (gameManager.Ironite >= 100 && wallHealth.currentHealth != wallHealth.maxHealth)
            {
                gameManager.Ironite -= 100;

                int newHealth = wallHealth.currentHealth + 1000;
                wallHealth.currentHealth = (newHealth > wallHealth.maxHealth) ? wallHealth.maxHealth : newHealth;

                var window = UI.transform.Find("Building Windows/Wall Window").gameObject;
                var health = window.transform.Find("Wall Health").gameObject;

                health.GetComponent<Text>().text = "Health: (" + wallHealth.currentHealth + "/" + wallHealth.maxHealth + ")";
            }
        }
    }

    public void DestroyWall()
    {
        //var turret = gm.GetComponent<GameManager>().ActiveTurret;
        //Destroy(turret);
    }

    public void RepairGate()
    {
        var gate = gm.GetComponent<GameManager>().ActiveGate;
        if (gate)
        {
            var gateHealth = gate.GetComponent<Unit_Health>();
            if (gameManager.Ironite >= 100 && gateHealth.currentHealth != gateHealth.maxHealth)
            {
                gameManager.Ironite -= 100;

                int newHealth = gateHealth.currentHealth + 1000;
                gateHealth.currentHealth = (newHealth > gateHealth.maxHealth) ? gateHealth.maxHealth : newHealth;

                var window = UI.transform.Find("Building Windows/Gate Window").gameObject;
                var health = window.transform.Find("Gate Health").gameObject;

                health.GetComponent<Text>().text = "Health: (" + gateHealth.currentHealth + "/" + gateHealth.maxHealth + ")";
            }
        }
    }

    public void OpenGate()
    {
        var gate = gm.GetComponent<GameManager>().ActiveGate;
        var phase = gate.transform.GetChild(gate.transform.childCount - 1).gameObject;
        phase.SetActive(false);
    }

    public void CloseGate()
    {
        var gate = gm.GetComponent<GameManager>().ActiveGate;
        var phase = gate.transform.GetChild(gate.transform.childCount - 1).gameObject;
        phase.SetActive(true);
    }

    public void CreateMeleeUnit()
    {
        if (gm.GetComponent<GameManager>().Ironite >= 250)
        {
            gm.GetComponent<GameManager>().Ironite -= 250;
            var prod = gm.GetComponent<GameManager>().ActiveTroopProd;
            var spawnLoc = prod.transform.GetChild(prod.transform.childCount - 1);

            Instantiate(melee, spawnLoc.position, spawnLoc.rotation);
        }
    }

    public void CreateRangedUnit()
    {
        if (gm.GetComponent<GameManager>().Ironite >= 250)
        {
            gm.GetComponent<GameManager>().Ironite -= 250;
            var prod = gm.GetComponent<GameManager>().ActiveTroopProd;
            var spawnLoc = prod.transform.GetChild(prod.transform.childCount - 1);

            Instantiate(ranged, spawnLoc.position, spawnLoc.rotation);
        }
    }
}
