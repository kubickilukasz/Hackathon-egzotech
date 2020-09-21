using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class FlameAnim : MonoBehaviour
{
    [SerializeField] List<Sprite> sprites;
    [SerializeField] int frequency = 1;

    SpriteRenderer spriteRenderer;
    int counter;
    int currentSpriteIndex;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update() {
        transform.rotation = Quaternion.identity;   
        if(counter % frequency == 0) {
            currentSpriteIndex++;
            if(sprites.Count == currentSpriteIndex) {
                currentSpriteIndex = 0;
            }
            spriteRenderer.sprite = sprites[currentSpriteIndex];
        }
        counter++;
    }
}
