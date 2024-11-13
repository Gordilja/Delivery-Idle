using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RestaurantAdressSO", menuName = "Scriptable Objects/RestaurantAdress")]
public class RestaurantAdressSO : ScriptableObject
{
    public Sprite Icon;
    public List<Adress> Adresses;
}