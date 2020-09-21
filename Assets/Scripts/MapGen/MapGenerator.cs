using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour {

    [System.Serializable]
    public class Stream {

        [SerializeField] float yPos;
        
        List<GameObject> prefabs;
        Transform parent;

        public void Init(List<GameObject> prefabs,Transform parent) {
            this.prefabs = prefabs;
            this.parent = parent;
        }

        public IEnumerator KeepGenerating() {
            while (true) {
                GameObject randomPrefab = prefabs[UnityEngine.Random.Range(0, prefabs.Count)];
                MovableParams data;
                Generate(randomPrefab, out data);
                yield return new WaitForSeconds(data.strength.val + data.delay.val);
            }
        }

        public MovableObject Generate(GameObject prefab, out MovableParams data) {
            data = Instantiate(prefab.GetComponent<MovableObject>().minMaxParams);
            data.RandomizeParams();
            GameObject go = Instantiate(prefab, parent);
            go.transform.position = new Vector3(parent.position.x, yPos, parent.position.z);
            MovableObject obj = go.GetComponent<MovableObject>();
            obj.Init(data);
            return obj;
        }
    }

    [SerializeField] List<GameObject> prefabs;
    [SerializeField] List<Stream> streams;
    [Space]
    [SerializeField] GameObject boss;
    [SerializeField] Counter counter;

    List<Coroutine> coroutines;
    int[] targetCount = { 1, 1, 0 };
    bool spawnedBoss;

    void Start() {
        coroutines = new List<Coroutine>();
        //counter.onPointChange += CheckBoss;
        foreach(Stream stream in streams) {
            stream.Init(prefabs, transform);
            coroutines.Add(StartCoroutine(stream.KeepGenerating()));
        }
    }

    private void CheckBoss(int[] count) {
        for (int i = 0; i < count.Length - 1; i++) { //-1 is cause boss
            if(count[i] < targetCount[i]) {
                return;
            }
        }
        SpawnBoss();
    }

    public void SpawnBoss() {
        if (spawnedBoss) {
            return;
        }
        foreach(Coroutine cor in coroutines) {
            StopCoroutine(cor);
        }
        MovableParams data;
        streams[0].Generate(boss, out data);
        spawnedBoss = true;
    }
}
