using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Data/MovableParameters")]
public class MovableParams : ScriptableObject
{
    [System.Serializable]
    public class Parameter {
        public float val {
            set {
                _val = Mathf.Clamp(value, min, max);
            }
            get {
                return _val;
            }
        }
        public float min;
        public float max;
        [SerializeField] float _val;
    }

    public Parameter strength; // in s
    public Parameter delay; // in s how much additional dist to create based on speed
    public Parameter speed;// units/s

    public float Dist {
        get {
            return speed.val * (strength.val + delay.val);
        }
    }

    public void RandomizeParams(bool keepdifficulty = true ) {
        RandomizeParameter(ref speed);
        RandomizeParameter(ref strength);
        RandomizeParameter(ref delay);
    }

    void RandomizeParameter(ref Parameter param) {
        param.val = Random.Range(param.min, param.max);
    }
}
