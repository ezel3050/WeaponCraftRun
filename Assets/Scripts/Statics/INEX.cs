using System;
using System.Collections;
using System.Linq;
using UnityEngine;

public class INEX : MonoBehaviour
{
    // VECTOR3 EULER ANGLE LERP
    public static Vector3 LerpEulerAngles(Vector3 a, Vector3 b, float t)
    {
        return new Vector3
        (
            Mathf.LerpAngle(a.x, b.x, t),
            Mathf.LerpAngle(a.y, b.y, t),
            Mathf.LerpAngle(a.z, b.z, t)
        );
    }

    // TURNS TRANSFORM EULER ANGLES TO UNITY INSPECTOR LIKE ONES
    public static float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;
        return angle;
    }

    // TURNS TRANSFORM EULER ANGLES TO UNITY INSPECTOR LIKE ONES
    public static Vector3 WrapAngle(Vector3 rotation)
    {
        rotation.x = WrapAngle(rotation.x);
        rotation.y = WrapAngle(rotation.y);
        rotation.z = WrapAngle(rotation.z);

        return rotation;
    }

    // TURNS UNITY INSPECTOR LIKE ANGLES TO TRANSFORM EULER ANGLES
    public static float UnwrapAngle(float angle)
    {
        if (angle >= 0)
            return angle;

        angle = -angle % 360;

        return 360 - angle;
    }

    // TURNS TRANSFORM EULER ANGLES TO UNITY INSPECTOR LIKE ONES
    public static Vector3 UnwrapAngle(Vector3 rotation)
    {
        rotation.x = UnwrapAngle(rotation.x);
        rotation.y = UnwrapAngle(rotation.y);
        rotation.z = UnwrapAngle(rotation.z);

        return rotation;
    }


    // QUICK SORT IMPLEMENTATION
    private static int Partition(int[] arr, int left, int right)
    {
        int pivot;
        pivot = arr[left];
        while (true)
        {
            while (arr[left] < pivot) left++;

            while (arr[right] > pivot) right--;

            if (left < right) (arr[right], arr[left]) = (arr[left], arr[right]);
            else return right;
        }
    }

    public static void QuickSort(int[] arr, int left, int right)
    {
        int pivot;
        if (left < right)
        {
            pivot = Partition(arr, left, right);
            if (pivot > 1) QuickSort(arr, left, pivot - 1);
            if (pivot + 1 < right) QuickSort(arr, pivot + 1, right);
        }
    }

    // IS ANIMATION PLAYING ANIMATION STATE
    public static bool IsAnimatorPlaying(Animator animator, string stateName) =>
        animator.GetCurrentAnimatorStateInfo(0).IsName(stateName);

    // IS ANIMATION PLAYING ANIMATION STATES
    public static bool IsAnimatorPlaying(Animator animator, string[] stateNames) =>
        stateNames.Any(stateName => animator.GetCurrentAnimatorStateInfo(0).IsName(stateName));


    // CALL ACTION AT THE END OF FRAME
    public static IEnumerator LateAction(Action action)
    {
        yield return new WaitForEndOfFrame();
        action?.Invoke();
    }

    // DELAYED INVOCATION OF ACTION
    public static IEnumerator InvokeAction(Action action, float delay)
    {
        yield return new WaitForSeconds(delay);
        action?.Invoke();
    }

    // FIND CHILD GAME OBJECT
    public static Transform GetChildWithName(Transform parent, string name)
    {
        Transform childTrans = parent.Find(name);
        return childTrans != null ? childTrans : null;
    }
    
    // USE FOLLOWING METHODS TO CHANGE COLOR OF MESH RENDERER
    public static void SetColor(MeshRenderer meshRenderer, int materialIndex, string shaderColorFieldName, Color toColor, bool changeAsset = false)
    {
        if (!changeAsset) meshRenderer.materials[materialIndex].SetColor(shaderColorFieldName, toColor);
        else meshRenderer.sharedMaterials[materialIndex].SetColor(shaderColorFieldName, toColor);
    }
    
    // USE FOLLOWING METHODS TO CHANGE COLOR OF MESH RENDERER
    public static void SetColor(MeshRenderer meshRenderer, string shaderColorFieldName, Color toColor, bool changeAsset = false)
    {
        if (!changeAsset) meshRenderer.material.SetColor(shaderColorFieldName, toColor);
        else meshRenderer.sharedMaterial.SetColor(shaderColorFieldName, toColor);
    }
}
