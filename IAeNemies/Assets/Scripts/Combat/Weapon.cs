using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Weapon", menuName = "Scriptable Objects/Weapon")]
public class Weapon : ScriptableObject
{
    [SerializeField] AnimatorOverrideController animatorOverride = null;
    [SerializeField] private GameObject weaponPrefab;

    public void Spawn(Transform handTransform, Animator animator)
    {
        Instantiate(weaponPrefab, handTransform);
        animator.runtimeAnimatorController = animatorOverride;
    }
}
