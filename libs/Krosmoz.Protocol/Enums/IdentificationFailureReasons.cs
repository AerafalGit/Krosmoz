// Copyright (c) Krosmoz 2025.
// Krosmoz licenses this file to you under the MIT license.
// See the license here https://github.com/AerafalGit/Krosmoz/blob/main/LICENSE.

namespace Krosmoz.Protocol.Enums;

public enum IdentificationFailureReasons
{
	BadVersion = 1,
	WrongCredentials = 2,
	Banned = 3,
	Kicked = 4,
	InMaintenance = 5,
	TooManyOnIp = 6,
	TimeOut = 7,
	BadIprange = 8,
	CredentialsReset = 9,
	EmailUnvalidated = 10,
	OtpTimeout = 11,
	ServiceUnavailable = 53,
	UnknownAuthError = 99,
	Spare = 100
}
