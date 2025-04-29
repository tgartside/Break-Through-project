using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "VectorValue", menuName = "ScriptableObjects/VectorValue")]
public class VectorValue : ScriptableObject
{

    public Vector2 initialValue;
    private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;

}
