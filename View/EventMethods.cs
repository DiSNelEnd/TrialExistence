using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EventMethods
{
    private static Dictionary<string, Dictionary<string, Dictionary<int, Action>>> methods;
    public static Action GetMethod(string animatorName,string animationName,int eventNumber)
    {
        return methods.TryGetValue(animatorName, out var animDic)
            ? animDic.TryGetValue(animationName, out var actionDic)
            ? actionDic.TryGetValue(eventNumber, out var method) 
            ? method : null 
            : null 
            : null;   
    }
    #region InitMethods
    public static void InitialMethods()
    {
        methods = new Dictionary<string, Dictionary<string, Dictionary<int, Action>>>
        {
            {"MenuBackground",new Dictionary<string, Dictionary<int, Action>>
            {
                {"StartMenuIdle",new Dictionary<int,Action>
                {
                    {0,DoMenuBackgroundStartMenuIdle0},
                    {1,DoMenuBackgroundStartMenuIdle1}
                }},
                {"BeginGame",new Dictionary<int, Action>
                {
                    {0, DoMenuBackgroundStartMenuIdle0},
                    {1, DoMenuBackgroundStartMenuIdle0},
                    {2, DoMenuBackgroundBeginGame2},
                    {3, DoMenuBackgroundStartMenuIdle1 },
                    {4, DoMenuBackgroundBeginGame4 },
                }},
                {"InGameMenuIdle",new Dictionary<int, Action>
                {
                    {0,DoMenuBackgroundInGameMenuIdle0 },
                }}
            }},
            {"MenuButtons",new Dictionary<string, Dictionary<int, Action>> 
            {
                {"Highlighted",new Dictionary<int, Action>
                {
                    {0,DoMenuButtonsHighlighted0},
                }},
            }},
            {"PlayGame",new Dictionary<string, Dictionary<int, Action>>
            {
                {"PlayAnimStartGame",new Dictionary<int, Action>
                {
                    {0,DoPlayGamePlayAnimStartGame0},
                    {1,DoPlayGamePlayAnimStartGame1},
                }},
                {"PlayAnimGameProcces",new Dictionary<int, Action>
                {
                    {0,DoPlayGamePlayAnimGameProcces0},
                }},
                {"PlayAnimDead",new Dictionary<int, Action>
                {
                    {0,DoPlayGamePlayAnimGameProcces0},
                    {1,DoPlayGamePlayAnimDead1},
                    {2,DoPlayGamePlayAnimStartGame0},
                    {3,DoPlayGamePlayAnimStartGame1},
                }},
            }},
            {"PersoneTest", new Dictionary<string, Dictionary<int, Action>>
            {
                {"CloseAnim", new Dictionary<int, Action>
                {
                    {0,DoPersoneTestCloseAnim0}
                }}
            }},
        };
    }
    #endregion
    #region Methods
    private static void DoPlayGamePlayAnimDead1()
    {
        CutSceenSoundsControl.Instance.PlaySongPlayGameDead();
    }

    private static void DoPlayGamePlayAnimGameProcces0()
    {
        CutSceenSoundsControl.Instance.PlaySongPlayGameJump();
    }

    private static void DoPlayGamePlayAnimStartGame0()
    {
        CutSceenSoundsControl.Instance.PlaySongPlayGameMenuWalk();
    }

    private static void DoPlayGamePlayAnimStartGame1()
    {
        CutSceenSoundsControl.Instance.PlaySongPlayGameMenuSelect();
    }

    private static void DoMenuBackgroundStartMenuIdle0()
    {
        MenuGame.Instance.PlaySoundStartMenuLamp0();
    }

    private static void DoMenuBackgroundStartMenuIdle1()
    {
        MenuGame.Instance.PlaySoundStartMenuLamp1();
    }

    private static void DoMenuBackgroundBeginGame2()
    {
        MenuGame.Instance.PlaySoundBeginLamp0();
    }

    private static void DoMenuBackgroundBeginGame4()
    {
        MenuGame.Instance.PlaySoundBeginLamp1();
        MenuGame.Instance.BeginGameAnimStart(false);
    }

    private static void DoMenuBackgroundInGameMenuIdle0()
    {
        MenuGame.Instance.PlayInGameEffect();
    }

    private static void DoMenuButtonsHighlighted0()
    {
        MenuGame.Instance.PlaySongMenuButton0();
    }

    private static void DoPersoneTestCloseAnim0()
    {
        PlayerAnimationControl.Instance.StopLastAnim();
    }
}
    #endregion
