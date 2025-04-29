using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IntegerValue", menuName = "ScriptableObjects/IntegerValue")]
public class IntegerValue : ScriptableObject
{

    public int initialValue;
    private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;

}
