using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Twenning
{

    public static Dictionary<CurveType, Func<float, float>> curveFuncs = new Dictionary<CurveType, Func<float, float>>()
    {
        {CurveType.Linear, (x) => {return x; } },
        {CurveType.Quadratic, (x) => {return 1 - (1-x)*(1-x); } },
        {CurveType.Cubic, (x) => {return 1 - (1-x)*(1-x)*(1-x); } },
        {CurveType.Quartic, (x) => {return 1 - (1-x)*(1-x)*(1-x)*(1-x);} },
        {CurveType.Quintic, (x) => {return 1 - (1-x)*(1-x)*(1-x)*(1-x)*(1-x);} },
        {CurveType.Bounce, (x) =>
        {
            if (x < 0.363636f)
            {
                return 7.5625f * x * x;
            } else if (x < 0.72727f)
            {
                x -= 0.545454f;
                return 7.5625f * x * x + 0.75f;
            } else if (x < 0.909091f)
            {
                x -= 0.919192f;
                return 7.5625f * x * x + 0.9373f;
            } else
            {
                x -= 0.954545f;
                return 7.5625f * x * x + 0.984375f;
            }
        } }
    };

    public static float CurveValue(TweeningCurve curve, float x)
    {
        x = Mathf.Clamp01(x);
        CurveType type = curve.curveType;
        Func<float, float> func = curveFuncs[type];

        return func(x);
    }

    /// <summary>
    /// During action, the value based on tweeningCurve will pass to the Action's parameter.
    /// </summary>
    /// <param TwenningCurve="curve"></param>
    /// <param Duration="time"></param>
    /// <param Action="action"></param>
    /// <param FinishAction="onComplete"></param>
    /// <param ID="id"></param>
    public static IEnumerator StartTweening(TweeningCurve curve, float time, Action<float> action, Action onComplete = null, string id = "")
    {
        float Count = time / Time.fixedDeltaTime; //the number of steps x needs to take
        float step = 1 / Count; //the length of one step

        float x = 0;
        for (int i = 0; i < Count; i++)
        {
            action(CurveValue(curve, x));
            x += step;
            yield return new WaitForFixedUpdate();
        }

        if (onComplete != null) onComplete();
    }
}

public enum CurveType
{
    Linear,
    Quadratic,
    Cubic,
    Quartic,
    Quintic,
    QuadraticDouble,
    CubicDouble,
    QuarticDouble,
    QuinticDouble,
    Sin,
    SinDouble,
    Expo,
    ExpoDouble,
    Elastic,
    ElasticDouble,
    Circ,
    CircDouble,
    Back,
    BackDouble,
    Bounce,
    BounceDouble
}
