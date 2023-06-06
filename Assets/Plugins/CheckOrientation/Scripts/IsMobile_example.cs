
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class IsMobile_example : MonoBehaviour
{

#if !UNITY_EDITOR && UNITY_WEBGL
        [System.Runtime.InteropServices.DllImport("__Internal")]
		private static extern bool IsMobile();

#endif


	public bool isMobile()
	{
#if !UNITY_EDITOR && UNITY_WEBGL
         return IsMobile();
#else
		return false;
#endif

	}
}
