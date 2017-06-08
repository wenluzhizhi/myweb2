using UnityEngine;
using System.Collections;
using System.Security.Cryptography;
using System;

public class Utils
{

	public static int getRandom1()
	{

		byte[] randomBytes = new byte[4];
		RNGCryptoServiceProvider rngCrypto =
			new RNGCryptoServiceProvider();
		rngCrypto.GetBytes(randomBytes);
		int rngNum = BitConverter.ToInt32(randomBytes, 0);
		return rngNum;
	}
}
