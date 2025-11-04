using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;

namespace Core
{
    public interface IAction
    {
        void Cancel();
    }
}