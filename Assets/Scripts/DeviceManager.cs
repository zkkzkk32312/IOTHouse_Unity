using System.Collections.Generic;
using UnityEngine;
using Unity.Cinemachine;
using DG.Tweening;
using Sirenix.OdinInspector;
using System.Linq;

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

    public void SelectDevice (string id)
    {
        if (lastDevice != null)
        {
            lastDevice.StopOutline();
            lastDevice = null;
        }

        Device device = devices.FirstOrDefault(x => x.ID == id);
        cm.Target.TrackingTarget.transform.DOMove(device.transform.position, 2f);
        device.StartOutline();
        lastDevice = device;
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
