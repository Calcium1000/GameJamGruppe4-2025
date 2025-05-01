using UnityEngine;

public class RadioBehaviour : MonoBehaviour
{
    [SerializeField] AudioSource musicPlayer;
    public AudioClip technoTrack;
    public AudioClip classical;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicPlayer = GetComponent<AudioSource>();
        musicPlayer.clip = technoTrack;
        musicPlayer.Play();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
