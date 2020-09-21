using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Flicker : MonoBehaviour
{
    [SerializeField] SpriteRenderer target;
    [SerializeField] int flickerStrength = 16;
    [SerializeField] float flickerMin = 0.2f;
    [SerializeField] [Range(-1, 1)] float flickerOverTime = 1;

    public void StartFlicker(float duration) {
        target.DOFade(flickerMin, duration).SetEase(Ease.Flash, flickerStrength, flickerOverTime);
    }

}
