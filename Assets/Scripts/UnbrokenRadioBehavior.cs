using Unity.VisualScripting;
using UnityEngine;
using Unity.Mathematics;
using System.Collections;

public class UnbrokenRadioBehavior : UnbrokenObjects
{
    public AudioSource musicPlayer;
    private AudioClip technoTrack;
    private AudioClip classical;
    private bool radioDestroyable = false;
    private bool classicalPlaying = false;
    void Awake()
    {
        Debug.Log("UnbrokenRadioBehavior instantiated");
        musicPlayer = GetComponent<AudioSource>();
        technoTrack = Resources.Load<AudioClip>("Music/techno gamejam 2025");
        classical = Resources.Load<AudioClip>("Music/hildegard gamejam");
        musicPlayer.clip = technoTrack;
        musicPlayer.Play();
        sFXManager = FindAnyObjectByType<SFXManager>();
    }
    private void ChangeToClassical()
    {
        musicPlayer.Stop();
        musicPlayer.clip = classical;
        musicPlayer.PlayDelayed(0.3f);
        
    }
    IEnumerator MakeRadioDestroyable()
    {
        Debug.Log("Radio is soon destroyable");
        yield return new WaitForSeconds(2);
        radioDestroyable = true;
        Debug.Log("Radio is destroyable");
    }
    public override void IsAttacked()
    {
        Debug.Log("Radio is hit");
        if (!classicalPlaying)
        {
            classicalPlaying = true;
            ChangeToClassical();
            StartCoroutine(MakeRadioDestroyable());
        }
        if (!radioDestroyable) 
        {
            sFXManager.PlayHitSound();
            return;
        }

        base.IsAttacked();
    }
}