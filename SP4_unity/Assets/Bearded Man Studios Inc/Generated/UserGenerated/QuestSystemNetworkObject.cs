using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0,0,0,0,0,0,0]")]
	public partial class QuestSystemNetworkObject : NetworkObject
	{
		public const int IDENTITY = 15;

		private byte[] _dirtyFields = new byte[1];

		#pragma warning disable 0067
		public event FieldChangedEvent fieldAltered;
		#pragma warning restore 0067
		[ForgeGeneratedField]
		private Vector3 _position;
		public event FieldEvent<Vector3> positionChanged;
		public InterpolateVector3 positionInterpolation = new InterpolateVector3() { LerpT = 0f, Enabled = false };
		public Vector3 position
		{
			get { return _position; }
			set
			{
				// Don't do anything if the value is the same
				if (_position == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x1;
				_position = value;
				hasDirtyFields = true;
			}
		}

		public void SetpositionDirty()
		{
			_dirtyFields[0] |= 0x1;
			hasDirtyFields = true;
		}

		private void RunChange_position(ulong timestep)
		{
			if (positionChanged != null) positionChanged(_position, timestep);
			if (fieldAltered != null) fieldAltered("position", _position, timestep);
		}
		[ForgeGeneratedField]
		private Vector3 _rotation;
		public event FieldEvent<Vector3> rotationChanged;
		public InterpolateVector3 rotationInterpolation = new InterpolateVector3() { LerpT = 0f, Enabled = false };
		public Vector3 rotation
		{
			get { return _rotation; }
			set
			{
				// Don't do anything if the value is the same
				if (_rotation == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_rotation = value;
				hasDirtyFields = true;
			}
		}

		public void SetrotationDirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_rotation(ulong timestep)
		{
			if (rotationChanged != null) rotationChanged(_rotation, timestep);
			if (fieldAltered != null) fieldAltered("rotation", _rotation, timestep);
		}
		[ForgeGeneratedField]
		private int _QuestID;
		public event FieldEvent<int> QuestIDChanged;
		public Interpolated<int> QuestIDInterpolation = new Interpolated<int>() { LerpT = 0f, Enabled = false };
		public int QuestID
		{
			get { return _QuestID; }
			set
			{
				// Don't do anything if the value is the same
				if (_QuestID == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_QuestID = value;
				hasDirtyFields = true;
			}
		}

		public void SetQuestIDDirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_QuestID(ulong timestep)
		{
			if (QuestIDChanged != null) QuestIDChanged(_QuestID, timestep);
			if (fieldAltered != null) fieldAltered("QuestID", _QuestID, timestep);
		}
		[ForgeGeneratedField]
		private float _HoldOutTime;
		public event FieldEvent<float> HoldOutTimeChanged;
		public InterpolateFloat HoldOutTimeInterpolation = new InterpolateFloat() { LerpT = 0f, Enabled = false };
		public float HoldOutTime
		{
			get { return _HoldOutTime; }
			set
			{
				// Don't do anything if the value is the same
				if (_HoldOutTime == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x8;
				_HoldOutTime = value;
				hasDirtyFields = true;
			}
		}

		public void SetHoldOutTimeDirty()
		{
			_dirtyFields[0] |= 0x8;
			hasDirtyFields = true;
		}

		private void RunChange_HoldOutTime(ulong timestep)
		{
			if (HoldOutTimeChanged != null) HoldOutTimeChanged(_HoldOutTime, timestep);
			if (fieldAltered != null) fieldAltered("HoldOutTime", _HoldOutTime, timestep);
		}
		[ForgeGeneratedField]
		private bool _IsActive;
		public event FieldEvent<bool> IsActiveChanged;
		public Interpolated<bool> IsActiveInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool IsActive
		{
			get { return _IsActive; }
			set
			{
				// Don't do anything if the value is the same
				if (_IsActive == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x10;
				_IsActive = value;
				hasDirtyFields = true;
			}
		}

		public void SetIsActiveDirty()
		{
			_dirtyFields[0] |= 0x10;
			hasDirtyFields = true;
		}

		private void RunChange_IsActive(ulong timestep)
		{
			if (IsActiveChanged != null) IsActiveChanged(_IsActive, timestep);
			if (fieldAltered != null) fieldAltered("IsActive", _IsActive, timestep);
		}
		[ForgeGeneratedField]
		private bool _IsFailed;
		public event FieldEvent<bool> IsFailedChanged;
		public Interpolated<bool> IsFailedInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool IsFailed
		{
			get { return _IsFailed; }
			set
			{
				// Don't do anything if the value is the same
				if (_IsFailed == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x20;
				_IsFailed = value;
				hasDirtyFields = true;
			}
		}

		public void SetIsFailedDirty()
		{
			_dirtyFields[0] |= 0x20;
			hasDirtyFields = true;
		}

		private void RunChange_IsFailed(ulong timestep)
		{
			if (IsFailedChanged != null) IsFailedChanged(_IsFailed, timestep);
			if (fieldAltered != null) fieldAltered("IsFailed", _IsFailed, timestep);
		}
		[ForgeGeneratedField]
		private bool _IsPassed;
		public event FieldEvent<bool> IsPassedChanged;
		public Interpolated<bool> IsPassedInterpolation = new Interpolated<bool>() { LerpT = 0f, Enabled = false };
		public bool IsPassed
		{
			get { return _IsPassed; }
			set
			{
				// Don't do anything if the value is the same
				if (_IsPassed == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x40;
				_IsPassed = value;
				hasDirtyFields = true;
			}
		}

		public void SetIsPassedDirty()
		{
			_dirtyFields[0] |= 0x40;
			hasDirtyFields = true;
		}

		private void RunChange_IsPassed(ulong timestep)
		{
			if (IsPassedChanged != null) IsPassedChanged(_IsPassed, timestep);
			if (fieldAltered != null) fieldAltered("IsPassed", _IsPassed, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			positionInterpolation.current = positionInterpolation.target;
			rotationInterpolation.current = rotationInterpolation.target;
			QuestIDInterpolation.current = QuestIDInterpolation.target;
			HoldOutTimeInterpolation.current = HoldOutTimeInterpolation.target;
			IsActiveInterpolation.current = IsActiveInterpolation.target;
			IsFailedInterpolation.current = IsFailedInterpolation.target;
			IsPassedInterpolation.current = IsPassedInterpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _position);
			UnityObjectMapper.Instance.MapBytes(data, _rotation);
			UnityObjectMapper.Instance.MapBytes(data, _QuestID);
			UnityObjectMapper.Instance.MapBytes(data, _HoldOutTime);
			UnityObjectMapper.Instance.MapBytes(data, _IsActive);
			UnityObjectMapper.Instance.MapBytes(data, _IsFailed);
			UnityObjectMapper.Instance.MapBytes(data, _IsPassed);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_position = UnityObjectMapper.Instance.Map<Vector3>(payload);
			positionInterpolation.current = _position;
			positionInterpolation.target = _position;
			RunChange_position(timestep);
			_rotation = UnityObjectMapper.Instance.Map<Vector3>(payload);
			rotationInterpolation.current = _rotation;
			rotationInterpolation.target = _rotation;
			RunChange_rotation(timestep);
			_QuestID = UnityObjectMapper.Instance.Map<int>(payload);
			QuestIDInterpolation.current = _QuestID;
			QuestIDInterpolation.target = _QuestID;
			RunChange_QuestID(timestep);
			_HoldOutTime = UnityObjectMapper.Instance.Map<float>(payload);
			HoldOutTimeInterpolation.current = _HoldOutTime;
			HoldOutTimeInterpolation.target = _HoldOutTime;
			RunChange_HoldOutTime(timestep);
			_IsActive = UnityObjectMapper.Instance.Map<bool>(payload);
			IsActiveInterpolation.current = _IsActive;
			IsActiveInterpolation.target = _IsActive;
			RunChange_IsActive(timestep);
			_IsFailed = UnityObjectMapper.Instance.Map<bool>(payload);
			IsFailedInterpolation.current = _IsFailed;
			IsFailedInterpolation.target = _IsFailed;
			RunChange_IsFailed(timestep);
			_IsPassed = UnityObjectMapper.Instance.Map<bool>(payload);
			IsPassedInterpolation.current = _IsPassed;
			IsPassedInterpolation.target = _IsPassed;
			RunChange_IsPassed(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _position);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _rotation);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _QuestID);
			if ((0x8 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _HoldOutTime);
			if ((0x10 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _IsActive);
			if ((0x20 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _IsFailed);
			if ((0x40 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _IsPassed);

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
				if (positionInterpolation.Enabled)
				{
					positionInterpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					positionInterpolation.Timestep = timestep;
				}
				else
				{
					_position = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_position(timestep);
				}
			}
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (rotationInterpolation.Enabled)
				{
					rotationInterpolation.target = UnityObjectMapper.Instance.Map<Vector3>(data);
					rotationInterpolation.Timestep = timestep;
				}
				else
				{
					_rotation = UnityObjectMapper.Instance.Map<Vector3>(data);
					RunChange_rotation(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (QuestIDInterpolation.Enabled)
				{
					QuestIDInterpolation.target = UnityObjectMapper.Instance.Map<int>(data);
					QuestIDInterpolation.Timestep = timestep;
				}
				else
				{
					_QuestID = UnityObjectMapper.Instance.Map<int>(data);
					RunChange_QuestID(timestep);
				}
			}
			if ((0x8 & readDirtyFlags[0]) != 0)
			{
				if (HoldOutTimeInterpolation.Enabled)
				{
					HoldOutTimeInterpolation.target = UnityObjectMapper.Instance.Map<float>(data);
					HoldOutTimeInterpolation.Timestep = timestep;
				}
				else
				{
					_HoldOutTime = UnityObjectMapper.Instance.Map<float>(data);
					RunChange_HoldOutTime(timestep);
				}
			}
			if ((0x10 & readDirtyFlags[0]) != 0)
			{
				if (IsActiveInterpolation.Enabled)
				{
					IsActiveInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					IsActiveInterpolation.Timestep = timestep;
				}
				else
				{
					_IsActive = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_IsActive(timestep);
				}
			}
			if ((0x20 & readDirtyFlags[0]) != 0)
			{
				if (IsFailedInterpolation.Enabled)
				{
					IsFailedInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					IsFailedInterpolation.Timestep = timestep;
				}
				else
				{
					_IsFailed = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_IsFailed(timestep);
				}
			}
			if ((0x40 & readDirtyFlags[0]) != 0)
			{
				if (IsPassedInterpolation.Enabled)
				{
					IsPassedInterpolation.target = UnityObjectMapper.Instance.Map<bool>(data);
					IsPassedInterpolation.Timestep = timestep;
				}
				else
				{
					_IsPassed = UnityObjectMapper.Instance.Map<bool>(data);
					RunChange_IsPassed(timestep);
				}
			}
		}

		public override void InterpolateUpdate()
		{
			if (IsOwner)
				return;

			if (positionInterpolation.Enabled && !positionInterpolation.current.UnityNear(positionInterpolation.target, 0.0015f))
			{
				_position = (Vector3)positionInterpolation.Interpolate();
				//RunChange_position(positionInterpolation.Timestep);
			}
			if (rotationInterpolation.Enabled && !rotationInterpolation.current.UnityNear(rotationInterpolation.target, 0.0015f))
			{
				_rotation = (Vector3)rotationInterpolation.Interpolate();
				//RunChange_rotation(rotationInterpolation.Timestep);
			}
			if (QuestIDInterpolation.Enabled && !QuestIDInterpolation.current.UnityNear(QuestIDInterpolation.target, 0.0015f))
			{
				_QuestID = (int)QuestIDInterpolation.Interpolate();
				//RunChange_QuestID(QuestIDInterpolation.Timestep);
			}
			if (HoldOutTimeInterpolation.Enabled && !HoldOutTimeInterpolation.current.UnityNear(HoldOutTimeInterpolation.target, 0.0015f))
			{
				_HoldOutTime = (float)HoldOutTimeInterpolation.Interpolate();
				//RunChange_HoldOutTime(HoldOutTimeInterpolation.Timestep);
			}
			if (IsActiveInterpolation.Enabled && !IsActiveInterpolation.current.UnityNear(IsActiveInterpolation.target, 0.0015f))
			{
				_IsActive = (bool)IsActiveInterpolation.Interpolate();
				//RunChange_IsActive(IsActiveInterpolation.Timestep);
			}
			if (IsFailedInterpolation.Enabled && !IsFailedInterpolation.current.UnityNear(IsFailedInterpolation.target, 0.0015f))
			{
				_IsFailed = (bool)IsFailedInterpolation.Interpolate();
				//RunChange_IsFailed(IsFailedInterpolation.Timestep);
			}
			if (IsPassedInterpolation.Enabled && !IsPassedInterpolation.current.UnityNear(IsPassedInterpolation.target, 0.0015f))
			{
				_IsPassed = (bool)IsPassedInterpolation.Interpolate();
				//RunChange_IsPassed(IsPassedInterpolation.Timestep);
			}
		}

		private void Initialize()
		{
			if (readDirtyFlags == null)
				readDirtyFlags = new byte[1];

		}

		public QuestSystemNetworkObject() : base() { Initialize(); }
		public QuestSystemNetworkObject(NetWorker networker, INetworkBehavior networkBehavior = null, int createCode = 0, byte[] metadata = null) : base(networker, networkBehavior, createCode, metadata) { Initialize(); }
		public QuestSystemNetworkObject(NetWorker networker, uint serverId, FrameStream frame) : base(networker, serverId, frame) { Initialize(); }

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}
