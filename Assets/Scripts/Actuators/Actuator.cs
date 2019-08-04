using UnityEngine;

public abstract class Actuator : MonoBehaviour {
	[SerializeField]
	protected Trigger trigger;

    [Tooltip("If true, actuator works automatically, without the need of being triggered")]
    public bool overrideTrigger = false;

	public void Start() {
        if (!overrideTrigger) {
            this.trigger.AddActuator(this);
        }
        else {
            OnActivate();
        }
    }

	public abstract void OnActivate();
	public abstract void OnDeactivate();
}
