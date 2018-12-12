using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Patchsizescript : MonoBehaviour {


    public Image image;
    public float width;
    public float height;
    public float speedW;
    public float speedH;
    public float Wlimit;
    public float Hlimit;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (width > Wlimit) width -= speedW;
        if (height > Hlimit) height -= speedH;

        image.rectTransform.sizeDelta = new Vector2(width, height);

    }


}


