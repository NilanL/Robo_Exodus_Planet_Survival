using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trade : MonoBehaviour
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
    GameObject Marker;
    Vector3 tempPos;

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
        if (int.TryParse(textField.text, out Zornum))
        {
            
        }
        if (int.TryParse(textField2.text, out Aurnum))
        {

        }
        if (int.TryParse(textField3.text, out Blonum))
        {

        }
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

        if(int.TryParse(customNumber, out custnum))
        {
            Zornum = Zornum + custnum;
            Aurnum = Aurnum - custnum;
        }

        textField.text = "" + Zornum;
        textField2.text = "" + Aurnum;
        erase.text = "";
    }
    public void DonateAurariumToCoglings()
    {
        Aurnum -= 1;
        textField2.text = "" + Aurnum;
        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += 1f;
        Marker.transform.position = tempPos;
    }
    public void DonateAurariumToCoglings10()
    {
        Aurnum -= 10;
        textField2.text = "" + Aurnum;
        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += 10f;
        Marker.transform.position = tempPos;
    }
    public void DonateAurariumToCoglingsCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            Aurnum = Aurnum - custnum;
        }

        textField2.text = "" + Aurnum;
        erase.text = "";

        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += custnum;
        Marker.transform.position = tempPos;
    }
    public void ZoriumforBloodstone()
    {
        Zornum += 1;
        Blonum -= 1;

        textField.text = "" + Zornum;
        textField3.text = "" + Blonum;
    }
    public void ZoriumforBloodstone10()
    {
        Zornum += 10;
        Blonum -= 10;

        textField.text = "" + Zornum;
        textField3.text = "" + Blonum;
    }
    public void ZoriumforBloodstoneCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            Zornum = Zornum + custnum;
            Blonum = Blonum - custnum;
        }

        textField.text = "" + Zornum;
        textField3.text = "" + Blonum;
        erase.text = "";
    }
    public void DonateBloodstoneToCoglings()
    {
        Blonum -= 1;
        textField3.text = "" + Blonum;
        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += 1f;
        Marker.transform.position = tempPos;
    }
    public void DonateBloodstoneToCoglings10()
    {
        Blonum -= 10;
        textField3.text = "" + Blonum;
        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += 10f;
        Marker.transform.position = tempPos;
    }
    public void DonateBloodstoneToCoglingsCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            Blonum = Blonum - custnum;
        }

        textField3.text = "" + Blonum;
        erase.text = "";

        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += custnum;
        Marker.transform.position = tempPos;
    }
    public void AurariumforZorium()
    {
        Aurnum += 1;
        Zornum -= 1;

        textField2.text = "" + Aurnum;
        textField.text = "" + Zornum;
    }
    public void AurariumforZorium10()
    {
        Aurnum += 10;
        Zornum -= 10;

        textField2.text = "" + Aurnum;
        textField.text = "" + Zornum;
    }
    public void AurariumforZoriumCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            Aurnum = Aurnum + custnum;
            Zornum = Zornum - custnum;
        }

        textField2.text = "" + Aurnum;
        textField.text = "" + Zornum;
        erase.text = "";
    }
    public void DonateZoriumToSleemasi()
    {
        Zornum -= 1;
        textField.text = "" + Zornum;
        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += 1f;
        Marker.transform.position = tempPos;
    }
    public void DonateZoriumToSleemasi10()
    {
        Zornum -= 10;
        textField.text = "" + Zornum;
        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += 10f;
        Marker.transform.position = tempPos;
    }
    public void DonateZoriumToSleemasiCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            Zornum = Zornum - custnum;
        }

        textField.text = "" + Zornum;
        erase.text = "";

        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += custnum;
        Marker.transform.position = tempPos;
    }
    public void AurariumforBloodstone()
    {
        Aurnum += 1;
        Blonum -= 1;

        textField2.text = "" + Aurnum;
        textField3.text = "" + Blonum;
    }
    public void AurariumforBloodstone10()
    {
        Aurnum += 10;
        Blonum -= 10;

        textField2.text = "" + Aurnum;
        textField3.text = "" + Blonum;
    }
    public void AurariumforBloodstoneCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            Aurnum = Aurnum + custnum;
            Blonum = Blonum - custnum;
        }

        textField2.text = "" + Aurnum;
        textField3.text = "" + Blonum;
        erase.text = "";
    }
    public void DonateBloodstoneToSleemasi()
    {
        Blonum -= 1;
        textField3.text = "" + Blonum;
        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += 1f;
        Marker.transform.position = tempPos;
    }
    public void DonateBloodstoneToSleemasi10()
    {
        Blonum -= 10;
        textField3.text = "" + Blonum;
        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += 10f;
        Marker.transform.position = tempPos;
    }
    public void DonateBloodstoneToSleemasiCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            Blonum = Blonum - custnum;
        }

        textField3.text = "" + Blonum;
        erase.text = "";

        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += custnum;
        Marker.transform.position = tempPos;
    }
    public void BloodstoneforZorium()
    {
        Blonum += 1;
        Zornum -= 1;

        textField3.text = "" + Blonum;
        textField.text = "" + Zornum;
    }
    public void BloodstoneforZorium10()
    {
        Blonum += 10;
        Zornum -= 10;

        textField3.text = "" + Blonum;
        textField.text = "" + Zornum;
    }
    public void BloodstoneforZoriumCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            Blonum = Blonum + custnum;
            Zornum = Zornum - custnum;
        }

        textField3.text = "" + Blonum;
        textField.text = "" + Zornum;
        erase.text = "";
    }
    public void DonateZoriumToGraxxians()
    {
        Zornum -= 1;
        textField.text = "" + Zornum;
        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += 1f;
        Marker.transform.position = tempPos;
    }
    public void DonateZoriumToGraxxians10()
    {
        Zornum -= 10;
        textField.text = "" + Zornum;
        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += 10f;
        Marker.transform.position = tempPos;
    }
    public void DonateZoriumToGraxxiansCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            Zornum = Zornum - custnum;
        }

        textField.text = "" + Zornum;
        erase.text = "";

        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += custnum;
        Marker.transform.position = tempPos;
    }
    public void BloodstoneforAurarium()
    {
        Blonum += 1;
        Aurnum -= 1;

        textField3.text = "" + Blonum;
        textField2.text = "" + Aurnum;
    }
    public void BloodstoneforAurarium10()
    {
        Blonum += 10;
        Aurnum -= 10;

        textField3.text = "" + Blonum;
        textField2.text = "" + Aurnum;
    }
    public void BloodstoneforAurariumCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            Blonum = Blonum + custnum;
            Aurnum = Aurnum - custnum;
        }

        textField3.text = "" + Blonum;
        textField2.text = "" + Aurnum;
        erase.text = "";
    }
    public void DonateAurariumToGraxxians()
    {
        Aurnum -= 1;
        textField2.text = "" + Aurnum;
        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += 1f;
        Marker.transform.position = tempPos;
    }
    public void DonateAurariumToGraxxians10()
    {
        Aurnum -= 10;
        textField2.text = "" + Aurnum;
        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += 10f;
        Marker.transform.position = tempPos;
    }
    public void DonateAurariumToGraxxiansCustom(string number)
    {
        customNumber = inputField.GetComponent<Text>().text;

        if (int.TryParse(customNumber, out custnum))
        {
            Aurnum = Aurnum - custnum;
        }

        textField2.text = "" + Aurnum;
        erase.text = "";

        Marker = GameObject.Find("Marker");
        tempPos = Marker.transform.position;
        tempPos.x += custnum;
        Marker.transform.position = tempPos;
    }
}