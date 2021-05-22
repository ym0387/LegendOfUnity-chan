using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    //PlayerのAnimatorコンポーネント保存用
    private Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Aを押すとjab
        if (Input.GetKey(KeyCode.A))
        {
            animator.SetBool("Jab", true);
        }

        //Sを押すとHikick
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetBool("Hikick", true);
        }

        //Dを押すとSpinkick
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetBool("Spinkick", true);
        }

    }
}
