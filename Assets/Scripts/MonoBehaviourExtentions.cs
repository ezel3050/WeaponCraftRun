using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public static class MonoBehaviourExtentions
{
    #region Delayed Invoke

    public static void CallWithDelay(this MonoBehaviour mono, Action action, float delay)
    {
        mono.StartCoroutine(TheCoroutine(action, delay));
    }

    public static IEnumerator TheCoroutine(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    #endregion

    public static string TimeSpanToString(this MonoBehaviour mono, TimeSpan timeSpan)
    {
        string minutesString, secondsString;
        minutesString = timeSpan.Minutes > 0 ? timeSpan.Minutes.ToString() + "m " : "";
        secondsString = timeSpan.Seconds.ToString();
        return minutesString + secondsString + "s";
    }


    #region Shuffling

    // shuffle array extension method
    public static void Shuffle<T>(this T[] array)
    {
        for (int i = 0; i < array.Length; i++)
        {
            T temp = array[i];
            int randomIndex = Random.Range(i, array.Length);
            array[i] = array[randomIndex];
            array[randomIndex] = temp;
        }
    }

    // shuffle list extension method
    public static void Shuffle<T>(this List<T> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            T temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    #endregion
}