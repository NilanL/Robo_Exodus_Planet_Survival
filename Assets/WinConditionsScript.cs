using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinConditionsScript : MonoBehaviour
{
    private GameObject gm;

    private void Start()
    {
        gm = GameObject.Find("GameManager");
    }

    public void WinGameByLaunch()
    {
        gm.GetComponent<GameManager>().WinGame();
    }
}
