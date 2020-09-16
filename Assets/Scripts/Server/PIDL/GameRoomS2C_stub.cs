﻿




// Generated by PIDL compiler.
// Do not modify this file, but modify the source .pidl file.

using System;
using System.Net;	     

namespace GameRoomS2C
{
	internal class Stub:Nettention.Proud.RmiStub
	{
public AfterRmiInvocationDelegate AfterRmiInvocation = delegate(Nettention.Proud.AfterRmiSummary summary) {};
public BeforeRmiInvocationDelegate BeforeRmiInvocation = delegate(Nettention.Proud.BeforeRmiSummary summary) {};

		public delegate bool NotifyUserConnectedDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, String nickname, bool isReady);  
		public NotifyUserConnectedDelegate NotifyUserConnected = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, String nickname, bool isReady)
		{ 
			return false;
		};
		public delegate bool NotifyUserDisconnectedDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, String nickname);  
		public NotifyUserDisconnectedDelegate NotifyUserDisconnected = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, String nickname)
		{ 
			return false;
		};
		public delegate bool NotifyUserReadyDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, String nickname);  
		public NotifyUserReadyDelegate NotifyUserReady = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext, String nickname)
		{ 
			return false;
		};
		public delegate bool NotifyGameStartedDelegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext);  
		public NotifyGameStartedDelegate NotifyGameStarted = delegate(Nettention.Proud.HostID remote,Nettention.Proud.RmiContext rmiContext)
		{ 
			return false;
		};
	public override bool ProcessReceivedMessage(Nettention.Proud.ReceivedMessage pa, Object hostTag) 
	{
		Nettention.Proud.HostID remote=pa.RemoteHostID;
		if(remote==Nettention.Proud.HostID.HostID_None)
		{
			ShowUnknownHostIDWarning(remote);
		}

		Nettention.Proud.Message __msg=pa.ReadOnlyMessage;
		int orgReadOffset = __msg.ReadOffset;
        Nettention.Proud.RmiID __rmiID = Nettention.Proud.RmiID.RmiID_None;
        if (!__msg.Read( out __rmiID))
            goto __fail;
					
		switch(__rmiID)
		{
        case Common.NotifyUserConnected:
            ProcessReceivedMessage_NotifyUserConnected(__msg, pa, hostTag, remote);
            break;
        case Common.NotifyUserDisconnected:
            ProcessReceivedMessage_NotifyUserDisconnected(__msg, pa, hostTag, remote);
            break;
        case Common.NotifyUserReady:
            ProcessReceivedMessage_NotifyUserReady(__msg, pa, hostTag, remote);
            break;
        case Common.NotifyGameStarted:
            ProcessReceivedMessage_NotifyGameStarted(__msg, pa, hostTag, remote);
            break;
		default:
			 goto __fail;
		}
		return true;
__fail:
	  {
			__msg.ReadOffset = orgReadOffset;
			return false;
	  }
	}
    void ProcessReceivedMessage_NotifyUserConnected(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        String nickname; Nettention.Proud.Marshaler.Read(__msg,out nickname);	
bool isReady; Nettention.Proud.Marshaler.Read(__msg,out isReady);	
core.PostCheckReadMessage(__msg, RmiName_NotifyUserConnected);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=nickname.ToString()+",";
parameterString+=isReady.ToString()+",";
        NotifyCallFromStub(Common.NotifyUserConnected, RmiName_NotifyUserConnected,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.NotifyUserConnected;
        summary.rmiName = RmiName_NotifyUserConnected;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =NotifyUserConnected (remote,ctx , nickname, isReady );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_NotifyUserConnected);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.NotifyUserConnected;
        summary.rmiName = RmiName_NotifyUserConnected;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_NotifyUserDisconnected(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        String nickname; Nettention.Proud.Marshaler.Read(__msg,out nickname);	
core.PostCheckReadMessage(__msg, RmiName_NotifyUserDisconnected);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=nickname.ToString()+",";
        NotifyCallFromStub(Common.NotifyUserDisconnected, RmiName_NotifyUserDisconnected,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.NotifyUserDisconnected;
        summary.rmiName = RmiName_NotifyUserDisconnected;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =NotifyUserDisconnected (remote,ctx , nickname );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_NotifyUserDisconnected);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.NotifyUserDisconnected;
        summary.rmiName = RmiName_NotifyUserDisconnected;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_NotifyUserReady(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        String nickname; Nettention.Proud.Marshaler.Read(__msg,out nickname);	
core.PostCheckReadMessage(__msg, RmiName_NotifyUserReady);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
        parameterString+=nickname.ToString()+",";
        NotifyCallFromStub(Common.NotifyUserReady, RmiName_NotifyUserReady,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.NotifyUserReady;
        summary.rmiName = RmiName_NotifyUserReady;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =NotifyUserReady (remote,ctx , nickname );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_NotifyUserReady);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.NotifyUserReady;
        summary.rmiName = RmiName_NotifyUserReady;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
    void ProcessReceivedMessage_NotifyGameStarted(Nettention.Proud.Message __msg, Nettention.Proud.ReceivedMessage pa, Object hostTag, Nettention.Proud.HostID remote)
    {
        Nettention.Proud.RmiContext ctx = new Nettention.Proud.RmiContext();
        ctx.sentFrom=pa.RemoteHostID;
        ctx.relayed=pa.IsRelayed;
        ctx.hostTag=hostTag;
        ctx.encryptMode = pa.EncryptMode;
        ctx.compressMode = pa.CompressMode;

        core.PostCheckReadMessage(__msg, RmiName_NotifyGameStarted);
        if(enableNotifyCallFromStub==true)
        {
        string parameterString = "";
                NotifyCallFromStub(Common.NotifyGameStarted, RmiName_NotifyGameStarted,parameterString);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.BeforeRmiSummary summary = new Nettention.Proud.BeforeRmiSummary();
        summary.rmiID = Common.NotifyGameStarted;
        summary.rmiName = RmiName_NotifyGameStarted;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        BeforeRmiInvocation(summary);
        }

        long t0 = Nettention.Proud.PreciseCurrentTime.GetTimeMs();

        // Call this method.
        bool __ret =NotifyGameStarted (remote,ctx  );

        if(__ret==false)
        {
        // Error: RMI function that a user did not create has been called. 
        core.ShowNotImplementedRmiWarning(RmiName_NotifyGameStarted);
        }

        if(enableStubProfiling)
        {
        Nettention.Proud.AfterRmiSummary summary = new Nettention.Proud.AfterRmiSummary();
        summary.rmiID = Common.NotifyGameStarted;
        summary.rmiName = RmiName_NotifyGameStarted;
        summary.hostID = remote;
        summary.hostTag = hostTag;
        summary.elapsedTime = Nettention.Proud.PreciseCurrentTime.GetTimeMs()-t0;
        AfterRmiInvocation(summary);
        }
    }
#if USE_RMI_NAME_STRING
// RMI name declaration.
// It is the unique pointer that indicates RMI name such as RMI profiler.
public const string RmiName_NotifyUserConnected="NotifyUserConnected";
public const string RmiName_NotifyUserDisconnected="NotifyUserDisconnected";
public const string RmiName_NotifyUserReady="NotifyUserReady";
public const string RmiName_NotifyGameStarted="NotifyGameStarted";
       
public const string RmiName_First = RmiName_NotifyUserConnected;
#else
// RMI name declaration.
// It is the unique pointer that indicates RMI name such as RMI profiler.
public const string RmiName_NotifyUserConnected="";
public const string RmiName_NotifyUserDisconnected="";
public const string RmiName_NotifyUserReady="";
public const string RmiName_NotifyGameStarted="";
       
public const string RmiName_First = "";
#endif
		public override Nettention.Proud.RmiID[] GetRmiIDList { get{return Common.RmiIDList;} }
		
	}
}

