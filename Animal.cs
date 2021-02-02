using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class Animal : MonoBehaviour
{

    [SerializeField, Min(.1f), Header("Animal speed")] private float speed = 300f;
    public UnityEvent moveEvent;
    public UnityEvent idleEvent;

    [SerializeField]
    private Vector2 positionGoal = Vector2.zero;

    private SpriteRenderer _spriteRenderer;
    
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        StartCoroutine(idle());
    }

    IEnumerator idle()
    {
        idleEvent.Invoke();

        positionGoal = (Vector2)transform.position + Random.insideUnitCircle * 3;
        
        yield return new WaitForSeconds(Random.value * 5);

        StartCoroutine(Move());
    }
    
    IEnumerator Move()
    {
        moveEvent.Invoke();

        Vector2 posFlip = (Vector2)transform.position - positionGoal;
        _spriteRenderer.flipX = posFlip.x > 0;
        
        while (true)
        {
            Vector2 pos = (positionGoal - (Vector2) transform.position).normalized / speed;
            transform.Translate(pos);

            if (Vector2.Distance(positionGoal, transform.position) <= 1)
                break;
            
            yield return null;
        }

        StartCoroutine(idle());
    }

}
