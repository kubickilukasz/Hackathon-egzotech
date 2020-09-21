using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;


public class Counter : MonoBehaviour
{
    public Action<int[]> onPointChange;

    public enum Enemy
    {
        Onion, 
        Carrot, 
        Potato,
        Broccoli
    }

    public static Counter instance;
    
    private int [] points = { 0 , 0 , 0 , 0};

    [Header("0 - Onion, 1 - Carrot, 2 - Potato, 3 - Broccoli")]
    [SerializeField]
    private GameObject [] enemies;

    private GameObject[] enemiesOnScene = { null , null , null , null};


    public void AddPoint(Enemy enemy)
    {
        points[(int)enemy]++;

        if(enemiesOnScene[(int)enemy] == null)
        {
            enemiesOnScene[(int)enemy] = Instantiate(enemies[(int)enemy], transform);
        }

        enemiesOnScene[(int)enemy].GetComponentInChildren<TextMeshProUGUI>().text = points[(int)enemy].ToString();
        onPointChange?.Invoke(points);
    }

    void Awake()
    {
        instance = this;

        LifePoints.onPlayerDeath += () => {

            foreach (Transform child in transform)
            {
                GameObject.Destroy(child.gameObject);
            }
        };
    }



}
