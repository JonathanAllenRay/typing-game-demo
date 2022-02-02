using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainWordQueue : MonoBehaviour
{

    private const int wordLimit = 3;
    public WordManager wordManager;
    private List<string> words = new List<string>();
    public Text mainWordQueue;
    public delegate void wordQueueHit();
    public static event wordQueueHit hit;
    // Start is called before the first frame update
    void Start()
    {
        if (wordManager == null)
        {
            wordManager = GameObject.Find("WordManager").GetComponent<WordManager>();
        }
        
        if (mainWordQueue == null)
        {
            mainWordQueue = GameObject.Find("MainWordQueue").GetComponent<Text>();
        }

        for (int i = 0; i < wordLimit; i++)
        {
            words.Add(wordManager.GetWord());
        }
        ListToTextString();
    }

    public bool PopWord(string word)
    {
        if (word == words[0])
        {
            words.RemoveAt(0);
            words.Add(wordManager.GetWord());
            ListToTextString();
            return true;
        }
        else
        {
            return false;
        }
    }

    private void ListToTextString()
    {
        mainWordQueue.text = "<b>";
        for (int i = 0; i < words.Count; i++)
        {
            mainWordQueue.text += words[i] + " ";
        }
        mainWordQueue.text += "</b>";
    }
}
