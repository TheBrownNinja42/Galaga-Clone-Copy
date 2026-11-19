using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponStats", menuName = "Objects!/Weapon Stats")]
public class WeaponStats : ScriptableObject
{
    [SerializeField] private string WeaponName;
    [SerializeField] private int Damage;
    [SerializeField] private float TimeBetweenShots;

    public string Describe()
    {
        return WeaponName + ": " + Damage +  " damage" + TimeBetweenShots + " seconds"; 
    }
}
