using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Animal : MonoBehaviour
{

    [SerializeField, Min(.1f), Header("Animal speed")] private float speed = 3f;
    public UnityEvent moveEvent;
    public UnityEvent idleEvent;

    private void Awake() => StartCoroutine(idle());

    IEnumerator idle()
    {
        idleEvent.Invoke();
        
        while (true)
        {
            if(Random.value <= .30f) break;
            else if(Random.value <= .20f)
                transform.rotation = Quaternion.Euler(0, transform.rotation.y == 0 ? 180 : 0, 0);
            yield return new WaitForSeconds(1); 
        }

        StartCoroutine(Move());
    }
    
    IEnumerator Move()
    {
        moveEvent.Invoke();
        
        while (true)
        {
            transform.Translate(Vector2.right * (Time.deltaTime * speed));
            if(Random.value <= .004f) break;
            yield return null;
        }

        StartCoroutine(idle());
    }

}
