using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class SimpleSpriteAnim : MonoBehaviour
{
    public bool enableRefresh = false;
    [SerializeField] List<Sprite> sprites;
    [SerializeField] int refreshFrequency;
    SpriteRenderer rend;
    int i = 0;
    int j = 0;

    IEnumerator Start() {
        rend = GetComponent<SpriteRenderer>();
        while (true) {
            if (enableRefresh) {
                ManualRefreshCall();
                yield return null;
            }
        }
    }

    public void ManualRefreshCall() {
        if (i % refreshFrequency == 0) {
            rend.sprite = sprites[j % sprites.Count];
            j++;
        }
        i++;
    }
}
