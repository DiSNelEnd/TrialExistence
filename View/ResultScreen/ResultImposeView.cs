using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResultImposeView : MonoBehaviour
{
    private readonly List<GameObject> frames=new List<GameObject>();
    private int lastId;

    public void Show()
    {
        if (GameRoot.IsGameNotStart) return;
        DestroyFrames();
        Fill();
    }

    private void Fill()
    {
        foreach(var conName in GameRoot.Game.ResultData.ImposeConditions)
        {
            var frame = Instantiate(Resources.Load<ImposeView>("Prefabs/ImposeConditionView"), transform);
            frame.SetConditionName(conName);
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
