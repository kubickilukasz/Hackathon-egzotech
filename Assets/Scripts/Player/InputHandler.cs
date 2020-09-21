using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using WebSocketSharp;


public class InputHandler : MonoBehaviour {

    public class LunaClass {
        public float value;
    }

    [SerializeField]
    private Player player;

    string vertAxis = "Vertical";

    float axis = 0f;

    public void OnValidate() {
        if (player == null)
            Debug.LogError("Brak player w EgzoInputs");
    }

    public void Update() {

        bool touchUp = Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended;
        bool space = Input.GetKeyUp(KeyCode.Space);

        if (touchUp || space) {
            player.MoveButton();
        }
    }
}
