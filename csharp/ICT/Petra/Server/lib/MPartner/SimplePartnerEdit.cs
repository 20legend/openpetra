//
// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//       timop
//
// Copyright 2004-2010 by OM International
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
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Data;
using System.Data.Odbc;
using System.Xml;
using System.IO;
using Mono.Unix;
using Ict.Common;
using Ict.Common.IO;
using Ict.Common.DB;
using Ict.Common.Verification;
using Ict.Petra.Shared.MPartner;
using Ict.Petra.Shared.MPartner.Partner.Data;
using Ict.Petra.Shared.MPartner.Mailroom.Data;
using Ict.Petra.Server.MPartner.Partner.Data.Access;
using Ict.Petra.Server.MPartner.Mailroom.Data.Access;
using Ict.Petra.Server.MPartner.Common;

namespace Ict.Petra.Server.MPartner.Partner.WebConnectors
{
    /// <summary>
    /// functions for creating new partners and to edit partners
    /// </summary>
    public class TSimplePartnerEditWebConnector
    {
        /// <summary>
        /// get a partner key for a new partner
        /// </summary>
        /// <param name="AFieldPartnerKey">can be -1, then the default site key is used</param>
        public static Int64 NewPartnerKey(Int64 AFieldPartnerKey)
        {
            return TNewPartnerKey.GetNewPartnerKey(AFieldPartnerKey);
        }

        /// <summary>
        /// return the existing data of a partner
        /// </summary>
        /// <returns></returns>
        public static PartnerEditTDS GetPartnerDetails(Int64 APartnerKey, bool AWithAddressDetails, bool AWithSubscriptions, bool AWithRelationships)
        {
            PartnerEditTDS MainDS = new PartnerEditTDS();

            TDBTransaction Transaction = DBAccess.GDBAccessObj.BeginTransaction(IsolationLevel.Serializable);

            PPartnerAccess.LoadByPrimaryKey(MainDS, APartnerKey, Transaction);

            if (MainDS.PPartner.Rows.Count == 0)
            {
                return null;
            }

            switch (MainDS.PPartner[0].PartnerClass)
            {
                case MPartnerConstants.PARTNERCLASS_FAMILY:
                    PFamilyAccess.LoadByPrimaryKey(MainDS, APartnerKey, Transaction);
                    break;

                case MPartnerConstants.PARTNERCLASS_PERSON:
                    PPersonAccess.LoadByPrimaryKey(MainDS, APartnerKey, Transaction);
                    break;

                case MPartnerConstants.PARTNERCLASS_CHURCH:
                    PChurchAccess.LoadByPrimaryKey(MainDS, APartnerKey, Transaction);
                    break;

                case MPartnerConstants.PARTNERCLASS_ORGANISATION:
                    POrganisationAccess.LoadByPrimaryKey(MainDS, APartnerKey, Transaction);
                    break;
            }

            if (AWithAddressDetails)
            {
                PPartnerLocationAccess.LoadViaPPartner(MainDS, APartnerKey, Transaction);
                PLocationAccess.LoadViaPPartner(MainDS, APartnerKey, Transaction);
            }

            if (AWithRelationships)
            {
                PPartnerRelationshipAccess.LoadViaPPartnerPartnerKey(MainDS, APartnerKey, Transaction);
            }

            if (AWithSubscriptions)
            {
                PSubscriptionAccess.LoadViaPPartnerPartnerKey(MainDS, APartnerKey, Transaction);
            }

            DBAccess.GDBAccessObj.RollbackTransaction();

            return null;
        }

        /// <summary>
        /// store the currently edited partner
        /// </summary>
        /// <returns></returns>
        public static bool SavePartner(PartnerEditTDS AMainDS, out TVerificationResultCollection AVerificationResult)
        {
            Console.WriteLine("save partner " + AMainDS.PPartner.Rows.Count.ToString());
            return PartnerEditTDSAccess.SubmitChanges(AMainDS, out AVerificationResult) == TSubmitChangesResult.scrOK;
        }
    }
}