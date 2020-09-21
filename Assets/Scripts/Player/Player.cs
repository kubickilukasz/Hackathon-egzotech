using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class Player : MonoBehaviour
{

    public Action onStartBoostLoading;
    public Action onEndBoostLoading;

    public static Player instance {
        get {
            if(_instance == null) {
                _instance = FindObjectOfType<Player>();
            }
            if(_instance == null) {
                Debug.LogError("No player");
            }
            return _instance;
        }
    }
    static Player _instance;

    public enum MoveState
    {
        OnChangeUp,
        OnChangeDown,
        Up,
        Down
    }

    [SerializeField]
    private Camera camera;

    [SerializeField]
    private PlayerAnimatorController playerAnimatorController;

    [SerializeField]
    private GameObject arrow;

    [SerializeField]
    private GameObject loadShot;

    [SerializeField]
    private Transform arrows;

    [SerializeField]
    private float defaultTimeToShot = 3f;

    [Space()]

    [SerializeField]
    private LayerMask rayToCloseEnemy;

    [SerializeField]
    private LayerMask layerDefault;


    [SerializeField]
    private float speed = 2f;

    [SerializeField]
    private float multiplerLoading = 1.1f;

    [SerializeField]
    private Vector2 upPosition = new Vector2(0, 1);

    [SerializeField]
    private Vector2 downPosition = new Vector2(0, -1);

    [SerializeField]
    private float minTimeOnPath = 0.5f;

    [SerializeField]
    private float minValueInput = 0f;

    [SerializeField]
    private float maxValueInput = 0.6f;

    private MoveState prevMoveState;
    private MoveState actualMoveState = MoveState.Down;
    public MoveState moveState
    {
        get
        {
            return actualMoveState;
        }
        private set
        {
            actualMoveState = value;
            if (actualMoveState != prevMoveState)
            {

                ResetTarget();

            }

            prevMoveState = actualMoveState;

        }
    }


    public float PercentToShot
    {
        get
        {
            return target ? timerToShot / timeToShot : 0f;
        }
    }

    private float timeToShot = 3f;
    private float timerMinTimeOnPath = 0f;
    private float timerToShot = 0f;
    private float distanceToRight;
    private MovableObject target;
    private GameObject targetLoadShot;


    public void MoveButton() {
        if(moveState == MoveState.Down) {
            moveState = MoveState.OnChangeUp;
            timerToShot *= multiplerLoading;
            onStartBoostLoading?.Invoke();
        }
        else if(moveState == MoveState.Up) {
            moveState = MoveState.OnChangeDown;
            timerToShot *= multiplerLoading;
            onStartBoostLoading?.Invoke();
        }
    }

    public void Move()
    {
        // if (pressPressure >= maxValueInput)
        if (MoveState.OnChangeUp == moveState)
        {
            //transform.position = upPosition;


            if (transform.position.y < upPosition.y)
            {
                transform.position += ((Vector3)upPosition - transform.position).normalized * Time.deltaTime * speed;
                moveState = MoveState.OnChangeUp;

            }
            else
            {
                transform.position = upPosition;
                moveState = MoveState.Up;
                timerMinTimeOnPath = 0f;
            }

        }
        else if (MoveState.OnChangeDown == moveState)
        {

            if (transform.position.y > downPosition.y)
            {
                transform.position += ((Vector3)downPosition - transform.position).normalized * Time.deltaTime * speed;
                moveState = MoveState.OnChangeDown;
            }
            else
            {
                transform.position = downPosition;
                moveState = MoveState.Down;
                timerMinTimeOnPath = 0f;
            }


        }



    }


    public void FireArrow()
    {
        var currentArrow = Instantiate(arrow, transform.position, Quaternion.identity);

        currentArrow.GetComponent<Arrow>().SetTarget(target);

        currentArrow.transform.SetParent(arrows);

        target.gameObject.layer = layerDefault;

        timerToShot = 0f;

        ResetTarget();

    }


    void ResetTarget()
    {
        //timerToShot = 0f;

        if (targetLoadShot)
            Destroy(targetLoadShot);

        RaycastHit2D ray = Physics2D.Raycast(transform.position, Vector2.right, distanceToRight, rayToCloseEnemy);

        if (ray.collider && (moveState != MoveState.OnChangeUp || moveState != MoveState.OnChangeDown))
        {
            var movableObject = ray.collider.GetComponent<MovableObject>();

            if (movableObject == null)
                return;

            if(movableObject.transform.position.x - transform.position.x <= 0)
            {
                movableObject.gameObject.layer = layerDefault;
                target = null;
                return;
            }

            target = movableObject;

            timeToShot = movableObject.myParams.strength.val;

            targetLoadShot = Instantiate(loadShot , target.transform);

        }
        else
        {
            target = null;
            timeToShot = defaultTimeToShot;
            playerAnimatorController.SetShotting(false);
        }
    }
   

    void Start()
    {
        transform.position = downPosition;

        LifePoints.onPlayerHit += ResetTarget;

        Vector3 rightCorner = camera.ViewportToWorldPoint(Vector3.right);

        distanceToRight = Vector3.Distance(transform.position, rightCorner);
    }

    void OnDestroy() {
        LifePoints.onPlayerHit -= ResetTarget;
    }


    void Update()
    {
        timerMinTimeOnPath += Time.deltaTime;
        Move();

        if (timerToShot >= timeToShot && target)
        {
            //timerToShot = 0f;
            FireArrow();

        } else if (!target)
        {
            ResetTarget();
        }
        else
        {
            if (target.transform.position.x - transform.position.x <= 0)
            {
                target = null;
                if (targetLoadShot)
                    Destroy(targetLoadShot);
            }
            else
            {

                if (targetLoadShot)
                    targetLoadShot.GetComponent<LoadShot>().SetPercent(PercentToShot);

                playerAnimatorController.SetShotting(true, PercentToShot);

            }
        }

        timerToShot += Time.deltaTime;

    }

    public void OnValidate()
    {
        if (arrow == null)
            Debug.LogError("Brak Arrow dla Player");
    }

}
