using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;

using System.Security.Claims;
using Newtonsoft.Json.Converters;

namespace DataEntryApp.ApiClasses
{
    public class BookSts 
    {
        public int packageUserId { get; set; }
        public Nullable<int> packageId { get; set; }
        public Nullable<int> userId { get; set; }
        public string packageSaleCode { get; set; }
        public string packageNumber { get; set; }//1- Book num
        public Nullable<int> customerId { get; set; }
        public string customerServerCode { get; set; }

        public string notes { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }
        //public Nullable<System.DateTime> updateDate { get; set; }
        //public Nullable<int> createUserId { get; set; }
        //public Nullable<int> updateUserId { get; set; }

        public bool canDelete { get; set; }
        public string userName { get; set; }
        public string userLastName { get; set; }
        public string customerName { get; set; }
        public string customerLastName { get; set; }
        public string customerCompany { get; set; } //6-customer
        public Nullable<int> customerCountryId { get; set; }
        public string customerCountry { get; set; }
        public Nullable<int> countryPackageId { get; set; }
        public bool canRenew { get; set; }

        public int oldPackageId { get; set; }


        public int monthCount { get; set; }


        public Nullable<int> oldCountryPackageId { get; set; }

        public string packageName { get; set; }//4-Package Name


        public string currency { get; set; }
        public string programName { get; set; }//Program Name 
        public string verName { get; set; }//Version Name 
        public int branchCount { get; set; }
        public int programId { get; set; }
        public int verId { get; set; }
        public int totalsalesInvCount { get; set; }



        public bool isBooked { get; set; }


        public Nullable<System.DateTime> bookDate { get; set; }//2- Book Date
        public Nullable<int> isActive { get; set; }
        public Nullable<System.DateTime> expireDate { get; set; }
        public Nullable<bool> isOnlineServer { get; set; }


        public string typePu { get; set; }
        public bool isPayed { get; set; }
        public int salesInvCount { get; set; }

        public Nullable<System.DateTime> activatedate { get; set; }
        public bool isServerActivated { get; set; }
        public bool islimitDate { get; set; }

        //agent
        public Nullable<int> agentId { get; set; }
        public string agentName { get; set; }// 5- Agent name 
        public string agentAccountName { get; set; }//5- Agent AccountName
        public string agentLastName { get; set; }//5- Agent LastName
        public string agentCompany { get; set; }
        public string agentType { get; set; }
        public Nullable<int> agentCountryId { get; set; }
        public string agentCountry { get; set; }
        // createUser payOp
        public string CusrName { get; set; }//
        public string CusrAccountName { get; set; }//
        public string CusrLastName { get; set; }//
        public string CusrCompany { get; set; }
        public string CusrType { get; set; }
        //countries
        public string countryName { get; set; }
        public Nullable<int> countryId { get; set; }
        //paty OP
        public int payOpId { get; set; }
        public decimal Price { get; set; }//7- price
        public string codePo { get; set; }
        public string typePo { get; set; }
        public decimal discountValue { get; set; }
        public Nullable<int> agentIdPo { get; set; }
        public Nullable<int> customerIdPo { get; set; }
        public Nullable<int> countryPackageIdPo { get; set; }
        public decimal totalnet { get; set; }//paid
        public Nullable<int> packageIdPo { get; set; }

        public Nullable<int> createUserIdPo { get; set; }
        public Nullable<int> updateUserIdPo { get; set; }
        public Nullable<System.DateTime> createDatePo { get; set; }//3-Upgrade Date
        public Nullable<System.DateTime> updateDatePo { get; set; }
   



    }
    public class PaymentsSts 
    {
        public int packageUserId { get; set; }
        public Nullable<int> packageId { get; set; }
        public Nullable<int> userId { get; set; }
        public string packageSaleCode { get; set; }
        public string packageNumber { get; set; }//1- Book num
        public Nullable<int> customerId { get; set; }
        public string customerServerCode { get; set; }

        public string notes { get; set; }
        public Nullable<System.DateTime> createDate { get; set; }


        public bool canDelete { get; set; }
        public string userName { get; set; }
        public string userLastName { get; set; }
        public string customerName { get; set; }
        public string customerLastName { get; set; }
        public string customerCompany { get; set; }//6- customer
        public Nullable<int> customerCountryId { get; set; }
        public string customerCountry { get; set; }
        public Nullable<int> countryPackageId { get; set; }
        public bool canRenew { get; set; }

        public int oldPackageId { get; set; }


        public int monthCount { get; set; }


        public Nullable<int> oldCountryPackageId { get; set; }

        public string packageName { get; set; }//4-Package Name


        public string currency { get; set; }
        public string programName { get; set; }//Program Name 
        public string verName { get; set; }//Version Name 
        public int branchCount { get; set; }
        public int programId { get; set; }
        public int verId { get; set; }
        public int totalsalesInvCount { get; set; }



