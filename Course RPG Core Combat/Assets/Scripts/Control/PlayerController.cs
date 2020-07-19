﻿using UnityEngine;
using RPG.Movement;
using RPG.Combat;
using RPG.Core;

namespace RPG.Control
{
    public class PlayerController : MonoBehaviour
    {

        private Health _health = null;

        private void Start()
        {
            _health = GetComponent<Health>();
        }

        private void Update()
        {
            if (_health.IsDead()) return;
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
            print("Nothing to do.");
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (var hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null)
                {
                    continue;
                }
                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }
                if(Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;
            }
            return false;
        }

        private bool InteractWithMovement()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            if (hits != null)
            {
                foreach (var hit in hits)
                {
                    if (hit.transform.tag == "Walkable")
                    {
                        if (Input.GetMouseButton(0))
                        {
                            GetComponent<Mover>().StartMoveAction(hit.point, 1f);
                        }
                        return true;
                    }
                }
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}