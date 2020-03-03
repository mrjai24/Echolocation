using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Mesh mesh = new Mesh();
        Vector3[] verticies = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        verticies[0] = new Vector3(0, 0);
        verticies[1] = new Vector3(0, 10);
        verticies[2] = new Vector3(10, 10);
        verticies[3] = new Vector3(10, 0);

        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 1);
        uv[3] = new Vector2(1, 0);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        mesh.vertices = verticies;
        mesh.uv = uv;
        mesh.triangles = triangles;


        GetComponent<MeshFilter>().mesh = mesh;
        
    }

}
