using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0]")]
	public partial class EnemySpawnerNetworkObject : NetworkObject
	{
		public const int IDENTITY = 18;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private float _RespawnTimer;
		public event FieldEvent<float> RespawnTimerChanged;
		public InterpolateFloat RespawnTimerInterpolation = new InterpolateFloat() { LerpT = 0f, Enabled = false };
		public float RespawnTimer
		{
			get { return _RespawnTimer; }
			set
			{
				// Don't do anything if the value is the same
				if (_RespawnTimer == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_RespawnTimer = value;
				hasDirtyFields = true;
			}
		}

		public void SetRespawnTimerDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_RespawnTimer(ulong timestep)
		{
			if (RespawnTimerChanged != null) RespawnTimerChanged(_RespawnTimer, timestep);
			if (fieldAltered != null) fieldAltered("RespawnTimer", _RespawnTimer, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			RespawnTimerInterpolation.current = RespawnTimerInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _RespawnTimer);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_RespawnTimer = UnityObjectMapper.Instance.Map<float>(payload);
			RespawnTimerInterpolation.current = _RespawnTimer;
			RespawnTimerInterpolation.target = _RespawnTimer;
			RunChange_RespawnTimer(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _RespawnTimer);

			// Reset all the dirty fields
			for (int i = 0; i < _dirtyFields.Length; i++)
				_dirtyFields[i] = 0;

			return dirtyFieldsData;
		}

		protected override void ReadDirtyFields(BMSByte data, ulong timestep)
		{
			if (readDirtyFlags == null)
				Initialize();

			Buffer.BlockCopy(data.byteArr, data.StartIndex(), readDirtyFlags, 0, readDirtyFlags.Length);
			data.MoveStartIndex(readDirtyFlags.Length);

			if ((0x1 & readDirtyFlags[0]) != 0)
			{
				if (RespawnTimerInterpolation.Enabled)
				{
					RespawnTimerInterpolation.target = UnityObjectMapper.Instance.Map<float>(data);
					RespawnTimerInterpolation.Timestep = timestep;
				}
				else
				{
					_RespawnTimer = UnityObjectMapper.Instance.Map<float>(data);
					RunChange_RespawnTimer(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (RespawnTimerInterpolation.Enabled && !RespawnTimerInterpolation.current.UnityNear(RespawnTimerInterpolation.target, 0.0015f))
			{
				_RespawnTimer = (float)RespawnTimerInterpolation.Interpolate();
				//RunChange_RespawnTimer(RespawnTimerInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public EnemySpawnerNetworkObject() : base() { Initialize(); }
		public EnemySpawnerNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public EnemySpawnerNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
