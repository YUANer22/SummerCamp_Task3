using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.CorgiEngine;
using MoreMountains.Tools;
using UnityEngine.SceneManagement;

public class BGMManager : MonoBehaviour
{
    public AudioClip source;
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    { 
        MMSoundManager.Instance.PlaySound(source, MMSoundManager.MMSoundManagerTracks.Music, new Vector3(0, 0, 0), true, persistent: true, ID: 1);
        MMSoundManager.Instance.SetTrackVolume(MMSoundManager.MMSoundManagerTracks.Music, 0.25f);
    }
}
