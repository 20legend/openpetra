﻿//
// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//       timop
//
// Copyright 2004-2018 by OM International
//
// This file is part of OpenPetra.org.
//
// OpenPetra.org is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// OpenPetra.org is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with OpenPetra.org.  If not, see <http://www.gnu.org/licenses/>.
//
using System;
using System.Data;
using System.IO;
using System.Collections;
using System.Security.Principal;
using System.Web;
using Ict.Petra.Shared;
using Ict.Common;
using Ict.Common.Remoting.Server;
using Ict.Common.DB;
using Ict.Common.Session;
using Ict.Petra.Server.App.Core.Security;
using Ict.Petra.Shared.Security;

namespace Ict.Petra.Server.App.Core.ServerAdmin.WebConnectors
{
    ///<summary>
    /// This connector provides methods for the server admin console
    ///</summary>
    public class TServerAdminWebConnector
    {
        /// <summary>
        /// login the server admin console without a password
        /// </summary>
        [CheckServerAdminToken]
        public static bool LoginServerAdmin()
        {
            // create a new session, with database connection and everything that is needed
            // see also Ict.Petra.Server.App.WebService.TOpenPetraOrgSessionManager.Login()
            if (DBAccess.GDBAccessObj == null)
            {
                TServerManager.TheCastedServerManager.EstablishDBConnection();
            }

            string WelcomeMessage;
            bool SystemEnabled;
            IPrincipal LocalUserInfo;
            Int32 ClientID;

            TConnectedClient CurrentClient = TClientManager.ConnectClient(
                "SYSADMIN", string.Empty,
                HttpContext.Current.Request.UserHostName,
                HttpContext.Current.Request.UserHostAddress,
                TFileVersionInfo.GetApplicationVersion().ToVersion(),
                TClientServerConnectionType.csctRemote,
                out ClientID,
                out WelcomeMessage,
                out SystemEnabled,
                out LocalUserInfo);
            TSession.SetVariable("LoggedIn", true);

            // the following values are stored in the session object
            DomainManager.GClientID = ClientID;
            DomainManager.CurrentClient = CurrentClient;
            UserInfo.GUserInfo = (TPetraPrincipal)LocalUserInfo;

            DBAccess.GDBAccessObj.UserID = "SYSADMIN";

            TServerManager.TheCastedServerManager.AddDBConnection(DBAccess.GDBAccessObj);

            return true;
        }

        /// <summary>
        /// get the number of clients that have connected to this server since the last restart
        /// </summary>
        [RequireModulePermission("SYSMAN")]
        public static int GetClientsConnectedTotal()
        {
            return TServerManagerBase.TheServerManager.ClientsConnectedTotal;
        }

        /// <summary>
        /// get number of currently connected clients
        /// </summary>
        [RequireModulePermission("SYSMAN")]
        public static int GetClientsConnected()
        {
            return TServerManagerBase.TheServerManager.ClientsConnected;
        }

        /// <summary>
        /// get the site key
        /// </summary>
        [RequireModulePermission("SYSMAN")]
        public static Int64 GetSiteKey()
        {
            return TServerManagerBase.TheServerManager.SiteKey;
        }

        /// <summary>
        /// get the count of attemts to reconnect to the database
        /// </summary>
        [RequireModulePermission("SYSMAN")]
        public static Int64 GetDBReconnectionAttemptsCounter()
        {
            return TServerManagerBase.TheServerManager.DBReconnectionAttemptsCounter;
        }

        /// <summary>
        /// get the interval of checking the DB connection
        /// </summary>
        [RequireModulePermission("SYSMAN")]
        public static Int64 GetDBConnectionCheckInterval()
        {
            return TServerManagerBase.TheServerManager.DBConnectionCheckInterval;
        }

        /// <summary>
        /// get list of clients
        /// </summary>
        [RequireModulePermission("SYSMAN")]
        public static ArrayList GetClientList()
        {
            return TServerManagerBase.TheServerManager.ClientList;
        }

        /// <summary>
        /// version of the server
        /// </summary>
        [RequireModulePermission("SYSMAN")]
        public static String GetServerInfoVersion()
        {
            return TServerManagerBase.TheServerManager.ServerInfoVersion;
        }

