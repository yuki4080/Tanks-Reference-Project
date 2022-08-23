using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tanks.UI
{
	[RequireComponent(typeof(Button))]

	/// <summary>
	/// Tracks opening order of menus and modals to fire the correct back button delegate when Android back button/Esc key is pressed.
	/// </summary>
	public class BackButton : MonoBehaviour
	{
		private static List<BackButton> s_buttonStack = new List<BackButton>();

		protected Button m_BackButton;

		protected Controls input;

		protected virtual void Awake()
		{
			input = new Controls();
			input.Enable();
			input.UI.Cancel.started += ctx => 
			{
				// Make sure we're on top of the stack and are enabled and interactive
				if (s_buttonStack.Count > 0 &&
				    s_buttonStack[s_buttonStack.Count - 1] == this &&
				    m_BackButton.interactable &&
				    m_BackButton.enabled)
				{
					OnBackPressed();
				}
			};
		}

		protected virtual void OnEnable()
		{
			m_BackButton = GetComponent<Button>();

			s_buttonStack.Add(this);
		}

		protected virtual void OnDisable()
		{
			input.Disable();

			// Pop this button
			s_buttonStack.Remove(this);
		}

		//We use the back button logic on this screen to ensure clean closure.
		protected virtual void OnBackPressed()
		{
			m_BackButton.onClick.Invoke();
		}
	}
}