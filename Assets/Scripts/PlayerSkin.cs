using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class PlayerSkin : ScriptableObject {

    public new string name;
    public Material[] materials;
    public int cost;
    public bool isDefault = false;
}
