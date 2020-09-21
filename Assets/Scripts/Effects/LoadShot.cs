using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadShot : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer renderer;

    [SerializeField]
    private Sprite [] sprites;

    [SerializeField][Range(0,1)]
    float percent = 0f;

    public void SetPercent(float p)
    {
        percent = p;
    }

    void Update()
    {

        int index = (int)(sprites.Length * percent);

        if (index >= sprites.Length)
            index = sprites.Length - 1;

        if (index < 0)
            index = 0;

        renderer.sprite = sprites[sprites.Length - index - 1];

    }


}
