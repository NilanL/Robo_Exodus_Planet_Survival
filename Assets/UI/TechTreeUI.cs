using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TechTreeUI : MonoBehaviour
{
    public GameObject gm;
    public Button hp1;
    public Button hp2;
    public Button hp3;
    public Button speed1;
    public Button speed2;
    public Button speed3;
    public Button dmg1;
    public Button dmg2;
    public Button dmg3;
    public Button mine1;
    public Button mine2;
    public Button mine3;
    public Button cog;
    public Button slee;
    public Button graxx;

    // You can't find buttons that are inactive so instead of on start
    // I call this when tech tree is opened
    public void GetTechTreeReady()
    {
        gm = GameObject.Find("GameManager");
        hp1 = GameObject.Find("HP1").GetComponent<Button>();
        hp2 = GameObject.Find("HP2").GetComponent<Button>();
        hp3 = GameObject.Find("HP3").GetComponent<Button>();
        speed1 = GameObject.Find("Speed1").GetComponent<Button>();
        speed2 = GameObject.Find("Speed2").GetComponent<Button>();
        speed3 = GameObject.Find("Speed3").GetComponent<Button>();
        dmg1 = GameObject.Find("Dmg1").GetComponent<Button>();
        dmg2 = GameObject.Find("Dmg2").GetComponent<Button>();
        dmg3 = GameObject.Find("Dmg3").GetComponent<Button>();
        mine1 = GameObject.Find("Mine1").GetComponent<Button>();
        mine2 = GameObject.Find("Mine2").GetComponent<Button>();
        mine3 = GameObject.Find("Mine3").GetComponent<Button>();
        cog = GameObject.Find("Cog").GetComponent<Button>();
        slee = GameObject.Find("Slee").GetComponent<Button>();
        graxx = GameObject.Find("Graxx").GetComponent<Button>();
        

        hp2.interactable = false;
        hp3.interactable = false;
        speed2.interactable = false;
        speed3.interactable = false;
        dmg2.interactable = false;
        dmg3.interactable = false;
        mine2.interactable = false;
        mine3.interactable = false;
    }
    

    public void HPUpgrade1()
    {
        //call script which updates health
        if(gm.GetComponent<GameManager>().Aurarium >= 250 && gm.GetComponent<GameManager>().Ironite >= 250)
        {
            gm.GetComponent<GameManager>().Aurarium -= 250;
            gm.GetComponent<GameManager>().Ironite -= 250;

            hp1.interactable = false;
            hp2.interactable = true;

        }
            
        

    }
    
    public void HPUpgrade2()
    {
        //call script which updates health
        hp2.interactable = false;
        hp3.interactable = true;

    }
    public void HPUpgrade3()
    {
        //call script which updates health
        hp3.interactable = false;

    }
    public void SpeedUpgrade1()
    {
        //call script which updates health
        speed1.interactable = false;
        speed2.interactable = true;

    }
    public void SpeedUpgrade2()
    {
        //call script which updates health
        speed2.interactable = false;
        speed3.interactable = true;

    }
    public void SpeedUpgrade3()
    {
        //call script which updates speed
        speed3.interactable = false;

    }
    public void DamageUpgrade1()
    {
        //call script which updates health
        dmg1.interactable = false;
        dmg2.interactable = true;

    }
    public void DamageUpgrade2()
    {
        //call script which updates health
        dmg2.interactable = false;
        dmg3.interactable = true;

    }
    public void DamageUpgrade3()
    {
        //call script which updates health
        dmg3.interactable = false;

    }
    public void MineUpgrade1()
    {
        //call script which updates health
        mine1.interactable = false;
        mine2.interactable = true;

    }
    public void MineUpgrade2()
    {
        //call script which updates health
        mine2.interactable = false;
        mine3.interactable = true;

    }
    public void MineUpgrade3()
    {
        //call script which updates health
        mine3.interactable = false;

    }
    public void CoglingUpgrade()
    {
        //call script which updates health
        cog.interactable = false;

    }
    public void SleemasiUpgrade()
    {
        //call script which updates health
        slee.interactable = false;

    }
    public void GraxxianUpgrade()
    {
        //call script which updates health
        graxx.interactable = false;

    }
}