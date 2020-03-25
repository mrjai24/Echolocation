using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEditor;
[ExecuteInEditMode]
public class HideInPlayMode : MonoBehaviour
{
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
}