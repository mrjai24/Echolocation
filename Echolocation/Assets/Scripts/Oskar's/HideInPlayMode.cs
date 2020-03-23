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
        if (EditorApplication.isPlayingOrWillChangePlaymode)
        {
            GetComponent<TilemapRenderer>().enabled = false;
        }
        else
            GetComponent<TilemapRenderer>().enabled = true;

    }
}