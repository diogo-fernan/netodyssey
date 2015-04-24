using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NetOdysseyModule
{
	class clsTaskArgumentWrapper
	{
		ModuleTask _task;
		Flow _clsFlow;
		PacketDotNet.Packet _packet;
		int _windowSize;
		ulong _bctu;

		public clsTaskArgumentWrapper(ModuleTask inModuleTask)
		{
			_task = inModuleTask;
		}

		public clsTaskArgumentWrapper(ModuleTask inModuleTask, 
										Flow _inFlow, 
										int inWindowSize)
		{
			_task = inModuleTask;
			_clsFlow = _inFlow;
			_windowSize = inWindowSize;
		}

		public clsTaskArgumentWrapper(ModuleTask inModuleTask, 
										PacketDotNet.Packet inPacket, 
										int inWindowSize)
		{
			_task = inModuleTask;
			_packet = inPacket;
			_windowSize = inWindowSize;
		}

		public clsTaskArgumentWrapper(ModuleTask inModuleTask, 
										ulong inBCTU, 
										int inWindowSize)
		{
			_task = inModuleTask;
			_bctu = inBCTU;
			_windowSize = inWindowSize;
		}

		public ModuleTask ModuleTask
		{
			get { return _task; }
			set { _task = value; }
		}

		public Flow Flow
		{
			get { return _clsFlow; }
			set { _clsFlow = value; }
		}

		public PacketDotNet.Packet Packet
		{
			get { return _packet; }
			set { _packet = value; }
		}

		public int WindowSize
		{
			get { return _windowSize; }
			set { _windowSize = value; }
		}

		public ulong BCTU
		{
			get { return _bctu; }
			set { _bctu = value; }
		}
	}
}