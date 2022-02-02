using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class BattleManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        Player.died += PlayerDied;
    }

    private void OnDisable()
    {
        Player.died -= PlayerDied;
    }

    private void PlayerDied()
    {
        Invoke("ReturnToMenu", 5.0f);
    }

    private void ReturnToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
