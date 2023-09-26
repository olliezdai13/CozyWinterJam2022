using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeartContainer : MonoBehaviour
{
    public HeartContainer next;

    public void SetHeart(float count)
    {
        if (count <= 0)
        {
            GetComponent<Image>().color = Color.black;
        } else
        {
            GetComponent<Image>().color = Color.white;
        }
        count--;
        if (next != null)
        {
            next.SetHeart(count);
        }
    }
}
