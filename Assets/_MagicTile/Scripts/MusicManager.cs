using TTHUnityBase.Base.DesignPattern;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    [SerializeField] AudioSource clickAudio;

    public void PlayBGSound()
    {
        audioSource.Stop(); // Start playing music
        audioSource.Play(); // Start playing music
    }
    public void OnClickTileSound()
    {
        clickAudio.Play();
    }

}
public class MySFX : SingletonMonoBehaviour<MusicManager> { }
