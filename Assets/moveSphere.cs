using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveSphere : MonoBehaviour
{
    float t = 1;
    int sans = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (sans == 1)
        {
            t = t - Time.deltaTime;
            transform.position = new Vector3(t, transform.position.y, transform.position.z);
            if (t<-1)
            {
                sans = 0;
            }
        }
        else
        {
            t = t + Time.deltaTime;
            transform.position = new Vector3(t, transform.position.y, transform.position.z);
            if (t > 1)
            {
                sans = 1;
            }
        }
    }
}
