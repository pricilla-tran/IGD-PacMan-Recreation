using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private GameObject character;
    private Tweener tweener;
    public Animator animatorController;

    // Start is called before the first frame update
    void Start()
    {
        tweener = gameObject.GetComponent<Tweener>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animatorController.SetTrigger("UpParam");
        }

        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            animatorController.SetTrigger("DownParam");
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            animatorController.SetTrigger("LeftParam");
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            animatorController.SetTrigger("RightParam");
        }
    }
}
