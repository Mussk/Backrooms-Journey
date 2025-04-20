using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoRoomController : MonoBehaviour
{
    [SerializeField]
    private List<VideoPlayer> videoPlayers;

    [SerializeField]
    private List<VideoClip> videoClips;

    [SerializeField]
    int videoPlayDelay;

   [SerializeField]
    private string playerTag;

   
 
    private void Start()
    {
        AssignClipsToPlayers(videoPlayers, videoClips);

    }

    private void AssignClipsToPlayers(List<VideoPlayer> videoPlayers, List<VideoClip> videoClips)
    {
        int randIndex;

        foreach (VideoPlayer videoPlayer in videoPlayers)
        {
            videoPlayer.playOnAwake = false;
            
            randIndex = Random.Range(0, videoClips.Count);   
            videoPlayer.clip = videoClips[randIndex];
            
            videoClips.RemoveAt(randIndex);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            StartCoroutine(PlayVideosWithDelayCoroutine(videoPlayers, videoPlayDelay));
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(playerTag))
        {
            StartCoroutine(StopVideosWithDelayCoroutine(videoPlayers, videoPlayDelay));
        }
    }

    private IEnumerator PlayVideosWithDelayCoroutine(List<VideoPlayer> videoPlayers, int delay)
    {
        foreach (VideoPlayer videoPlayer in videoPlayers)
        {
            videoPlayer.gameObject.SetActive(true);
            videoPlayer.Play();

            yield return new WaitForSeconds(delay);
        }

       
    }

    private IEnumerator StopVideosWithDelayCoroutine(List<VideoPlayer> videoPlayers, int delay)
    {
        foreach (VideoPlayer videoPlayer in videoPlayers)
        {
            videoPlayer.Stop();
            videoPlayer.gameObject.SetActive(false);
           
            yield return new WaitForSeconds(delay);
        }


    }
}
