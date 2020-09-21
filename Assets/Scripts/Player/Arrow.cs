using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Arrow : MonoBehaviour
{

    [SerializeField]
    private float speed;

    private Vector2 direction = new Vector2(1f, 0f );

    private MovableObject target;

    public void SetTarget(MovableObject m)
    {
        target = m;
    }

    void Update()
    {
        transform.Translate(direction * speed);
    }

    public void OnTriggerEnter2D(Collider2D col)
    {

        var movableObject = col.GetComponent<MovableObject>();

        if (movableObject == null || movableObject != target)
            return;

        movableObject.Kill();

        Destroy(gameObject);
        

    }

    public void OnBecameInvisible()
    {
        Destroy(gameObject);
    }




}
