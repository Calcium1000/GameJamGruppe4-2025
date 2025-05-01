using UnityEngine;

public class SFXManager : MonoBehaviour
{
    [SerializeField] AudioSource SFXSource;
    [SerializeField] AudioSource WalkingSoundSource;
    private static SFXManager instance;

    private AudioClip[] dialogueAv;
    private AudioClip[] doorSounds;
    private AudioClip[] femmeAv;
    private AudioClip[] hitSounds;
    private AudioClip[] mascAv;
    private AudioClip[] swingSounds;
    private AudioClip[] toiletDoorSounds;
    private AudioClip walkingSound;

    private bool isWalkingSoundPlaying = false;

    System.Random rand = new System.Random(); //RNG used for playing random sound from folders of sounds
    int currentRandNum;
    int previousRandNum;

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
        dialogueAv = Resources.LoadAll<AudioClip>("SFX/dialogueAv");
        doorSounds = Resources.LoadAll<AudioClip>("SFX/doorSounds");
        femmeAv = Resources.LoadAll<AudioClip>("SFX/femmeAv");
        hitSounds = Resources.LoadAll<AudioClip>("SFX/hitSounds");
        mascAv = Resources.LoadAll<AudioClip>("SFX/mascAv");
        swingSounds = Resources.LoadAll<AudioClip>("SFX/swingSounds");
        toiletDoorSounds = Resources.LoadAll<AudioClip>("SFX/toiletDoorSounds");
        walkingSound = Resources.Load<AudioClip>("SFX/gålyd2");
    }
    private void Start()
    {
        
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
    public void PlayWalkingSound(bool isWalking)
    {
        if (isWalking)
        {
            if (!isWalkingSoundPlaying)
            {
                WalkingSoundSource.clip = walkingSound;
                WalkingSoundSource.Play();
                WalkingSoundSource.loop = true;
                isWalkingSoundPlaying = true;
            }
        }
        else
        {
            if (isWalkingSoundPlaying)
            {
                WalkingSoundSource.Stop();
                WalkingSoundSource.loop = false;
                isWalkingSoundPlaying = false;
            }
        }
    }

    //public void OtherPlayFemmeSound()
    //{
    //    SFXSource.PlayOneShot(femmeAv[NonRepeatingRNG(femmeAv.Length)]);
    //}
    //public int NonRepeatingRNG(int upperLimit)
    //{
    //    currentRandNum = rand.Next(upperLimit);
    //    if (currentRandNum == previousRandNum)
    //    {
    //        NonRepeatingRNG(upperLimit);
    //        return currentRandNum;
    //    }
    //    previousRandNum = currentRandNum;
    //    return currentRandNum;
    //}
}