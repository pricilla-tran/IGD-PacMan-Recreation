using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    public Animator Ghost1Animator;
    public Animator Ghost2Animator;
    public Animator Ghost3Animator;
    public Animator Ghost4Animator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScaredState()
    {
        Ghost1Animator.SetTrigger("Scared");
        //Ghost2Animator.SetTrigger("Scared");
        //Ghost3Animator.SetTrigger("Scared");
        //Ghost4Animator.SetTrigger("Scared");

    }

}
