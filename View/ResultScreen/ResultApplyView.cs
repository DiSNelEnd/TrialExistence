using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultApplyView : MonoBehaviour
{
    private readonly List<GameObject> frames= new List<GameObject>();

    public void Show()
    {
        if (GameRoot.IsGameNotStart) return;
        DestroyFrames();
        Fill();
    }

    private void Fill()
    {
        foreach (var data in GameRoot.Game.ResultData.ApplyConditions)
        {
            var frame = Instantiate(Resources.Load<ApplyView>("Prefabs/ApplyConditionView"), transform);
            frame.SetData(data.Key,data.Value);
            frames.Add(frame.gameObject);
        }
    }

    private void DestroyFrames()
    {
        foreach (var frame in frames)
            Destroy(frame);
        frames.Clear();
    }
}
