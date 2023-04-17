using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroManager : MonoBehaviour
{

    private Queue<string> sentences;
    private Queue<string> sentences2;
    string to = "";
    string dou = "";
    GameObject go;
    public GameObject Panel;
    public GameObject textPanel;
    public Text dialaugeText;
    public Text dialaugeText2;

    // Start is called before the first frame update
    void Start()
    {
        go = GameObject.Find("UI");
        go.SetActive(false);
        sentences = new Queue<string>();
        sentences2 = new Queue<string>();
    }

    public void StartIntro(Intro intro)
    {
        //Debug.Log("Starting Intro");

        sentences.Clear();

        foreach(var sentence in intro.sentences)
        {
            sentences.Enqueue(sentence);
        }
        foreach (var sentence in intro.sentences2)
        {
            sentences2.Enqueue(sentence);
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

    public void ShowSentence2()
    {
        if (sentences2.Count == 0)
        {
            EndDialog2();
            return;
        }
        
        string sentence = sentences2.Dequeue();
        dou = dou + sentence;
        dialaugeText2.text = dou;
        if (sentences2.Count % 2 == 0)
            dou = "";
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
        textPanel.SetActive(true);
        ShowSentence2();
    }

    public void EndDialog2()
    {
        Debug.Log("End");
        textPanel.SetActive(false);
        GameObject.Find("Intro").SetActive(false);

        //Change after I get Nilans SHIT!!!!!!
        Time.timeScale = 1;

    }

}
