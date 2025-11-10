using UnityEngine;
using UnityEngine.Playables;

public class CinematicTrigger : MonoBehaviour
{
    [SerializeField] PlayableDirector director;
    bool alreadyTriggered = false;
    void Start()
    {
        if (director != null)
        {
            director.Stop();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        
        if (!alreadyTriggered && other.CompareTag("Player"))
        {
            alreadyTriggered = true;
            director.Play();
        }
        
        
    }
}