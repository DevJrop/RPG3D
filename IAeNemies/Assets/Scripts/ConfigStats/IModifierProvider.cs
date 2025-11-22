using System.Collections.Generic;
using UnityEngine;

namespace ConfigStats
{
    public interface IModifierProvider
    {
       IEnumerable<float> GetAdditiveModifiers(Stat stat);
       IEnumerable<float> GetPercentageModifiers(Stat stat);
    }
}
