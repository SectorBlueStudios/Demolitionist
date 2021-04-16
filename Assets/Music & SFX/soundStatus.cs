using UnityEngine;
using UnityEngine.Audio;

public class soundStatus : MonoBehaviour
{
    public AudioMixer masterMusicMixer;
    public AudioMixer masterSFXMixer;

    public GameObject player;

    void Start()
    {
        player.GetComponent<Player>().LoadPlayer();

        if (player.GetComponent<Player>().musicStatus == true)
        {
            if (masterMusicMixer != null)
                masterMusicMixer.SetFloat("VolumeParam", 0f);
        }
        else
        {
            if (masterMusicMixer != null)
                masterMusicMixer.SetFloat("VolumeParam", -80f);
        }


        if (player.GetComponent<Player>().soundFXStatus == true)
        {
            if (masterSFXMixer != null)
                masterSFXMixer.SetFloat("VolumeParam", 0f);
        }
        else
        {
            if (masterSFXMixer != null)
                masterSFXMixer.SetFloat("VolumeParam", -80f);
        }
    }

    public void MusicEnabled()
    {
        if (masterMusicMixer != null)
            masterMusicMixer.SetFloat("VolumeParam", 0f);
    }
    public void MusicDisabled()
    {
        if (masterMusicMixer != null)
            masterMusicMixer.SetFloat("VolumeParam", -80f);
    }
    public void SFXEnabled()
    {
        if (masterSFXMixer != null)
            masterSFXMixer.SetFloat("VolumeParam", 0f);
    }
    public void SFXDisabled()
    {
        if (masterSFXMixer != null)
            masterSFXMixer.SetFloat("VolumeParam", -80f);
    }
}
