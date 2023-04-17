using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{

    private Queue<string> sentences;
    string to = "";
    GameObject go;
    public GameObject Panel;
    public Text dialaugeText;

    // Start is called before the first frame update
    void Start()
    {
        go = GameObject.Find("UI");
        go.SetActive(false);
        sentences = new Queue<string>();
    }

    public void StartIntro(Intro intro)
    {
        //Debug.Log("Starting Intro");

        sentences.Clear();

        foreach(var sentence in intro.sentences)
        {
            sentences.Enqueue(sentence);
        }


        ShowSentence();

    }

    public void ShowSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialog();
            return;
        }
        
        string sentence = sentences.Dequeue();
        to = to + sentence;
        dialaugeText.text = to;
    }

    IEnumerator QueSentencies()
    {
        
        
            yield return new WaitForSeconds(1);
            string sentence = sentences.Dequeue();
            Debug.LogError(sentence);
        
    }

    public void EndDialog()
    {
        Debug.Log("End");
        go.SetActive(true);
        Panel.SetActive(false);
    }

}
