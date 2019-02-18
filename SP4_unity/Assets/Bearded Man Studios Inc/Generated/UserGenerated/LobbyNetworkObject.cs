using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.15]")]
	public partial class LobbyNetworkObject : NetworkObject
	{
		public const int IDENTITY = 9;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private float _ready_cd;
		public event FieldEvent<float> ready_cdChanged;
		public InterpolateFloat ready_cdInterpolation = new InterpolateFloat() { LerpT = 0.15f, Enabled = true };
		public float ready_cd
		{
			get { return _ready_cd; }
			set
			{
				// Don't do anything if the value is the same
				if (_ready_cd == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_ready_cd = value;
				hasDirtyFields = true;
			}
		}

		public void Setready_cdDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_ready_cd(ulong timestep)
		{
			if (ready_cdChanged != null) ready_cdChanged(_ready_cd, timestep);
			if (fieldAltered != null) fieldAltered("ready_cd", _ready_cd, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			ready_cdInterpolation.current = ready_cdInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _ready_cd);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_ready_cd = UnityObjectMapper.Instance.Map<float>(payload);
			ready_cdInterpolation.current = _ready_cd;
			ready_cdInterpolation.target = _ready_cd;
			RunChange_ready_cd(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _ready_cd);

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
				if (ready_cdInterpolation.Enabled)
				{
					ready_cdInterpolation.target = UnityObjectMapper.Instance.Map<float>(data);
					ready_cdInterpolation.Timestep = timestep;
				}
				else
				{
					_ready_cd = UnityObjectMapper.Instance.Map<float>(data);
					RunChange_ready_cd(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (ready_cdInterpolation.Enabled && !ready_cdInterpolation.current.UnityNear(ready_cdInterpolation.target, 0.0015f))
			{
				_ready_cd = (float)ready_cdInterpolation.Interpolate();
				//RunChange_ready_cd(ready_cdInterpolation.Timestep);
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
