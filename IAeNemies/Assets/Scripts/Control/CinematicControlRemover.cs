using Core;
using UnityEngine;
using UnityEngine.Playables;

namespace Control
{
    public class CinematicControlRemover : MonoBehaviour
    {
        private GameObject player;
        void Start()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnableControl;
        }

        void DisableControl(PlayableDirector pd)
        {
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }
        void EnableControl(PlayableDirector pd)
        {
            player.GetComponent<PlayerController>().enabled = true;
            Debug.Log("Enable");
        }
    }
}

