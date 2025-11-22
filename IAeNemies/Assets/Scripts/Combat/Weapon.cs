using Combat;
using Core;
using Recourses;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] AnimatorOverrideController animatorOverride = null;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] float weaponRange = 1f;
    [SerializeField] float weaponDamage = 1f;
    [SerializeField] float percentageBonus = 0f;
    [SerializeField] private bool isRightHanded = true;
    [SerializeField] Arrow projectile;

    public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
    {
        if (weaponPrefab != null)
        {
            var handTransform = GetTransform(rightHand, leftHand);
            Instantiate(weaponPrefab, handTransform);
        }

        if (animatorOverride != null)
        {
            animator.runtimeAnimatorController = animatorOverride;
        }
        
    }

    private Transform GetTransform(Transform rightHand, Transform leftHand)
    {
        Transform handTransform;
        if (isRightHanded) handTransform = rightHand;
        else handTransform = leftHand;
        return handTransform;
    }

    public bool HasProjectile()
    {
        return projectile != null;
    }

    public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target, GameObject instigator, float calculateDamage)
    {
       Arrow projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
       projectileInstance.SetTarget(target, instigator ,weaponDamage);
    }

    public float GetDamage()
    {
        return weaponDamage;
    }

    public float GetPercentageBonus()
    {
        return percentageBonus;
    }
    public float GetRange()
    {
        return weaponRange;
    }
    
    
}
