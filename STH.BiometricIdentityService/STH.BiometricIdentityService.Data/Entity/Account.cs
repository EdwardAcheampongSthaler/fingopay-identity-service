using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STH.BiometricIdentityService.Data.Entity
{
    public class Account
    {
        public string Id { get; set; }
        public string EmailAddress { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string EnrollmentPinCode { get; set; }
        public bool Active { get; set; }
        public string UserId { get; set; }
    }
    // id, emailaddress, userid, createddate, updateddate, enrollmentpin, active
    // transactions -> id, raw, accountid 
    // TransactionType
    // accountbiometric -> accountid, biometricid, createddate,uupdatedate
    // Biometrics
    // paymentoption id pamentoptionid paymenttoken, active audit

    // paymentoption id name description audit
}
