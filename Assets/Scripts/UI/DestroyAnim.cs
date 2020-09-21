using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DestroyAnim : MonoBehaviour
{
    [SerializeField] float tweenLength = 0.1f;

    public void DestroyMe() {
        Vector3 oldScale = transform.localScale;
        Sequence seq = DOTween.Sequence();
        seq.Append(transform.DOScaleX(0, tweenLength));
        seq.Join(transform.DOScaleY(transform.localScale.y * 2, tweenLength));
        seq.SetEase(Ease.OutCubic);
        seq.onComplete += () => Destroy(gameObject);
    }
}
