using System;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Sirenix.OdinInspector;

[Serializable]
public class Device : MonoBehaviour
{
    [SerializeField]
    public string ID;
    [SerializeField]
    public string Type;
    [SerializeField]
    public List<string> TelemetryNames;
    [SerializeField]
    public bool UseCustomMeshes;
    [SerializeField]
    public List<GameObject> Meshes;

    List<Outline> Outlines = new List<Outline>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!UseCustomMeshes)
        {
            var outline = GetComponent<Outline>();
            Outlines.Add(outline);
        }
        else
        {
            foreach(GameObject go in Meshes)
            {
                var outline = go.GetComponent<Outline>();
                Outlines.Add(outline);
            }
        }
    }

    [Button]
    public void StartOutline()
    {
        foreach (Outline ol in Outlines)
        {
            ol.enabled = true;
            ol.OutlineWidth = 0;
            DOTween.To(() => ol.OutlineWidth, x =>
            {
                ol.OutlineWidth = x;
            }, 6, .7f).SetLoops(-1, LoopType.Yoyo);
        }
    }

    [Button]
    public void StopOutline()
    {
        foreach (Outline ol in Outlines)
        {
            ol.enabled = false;
            ol.OutlineWidth = 0;
        }
    }
}
