using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class TimeBillView : MonoBehaviour
{
    private Text time;
    private void Awake()
    {
        time = GetComponent<Text>();
    }

    private void OnEnable()
    {
        if (GameRoot.IsGameNotStart) return;
        if (GameRoot.Game.BillControler.BillNotSet) return;
        time.text = GameRoot.Game.BillControler.Bill.Time;
    }
}
