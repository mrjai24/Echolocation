using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FootEcho : MonoBehaviour
{
    [SerializeField]
    private int lineMaxDistance = 100;
    [SerializeField]
    private int numberMaxReflect = 30;
    [SerializeField]
    private float iceVariation = 1f;



    public List<GameObject> lines = new List<GameObject>();

    public List<LineRenderer> lineRenderers;

    public bool shootLines = false;

    public Material mudMaterial;



    private Vector3 position;
    private Vector3 vectDirection;
    private int reflectionCount;
    private bool canShoot = true;
    private bool doorhit;
    private string currentGroundType;
    private GameObject player;
    private int stepCount;
    private GameObject doorObject;




    void Start()
    {
        currentGroundType = GetComponent<Footstep>().currentGroundType;
        player =  GameObject.FindWithTag("Player");
        stepCount = player.GetComponent<PlayerMovement>().steps;
        switch (currentGroundType)
        {
            case "Normal":
                Debug.Log("Walking on nomral ground");
                foreach (var line in lines)
                {
                    lineRenderers.Add(line.GetComponent<LineRenderer>());
                }
                DrawNormalLasers();
                StartCoroutine(FadeLines());
                break;
            case "Ice":
                foreach (var line in lines)
                {
                    lineRenderers.Add(line.GetComponent<LineRenderer>());
                }
                DrawIceLasers();
                StartCoroutine(FadeLines());
                break;
            case "Mud":
                foreach (var line in lines)
                {
                    lineRenderers.Add(line.GetComponent<LineRenderer>());
                }
                DrawMudLasers();
                StartCoroutine(FadeLines());
                break;
        }
            
    }



    void DrawNormalLasers()
    {
        var layerMask = LayerMask.GetMask("Wall","Door");
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
                    vectDirection = Vector3.Reflect(vectDirection, hit.normal);
                    position = (Vector2)vectDirection.normalized + hit.point;
                    currentLine.SetPosition(reflectionCount - 1, hit.point);
                    if (hit.transform.gameObject.layer == LayerMask.NameToLayer("Door")){
                        doorhit = true;
                        doorObject = hit.transform.gameObject;
                    }
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
        if (doorhit)
        {
            doorObject.GetComponent<Door>().ShowDoor(Vector3.Distance(transform.position, doorObject.transform.position));
        }
        
    }

    void DrawIceLasers()
    {
        var layerMask = 1 << LayerMask.NameToLayer("Wall");
        for (int i = 0; i < lineRenderers.Count; i++)
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
                RaycastHit2D hit = Physics2D.Raycast(position, vectDirection, lineMaxDistance, layerMask);
                if (hit)
                {
                    Vector2 prevPos = currentLine.GetPosition(reflectionCount - 1);
                    Vector2 prevPosLocal = prevPos + (-prevPos);

                    Vector2 endPos = hit.point;
                    Vector2 endPosLocal = new Vector2(endPos.x - prevPos.x, endPos.y - prevPos.y);

                    float distance = Vector2.Distance(prevPos, endPos);
                    int iceDivideCount = (int)(distance / iceVariation);
                    for (int j = 1; j<= iceDivideCount; j++)
                    {
                        Vector2 midPos = new Vector2 (prevPos.x + (float)j / iceDivideCount * (hit.point.x - prevPos.x),prevPos.y + (float)j / iceDivideCount * (hit.point.y - prevPos.y));
                        Vector2 midPosLocal = new Vector2(prevPosLocal.x + (float)j / iceDivideCount * (endPosLocal.x - prevPosLocal.x), prevPosLocal.y + (float)j / iceDivideCount * (endPosLocal.y - prevPosLocal.y));

                        Vector2 perpVect = iceVariation * Vector2.Perpendicular(midPosLocal.normalized);
                        Vector2 offsetVect;

                        if (j%2 ==0 )
                            offsetVect = midPos + perpVect;
                        else
                            offsetVect = midPos - perpVect;

                        reflectionCount++;
                        currentLine.positionCount = reflectionCount;
                        currentLine.SetPosition(reflectionCount - 1, offsetVect);

                    }
                    vectDirection = Vector3.Reflect(vectDirection, hit.normal);
                    position = (Vector2)vectDirection.normalized + hit.point;
                    currentLine.SetPosition(reflectionCount - 1, hit.point);
                    
                }
                else
                {
                    Vector2 prevPos = currentLine.GetPosition(reflectionCount - 1);
                    Vector2 prevPosLocal = prevPos + (-prevPos);
                    Vector2 endPos =  position + (vectDirection * lineMaxDistance);
                    Vector2 endPosLocal = new Vector2(endPos.x - prevPos.x, endPos.y - prevPos.y);

                    float distance = Vector2.Distance(prevPos, endPos);
                    int iceDivideCount = (int)(distance / iceVariation);
                    for (int j = 1; j <= iceDivideCount; j++)
                    {
                        Vector2 midPos = new Vector2(prevPos.x + (float)j / iceDivideCount * (endPos.x - prevPos.x), prevPos.y + (float)j / iceDivideCount * (endPos.y - prevPos.y));
                        Vector2 midPosLocal = new Vector2(prevPosLocal.x + (float)j / iceDivideCount * (endPosLocal.x - prevPosLocal.x), prevPosLocal.y + (float)j / iceDivideCount * (endPosLocal.y - prevPosLocal.y));

                        Vector2 perpVect = iceVariation * Vector2.Perpendicular(midPosLocal.normalized);
                        Vector2 offsetVect;

                        if (j % 2 == 0)
                            offsetVect = midPos + perpVect;
                        else
                            offsetVect = midPos - perpVect;

                        reflectionCount++;
                        currentLine.positionCount = reflectionCount;
                        currentLine.SetPosition(reflectionCount - 1, offsetVect);

                    }
                    canShoot = false;
                }
                if (reflectionCount > numberMaxReflect*10)
                    canShoot = false;
            }

        }

    }
    void DrawMudLasers()
    {
        var layerMask = 1 << LayerMask.NameToLayer("Wall");
        for (int i = 0; i < lineRenderers.Count; i++)
        {

            canShoot = true;
            reflectionCount = 1;
            var currentLine = lineRenderers[i];
            position = lines[i].GetComponent<Transform>().position;
            float angle = 180f - (i + 1) * 41;
            vectDirection = RadianToVector2(angle * Mathf.Deg2Rad) * lines[i].GetComponent<Transform>().transform.parent.gameObject.transform.localScale;
            currentLine.SetPosition(0, position);
            currentLine.material = mudMaterial;
            currentLine.textureMode = LineTextureMode.Tile;
            currentLine.startWidth = 0.3f;
            currentLine.endWidth = 0.3f;

            while (canShoot)
            {
                RaycastHit2D hit = Physics2D.Raycast(position, vectDirection, lineMaxDistance, layerMask);
                if (hit)
                {
                    reflectionCount++;
                    currentLine.positionCount = reflectionCount;
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
        Color iceColor = new Color(0, 179, 239);

        if (currentGroundType == "Normal")
        {
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(Color.black, 0.2f), new GradientColorKey(Color.gray, 0.0f), new GradientColorKey(Color.white, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(alpha - 0.5f, 0.0f), new GradientAlphaKey(alpha, 0.5f), new GradientAlphaKey(alpha, 1.0f) }
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
                alpha -= 0.1f;
            }

        }
        else if(currentGroundType == "Ice")
        {
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(iceColor, 0f), new GradientColorKey(iceColor, 0.0f), new GradientColorKey(iceColor, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(alpha - 0.5f, 0.0f), new GradientAlphaKey(alpha, 0.5f), new GradientAlphaKey(alpha, 1.0f) }
            );
            while (true)
            {
                gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(iceColor, 0.2f), new GradientColorKey(iceColor, 0.0f), new GradientColorKey(iceColor, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(alpha - 0.5f, 1.0f), new GradientAlphaKey(alpha, 0.4f), new GradientAlphaKey(alpha, 0.0f) }
            );
                for (int i = 0; i < lineRenderers.Count; i++)
                {
                    var currentLine = lineRenderers[i];
                    currentLine.colorGradient = gradient;
                }
                if (stepCount % 2 == 0)
                {
                    yield return new WaitForSeconds(0.05f);
                    alpha -= 0.2f;
                }
                else
                {
                    yield return new WaitForSeconds(0.02f);
                    alpha -= 0.3f;
                }

            }
        }
        else if (currentGroundType == "Mud")
        {
            gradient.SetKeys(
                new GradientColorKey[] { new GradientColorKey(Color.black, 0.2f), new GradientColorKey(Color.gray, 0.0f), new GradientColorKey(Color.white, 1.0f) },
                new GradientAlphaKey[] { new GradientAlphaKey(alpha - 0.5f, 0.0f), new GradientAlphaKey(alpha, 0.5f), new GradientAlphaKey(alpha, 1.0f) }
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
                alpha -= 0.1f;
            }

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
