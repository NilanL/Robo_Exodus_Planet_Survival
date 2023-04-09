using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TradeOld : MonoBehaviour
{
    Text textField;
    Text textField2;
    Text textField3;
    int Zornum;
    int Aurnum;
    int Blonum;
    public GameObject inputField;
    public InputField erase;
    public string customNumber;
    int custnum;
    GameObject gm;

    // Start is called before the first frame update
    void Start()
    {
        textField = GameObject.Find("Zorium").GetComponent<Text>();
        textField2 = GameObject.Find("Aurarium").GetComponent<Text>();
        textField3 = GameObject.Find("Bloodstone").GetComponent<Text>();
        gm = GameObject.Find("GameManager");

    }
    void Update()
    {
        //textField.text = "(Zorium Here) " + gm.GetComponent<GameManager>().Zorium;
        //textField2.text = "(Aurarium Here) " + gm.GetComponent<GameManager>().Aurarium;
        //textField3.text = "(Bloodstone Here) " + gm.GetComponent<GameManager>().BloodStone;
        //textField.text = "(Zorium Here) " + Zornum;
        //textField2.text = "(Aurarium Here) " + Aurnum;
        //textField3.text = "(Bloodstone Here) " + Blonum;
    }
    public void ZoriumforAurarium()
    {
        Zornum += 1;
        Aurnum -= 1;

        textField.text = "" + Zornum;
        textField2.text = "" + Aurnum;
    }
    public void ZoriumforAurarium10()
    {
        Zornum += 10;
        Aurnum -= 10;

        textField.text = "" + Zornum;
        textField2.text = "" + Aurnum;
    }
    public void ZoriumforAurariumCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            Zornum = Zornum + custnum;
            Aurnum = Aurnum - custnum;
        }

        textField.text = "(Zorium Here) " + Zornum;
        textField2.text = "(Aurarium Here) " + Aurnum;
        erase.text = "";
    }
    public void ZoriumforBloodstone()
    {
        Zornum += 1;
        Blonum -= 1;

        textField.text = "(Zorium Here) " + Zornum;
        textField3.text = "(Bloodstone Here) " + Blonum;
    }
    public void ZoriumforBloodstone10()
    {
        Zornum += 10;
        Blonum -= 10;

        textField.text = "(Zorium Here) " + Zornum;
        textField3.text = "(Bloodstone Here) " + Blonum;
    }
    public void ZoriumforBloodstoneCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            Zornum = Zornum + custnum;
            Blonum = Blonum - custnum;
        }

        textField.text = "(Zorium Here) " + Zornum;
        textField3.text = "(Bloodstone Here) " + Blonum;
        erase.text = "";
    }
    public void AurariumforZorium()
    {
        Aurnum += 1;
        Zornum -= 1;

        textField2.text = "(Aurarium Here) " + Aurnum;
        textField.text = "(Zorium Here) " + Zornum;
    }
    public void AurariumforZorium10()
    {
        Aurnum += 10;
        Zornum -= 10;

        textField2.text = "(Aurarium Here) " + Aurnum;
        textField.text = "(Zorium Here) " + Zornum;
    }
    public void AurariumforZoriumCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            Aurnum = Aurnum + custnum;
            Zornum = Zornum - custnum;
        }

        textField2.text = "(Aurarium Here) " + Aurnum;
        textField.text = "(Zorium Here) " + Zornum;
        erase.text = "";
    }
    public void AurariumforBloodstone()
    {
        Aurnum += 1;
        Blonum -= 1;

        textField2.text = "(Aurarium Here) " + Aurnum;
        textField3.text = "(Bloodstone Here) " + Blonum;
    }
    public void AurariumforBloodstone10()
    {
        Aurnum += 10;
        Blonum -= 10;

        textField2.text = "(Aurarium Here) " + Aurnum;
        textField3.text = "(Bloodstone Here) " + Blonum;
    }
    public void AurariumforBloodstoneCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            Aurnum = Aurnum + custnum;
            Blonum = Blonum - custnum;
        }

        textField2.text = "(Aurarium Here) " + Aurnum;
        textField3.text = "(Bloodstone Here) " + Blonum;
        erase.text = "";
    }
    public void BloodstoneforZorium()
    {
        Blonum += 1;
        Zornum -= 1;

        textField3.text = "(Bloodstone Here) " + Blonum;
        textField.text = "(Zorium Here) " + Zornum;
    }
    public void BloodstoneforZorium10()
    {
        Blonum += 10;
        Zornum -= 10;

        textField3.text = "(Bloodstone Here) " + Blonum;
        textField.text = "(Zorium Here) " + Zornum;
    }
    public void BloodstoneforZoriumCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            Blonum = Blonum + custnum;
            Zornum = Zornum - custnum;
        }

        textField3.text = "(Bloodstone Here) " + Blonum;
        textField.text = "(Zorium Here) " + Zornum;
        erase.text = "";
    }
    public void BloodstoneforAurarium()
    {
        Blonum += 1;
        Aurnum -= 1;

        textField3.text = "(Bloodstone Here) " + Blonum;
        textField2.text = "(Aurarium Here) " + Aurnum;
    }
    public void BloodstoneforAurarium10()
    {
        Blonum += 10;
        Aurnum -= 10;

        textField3.text = "(Bloodstone Here) " + Blonum;
        textField2.text = "(Aurarium Here) " + Aurnum;
    }
    public void BloodstoneforAurariumCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            Blonum = Blonum + custnum;
            Aurnum = Aurnum - custnum;
        }

        textField3.text = "(Bloodstone Here) " + Blonum;
        textField2.text = "(Aurarium Here) " + Aurnum;
        erase.text = "";
    }
}