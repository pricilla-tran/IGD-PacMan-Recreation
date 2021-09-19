using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tweener : MonoBehaviour
{
    private List<Tween> activeTweens;
    float distance;
    float timeFraction;

    // Start is called before the first frame update
    void Start()
    {
        activeTweens = new List<Tween>();
    }

    // Update is called once per frame
    void Update()
    {
        if (activeTweens != null)
        {
            for (int i = 0; i < activeTweens.Count; i++)
            {
                distance = Vector3.Distance(activeTweens[i].Target.position, activeTweens[i].EndPos);
                // Linear Interpolation 
                timeFraction = (Time.time - activeTweens[i].StartTime) / activeTweens[i].Duration;
                // Cubic Ease In Interpolation
                //timeFraction = timeFraction * timeFraction * timeFraction;

                if (distance > 0.1f)
                {
                    activeTweens[i].Target.position = Vector3.Lerp(activeTweens[i].StartPos, activeTweens[i].EndPos, timeFraction);
                }
                else
                {
                    activeTweens[i].Target.position = activeTweens[i].EndPos;
                    activeTweens.Remove(activeTweens[i]);
                }
            }
        }
    }

    public bool AddTween(Transform targetObject, Vector3 startPos, Vector3 endPos, float duration)
    {
        if (activeTweens == null)
        {
            activeTweens.Add(new Tween(targetObject, startPos, endPos, Time.time, duration));
        }

        if (!TweenExists(targetObject))
        {
            //Tween newTween = new Tween(activeTweens[i].StartPos,);
            activeTweens.Add(new Tween(targetObject, startPos, endPos, Time.time, duration));
            return true;
        }
        else
        {
            return false;
        }

    }

    public bool TweenExists(Transform target)
    {
        foreach (Tween tween in activeTweens)
        {
            if (tween.Target.transform == target)
            {
                return true;
            }
        }
        return false;
    }

}
