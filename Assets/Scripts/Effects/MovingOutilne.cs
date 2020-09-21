using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[RequireComponent(typeof(Outline))]
public class MovingOutilne : MonoBehaviour
{
    [SerializeField] float period;
    [SerializeField] Vector2 min;
    [SerializeField] Vector2 max;
    [SerializeField] Ease ease;

    private void Start() {
        Outline outline = GetComponent<Outline>();
        outline.effectDistance = min;
        DOTween.To(() => outline.effectDistance, x => outline.effectDistance = x, max, period)
            .SetLoops(-1, LoopType.Yoyo).SetEase(ease);
        
    }
}
