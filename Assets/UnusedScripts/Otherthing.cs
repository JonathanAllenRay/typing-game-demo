using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Otherthing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DelegateSample.delegateInstance += doThing;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void doThing(bool poop)
    {
        Debug.Log(poop);
    }
}
