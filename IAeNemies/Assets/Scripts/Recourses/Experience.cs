using System;
using UnityEngine;

namespace Recourses
{
    public class Experience : MonoBehaviour
    {
        [SerializeField] private float experiencePoints = 0;
        public event Action onExperienceGained;

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            onExperienceGained();
        }

        public float GetPoints()
        {
            return experiencePoints;
        }
    }
}
