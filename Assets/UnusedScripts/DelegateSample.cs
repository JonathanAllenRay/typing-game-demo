using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelegateSample : MonoBehaviour
{
    public delegate void testDelegate(bool isGood);
    public static event testDelegate delegateInstance;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        delegateInstance(true);

    }
    public void OnButtonClick()
    {
    }
}
