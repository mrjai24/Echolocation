using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//Script that shows colliders
//Used to show water position in Scene
[ExecuteInEditMode]
public class DrawGizmos : MonoBehaviour
{
    public Color color;
    private BoxCollider2D boxCollider;

    void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = color;
        if (boxCollider != null)
        {
            Vector3 Pos = transform.position;
            Vector3 Size = new Vector3(boxCollider.size.x * transform.lossyScale.x, boxCollider.size.y * transform.lossyScale.y, 1f);
            Size /= 2f;

            Vector3[] Points = new Vector3[4];  
            Points[0].Set(Pos.x - Size.x, Pos.y + Size.y, Pos.z);
            Points[1].Set(Pos.x + Size.x, Pos.y + Size.y, Pos.z);
            Points[2].Set(Pos.x + Size.x, Pos.y - Size.y, Pos.z);
            Points[3].Set(Pos.x - Size.x, Pos.y - Size.y, Pos.z);

            float RotationInRad = transform.eulerAngles.z * Mathf.Deg2Rad;

            for (int i = 0; i < 4; i++)
            {
                float x = Points[i].x - Pos.x, y = Points[i].y - Pos.y;

                Points[i].x = x * Mathf.Cos(RotationInRad) - y * Mathf.Sin(RotationInRad);
                Points[i].y = x * Mathf.Sin(RotationInRad) + y * Mathf.Cos(RotationInRad);

                Points[i].x = Points[i].x + Pos.x;
                Points[i].y = Points[i].y + Pos.y;
            }

            Mesh mesh = new Mesh();

            Vector3[] vertices = new Vector3[4];

            vertices[0] = Points[2];
            vertices[1] = Points[1];
            vertices[2] = Points[3];
            vertices[3] = Points[0];

            mesh.vertices = vertices;

            int[] tri = new int[6];

            tri[0] = 0;
            tri[1] = 2;
            tri[2] = 1;

            tri[3] = 2;
            tri[4] = 3;
            tri[5] = 1;

            mesh.triangles = tri;

            Vector3[] normals = new Vector3[4];

            normals[0] = -Vector3.forward;
            normals[1] = -Vector3.forward;
            normals[2] = -Vector3.forward;
            normals[3] = -Vector3.forward;

            mesh.normals = normals;

            Vector2[] uv = new Vector2[4];

            uv[0] = new Vector2(0, 0);
            uv[1] = new Vector2(1, 0);
            uv[2] = new Vector2(0, 1);
            uv[3] = new Vector2(1, 1);

            mesh.uv = uv;
            Gizmos.DrawMesh(mesh);
        }
    }
}
