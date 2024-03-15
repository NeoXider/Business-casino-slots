using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonStart : MonoBehaviour
{
    [SerializeField] private SpinController _spinController;

    private void OnMouseDown()
    {
        _spinController.StartSpin();
    }
}
