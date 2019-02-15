using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.15]")]
	public partial class w_ftNetworkObject : NetworkObject
	{
		public const int IDENTITY = 11;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		private Quaternion _fireRotation;
		public event FieldEvent<Quaternion> fireRotationChanged;
		public InterpolateQuaternion fireRotationInterpolation = new InterpolateQuaternion() { LerpT = 0.15f, Enabled = true };
		public Quaternion fireRotation
		{
			get { return _fireRotation; }
			set
			{
				// Don't do anything if the value is the same
				if (_fireRotation == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_fireRotation = value;
				hasDirtyFields = true;
			}
		}

		public void SetfireRotationDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_fireRotation(ulong timestep)
		{
			if (fireRotationChanged != null) fireRotationChanged(_fireRotation, timestep);
			if (fieldAltered != null) fieldAltered("fireRotation", _fireRotation, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			fireRotationInterpolation.current = fireRotationInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _fireRotation);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_fireRotation = UnityObjectMapper.Instance.Map<Quaternion>(payload);
			fireRotationInterpolation.current = _fireRotation;
			fireRotationInterpolation.target = _fireRotation;
			RunChange_fireRotation(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _fireRotation);

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
				if (fireRotationInterpolation.Enabled)
				{
					fireRotationInterpolation.target = UnityObjectMapper.Instance.Map<Quaternion>(data);
					fireRotationInterpolation.Timestep = timestep;
				}
				else
				{
					_fireRotation = UnityObjectMapper.Instance.Map<Quaternion>(data);
					RunChange_fireRotation(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (fireRotationInterpolation.Enabled && !fireRotationInterpolation.current.UnityNear(fireRotationInterpolation.target, 0.0015f))
			{
				_fireRotation = (Quaternion)fireRotationInterpolation.Interpolate();
				//RunChange_fireRotation(fireRotationInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public w_ftNetworkObject() : base() { Initialize(); }
		public w_ftNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public w_ftNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
