using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0]")]
	public partial class NetworkMapGenerationNetworkObject : NetworkObject
	{
		public const int IDENTITY = 14;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private int __seed;
		public event FieldEvent<int> _seedChanged;
		public Interpolated<int> _seedInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int _seed
		{
			get { return __seed; }
			set
			{
				// Don't do anything if the value is the same
				if (__seed == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				__seed = value;
				hasDirtyFields = true;
			}
		}

		public void Set_seedDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange__seed(ulong timestep)
		{
			if (_seedChanged != null) _seedChanged(__seed, timestep);
			if (fieldAltered != null) fieldAltered("_seed", __seed, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			_seedInterpolation.current = _seedInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, __seed);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			__seed = UnityObjectMapper.Instance.Map<int>(payload);
			_seedInterpolation.current = __seed;
			_seedInterpolation.target = __seed;
			RunChange__seed(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, __seed);

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
				if (_seedInterpolation.Enabled)
				{
					_seedInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					_seedInterpolation.Timestep = timestep;
				}
				else
				{
					__seed = UnityObjectMapper.Instance.Map<int>(data);
					RunChange__seed(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (_seedInterpolation.Enabled && !_seedInterpolation.current.UnityNear(_seedInterpolation.target, 0.0015f))
			{
				__seed = (int)_seedInterpolation.Interpolate();
				//RunChange__seed(_seedInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public NetworkMapGenerationNetworkObject() : base() { Initialize(); }
		public NetworkMapGenerationNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public NetworkMapGenerationNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
