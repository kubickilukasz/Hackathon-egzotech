using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CanvasGroup))]
public class GameOverScreen : MonoBehaviour
{
    [SerializeField] TMPro.TextMeshProUGUI text;
    [SerializeField] int autoRestartAfterSeconds;

    void Awake() {
        LifePoints.onPlayerDeath += ShowGameOverScreen;
        MovableObject.onBossKill += ShowGameOverScreen;
    }

    private void OnDestroy() {
        LifePoints.onPlayerDeath -= ShowGameOverScreen;
        MovableObject.onBossKill -= ShowGameOverScreen;
    }

    public void SceneReload() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void ShowGameOverScreen() {
        CanvasGroup cg = GetComponent<CanvasGroup>();
        cg.alpha = 1;
        cg.interactable = true;
        StartCoroutine(AutoRestartLoop(autoRestartAfterSeconds));
    }

    IEnumerator AutoRestartLoop(int secondsWait) {
        WaitForSecondsRealtime waitForSeconds = new WaitForSecondsRealtime(1);
        int i = secondsWait;
        while (i > 0) {
            text.text = string.Format("Auto restart in {0} s.", i);
            yield return waitForSeconds;
            i--;
        }
        SceneReload();
    }
}
