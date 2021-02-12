using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IKTargetMouvement : MonoBehaviour
{

    public float Speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Horizontal"))
        {
            transform.position += Vector3.right * Time.deltaTime * Speed * Input.GetAxis("Horizontal");
        }
        if (Input.GetButton("Vertical"))
        {
            transform.position += Vector3.forward * Time.deltaTime * Speed * Input.GetAxis("Vertical");
        }
    }
}
