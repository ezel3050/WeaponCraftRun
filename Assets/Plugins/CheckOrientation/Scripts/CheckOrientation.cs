using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckOrientation : MonoBehaviour
{
    [SerializeField]private Transform panelOrientation;
	[SerializeField]private Transform gameCanvas;
	[SerializeField] private Orientation orientationScreen;
	[SerializeField] private bool OrietationinOnlyMobile;
	public static CheckOrientation Instance;
#if !UNITY_EDITOR && UNITY_WEBGL
        [System.Runtime.InteropServices.DllImport("__Internal")]
		private static extern bool IsMobile();

#endif

	private void Awake()
    {
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(this);
		}
		else
		{
			Destroy(gameObject);
		}
    }

    void FixedUpdate()
	{
		if (OrietationinOnlyMobile)
		{
			if (isMobile())
			{
				CheckOrietationScreen();
			}
		}
		else
		{
			CheckOrietationScreen();
		}

		
	}
	private void CheckOrietationScreen()
	{
		if (orientationScreen == Orientation.Landscape)
		{
			if (((RectTransform)gameCanvas).rect.width < ((RectTransform)gameCanvas).rect.height)
			{
				//codes for portrait
				panelOrientation.gameObject.SetActive(true);
			}
			else
			{
				//codes for Landspace;
				panelOrientation.gameObject.SetActive(false);
			}
		}
		else
		{
			if (((RectTransform)gameCanvas).rect.width > ((RectTransform)gameCanvas).rect.height)
			{
				//codes for portrait
				panelOrientation.gameObject.SetActive(true);
			}
			else
			{
				//codes for Landspace;
				panelOrientation.gameObject.SetActive(false);
			}
		}
	}
	public bool isMobile()
	{
#if !UNITY_EDITOR && UNITY_WEBGL
         return IsMobile();
#else
		return false;
#endif

	}

	public enum Orientation
	{ 
	Landscape,
	Portrait
	}
}
