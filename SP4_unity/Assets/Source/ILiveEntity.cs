using UnityEngine;

public interface ILiveEntity{
	bool TakeDamage(float _damage, GlobalDamage.DamageTypes _type);
}
