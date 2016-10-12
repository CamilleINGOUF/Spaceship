using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Done_BGScroller : MonoBehaviour
{
    public float backgroundSpeed = 0.02f;

    void Update()
    {
        float backgroundOffset = Time.timeSinceLevelLoad * backgroundSpeed;

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(0, backgroundOffset);
    }
}
