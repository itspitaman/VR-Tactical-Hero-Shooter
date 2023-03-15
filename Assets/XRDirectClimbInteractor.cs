using System;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class XRDirectClimbInteractor : XRDirectInteractor
{
    public static event Action<string> ClimbHandActivated;
    public static event Action<string> ClimbHandDeactivated;

    private string controllerName;

    protected override void Start()
    {
        base.Start();
        controllerName = gameObject.name;
    }

    protected override void OnSelectEntered(SelectEnterEventArgs interactor)
    {
        base.OnSelectEntered(interactor);
        Debug.Log("ENTERED");

        if (interactor.interactableObject.transform.gameObject.tag == "Climbable")
        {
            ClimbHandActivated?.Invoke(controllerName);
        }

    }

    protected override void OnSelectExited(SelectExitEventArgs interactor)
    {
        base.OnSelectExited(interactor);
        Debug.Log("EXITED");

        ClimbHandDeactivated?.Invoke(controllerName);
    }
}
