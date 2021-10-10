using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CherryController : MonoBehaviour
{
    private float cherryTimer = 0.0f;
    public GameObject cherry;
    private float cherryMoveWait = 10.0f;
    private Vector3[] cherrySpawnPoint;
    private Tweener cherryTweener;
    private Vector3 cherryNewPos;
    private int arrayPos = 0;


    // Start is called before the first frame update
    void Start()
    {
        //cherryTimer += Time.time;
        cherrySpawnPoint = new Vector3[4];
        cherryTweener = gameObject.GetComponent<Tweener>();
        cherrySpawnPoint[0] = new Vector3(-12.5f, 13.5f, 0);
        cherrySpawnPoint[1] = new Vector3(12.5f, 13.5f, 0);
        cherrySpawnPoint[2] = new Vector3(12.5f, -13.5f, 0);
        cherrySpawnPoint[3] = new Vector3(-12.5f, -13.5f, 0);
        
        cherry.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        cherryTimer += Time.deltaTime;
        if (cherryTimer > cherryMoveWait)
        {
            SpawnCherry();
            cherryTimer = 0;
            //cherryTimer += Time.deltaTime;
        }
    }

    void SpawnCherry()
    {
        //for (int i = 0; i < cherrySpawnPoint.Length; i++)
        //{

        if (arrayPos <= 3)
        {
            cherryNewPos = cherrySpawnPoint[arrayPos];

            //cherry.SetActive(true);
            cherry.transform.position = cherryNewPos;
            StartCoroutine(MoveCherry(cherryNewPos));
            arrayPos++;
        }
        else
        {
            arrayPos = 0;
            cherry.SetActive(false);
        }
        //}
        
    }

    private IEnumerator MoveCherry(Vector3 currentPos)
    {
        if (currentPos == cherrySpawnPoint[0])
        {
            //cherryTweener.AddTween(cherry.transform, cherry.transform.position, cherrySpawnPoint[1], 1.75f);
            //Instantiate(cherry, currentPos, cherry.transform.rotation);
            cherry.SetActive(true);
            CreateCherryTween(cherrySpawnPoint[1], 2f);
            //currentPos = cherrySpawnPoint[1];
            //cherryTweener.AddTween(cherry.transform, currentPos, cherrySpawnPoint[1], 1.75f);
            //gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            
        }
        else if (currentPos == cherrySpawnPoint[1])
        {
            //cherryTweener.AddTween(cherry.transform, cherry.transform.position, cherrySpawnPoint[2], 1.75f);
            //Instantiate(cherry, currentPos, cherry.transform.rotation);
            CreateCherryTween(cherrySpawnPoint[2], 2f);
            yield return new WaitForSeconds(0.5f);
        }
        else if (currentPos == cherrySpawnPoint[2])
        {
            //cherryTweener.AddTween(cherry.transform, cherry.transform.position, cherrySpawnPoint[3], 1.75f);
            //Instantiate(cherry, currentPos, cherry.transform.rotation);
            CreateCherryTween(cherrySpawnPoint[3], 2f);
            yield return new WaitForSeconds(0.5f);
        }
        else if (currentPos == cherrySpawnPoint[3])
        {
            //cherryTweener.AddTween(cherry.transform, cherry.transform.position, cherrySpawnPoint[0], 1.75f);
            //Instantiate(cherry, currentPos, cherry.transform.rotation);
            CreateCherryTween(cherrySpawnPoint[0], 2f);
            //cherry.SetActive(false);
            yield return new WaitForSeconds(0.5f);
            //cherry.SetActive(false);
        }

        //gameObject.SetActive(false);
    }

    public void CreateCherryTween(Vector3 endPosition, float duration)
    {
        bool addedTween = cherryTweener.AddTween(cherry.transform, cherry.transform.position, endPosition, duration);
    }

}
