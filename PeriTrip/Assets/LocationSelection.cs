using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
namespace VRStandardAssets.Utils
{
	public class LocationSelection: MonoBehaviour
	{


		[SerializeField] private AudioSource m_Audio;                       // Reference to the audio source that will play effects when the user looks at it and when it fills.
		[SerializeField] private VRInteractiveItem m_InteractiveItem;       // Reference to the VRInteractiveItem to determine when to fill the bar.
		[SerializeField] private VRInput m_VRInput;                         // Reference to the VRInput to detect button presses.

		private bool m_GazeOver;                                            // Whether the user is currently looking at the bar.
		private float m_Timer;               

		private void Awake(){
			//initialize variables
			m_InteractiveItem = GetComponent<VRInteractiveItem> ();
		}
		private void OnEnable ()
		{
			m_VRInput.OnDown += HandleDown;
			m_VRInput.OnUp += HandleUp;

			m_InteractiveItem.OnOver += HandleOver;
			m_InteractiveItem.OnOut += HandleOut;
		}


		private void OnDisable ()
		{
			m_VRInput.OnDown -= HandleDown;
			m_VRInput.OnUp -= HandleUp;

			m_InteractiveItem.OnOver -= HandleOver;
			m_InteractiveItem.OnOut -= HandleOut;
		}






		//when user taps
		private void HandleDown ()
		{
			//user is over object
			if (m_GazeOver) {
				//make 360 image
			}
				
		}

		//when user lets go after clicking
		private void HandleUp ()
		{

		}


		private void HandleOver ()
		{
			// The user is now looking at the object
			m_GazeOver = true;
			//play audio 
			m_Audio.Play();
		}


		private void HandleOut ()
		{
			// The user is no longer looking at the bar.
			m_GazeOver = false;
		}
	}
}