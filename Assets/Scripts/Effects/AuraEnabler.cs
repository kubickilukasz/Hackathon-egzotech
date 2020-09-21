using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class AuraEnabler : MonoBehaviour {

    SimpleSpriteAnim spriteAnim;

    private void Start() {
        spriteAnim = GetComponent<SimpleSpriteAnim>();
        Player.instance.onStartBoostLoading += () => SetAura(true);
        Player.instance.onEndBoostLoading += () => SetAura(false);
        SetAura(false);
    }

    private void OnDestroy() {
        Player.instance.onStartBoostLoading -= () => SetAura(true);
        Player.instance.onEndBoostLoading -= () => SetAura(false);
    }

    void SetAura(bool enabled) {
        gameObject.SetActive(enabled);
        spriteAnim.enableRefresh = enabled;
    }

}