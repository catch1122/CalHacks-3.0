using System;
using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
namespace VRStandardAssets.Utils
{
	public class SelectionDropDown : MonoBehaviour
	{
		

		[SerializeField] private AudioSource m_Audio;                       // Reference to the audio source that will play effects when the user looks at it and when it fills.
		[SerializeField] private VRInteractiveItem m_InteractiveItem;       // Reference to the VRInteractiveItem to determine when to fill the bar.
		[SerializeField] private VRInput m_VRInput;                         // Reference to the VRInput to detect button presses.

		private bool m_GazeOver;                                            // Whether the user is currently looking at the bar.
		private float m_Timer;                                              // Used to determine how much of the bar should be filled.
		PointerEventData pointer;

		private void Awake(){
			//initialize variables
			pointer	= new PointerEventData(EventSystem.current);
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




	
	

		private void HandleDown ()
		{
			//simulate mouse click when user taps and gazes over object
			if (m_GazeOver)
				ExecuteEvents.Execute (this.gameObject, pointer, ExecuteEvents.submitHandler);
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