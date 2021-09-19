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
        // (-20.5, 12.5, 0)
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animatorController.SetTrigger("UpParam");
            CreateTween(new Vector3(-20.5f, 12.5f, 0), 1.5f);
        }

        // (-15.5, 8.5, 0)
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            animatorController.SetTrigger("DownParam");
            CreateTween(new Vector3(-15.5f, 8.5f, 0), 1.5f);
        }

        // (-20.5, 8.5, 0)
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            animatorController.SetTrigger("LeftParam");
            CreateTween(new Vector3(-20.5f, 8.5f, 0), 1.5f);
        }

        // (-15.5, 12.5, 0)
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            animatorController.SetTrigger("RightParam");
            CreateTween(new Vector3(-15.5f, 12.5f, 0), 1.5f);
        }
    }

    public void CreateTween(Vector3 endPosition, float duration)
    {
        //for (int i = 0; i < itemList.Count; i++)
        //{
            bool addedTween = tweener.AddTween(character.transform, character.transform.position, endPosition, duration);
            /*
            if (addedTween)
            {
                break;
            }
            else if (!addedTween)
            {

            }
            */
        //}
    }
}
