using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LifeUI : MonoBehaviour
{
    [SerializeField] LifePoints lifePoints;
    [SerializeField] GameObject heartContainer;

    Stack<DestroyAnim> destroyAnims = new Stack<DestroyAnim>();

    private void Start() {
        for (int i = 0; i < lifePoints.Points; i++) {
            DestroyAnim anim = Instantiate(heartContainer, transform).GetComponent<DestroyAnim>();
            destroyAnims.Push(anim);
        }
        LifePoints.onPlayerHit += RemoveHeart;
    }

    private void OnDestroy() {
        LifePoints.onPlayerHit -= RemoveHeart;
    }

    void RemoveHeart() {
        destroyAnims.Pop().DestroyMe();

    }
}
