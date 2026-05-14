using UnityEngine;
using UnityEngine.Video;

public class StoryButtons : MonoBehaviour
{
    [SerializeField] GameObject[] elementsToShow;
    [SerializeField] GameObject[] elementsToDisable;
    [SerializeField] AudioListener audioListener;
    [SerializeField] VideoPlayer videoPlayer;

    private void Awake()
    {
        videoPlayer.loopPointReached += Skip;
    }
    public void StartStory()
    {
        foreach (var element in elementsToShow)
        {
            element.gameObject.SetActive(true);
        }
        foreach (var element in elementsToDisable)
        {
            element.gameObject.SetActive(false);
        }
        audioListener.enabled = false;
    }
    public void Skip(VideoPlayer videoPlayer)
    {
        foreach (var element in elementsToShow)
        {
            element.gameObject.SetActive(false);
        }
        foreach (var element in elementsToDisable)
        {
            element.gameObject.SetActive(true);
        }
        audioListener.enabled = true;
    }
}
