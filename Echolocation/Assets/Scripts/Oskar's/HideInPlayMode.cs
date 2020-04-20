using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;


//Script that shows grid in Edit mode and hides it in Play mode
[ExecuteInEditMode]

public class HideInPlayMode : MonoBehaviour
{
#if UNITY_EDITOR
    void Start()
    {
    
        if (this.gameObject.name == "Walls" )
        {

            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                GetComponent<Tilemap>().color = new Color(0, 0, 0, 1);
            }
            else
                GetComponent<Tilemap>().color = new Color(255, 255, 255, 1);
        }
        else
        {
            if (EditorApplication.isPlayingOrWillChangePlaymode)
            {
                GetComponent<TilemapRenderer>().enabled = false;
            }
            else
                GetComponent<TilemapRenderer>().enabled = true;
        }

    }
#endif

#if !Unity_EDITOR

    void Awake()
    {
        if (this.gameObject.name == "Walls")
        {
            GetComponent<Tilemap>().color = new Color(0, 0, 0, 1);
        }
        else
            GetComponent<TilemapRenderer>().enabled = false;
    }
#endif
}