        /// <summary>
        /// state of the server
        /// </summary>
        [RequireModulePermission("SYSMAN")]
        public static String GetServerInfoState()
        {
            return TServerManagerBase.TheServerManager.ServerInfoState;
        }

        /// <summary>
        /// how much memory is used by the server
        /// </summary>
        [RequireModulePermission("SYSMAN")]
        public static System.Int64 GetServerInfoMemory()
        {
            return TServerManagerBase.TheServerManager.ServerInfoMemory;
        }

        /// <summary>
        /// command to stop the server in a more controlled way than <see cref="StopServer" /> (gets all connected clients to disconnect)
        /// </summary>
        [RequireModulePermission("SYSMAN")]
        public static bool StopServerControlled(bool AForceAutomaticClosing)
        {
            return TServerManagerBase.TheServerManager.StopServerControlled(AForceAutomaticClosing);
        }

        /// <summary>
        /// command to stop the server unconditionally (forces 'hard' disconnection of all Clients!)
        /// </summary>
        [RequireModulePermission("SYSMAN")]
        public static void StopServer()
        {
            TServerManagerBase.TheServerManager.StopServer();
        }

        /// <summary>
        /// perform garbage collection on the server
        /// </summary>
        /// <returns></returns>
        [RequireModulePermission("SYSMAN")]
        public static System.Int64 PerformGC()
        {
            return TServerManagerBase.TheServerManager.PerformGC();
        }

        /// <summary>
        /// get a formatted list of clients
        /// </summary>
        /// <param name="AListDisconnectedClients"></param>
        /// <returns></returns>
        [RequireModulePermission("SYSMAN")]
        public static String FormatClientList(Boolean AListDisconnectedClients)
        {
            return TServerManagerBase.TheServerManager.FormatClientList(AListDisconnectedClients);
        }

        /// <summary>
        /// get a formatted list for sysadm (also useful for webmin???)
        /// </summary>
        /// <param name="AListDisconnectedClients"></param>
        /// <returns></returns>
        [RequireModulePermission("SYSMAN")]
        public static String FormatClientListSysadm(Boolean AListDisconnectedClients)
        {
            return TServerManagerBase.TheServerManager.FormatClientListSysadm(AListDisconnectedClients);
        }

        /// <summary>
        /// put a task into a queue on the server
        /// </summary>
        /// <param name="AClientID"></param>
        /// <param name="ATaskGroup"></param>
        /// <param name="ATaskCode"></param>
        /// <param name="ATaskPriority"></param>
        /// <param name="ATaskParameter1">Parameter #1 for the Task (depending on the TaskGroup
        /// this can be left empty)</param>
        /// <param name="ATaskParameter2">Parameter #2 for the Task (depending on the TaskGroup
        /// this can be left empty)</param>
        /// <param name="ATaskParameter3">Parameter #3 for the Task (depending on the TaskGroup
        /// this can be left empty)</param>
        /// <param name="ATaskParameter4">Parameter #4 for the Task (depending on the TaskGroup
        /// this can be left empty)</param>
        /// <returns></returns>
        [RequireModulePermission("SYSMAN")]
        public static bool QueueClientTask(System.Int16 AClientID, String ATaskGroup, String ATaskCode, object ATaskParameter1,
            object ATaskParameter2, object ATaskParameter3, object ATaskParameter4, System.Int16 ATaskPriority)
        {
            return TServerManagerBase.TheServerManager.QueueClientTask(AClientID, ATaskGroup, ATaskCode, ATaskParameter1, ATaskParameter2, ATaskParameter3, ATaskParameter4, ATaskPriority);
        }

        /// <summary>
        /// disconnect a client
        /// </summary>
        /// <param name="AClientID"></param>
        /// <param name="ACantDisconnectReason"></param>
        /// <returns></returns>
        [RequireModulePermission("SYSMAN")]
        public static bool DisconnectClient(System.Int16 AClientID, out String ACantDisconnectReason)
        {
            return TServerManagerBase.TheServerManager.DisconnectClient(AClientID, out ACantDisconnectReason);
        }

