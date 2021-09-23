using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private GameObject character;
    private Tweener tweener;
    public Animator animatorController;
    private float timer = 0f;
    const float moveWait = 2.0f;
    private int moveCycle = 0;

    // Start is called before the first frame update
    void Start()
    {
        tweener = gameObject.GetComponent<Tweener>();
        //animatorController.SetTrigger("UpParam");
        //CreateTween(new Vector3(-12.5f, 13.5f, 0), 1.5f);
    }

    // Update is called once per frame
    void Update()
    {

        MoveCharacter();

        /*
        // (-20.5, 12.5, 0)
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            animatorController.SetTrigger("UpParam");
            CreateTween(new Vector3(-12.5f, 13.5f, 0), 1.5f);
        }

        // (-15.5, 8.5, 0)
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            animatorController.SetTrigger("DownParam");
            CreateTween(new Vector3(-7.5f, 9.5f, 0), 1.5f);
        }

        // (-20.5, 8.5, 0)
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            animatorController.SetTrigger("LeftParam");
            CreateTween(new Vector3(-12.5f, 9.5f, 0), 1.5f);
        }

        // (-15.5, 12.5, 0)
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            animatorController.SetTrigger("RightParam");
            CreateTween(new Vector3(-7.5f, 13.5f, 0), 1.5f);
        }
        */
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

    private void MoveCharacter()
    {
        timer += Time.deltaTime;
        if ((int)timer == (int)moveWait && moveCycle == 0)
        {
            timer = 0;
            animatorController.SetTrigger("RightParam");
            CreateTween(new Vector3(-7.5f, 13.5f, 0), 1.5f);
            //character.transform.position += Vector3.right * 5;
            moveCycle++;
        }
        if ((int)timer == (int)moveWait && moveCycle == 1)
        {
            timer = 0;
            animatorController.SetTrigger("DownParam");
            CreateTween(new Vector3(-7.5f, 9.5f, 0), 1.5f);
            //character.transform.position += Vector3.down * 4;
            moveCycle++;
        }
        if ((int)timer == (int)moveWait && moveCycle == 2)
        {
            timer = 0;
            animatorController.SetTrigger("LeftParam");
            CreateTween(new Vector3(-12.5f, 9.5f, 0), 1.5f);
            //character.transform.position += Vector3.left * 5;
            moveCycle++;
        }
        if ((int)timer == (int)moveWait && moveCycle == 3)
        {
            timer = 0;
            animatorController.SetTrigger("UpParam");
            CreateTween(new Vector3(-12.5f, 13.5f, 0), 1.5f);
            //character.transform.position += Vector3.up * 4;
            moveCycle = 0;
        }
    }

}
