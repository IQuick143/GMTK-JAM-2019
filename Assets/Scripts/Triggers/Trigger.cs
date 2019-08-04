using System.Collections.Generic;
using UnityEngine;

public abstract class Trigger : MonoBehaviour {
	[SerializeField]
	protected WireMaster wire;
	protected List<Actuator> triggeredActuators;
	private bool _active = false;

    public void Awake() {
        triggeredActuators = new List<Actuator>();
    }

    public bool active {
		protected set {
			if (_active != value) {
				_active = value;
				UpdateGizmos();
				UpdateWire();
			}
		}
		get {
			return _active;
		}
	}

	void UpdateGizmos() {
		this.triggeredActuators.ForEach(
			(a) => {
				if (this._active) {
					a.OnActivate();
				} else {
					a.OnDeactivate();
				}
			}
		);
	}

	void UpdateWire() {
		if (wire != null) wire.SetState(this._active); 
	}

	public void AddActuator(Actuator a) {
		this.triggeredActuators.Add(a);
	}
}
