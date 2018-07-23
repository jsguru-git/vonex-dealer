using System;
using System.Collections.Generic;
using System.Text;

namespace Fiver.Api.HttpClient.Models
{
    public class DomainInfo
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<string> Alias { get; set; }
        public string Country { get; set; }
        public int Extensions { get; set; }
        public int Accounts { get; set; }
        public bool Visible { get; set; }
        public int Registrations { get; set; }
        public int Regs { get; set; }
        public int ActiveExtensions { get; set; }
    }


}

/*
 * [{"id":78,
 * "name":"vonex-monitoring-donotdel.biz",
 * "alias":["vonex-monitoring-donotdel.biz"],
 * "country":"",
 * "extensions":0,
 * "accounts":1,
 * "visible":true,
 * "registrations":-1,
 * "regs":0},
 * {"id":85,"name":"bbbbbbbb","alias":["bbbbbbb"],"country":"","extensions":0,"accounts":0,"visible":true,"registrations":-1,"regs":0},{"id":87,"name":"test.22.vonex.biz","alias":["test.22.vonex.biz"],"country":"","extensions":5,"accounts":9,"visible":true,"registrations":60,"regs":3},{"id":90,"name":"localhost","alias":["localhost"],"country":"","extensions":0,"accounts":0,"visible":true,"registrations":-1,"regs":0}]
 * /

 *  
 */


