using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpawnParticleOnClickBehaviour : MonoBehaviour
{
    [SerializeField] GameObject m_ParticlePrefab;
    [SerializeField] bool m_ForceDestroyPrevious;

    private Camera m_MainCamera;

    private GameObject m_PreviousParticle;

    void Start()
    {
        m_MainCamera = Camera.main;
    }

    void OnClick()
    {
        if(m_PreviousParticle)
            Destroy(m_PreviousParticle);

        Vector3 mousePos = Mouse.current.position.ReadValue();
        Vector3 particleWorldPos = m_MainCamera.ScreenToWorldPoint(mousePos + Vector3.forward * 10);
        m_PreviousParticle = Instantiate(m_ParticlePrefab, particleWorldPos, Quaternion.identity, transform);
    }
}
