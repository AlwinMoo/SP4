using BeardedManStudios.Forge.Networking.Frame;
using System;
using MainThreadManager = BeardedManStudios.Forge.Networking.Unity.MainThreadManager;

namespace BeardedManStudios.Forge.Networking.Generated
{
	public partial class NetworkObjectFactory : NetworkObjectFactoryBase
	{
		public override void NetworkCreateObject(NetWorker networker, int identity, uint id, FrameStream frame, Action<NetworkObject> callback)
		{
			if (networker.IsServer)
			{
				if (frame.Sender != null && frame.Sender != networker.Me)
				{
					if (!ValidateCreateRequest(networker, identity, id, frame))
						return;
				}
			}
			
			bool availableCallback = false;
			NetworkObject obj = null;
			MainThreadManager.Run(() =>
			{
				switch (identity)
				{
					case ChatManagerNetworkObject.IDENTITY:
						availableCallback = true;
						obj = new ChatManagerNetworkObject(networker, id, frame);
						break;
					case CubeForgeGameNetworkObject.IDENTITY:
						availableCallback = true;
						obj = new CubeForgeGameNetworkObject(networker, id, frame);
						break;
					case EnemyNetworkObject.IDENTITY:
						availableCallback = true;
						obj = new EnemyNetworkObject(networker, id, frame);
						break;
					case EnemySpawnerNetworkObject.IDENTITY:
						availableCallback = true;
						obj = new EnemySpawnerNetworkObject(networker, id, frame);
						break;
					case ExampleProximityPlayerNetworkObject.IDENTITY:
						availableCallback = true;
						obj = new ExampleProximityPlayerNetworkObject(networker, id, frame);
						break;
					case FlamethrowerNetworkObject.IDENTITY:
						availableCallback = true;
						obj = new FlamethrowerNetworkObject(networker, id, frame);
						break;
					case GameLogicNetworkObject.IDENTITY:
<<<<<<< HEAD
=======
						availableCallback = true;
						obj = new GameLogicNetworkObject(networker, id, frame);
						break;
					case LobbyNetworkObject.IDENTITY:
>>>>>>> Lobby_ready_check_branch
						availableCallback = true;
						obj = new GameLogicNetworkObject(networker, id, frame);
						break;
					case LobbyNetworkObject.IDENTITY:
						availableCallback = true;
						obj = new LobbyNetworkObject(networker, id, frame);
						break;
					case NetworkCameraNetworkObject.IDENTITY:
						availableCallback = true;
						obj = new NetworkCameraNetworkObject(networker, id, frame);
						break;
					case PlayerManagerNetworkObject.IDENTITY:
						availableCallback = true;
						obj = new PlayerManagerNetworkObject(networker, id, frame);
						break;
					case PlayerVehicleNetworkObject.IDENTITY:
						availableCallback = true;
						obj = new PlayerVehicleNetworkObject(networker, id, frame);
						break;
					case TestNetworkObject.IDENTITY:
						availableCallback = true;
						obj = new TestNetworkObject(networker, id, frame);
						break;
				}

				if (!availableCallback)
					base.NetworkCreateObject(networker, identity, id, frame, callback);
				else if (callback != null)
					callback(obj);
			});
		}

		// DO NOT TOUCH, THIS GETS GENERATED PLEASE EXTEND THIS CLASS IF YOU WISH TO HAVE CUSTOM CODE ADDITIONS
	}
}