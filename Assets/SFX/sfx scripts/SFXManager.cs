using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] AudioSource SFXSource;

    public AudioClip[] dialogueAv;
    public AudioClip[] doorSounds;
    public AudioClip[] femmeAv;
    public AudioClip[] hitSounds;
    public AudioClip[] mascAv;
    public AudioClip[] swingSounds;
    public AudioClip[] toiletDoorSounds;

    System.Random rand = new System.Random(); //RNG used for playing random sound from list
    int currentRandNum;
    int previousRandNum;


    private static SFXManager instance;

    // Makes the game object persist between scenes
    private void Awake()
    {
        
        if (instance == null) //Makes the class a singleton
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Make the GameObject persistent
        }
        else
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        dialogueAv = Resources.LoadAll<AudioClip>("SFX/dialogueAv");
        doorSounds = Resources.LoadAll<AudioClip>("SFX/doorSounds");
        femmeAv = Resources.LoadAll<AudioClip>("SFX/femmeAv");
        hitSounds = Resources.LoadAll<AudioClip>("SFX/hitSounds");
        mascAv = Resources.LoadAll<AudioClip>("SFX/mascAv");
        swingSounds = Resources.LoadAll<AudioClip>("SFX/swingSounds");
        toiletDoorSounds = Resources.LoadAll<AudioClip>("SFX/toiletDoorSounds");
    }

    //Plays audio clips and ensures that one sound is never played twice in a row
    public void PlayDoorSound()
    {
        currentRandNum = rand.Next(doorSounds.Length);

        if (currentRandNum == previousRandNum)
        {
            PlayDoorSound();
            return;
        }
        SFXSource.PlayOneShot(doorSounds[currentRandNum]);

        previousRandNum = currentRandNum;
    }
    public void PlayFemmeAvSound()
    {
        currentRandNum = rand.Next(femmeAv.Length);

        if (currentRandNum == previousRandNum)
        {
            PlayFemmeAvSound();
            return;
        }
        SFXSource.PlayOneShot(femmeAv[currentRandNum]);

        previousRandNum = currentRandNum;
    }
    public void PlayHitSound()
    {
        currentRandNum = rand.Next(hitSounds.Length);

        if (currentRandNum == previousRandNum)
        {
            PlayHitSound();
            return;
        }
        SFXSource.PlayOneShot(hitSounds[currentRandNum]);

        previousRandNum = currentRandNum;
    }
    public void PlayMascAvSound()
    {
        currentRandNum = rand.Next(mascAv.Length);

        if (currentRandNum == previousRandNum)
        {
            PlayMascAvSound();
            return;
        }
        SFXSource.PlayOneShot(mascAv[currentRandNum]);

        previousRandNum = currentRandNum;
    }
    public void PlaySwingSound()
    {
        currentRandNum = rand.Next(swingSounds.Length);

        if (currentRandNum == previousRandNum)
        {
            PlaySwingSound();
            return;
        }
        SFXSource.PlayOneShot(swingSounds[currentRandNum]);

        previousRandNum = currentRandNum;
    }
    public void PlayToiletDoorSound()
    {
        currentRandNum = rand.Next(toiletDoorSounds.Length);

        if (currentRandNum == previousRandNum)
        {
            PlayToiletDoorSound();
            return;
        }
        SFXSource.PlayOneShot(toiletDoorSounds[currentRandNum]);

        previousRandNum = currentRandNum;
    }
    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}