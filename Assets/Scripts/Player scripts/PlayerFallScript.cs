using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallScript : MonoBehaviour {
    private float test = 0;
    private Vector3 positionVector;
    public bool fallingToTheRight = false;
    public bool fallingToTheLeft = false;
    private Quaternion rotat;

    // Use this for initialization
    void Start() {


    }

    // Update is called once per frame
    void Update() {
        if (fallingToTheRight)
        {
            positionVector = positionVector + new Vector3(0.1f, -0.2f, 0);
            transform.position = positionVector;
            transform.rotation = Quaternion.Euler(rotat.eulerAngles.x, rotat.eulerAngles.y, rotat.eulerAngles.z+test);
            test = test - 2;
        }
        else if (fallingToTheLeft)
        {
            positionVector = positionVector + new Vector3(-0.1f, -0.2f, 0);
            transform.position = positionVector;
            transform.rotation = Quaternion.Euler(rotat.eulerAngles.x, rotat.eulerAngles.y, rotat.eulerAngles.z + test);
            test = test + 2;
        }
        else
        {
            positionVector = transform.position;
            rotat = transform.rotation;
        }


    }

}
