using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ParticleEffects : MonoBehaviour
{
    [SerializeField] protected ParticleSystem particles;

    public virtual void SetAndPlay(GameObject obj, float scale, float offset)
    {
        ParticleSystem particleSystem = Instantiate(particles);
        particleSystem.transform.SetParent(obj.transform);
        particleSystem.transform.position = obj.transform.position + Vector3.up * offset;
        particleSystem.transform.localScale = Vector3.one * scale;
        particleSystem.Play();
    }
}
