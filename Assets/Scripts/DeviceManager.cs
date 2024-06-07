using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;
using DG.Tweening;
using Sirenix.OdinInspector;

public class DeviceManager : MonoBehaviour
{
    public static DeviceManager Instance { get; private set; }
    public List<Device> devices;
    CinemachineCamera cm;
    Device lastDevice = null;

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
        cm = FindFirstObjectByType<CinemachineCamera>();
    }

    void Update()
    {
        
    }

    [Button]
    public void SelectDevice (int index)
    {
        if (lastDevice != null)
        {
            lastDevice.StopOutline();
            lastDevice = null;
        }

        Device device = devices[index];
        cm.Target.TrackingTarget.transform.DOMove(device.transform.position, 2f);
        device.StartOutline();
        lastDevice = device;
    }
}
