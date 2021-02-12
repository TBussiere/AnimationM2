using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimConScript : MonoBehaviour
{
    private Animator myAnimator;
    private CharacterController controller;

    public LayerMask lm;

    [Range(0,1)]
    public float distanceToGround;
    void Start()
    {
        myAnimator = GetComponent<Animator>();
        controller = GetComponent<CharacterController>();

    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"));
        myAnimator.SetFloat("Vspeed", Input.GetAxis("Vertical"));
        myAnimator.SetFloat("Hspeed", Input.GetAxis("Horizontal"));
        
        if (Input.GetButtonDown("Jump"))
        {
            myAnimator.SetBool("Jump", true);
            myAnimator.SetBool("Grounded", false);
        }
        else if (myAnimator.GetCurrentAnimatorStateInfo(0).IsName("Jump"))
        {
            myAnimator.SetBool("Jump", false);
        }
        else
        {
            myAnimator.SetBool("Grounded", controller.isGrounded);
        }

        //Debug.Log("vSpeed = " + Input.GetAxis("Vertical"));
    }

    private void OnAnimatorIK(int layerIndex)
    {
        myAnimator.SetIKPositionWeight(AvatarIKGoal.LeftFoot, 1f);
        myAnimator.SetIKRotationWeight(AvatarIKGoal.LeftFoot, 1f);
        myAnimator.SetIKPositionWeight(AvatarIKGoal.RightFoot, 1f);
        myAnimator.SetIKRotationWeight(AvatarIKGoal.RightFoot, 1f);

        RaycastHit hit;
        Ray r = new Ray(myAnimator.GetIKPosition(AvatarIKGoal.LeftFoot) + Vector3.up, Vector3.down);

        if (Physics.Raycast(r,out hit, distanceToGround + 1f,lm))
        {
            if (hit.transform.tag == "Terrain")
            {
                Vector3 pos = hit.point;
                pos.y += distanceToGround;
                myAnimator.SetIKPosition(AvatarIKGoal.LeftFoot, pos);
                myAnimator.SetIKRotation(AvatarIKGoal.LeftFoot, Quaternion.LookRotation(transform.forward, hit.normal));
            }
        }

        r = new Ray(myAnimator.GetIKPosition(AvatarIKGoal.RightFoot) + Vector3.up, Vector3.down);

        if (Physics.Raycast(r, out hit, distanceToGround + 1f, lm))
        {
            if (hit.transform.tag == "Terrain")
            {
                Vector3 pos = hit.point;
                pos.y += distanceToGround;
                myAnimator.SetIKPosition(AvatarIKGoal.RightFoot, pos);
                myAnimator.SetIKRotation(AvatarIKGoal.RightFoot, Quaternion.LookRotation(transform.forward, hit.normal));
            }
        }
    }
}
