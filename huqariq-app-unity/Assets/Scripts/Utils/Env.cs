using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Env", menuName = "Enviroment Variable")]
public class Env : ScriptableObject
{
    public string Value;
}

