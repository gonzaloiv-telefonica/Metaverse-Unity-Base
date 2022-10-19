using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Meta.VR
{

    public class TriggerableBehaviour : PhysicsGrabbable
    {

        [Header("TriggerableBehaviour")]
        [SerializeField] private Bullet bullet;
        private MonoPool<Bullet> bullets;

        private void Awake()
        {
            bullets = new MonoPool<Bullet>(bullet);
            bullet.SetActive(false);
        }

        protected override void SetInteractorListeners(InteractorBehaviour interactor)
        {
            base.SetInteractorListeners(interactor);
            interactor.onPrimaryClick += OnPrimaryClick;
        }

        protected override void RemoveInteractorListeners(InteractorBehaviour interactor)
        {
            base.RemoveInteractorListeners(interactor);
            interactor.onPrimaryClick -= OnPrimaryClick;
        }

        protected virtual void OnPrimaryClick()
        {
            Bullet bullet = bullets.GetInactive();
            bullet.Shoot();
        }

    }

}