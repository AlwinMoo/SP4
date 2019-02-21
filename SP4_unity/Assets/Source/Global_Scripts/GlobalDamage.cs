using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalDamage : MonoBehaviour {
	public static float g_fireDamageTickRatio = 0.1f;
	public static float g_HMGDamage = 5.0f;
    public static float g_RocketDirectDamage = 20.0f;
    public static float g_RocketAOEDamage = 10.0f;
	public enum DamageTypes
	{
		DAMAGE_BALLISTIC_SMALL = 0,
		DAMAGE_FIRE_NORMAL,
		DAMAGE_FIRE_TICK,
        DAMAGE_ROCKET,
        DAMAGE_AOE_ROCKET_DAMAGE,
	}
}
