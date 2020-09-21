using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    public float time { private set; get; }
    [SerializeField] TMPro.TextMeshProUGUI text;
    bool stoppedCounting;

    IEnumerator Start()
    {
        LifePoints.onPlayerDeath += () => stoppedCounting = true;
        while (!stoppedCounting) {
            time += Time.deltaTime;
            int s = (int)time;
            int m = (s / 60);
            int h = (m / 60);
            s = s % 60;
            m = m % 60;
            text.text = string.Format("{0} h {1} m {2} s", h, m, s);
            yield return null;
        }
    }

}
