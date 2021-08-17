using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{
    public static Difficulty Instance { get; private set; }
    private float time;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    private void Update()
    {
        time += Time.deltaTime;
    }

    public float GetDifficulty()
    {
        return Mathf.Clamp(Mathf.Log10(time) * 3, 0, 3);
    }

    public void SetTime(float timeToSet)
    {
        time = timeToSet;
    }
}
