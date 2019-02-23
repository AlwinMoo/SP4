using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0]")]
	public partial class NetworkMapGenerationNetworkObject : NetworkObject
	{
		public const int IDENTITY = 10;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private int _seed;
		public event FieldEvent<int> seedChanged;
		public Interpolated<int> seedInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int seed
		{
			get { return _seed; }
			set
			{
				// Don't do anything if the value is the same
				if (_seed == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_seed = value;
				hasDirtyFields = true;
			}
		}

		public void SetseedDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_seed(ulong timestep)
		{
			if (seedChanged != null) seedChanged(_seed, timestep);
			if (fieldAltered != null) fieldAltered("seed", _seed, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			seedInterpolation.current = seedInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _seed);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_seed = UnityObjectMapper.Instance.Map<int>(payload);
			seedInterpolation.current = _seed;
			seedInterpolation.target = _seed;
			RunChange_seed(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _seed);

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
				if (seedInterpolation.Enabled)
				{
					seedInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					seedInterpolation.Timestep = timestep;
				}
				else
				{
					_seed = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_seed(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (seedInterpolation.Enabled && !seedInterpolation.current.UnityNear(seedInterpolation.target, 0.0015f))
			{
				_seed = (int)seedInterpolation.Interpolate();
				//RunChange_seed(seedInterpolation.Timestep);
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
