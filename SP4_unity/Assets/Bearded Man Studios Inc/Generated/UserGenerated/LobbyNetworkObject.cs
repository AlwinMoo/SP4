using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.15]")]
	public partial class LobbyNetworkObject : NetworkObject
	{
		public const int IDENTITY = 7;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private float _readycd;
		public event FieldEvent<float> readycdChanged;
		public InterpolateFloat readycdInterpolation = new InterpolateFloat() { LerpT = 0.15f, Enabled = true };
		public float readycd
		{
			get { return _readycd; }
			set
			{
				// Don't do anything if the value is the same
				if (_readycd == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_readycd = value;
				hasDirtyFields = true;
			}
		}

		public void SetreadycdDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_readycd(ulong timestep)
		{
			if (readycdChanged != null) readycdChanged(_readycd, timestep);
			if (fieldAltered != null) fieldAltered("readycd", _readycd, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			readycdInterpolation.current = readycdInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _readycd);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_readycd = UnityObjectMapper.Instance.Map<float>(payload);
			readycdInterpolation.current = _readycd;
			readycdInterpolation.target = _readycd;
			RunChange_readycd(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _readycd);

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
				if (readycdInterpolation.Enabled)
				{
					readycdInterpolation.target = UnityObjectMapper.Instance.Map<float>(data);
					readycdInterpolation.Timestep = timestep;
				}
				else
				{
					_readycd = UnityObjectMapper.Instance.Map<float>(data);
					RunChange_readycd(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (readycdInterpolation.Enabled && !readycdInterpolation.current.UnityNear(readycdInterpolation.target, 0.0015f))
			{
				_readycd = (float)readycdInterpolation.Interpolate();
				//RunChange_readycd(readycdInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public LobbyNetworkObject() : base() { Initialize(); }
		public LobbyNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public LobbyNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