        public bool isBooked { get; set; }


        public Nullable<System.DateTime> bookDate { get; set; }//2- Book Date
        public Nullable<int> isActive { get; set; }
        public Nullable<System.DateTime> expireDate { get; set; }
        public Nullable<bool> isOnlineServer { get; set; }


        public string typePu { get; set; }
        public bool isPayed { get; set; }
        public int salesInvCount { get; set; }

        public Nullable<System.DateTime> activatedate { get; set; }
        public bool isServerActivated { get; set; }
        public bool islimitDate { get; set; }

        //agent
        public Nullable<int> agentId { get; set; }
        public string agentName { get; set; }// 5- Agent name 
        public string agentAccountName { get; set; }//5- Agent AccountName
        public string agentLastName { get; set; }//5- Agent LastName
        public string agentCompany { get; set; }
        public string agentType { get; set; }
        public Nullable<int> agentCountryId { get; set; }
        public string agentCountry { get; set; }
        // createUser payOp
        public string CusrName { get; set; }//
        public string CusrAccountName { get; set; }//
        public string CusrLastName { get; set; }//
        public string CusrCompany { get; set; }
        public string CusrType { get; set; }
        //countries
        public string countryName { get; set; }
        public Nullable<int> countryId { get; set; }
        //paty OP
        public int payOpId { get; set; }
        public decimal Price { get; set; }//7- price
        public string codePo { get; set; }
        public string typePo { get; set; }
        public decimal discountValue { get; set; }
        public Nullable<int> agentIdPo { get; set; }
        public Nullable<int> customerIdPo { get; set; }
        public Nullable<int> countryPackageIdPo { get; set; }
        public decimal totalnet { get; set; }//paid
        public Nullable<int> packageIdPo { get; set; }

        public Nullable<int> createUserIdPo { get; set; }
        public Nullable<int> updateUserIdPo { get; set; }
        public Nullable<System.DateTime> createDatePo { get; set; }//3-Upgrade Date
        public Nullable<System.DateTime> updateDatePo { get; set; }



    }
    public class Statistics
    {

    

        //private string urimainpath = "Statistics/";
        ///// <summary>
        ///// ///////////////////////////////////////
        ///// </summary>
        ///// <returns></returns>
        ///// 
      

        //public async Task<List<BookSts>> GetByAgentId(int agentId)
        //{
        //   List<BookSts> list = new List<BookSts>();
        //    Dictionary<string, string> parameters = new Dictionary<string, string>();
        //    parameters.Add("agentId", agentId.ToString());
        //    //#################
        //    IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetByAgentId", parameters);

        //    foreach (Claim c in claims)
        //    {
        //        if (c.Type == "scopes")
        //        {
        //            list.Add(JsonConvert.DeserializeObject<BookSts>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
        //        }
        //    }
        //    return list;

        //}

        //public async Task<List<BookSts>> GetAllBooks()
        //{
        //    List<BookSts> list = new List<BookSts>();
        //    Dictionary<string, string> parameters = new Dictionary<string, string>();
        //   // parameters.Add("agentId", agentId.ToString());
        //    //#################
        //    IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetAllBooks");

        //    foreach (Claim c in claims)
        //    {
        //        if (c.Type == "scopes")
        //        {
        //            list.Add(JsonConvert.DeserializeObject<BookSts>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
        //        }
        //    }
        //    return list;

        //}

        ////payments
        //public async Task<List<PaymentsSts>> GetPaymentsByAgentId(int agentId)
        //{
        //    List<PaymentsSts> list = new List<PaymentsSts>();
        //    Dictionary<string, string> parameters = new Dictionary<string, string>();
        //    parameters.Add("agentId", agentId.ToString());
        //    //#################
        //    IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetPaymentsByAgentId", parameters);

        //    foreach (Claim c in claims)
        //    {
        //        if (c.Type == "scopes")
        //        {
        //            list.Add(JsonConvert.DeserializeObject<PaymentsSts>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
        //        }
        //    }
        //    return list;

        //}

        //public async Task<List<PaymentsSts>> GetAllPayments()
        //{
        //    List<PaymentsSts> list = new List<PaymentsSts>();
        //    Dictionary<string, string> parameters = new Dictionary<string, string>();
        //    // parameters.Add("agentId", agentId.ToString());
        //    //#################
        //    IEnumerable<Claim> claims = await APIResult.getList(urimainpath + "GetAllPayments");

        //    foreach (Claim c in claims)
        //    {
        //        if (c.Type == "scopes")
        //        {
        //            list.Add(JsonConvert.DeserializeObject<PaymentsSts>(c.Value, new IsoDateTimeConverter { DateTimeFormat = "dd/MM/yyyy" }));
        //        }
        //    }
        //    return list;

        //}

        ///// ////



    }
}
