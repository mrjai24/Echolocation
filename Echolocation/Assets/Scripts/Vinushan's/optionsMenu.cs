using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class optionsMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject newObject = Resources.Load("Prefabs/Options") as GameObject;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
