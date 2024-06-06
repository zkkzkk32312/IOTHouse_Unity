using System.Collections.Generic;
using UnityEngine;

public class DeviceManager : MonoBehaviour
{
    public static DeviceManager Instance { get; private set; }
    public List<Device> devices;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
