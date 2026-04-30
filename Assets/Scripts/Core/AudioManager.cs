using UnityEngine;

namespace com.eudiko.slotmachine
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [Header("Audio Sources")]
        [SerializeField] private AudioSource loopSource;
        [SerializeField] private AudioSource sfxSource;

        [Header("Clips")]
        [SerializeField] private AudioClip spinLoopClip;
        [SerializeField] private AudioClip spinButtonClip;
        [SerializeField] private AudioClip reelStopClip;
        [SerializeField] private AudioClip betButtonClip;
        [SerializeField] private AudioClip winClip;
        [SerializeField] private AudioClip loseClip;

        void Awake()
        {
            Instance = this;
        }

        public void PlaySpinLoop()
        {
            loopSource.clip = spinLoopClip;
            loopSource.loop = true;
            loopSource.Play();
        }

        public void StopSpinLoop()
        {
            loopSource.loop = false;
            loopSource.Stop();
        }

        public void PlaySpinButton() => PlayOneShot(spinButtonClip);
        public void PlayReelStop()   => PlayOneShot(reelStopClip);
        public void PlayBetButton()  => PlayOneShot(betButtonClip);
        public void PlayWin()        => PlayOneShot(winClip);
        public void PlayLose()       => PlayOneShot(loseClip);

        private void PlayOneShot(AudioClip clip)
        {
            if (clip != null)
                sfxSource.PlayOneShot(clip);
        }
    }
}
