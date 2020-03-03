using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootEcho : MonoBehaviour
{
    [SerializeField]
    private int lineMaxDistance = 100;
    [SerializeField]
    private int numberMaxReflect = 30;



    public List<GameObject> lines = new List<GameObject>();

    public List<LineRenderer> lineRenderers;

    private Vector3 position;
    private Vector3 vectDirection;
    private int reflectionCount;
    private bool canShoot = true;
    


    void Start()
    {

        foreach(var line in lines)
        {
            lineRenderers.Add(line.GetComponent<LineRenderer>());
        }
        DrawNormalLasers();
        StartCoroutine(FadeLines());
    }

    void FixedUpdate()
    {
        
    }

    void DrawNormalLasers()
    {
        var layerMask = 1 << LayerMask.NameToLayer("Wall");
        for (int i= 0 ; i < lineRenderers.Count; i++ )
        {
            canShoot = true;
            reflectionCount = 1;
            var currentLine = lineRenderers[i];
            position = lines[i].GetComponent<Transform>().position;
            float angle = 180f - (i + 1) * 41; 
            vectDirection = RadianToVector2(angle * Mathf.Deg2Rad) * lines[i].GetComponent<Transform>().transform.parent.gameObject.transform.localScale;
            currentLine.SetPosition(0, position);

            while (canShoot)
            {
                RaycastHit2D hit = Physics2D.Raycast(position, vectDirection, lineMaxDistance,layerMask);
                if (hit)
                {
                    
                    reflectionCount++;
                    currentLine.positionCount = reflectionCount;
                    //Vector3[] newPos = new Vector3[currentLine.positionCount];
                    //Debug.Log("Line number " + i + currentLine.GetPositions(newPos));
                    //if ((Vector2)vectDirection.normalized+hit.point == (Vector2)currentLine.GetPosition(reflectionCount-1))
                    //    vectDirection = Vector3.Reflect(new Vector3(1f,0.2f), hit.normal);
                    //else
                        vectDirection = Vector3.Reflect(vectDirection, hit.normal);
                    position = (Vector2)vectDirection.normalized + hit.point;
                    currentLine.SetPosition(reflectionCount - 1, hit.point);
                }
                else
                {
                    reflectionCount++;
                    currentLine.positionCount = reflectionCount;
                    currentLine.SetPosition(reflectionCount - 1, position + (vectDirection.normalized * lineMaxDistance));
                    canShoot = false;
                }
                if (reflectionCount > numberMaxReflect)
                    canShoot = false;
            }
           
        }
        
    }
    IEnumerator FadeLines()
    {


        
        float alpha = 1.0f;
        Gradient gradient = new Gradient();
        gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.black, 0.2f), new GradientColorKey(Color.gray, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha-0.5f, 0.0f), new GradientAlphaKey(alpha, 0.5f), new GradientAlphaKey(alpha, 1.0f) }
        );

        while (true)
        {
            gradient.SetKeys(
            new GradientColorKey[] { new GradientColorKey(Color.black, 0.2f), new GradientColorKey(Color.gray, 0.0f), new GradientColorKey(Color.white, 1.0f) },
            new GradientAlphaKey[] { new GradientAlphaKey(alpha - 0.5f, 1.0f), new GradientAlphaKey(alpha, 0.4f), new GradientAlphaKey(alpha, 0.0f) }
        );
            for (int i = 0; i < lineRenderers.Count; i++)
            {
                var currentLine = lineRenderers[i];
                currentLine.colorGradient = gradient;
            }
            yield return new WaitForSeconds(0.05f);
            alpha-= 0.1f;
        }

    }
    public static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }

    public static Vector2 DegreeToVector2(float degree)
    {
        return RadianToVector2(degree * Mathf.Deg2Rad);
    }
}
