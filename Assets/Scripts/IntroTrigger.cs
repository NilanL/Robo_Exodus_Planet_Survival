using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroTrigger : MonoBehaviour
{

    public Intro intro;

    private void Start()
    {
        Time.timeScale = 0;
        TriggerDialogue();
    }

    public void TriggerDialogue()
    {
        FindObjectOfType<IntroManager>().StartIntro(intro);
    }    
}
