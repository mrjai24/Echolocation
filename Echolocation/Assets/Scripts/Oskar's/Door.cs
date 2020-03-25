using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public bool visibile;
    public float maxVisibleDistance = 5f;
    public void ShowDoor(float distance)
    {
        if(!visibile && distance < maxVisibleDistance)
            StartCoroutine(HideDoor(distance));
    }

    public IEnumerator HideDoor(float distance)
    {
        //Debug.Log(distance);
        visibile = true;
        float maxalpha = 100f - (distance / maxVisibleDistance)*100f;
        //Debug.Log(maxalpha);
        for (int i=(int)maxalpha/10;i>=0; i -= 1)
        {
            //Debug.Log(i);
            float alpha =maxalpha/100f;
            //Debug.Log(alpha);
            GetComponent<SpriteRenderer>().color = new Color(255, 255,255, 1f -alpha/i);
            yield return new WaitForSeconds(0.2f);

        }
        visibile = false;

    }

}
