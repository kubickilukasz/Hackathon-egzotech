using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System;


public class LifePoints : MonoBehaviour
{
    public static Action onPlayerDeath;
    public static Action onPlayerHit;

    public UnityEventFloat onPlayerBecomeInvincible;

    [SerializeField]
    private LayerMask layerToSet;

    [SerializeField]
    private int points = 3;

    [SerializeField]
    private float invincibleDuration = 3;

    public bool isHit { private set; get; }
    private float elapsedInvincibleDuration = 0f;


    public int Points
    {
        get
        {
            return points;
        }
    }


    public void Hit(int p = 1)
    {
        points -= p;

        isHit = true;

        onPlayerHit?.Invoke();
        onPlayerBecomeInvincible?.Invoke(invincibleDuration);

        if (points <= 0)
        {
            onPlayerDeath?.Invoke();
        }

    }

    public void Add(int p = 1)
    {
        points += p;
    }


    public void OnTriggerEnter2D(Collider2D col)
    {
       
        var movableObject = col.GetComponent<MovableObject>();
        if (movableObject == null)
            return;

        col.gameObject.layer = layerToSet;

        if (isHit)
            return;

        movableObject.Kill(false);
        Hit();
        

    }

    void Update()
    {

        if(isHit)
            elapsedInvincibleDuration += Time.deltaTime;

        if (elapsedInvincibleDuration > invincibleDuration)
        {
            isHit = false;
            elapsedInvincibleDuration = 0f;
        }

    }


    
}