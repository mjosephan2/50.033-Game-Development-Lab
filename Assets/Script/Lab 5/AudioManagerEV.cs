using UnityEngine;
public class AudioManagerEV : MonoBehaviour
{
    public AudioSource stompAudio;
    public AudioSource coinAudio;
    public AudioSource playerDieAudio;
    public AudioSource themeAudio;

    public void stopAudio(AudioSource audio){
        audio.Stop();
    }

    public void playAudioOnce(AudioSource audio){
        audio.PlayOneShot(audio.clip);
    }
}