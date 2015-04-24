using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Sockets;

namespace NetOdyssey
{
	class clsIPAddressRange
	{
		static AddressFamily _addressFamily = Program.prpSettings.LowerIPAddress.AddressFamily;
		public static byte[] _lowerIPAddressBytes = Program.prpSettings.LowerIPAddress.GetAddressBytes();
		public static byte[] _upperIPAddressBytes = Program.prpSettings.UpperIPAddress.GetAddressBytes();

		/// <summary>
		/// Determines whether an IP address is within an IP address range.
		/// </summary>
		/// <param name="inIPAddress">The IP address to compare if is within an IP address range.</param>
		/// <returns>True if the IP address is within, false otherwise.</returns>
		public static bool isInRange(IPAddress inIPAddress)
		{
			if (inIPAddress.AddressFamily != _addressFamily)
				return false;

			byte[] _ipAddressBytes = inIPAddress.GetAddressBytes();
			bool _lowerBoundary = true, _upperBoundary = true;

			for (int i = 0; i < _lowerIPAddressBytes.Length && (_lowerBoundary || _upperBoundary); i++)
			{
				if ((_lowerBoundary && _ipAddressBytes[i] < _lowerIPAddressBytes[i]) ||
					(_upperBoundary && _ipAddressBytes[i] > _upperIPAddressBytes[i]))
					return false;

				_lowerBoundary &= (_ipAddressBytes[i] == _lowerIPAddressBytes[i]);
				_upperBoundary &= (_ipAddressBytes[i] == _upperIPAddressBytes[i]);
			}

			return true;
		}
	}
}