        /// <summary>
        /// upgrades the database
        /// </summary>
        /// <returns>true if there has been an update</returns>
        [RequireModulePermission("SYSMAN")]
        public static bool UpgradeDatabase()
        {
            return TServerManagerBase.TheServerManager.UpgradeDatabase();
        }

        /// <summary>
        /// returns a string with yml.gz data
        /// </summary>
        /// <returns></returns>
        [RequireModulePermission("SYSMAN")]
        public static string BackupDatabaseToYmlGZ()
        {
            return TServerManagerBase.TheServerManager.BackupDatabaseToYmlGZ();
        }

        /// <summary>
        /// restore the database from a string with yml.gz data
        /// </summary>
        /// <returns></returns>
        [RequireModulePermission("SYSMAN")]
        public static bool RestoreDatabaseFromYmlGZ(string AYmlGzData)
        {
            // set the client id to avoid problems with missing ClientID for InitProgressTracker during restore of database
            DomainManager.GClientID = -1;

            return TServerManagerBase.TheServerManager.RestoreDatabaseFromYmlGZ(AYmlGzData);
        }

        /// <summary>
        /// Marks all DataTables in the Cache to be no longer up-to-date (=out of sync
        /// with the data that was originally placed in the DataTable).
        /// </summary>
        [RequireModulePermission("SYSMAN")]
        public static void RefreshAllCachedTables()
        {
            TServerManagerBase.TheServerManager.RefreshAllCachedTables();
        }

        /// <summary>
        /// Clears (flushes) all RDMBS Connection Pools and returns the new number of DB Connections after clearing all
        /// RDMBS Connection Pools.
        /// </summary>
        [RequireModulePermission("SYSMAN")]
        public static int ClearConnectionPoolAndGetNumberOfDBConnections()
        {
            return TServerManagerBase.TheServerManager.ClearConnectionPoolAndGetNumberOfDBConnections();
        }

        /// <summary>
        /// set the password of a user
        /// </summary>
        [RequireModulePermission("SYSMAN")]
        public static bool SetPassword(string AUserID, string APassword)
        {
            return TServerManagerBase.TheServerManager.SetPassword(AUserID, APassword);
        }

        /// <summary>
        /// add a new user
        /// </summary>
        /// <returns></returns>
        [RequireModulePermission("SYSMAN")]
        public static bool AddUser(string AUserID)
        {
            return TServerManagerBase.TheServerManager.AddUser(AUserID);
        }

        /// <summary>
        /// add a new user
        /// </summary>
        /// <returns></returns>
        [RequireModulePermission("SYSMAN")]
        public static bool AddUser(string AUserID, string APassword = "")
        {
            return TServerManagerBase.TheServerManager.AddUser(AUserID, APassword);
        }

        /// <summary>
        /// Lists the GPG keys for the Intranet server that are available to the Petra Server.
        /// </summary>
        /// <param name="List">Returns the output of the external gpg command.</param>
        /// <returns>Return code of gpg command.</returns>
        [RequireModulePermission("SYSMAN")]
        public static int ListGpgKeys(out string List)
        {
            return TServerManagerBase.TheServerManager.ListGpgKeys(out List);
        }

        /// <summary>
        /// Imports the GPG encryption keys for the Intranet server.
        /// </summary>
        /// <param name="List">Return the output of the gpg command.</param>
        /// <returns>Return code of external gpg command.</returns>
        [RequireModulePermission("SYSMAN")]
        public static int ImportGpgKeys(out string List)
        {
            return TServerManagerBase.TheServerManager.ImportGpgKeys(out List);
        }

        /// Allows the admin console to run a timed job now
        [RequireModulePermission("SYSMAN")]
        public static void PerformTimedProcessingNow(string AProcessName)
        {
            TServerManagerBase.TheServerManager.PerformTimedProcessingNow(AProcessName);
        }

        /// <summary>
        /// the host name of the smtp server
        /// </summary>
        [RequireModulePermission("SYSMAN")]
        public static string GetSmtpHost()
        {
            return TServerManagerBase.TheServerManager.SmtpHost;
        }
    }
}
