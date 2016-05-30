using UnityEngine;
using System.Collections;

abstract public class AbilityBaseClass : MonoBehaviour
{
    protected ChickController mCC;
    protected float mCDown;

    abstract public void Initialize();
    abstract public void ActivateAbility();
}
