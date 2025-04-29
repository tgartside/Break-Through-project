using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StringValue", menuName = "ScriptableObjects/StringValue")]
public class StringValue : ScriptableObject
{

    public string initialValue;
    private void OnEnable() => hideFlags = HideFlags.DontUnloadUnusedAsset;

}
