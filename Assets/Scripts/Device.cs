using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Device : MonoBehaviour
{
    [SerializeField]
    public string ID;
    [SerializeField]
    public string Type;
    [SerializeField]
    public List<string> TelemetryNames;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
}
