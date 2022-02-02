using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class WordManager : MonoBehaviour
{
    private List<string> words = new List<string>();
    private List<string> fullWords = new List<string>();
    public WordManager wordManager;

    // Start is called before the first frame update
    void Awake()
    {
        string path = "Assets/Resources/dictionary_test.txt";
        StreamReader reader = new StreamReader(path);
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            words.Add(line);
        }
        reader.Close();
        fullWords = new List<string>(words);
    }

    public string GetWord()
    {
        int index = Random.Range(0, words.Count - 1);
        string word = words[index];
        words.RemoveAt(index);
        if (words.Count == 0)
        {
            words = new List<string>(fullWords);
        }
        return word;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
