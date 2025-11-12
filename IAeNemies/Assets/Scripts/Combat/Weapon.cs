using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] AnimatorOverrideController animatorOverride = null;
    [SerializeField] private GameObject weaponPrefab;
    [SerializeField] float weaponRange = 2f;
    [SerializeField] float weaponDamage = 5f;
    [SerializeField] private bool isRightHanded = true;

    public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
    {
        if (weaponPrefab != null)
        {
            Transform handTransform;
            if (isRightHanded) handTransform = rightHand;
            else handTransform = leftHand;
            Instantiate(weaponPrefab, handTransform);
        }

        if (animatorOverride != null)
        {
            animator.runtimeAnimatorController = animatorOverride;
        }
        
    }

    public float GetDamage()
    {
        return weaponDamage;
    }
    
    public float GetRange()
    {
        return weaponRange;
    }
}
