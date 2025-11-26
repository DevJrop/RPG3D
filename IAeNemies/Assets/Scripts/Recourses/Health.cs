using System;
using ConfigStats;
using Core;
using UnityEngine;
using UnityEngine.Events;

namespace Recourses
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float regenerationPercentage = 70;
        [SerializeField] private UnityEvent<float> takeDamage;  
        float healthPoints = -1f;
        bool isDead = false;

        private void Start()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
            if (healthPoints < 0)
            {
               healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
            }
        }

        private void RegenerateHealth()
        {
            float regenHealthPoints = GetComponent<BaseStats>().GetStat(Stat.Health) * regenerationPercentage / 100f;
            healthPoints = Mathf.Max(healthPoints, regenHealthPoints);
        }

        public bool IsDead()
        {
            return isDead;
        }
        public void TakeDamage(GameObject instigator, float damage)
        {
            print(gameObject.name + " took damage: " + damage);
            healthPoints = Mathf.Max(healthPoints - damage, 0);
            
            if (healthPoints == 0)
            {
                Die();
                AwardExperience(instigator);
            }
            else
            {
                takeDamage.Invoke(damage);
            }
            
        }

        private void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += RegenerateHealth;
        }

        private void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= RegenerateHealth;
        }

        public float GetHealthPoints()
        {
            return healthPoints;
        }

        public float GetMaxHealthPoints()
        {
            return GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();
            if (experience == null) return;
            
            experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
        }

        public float GetPercentage()
        {
            return 100 * GetFraction();
        }

        public float GetFraction()
        {
            return healthPoints / GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        private void Die()
        {
            if (isDead)return;
            
            isDead = true;
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
        
    }
}