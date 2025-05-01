using UnityEngine;

public class RadioBehaviour : MonoBehaviour
{
    public AudioSource musicPlayer;
    private AudioClip technoTrack;
    private AudioClip classical;

    public static bool hitOnce = false;
    private bool classicalPlaying = false;
    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
        technoTrack = Resources.Load<AudioClip>("Music/techno gamejam 2025");
        classical = Resources.Load<AudioClip>("Music/hildegard gamejam");
        musicPlayer.clip = technoTrack;
        musicPlayer.Play();
    }
    public void ChangeToClassical()
    {
        musicPlayer.Stop();
        musicPlayer.clip = classical;
        musicPlayer.PlayDelayed(0.3f);
    }
    private void Update()
    {
        if (hitOnce && !classicalPlaying)
        {
            ChangeToClassical();
            classicalPlaying = true;
        }
    }
}
