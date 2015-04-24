using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace NetOdyssey
{
	public static class clsIPAddressExtensions
	{
		public static IPAddress GetBroadcastAddress(this IPAddress inIPAddress, IPAddress inSubnetMask)
		{
			byte[] _ipAdressBytes = inIPAddress.GetAddressBytes();
			byte[] _subnetMaskBytes = inSubnetMask.GetAddressBytes();

			if (_ipAdressBytes.Length != _subnetMaskBytes.Length)
				throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

			byte[] broadcastAddress = new byte[_ipAdressBytes.Length];
			for (int i = 0; i < broadcastAddress.Length; i++)
				broadcastAddress[i] = (byte) (_ipAdressBytes[i] | (_subnetMaskBytes[i] ^ 255));
			
			return new IPAddress(broadcastAddress);
		}

		public static IPAddress GetNetworkAddress(this IPAddress inIPAddress, IPAddress inSubnetMask)
		{
			byte[] _ipAdressBytes = inIPAddress.GetAddressBytes();
			byte[] _subnetMaskBytes = inSubnetMask.GetAddressBytes();

			if (_ipAdressBytes.Length != _subnetMaskBytes.Length)
				throw new ArgumentException("Lengths of IP address and subnet mask do not match.");

			byte[] _networkAddress = new byte[_ipAdressBytes.Length];
			for (int i = 0; i < _networkAddress.Length; i++)
				_networkAddress[i] = (byte) (_ipAdressBytes[i] & (_subnetMaskBytes[i]));

			return new IPAddress(_networkAddress);
		}

		public static bool IsInSameSubnet(this IPAddress inIPAddress2, IPAddress inIPAddress, IPAddress inSubnetMask)
		{
			IPAddress _network1 = inIPAddress.GetNetworkAddress(inSubnetMask);
			IPAddress _network2 = inIPAddress2.GetNetworkAddress(inSubnetMask);

			return _network1.Equals(_network2);
		}
	}
}
