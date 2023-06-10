using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatedEntity : MonoBehaviour
{
    private Animator animator;
    public Animator Animator => animator;
    public string Name => animator.runtimeAnimatorController.name;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        AnimatorsRepository.AddAnimated(this);
    }

    public void OnEventDoing(string animNameAndEventNumber)
    {
        var names= animNameAndEventNumber.Split('!');
        var action = EventMethods.GetMethod(Name, names[0],int.Parse(names[1]));
        action?.Invoke();
    }

    public override bool Equals(object other)
    {
        var ani=other as AnimatedEntity;
       return ani.Name == Name;
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }
}
