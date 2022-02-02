using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anotherthing : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DelegateSample.delegateInstance += doThing2;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void doThing2(bool poop)
    {
        Debug.Log("anotherway");
    }
}
