using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using TMPro;

public class TypingManager : MonoBehaviour
{

    private string input;
    private List<string> words = new List<string>();
    private Text inputText;
    public delegate void interpretText(string word);
    public Player player;
    public static event interpretText delegateInstance;
    public EnemyManager enemyManager;
    // Start is called before the first frame update
    void Start()
    {
        if (inputText == null)
        {
            inputText = GameObject.Find("Input").GetComponent<Text>();
        }
        if (player == null)
        {
            player = GameObject.Find("Player").GetComponent<Player>();
        }
        if (enemyManager == null)
        {
            enemyManager = GameObject.Find("EnemyManager").GetComponent<EnemyManager>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach (char c in Input.inputString)
        {
            if (c == '\b') // has backspace/delete been pressed?
            {
                if (input.Length != 0)
                {
                    input = input.Substring(0, input.Length - 1);
                    inputText.text = input;
                }
            }
            else if ((c == '\n') || (c == '\r') || (c == ' ')) // enter/return/space
            {
                if (delegateInstance != null)
                {
                    delegateInstance(input);
                }
                input = "";
                inputText.text = input;
            }
            else
            {
                input += c;
                inputText.text = input;
            }
        }
    }
}
