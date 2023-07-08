using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource = null;
    [SerializeField] AssetReference[] _soundTracks = null;
    int _playingSoundtrack = 0;

    IEnumerator Start()
    {
        while (true)
        {
            var currentMusicHandler = _soundTracks[_playingSoundtrack].LoadAssetAsync<AudioClip>();
            yield return currentMusicHandler;

            var newAudioSource = currentMusicHandler.Result;
            _audioSource.clip = newAudioSource;
            _audioSource.Play();

            yield return new WaitUntil(() => _audioSource.isPlaying == false);

            _audioSource.clip = null;
            Addressables.Release(currentMusicHandler);

            _playingSoundtrack = (_playingSoundtrack + 1) % _soundTracks.Length;
        }
    }


}
