﻿using RPG.Core;
using System;
using UnityEngine;

namespace RPG.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] AnimatorOverrideController animatorOverride = null;
        [SerializeField] int _weaponDamage = 1;
        [SerializeField] float _weaponRange = 1f;
        [SerializeField] bool isRightHand = true;
        [SerializeField] Projectile projectile = null;

        const string weaponName = "Weapon";

        public void Spawn(Transform rightHandTransform, Transform leftHandTransform, Animator animator)
        {
            DestroyOldWeapon(rightHandTransform, leftHandTransform);

            if (equippedPrefab != null)
            {
                Transform handTransform = GetTransform(rightHandTransform, leftHandTransform);
                GameObject weapon = Instantiate(equippedPrefab, handTransform);
                weapon.name = weaponName;
            }
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }

        private void DestroyOldWeapon(Transform rightHandTransform, Transform leftHandTransform)
        {
            Transform oldWeapon = rightHandTransform.Find(weaponName);

            if(oldWeapon == null)
            {
                oldWeapon = leftHandTransform.Find(weaponName);
            }
            if (oldWeapon == null) return;

            oldWeapon.name = "Removing Weapon";
            Destroy(oldWeapon.gameObject);
        }

        private Transform GetTransform(Transform rightHandTransform, Transform leftHandTransform)
        {
            Transform handTransform;
            if (isRightHand) handTransform = rightHandTransform;
            else handTransform = leftHandTransform;
            return handTransform;
        }

        public bool HasProjectile()
        {
            return projectile != null;
        }

        public void LaunchProjectile(Transform rightHand, Transform leftHand, Health target)
        {
            Projectile projectileInstance = Instantiate(projectile, GetTransform(rightHand, leftHand).position, Quaternion.identity);
            projectileInstance.SetTarget(target, _weaponDamage);
        }

        public float GetDamage()
        {
            return _weaponDamage;
        }

        public float GetRange()
        {
            return _weaponRange;
        }
    }
}