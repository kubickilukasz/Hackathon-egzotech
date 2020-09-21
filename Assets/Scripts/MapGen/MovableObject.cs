using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

[RequireComponent(typeof(DestroyAnim))]
[RequireComponent(typeof(Collider2D))]
public class MovableObject : MonoBehaviour
{
    public static Action onBossKill;

    public MovableParams minMaxParams;
    public MovableParams myParams { get; private set; }
    public Counter.Enemy enemy;

    [SerializeField] TextMeshPro weight;
    DestroyAnim anim;
    Collider2D col;
    bool initialized;
    const float deltaTimeCompensation = 50;

    public void Init( MovableParams parameters) {
        col = GetComponent<Collider2D>();
        anim = GetComponent<DestroyAnim>();
        myParams = parameters;
        initialized = true;
        weight.text = myParams.strength.val.ToString("F1");
    }


    public void Kill(bool countPoints = true)
    {
        if(countPoints)
            Counter.instance.AddPoint(enemy);
        anim.DestroyMe();
        if (enemy == Counter.Enemy.Broccoli) {
            onBossKill?.Invoke();
        }
    }

    void Update() {
        if (!initialized) {
            return;
        }
        UpdatePosition();
    }

    void UpdatePosition() {
        transform.position = transform.position + (Vector3.left * myParams.speed.val
            * Time.deltaTime * deltaTimeCompensation);
        if(Player.instance.transform.position.x > transform.position.x) {
            col.enabled = false;
            anim.DestroyMe();
        }
    }
}
