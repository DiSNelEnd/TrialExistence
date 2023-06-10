using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class DayBillView : MonoBehaviour
{
    private Text day;
    private void Awake()
    {
        day = GetComponent<Text>();
    }

    private void OnEnable()
    {
        if (GameRoot.IsGameNotStart) return;
        if (GameRoot.Game.BillControler.BillNotSet) return;
        day.text = GameRoot.Game.BillControler.Bill.Day;
    }
}
