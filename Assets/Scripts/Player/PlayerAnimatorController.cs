using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimatorController : MonoBehaviour
{


    [SerializeField]
    Animator animator;

    [SerializeField]
    float shotTimeAnimation = 1f;

    private float normalizedTime;

    public void SetShotting(bool shotting , float speed = 1f)
    {

        animator.SetFloat("normalized" , speed );
        animator.SetBool("shotting" , shotting);

    }




    void OnValidate()
    {
        if (animator == null)
            Debug.LogError("None of animator");
    }


    



}
