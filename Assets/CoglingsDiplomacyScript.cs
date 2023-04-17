using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoglingsDiplomacyScript : MonoBehaviour
{
    public int reputation;
    public bool isDestroyed;
    private GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = gameObject.GetComponent<GameManager>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
