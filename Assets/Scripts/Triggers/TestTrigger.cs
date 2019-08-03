using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Quick test to test trigers through the inspector

public class TestTrigger : Trigger
{
    public bool isActive = false;
    private bool wasActive = false;

    private void Update() {
        if (wasActive && !isActive) {
            wasActive = false;
            foreach (Actuator actuator in triggeredActuators)
            {
                actuator.OnDeactivate();
            }
        }
        else if (!wasActive && isActive) {
            wasActive = true;
            foreach (Actuator actuator in triggeredActuators)
            {
                actuator.OnActivate();
            }
        }

    }

}
