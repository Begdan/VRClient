using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class ShowUI : MonoBehaviour
{
    [SerializeField] private GameObject DirectHand;
    [SerializeField] private GameObject RayHand;
    [SerializeField] private GameObject UI;

    private ActionBasedController controller;

    private void Start()
    {
        controller = GetComponent<ActionBasedController>();
        bool isPressed = controller.activateAction.action.ReadValue<bool>();
        controller.activateAction.action.performed += Toggle;
    }

    private void Toggle(InputAction.CallbackContext context)
    {
        ActivateUI();
    }


    private void ActivateUI()
    {
        if (!UI.gameObject.activeSelf)
        {
            UI.SetActive(true);
            DirectHand.SetActive(true);
            RayHand.SetActive(false);
            controller.SendHapticImpulse(0.5f, 0.1f);
        }
        else if (UI.gameObject.activeSelf)
        {
            UI.SetActive(false);
            DirectHand.SetActive(false);
            RayHand.SetActive(true);
        }
    }
}
