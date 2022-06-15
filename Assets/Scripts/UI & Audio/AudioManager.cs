using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioSource audioSource;

    public void Awake()
    {
        if (this.name == "MusicManager")
        {
            DontDestroyOnLoad(this);
        }
    }

    // Use this for initialization
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {

    }
}
