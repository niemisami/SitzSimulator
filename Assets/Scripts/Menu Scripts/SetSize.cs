﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class SetSize : MonoBehaviour
{
    public Button butt;
    private float width;
    private float height;
    public float speedW;
    public float speedH;
    public float Wlimit;
    public float Hlimit;
    // Use this for initialization
    void Start()
    {
        width = 0;
        height = 0;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (width<Wlimit) width += speedW;
        if (height<Hlimit) height += speedH;

        butt.image.rectTransform.sizeDelta = new Vector2(width, height);
        
    }


}
