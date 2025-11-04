using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Combat;
using Movement;
using UnityEngine;

namespace Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction currenAction;
         
        public void StartAction(IAction action)
        {
            if (currenAction == action) return;
            if (currenAction != null)
            {
                currenAction.Cancel();
            }
            currenAction = action;
        }
        
    }
}