using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HouseAdressSO", menuName = "Scriptable Objects/HouseAdress")]
public class HouseAdressSO : ScriptableObject
{
    public Sprite Icon;
    public List<Adress> Adresses;
}