using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMagazScene : MonoBehaviour
{
    [SerializeField] private MeshRenderer _sky;
    [SerializeField] private GameObject _openDoorAudio;
    private Material _skyMaterial;
    void Start()
    {
        GameObject _audioObg = Instantiate(_openDoorAudio);
        AudioSource[] _sources = _audioObg.GetComponentsInChildren<AudioSource>();

        foreach (AudioSource source in _sources)
        {
            if (source.name == "OpenDoor1")
            {
                source.Play();
                break;
            }
        }

        _skyMaterial = _sky.material;
        float xSkyMat = 1;

        _skyMaterial.mainTextureScale = new Vector2(xSkyMat, 1);
        _skyMaterial = _sky.material;

        StartCoroutine(WaitSeconds());
    }

    IEnumerator WaitSeconds()
    {
        yield return new WaitForSeconds(2);

        Destroy(this);
    }
}
