using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Information : MonoBehaviour
{
    private static Dictionary<Type, Func<GameObject>> forms;
    private static Dictionary<string, GameObject> registers;
    private static Dictionary<string, GameObject> icons;
    [SerializeField] private Transform informParent;
    [SerializeField] private Transform iconParent;
    public void Awake()
    {
        registers = new Dictionary<string, GameObject>();
        icons = new Dictionary<string, GameObject>();
        InitialForms();
    }

    public static void CreateRegister(Condition condition)
    {
        var form = GetForm(condition.GetType());
        var conView = form.GetComponent<IConditionView>();
        conView.SetCondition(condition);
        var key = condition.Name;
        CreateIcon(condition);
        if (registers.ContainsKey(key))
            registers[key] = form;
        else
            registers.Add(key, form);
    }

    public static void DeleteRegister(string conName)
    {
        var form= registers.TryGetValue(conName, out var value)
            ? value : throw new KeyNotFoundException("register not found");
        Destroy(form);
        DeleteIcon(conName);
    }

    public static void DeleteAllRegister()
    {
        foreach (var form in registers)
            Destroy(form.Value);
        registers.Clear();
        DeleteAllIcons();
    }

    private static void CreateIcon(Condition condition)
    {
        if (condition.GetType() == typeof(StatusCondition)) return;
        var icon = GetForm(typeof(IconView));
        var iconView=icon.GetComponent<IconView>();
        var name = condition.Name;
        iconView.SetSprite(ImageRepository.GetConditionSprite(name));
        if (icons.ContainsKey(name))
            icons[name] = icon;
        else
            icons.Add(name, icon);
    }

    private static void DeleteAllIcons()
    {
        foreach (var icon in icons)
            Destroy(icon.Value);
        icons.Clear();
    }

    private static void DeleteIcon(string conName)
    {
        var icon = icons.TryGetValue(conName, out var obj)
            ? obj : throw new KeyNotFoundException("icon not found");
        Destroy(icon);
    }

    private static GameObject GetForm(Type type)
    {
        return forms.TryGetValue(type, out var value)
            ? value() : throw new KeyNotFoundException("form not found");
    }

    private void InitialForms()
    {
        forms = new Dictionary<Type, Func<GameObject>>()
        {
            {typeof(BaffCondition),()=> Instantiate(Resources.Load<GameObject>("Prefabs/InformBaff"),informParent) },
            {typeof(InfluenceCondition),()=> Instantiate(Resources.Load<GameObject>("Prefabs/InformInfluence"),informParent)},
            {typeof(HeroicCondition),()=> Instantiate(Resources.Load<GameObject>("Prefabs/InformHeroic"),informParent)},
            {typeof(StatusCondition),()=> Instantiate(Resources.Load<GameObject>("Prefabs/InformStatus"),informParent)},
            {typeof(CriticalCondition),()=> Instantiate(Resources.Load<GameObject>("Prefabs/InformCritical"),informParent)},
            {typeof(DependenceCondition),()=> Instantiate(Resources.Load<GameObject>("Prefabs/InformDepend"),informParent)},
            {typeof(DebaffCondition),()=> Instantiate(Resources.Load<GameObject>("Prefabs/InformDebaff"),informParent)},
            {typeof(IconView),()=> Instantiate(Resources.Load<GameObject>("Prefabs/ConIcon"),iconParent)} 
        };
    }
}
