using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveRota : MonoBehaviour
{
    float t = 90;
    float min = 60;
    int sans = 1;

    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sans == 1)
        {
            t = t - (Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(new Vector3(t, 0, 0));
            if (t < min)
            {
                sans = 0;
            }
        }
        else
        {
            t = t + (Time.deltaTime * speed);
            transform.rotation = Quaternion.Euler(new Vector3(t, 0, 0));
            if (t > 90)
            {
                sans = 1;
            }
        }
    }
}
