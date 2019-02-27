using BeardedManStudios.Forge.Networking.Frame;
using BeardedManStudios.Forge.Networking.Unity;
using System;
using UnityEngine;

namespace BeardedManStudios.Forge.Networking.Generated
{
	[GeneratedInterpol("{\"inter\":[0.15,0,0,0,0]")]
	public partial class LobbyNetworkObject : NetworkObject
	{
		public const int IDENTITY = 10;

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
		[ForgeGeneratedField]
		private short _player1;
		public event FieldEvent<short> player1Changed;
		public Interpolated<short> player1Interpolation = new Interpolated<short>() { LerpT = 0f, Enabled = false };
		public short player1
		{
			get { return _player1; }
			set
			{
				// Don't do anything if the value is the same
				if (_player1 == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x2;
				_player1 = value;
				hasDirtyFields = true;
			}
		}

		public void Setplayer1Dirty()
		{
			_dirtyFields[0] |= 0x2;
			hasDirtyFields = true;
		}

		private void RunChange_player1(ulong timestep)
		{
			if (player1Changed != null) player1Changed(_player1, timestep);
			if (fieldAltered != null) fieldAltered("player1", _player1, timestep);
		}
		[ForgeGeneratedField]
		private short _player2;
		public event FieldEvent<short> player2Changed;
		public Interpolated<short> player2Interpolation = new Interpolated<short>() { LerpT = 0f, Enabled = false };
		public short player2
		{
			get { return _player2; }
			set
			{
				// Don't do anything if the value is the same
				if (_player2 == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x4;
				_player2 = value;
				hasDirtyFields = true;
			}
		}

		public void Setplayer2Dirty()
		{
			_dirtyFields[0] |= 0x4;
			hasDirtyFields = true;
		}

		private void RunChange_player2(ulong timestep)
		{
			if (player2Changed != null) player2Changed(_player2, timestep);
			if (fieldAltered != null) fieldAltered("player2", _player2, timestep);
		}
		[ForgeGeneratedField]
		private short _player3;
		public event FieldEvent<short> player3Changed;
		public Interpolated<short> player3Interpolation = new Interpolated<short>() { LerpT = 0f, Enabled = false };
		public short player3
		{
			get { return _player3; }
			set
			{
				// Don't do anything if the value is the same
				if (_player3 == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x8;
				_player3 = value;
				hasDirtyFields = true;
			}
		}

		public void Setplayer3Dirty()
		{
			_dirtyFields[0] |= 0x8;
			hasDirtyFields = true;
		}

		private void RunChange_player3(ulong timestep)
		{
			if (player3Changed != null) player3Changed(_player3, timestep);
			if (fieldAltered != null) fieldAltered("player3", _player3, timestep);
		}
		[ForgeGeneratedField]
		private short _player4;
		public event FieldEvent<short> player4Changed;
		public Interpolated<short> player4Interpolation = new Interpolated<short>() { LerpT = 0f, Enabled = false };
		public short player4
		{
			get { return _player4; }
			set
			{
				// Don't do anything if the value is the same
				if (_player4 == value)
					return;

				// Mark the field as dirty for the network to transmit
				_dirtyFields[0] |= 0x10;
				_player4 = value;
				hasDirtyFields = true;
			}
		}

		public void Setplayer4Dirty()
		{
			_dirtyFields[0] |= 0x10;
			hasDirtyFields = true;
		}

		private void RunChange_player4(ulong timestep)
		{
			if (player4Changed != null) player4Changed(_player4, timestep);
			if (fieldAltered != null) fieldAltered("player4", _player4, timestep);
		}

		protected override void OwnershipChanged()
		{
			base.OwnershipChanged();
			SnapInterpolations();
		}
		
		public void SnapInterpolations()
		{
			readycdInterpolation.current = readycdInterpolation.target;
			player1Interpolation.current = player1Interpolation.target;
			player2Interpolation.current = player2Interpolation.target;
			player3Interpolation.current = player3Interpolation.target;
			player4Interpolation.current = player4Interpolation.target;
		}

		public override int UniqueIdentity { get { return IDENTITY; } }

		protected override BMSByte WritePayload(BMSByte data)
		{
			UnityObjectMapper.Instance.MapBytes(data, _readycd);
			UnityObjectMapper.Instance.MapBytes(data, _player1);
			UnityObjectMapper.Instance.MapBytes(data, _player2);
			UnityObjectMapper.Instance.MapBytes(data, _player3);
			UnityObjectMapper.Instance.MapBytes(data, _player4);

			return data;
		}

		protected override void ReadPayload(BMSByte payload, ulong timestep)
		{
			_readycd = UnityObjectMapper.Instance.Map<float>(payload);
			readycdInterpolation.current = _readycd;
			readycdInterpolation.target = _readycd;
			RunChange_readycd(timestep);
			_player1 = UnityObjectMapper.Instance.Map<short>(payload);
			player1Interpolation.current = _player1;
			player1Interpolation.target = _player1;
			RunChange_player1(timestep);
			_player2 = UnityObjectMapper.Instance.Map<short>(payload);
			player2Interpolation.current = _player2;
			player2Interpolation.target = _player2;
			RunChange_player2(timestep);
			_player3 = UnityObjectMapper.Instance.Map<short>(payload);
			player3Interpolation.current = _player3;
			player3Interpolation.target = _player3;
			RunChange_player3(timestep);
			_player4 = UnityObjectMapper.Instance.Map<short>(payload);
			player4Interpolation.current = _player4;
			player4Interpolation.target = _player4;
			RunChange_player4(timestep);
		}

		protected override BMSByte SerializeDirtyFields()
		{
			dirtyFieldsData.Clear();
			dirtyFieldsData.Append(_dirtyFields);

			if ((0x1 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _readycd);
			if ((0x2 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _player1);
			if ((0x4 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _player2);
			if ((0x8 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _player3);
			if ((0x10 & _dirtyFields[0]) != 0)
				UnityObjectMapper.Instance.MapBytes(dirtyFieldsData, _player4);

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
			if ((0x2 & readDirtyFlags[0]) != 0)
			{
				if (player1Interpolation.Enabled)
				{
					player1Interpolation.target = UnityObjectMapper.Instance.Map<short>(data);
					player1Interpolation.Timestep = timestep;
				}
				else
				{
					_player1 = UnityObjectMapper.Instance.Map<short>(data);
					RunChange_player1(timestep);
				}
			}
			if ((0x4 & readDirtyFlags[0]) != 0)
			{
				if (player2Interpolation.Enabled)
				{
					player2Interpolation.target = UnityObjectMapper.Instance.Map<short>(data);
					player2Interpolation.Timestep = timestep;
				}
				else
				{
					_player2 = UnityObjectMapper.Instance.Map<short>(data);
					RunChange_player2(timestep);
				}
			}
			if ((0x8 & readDirtyFlags[0]) != 0)
			{
				if (player3Interpolation.Enabled)
				{
					player3Interpolation.target = UnityObjectMapper.Instance.Map<short>(data);
					player3Interpolation.Timestep = timestep;
				}
				else
				{
					_player3 = UnityObjectMapper.Instance.Map<short>(data);
					RunChange_player3(timestep);
				}
			}
			if ((0x10 & readDirtyFlags[0]) != 0)
			{
				if (player4Interpolation.Enabled)
				{
					player4Interpolation.target = UnityObjectMapper.Instance.Map<short>(data);
					player4Interpolation.Timestep = timestep;
				}
				else
				{
					_player4 = UnityObjectMapper.Instance.Map<short>(data);
					RunChange_player4(timestep);
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
			if (player1Interpolation.Enabled && !player1Interpolation.current.UnityNear(player1Interpolation.target, 0.0015f))
			{
				_player1 = (short)player1Interpolation.Interpolate();
				//RunChange_player1(player1Interpolation.Timestep);
			}
			if (player2Interpolation.Enabled && !player2Interpolation.current.UnityNear(player2Interpolation.target, 0.0015f))
			{
				_player2 = (short)player2Interpolation.Interpolate();
				//RunChange_player2(player2Interpolation.Timestep);
			}
			if (player3Interpolation.Enabled && !player3Interpolation.current.UnityNear(player3Interpolation.target, 0.0015f))
			{
				_player3 = (short)player3Interpolation.Interpolate();
				//RunChange_player3(player3Interpolation.Timestep);
			}
			if (player4Interpolation.Enabled && !player4Interpolation.current.UnityNear(player4Interpolation.target, 0.0015f))
			{
				_player4 = (short)player4Interpolation.Interpolate();
				//RunChange_player4(player4Interpolation.Timestep);
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
