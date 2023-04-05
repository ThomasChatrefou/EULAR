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

    private BasicActions basicActions;

    private void Awake()
    {
        basicActions= new BasicActions();
        basicActions.UserInteraction.Click.performed += OnClick;
    }

    void Start()
    {
        m_MainCamera = Camera.main;
    }

    void OnClick(InputAction.CallbackContext context)
    {
        if(m_PreviousParticle && m_ForceDestroyPrevious)
            Destroy(m_PreviousParticle);

        Vector2 screenPos = basicActions.UserInteraction.Cursor.ReadValue<Vector2>();
        Vector3 particleWorldPos = m_MainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, 0.2f));
        m_PreviousParticle = Instantiate(m_ParticlePrefab, particleWorldPos, Quaternion.identity, m_MainCamera.transform);
    }

    private void OnEnable()
    {
        basicActions.UserInteraction.Enable();
    }

    private void OnDisable()
    {
        basicActions.UserInteraction.Disable();
    }
}
