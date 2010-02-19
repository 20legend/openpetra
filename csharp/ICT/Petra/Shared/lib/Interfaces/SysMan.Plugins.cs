﻿/*************************************************************************
 *
 * DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
 *
 * @Authors:
 *       timop
 *
 * Copyright 2004-2009 by OM International
 *
 * This file is part of OpenPetra.org.
 *
 * OpenPetra.org is free software: you can redistribute it and/or modify
 * it under the terms of the GNU General Public License as published by
 * the Free Software Foundation, either version 3 of the License, or
 * (at your option) any later version.
 *
 * OpenPetra.org is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
 * GNU General Public License for more details.
 *
 * You should have received a copy of the GNU General Public License
 * along with OpenPetra.org.  If not, see <http://www.gnu.org/licenses/>.
 *
 ************************************************************************/
using System;
using System.Xml;

namespace Ict.Petra.Shared.Interfaces.Plugins.MSysMan
{
    /// <summary>
    /// This interface defines which methods need to be implemented
    /// by a plugin for authentication of users
    /// e.g. you can authenticate against an LDAP server
    /// or use the authentication of another application that the users are already using
    /// </summary>
    public interface IUserAuthentication
    {
        /// <summary>
        /// return true if the user is known and the password is correct;
        /// otherwise returns false and an error message
        /// </summary>
        bool AuthenticateUser(string AUsername, string APassword, out string AMessage);
        
        /// <summary>
        /// this will allow the system administrator to change the password of the user
        /// </summary>
        bool SetPassword(string AUsername, string APassword);
    }
}