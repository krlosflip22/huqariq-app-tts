using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomHat : MonoBehaviour {
    [SerializeField] GameObject[] hats;

    void Awake()
    {
        hats[Random.Range(0, 2)].SetActive(true);
    }
}