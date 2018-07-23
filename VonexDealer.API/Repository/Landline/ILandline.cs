using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;

namespace VonexDealer.API.Repository
{
    public interface ILandline
    {
        Task<Landline> GetLandlinesOnOrder(Int64 orderID);
        Task<Landline> AddLandlineAsync(Landline Landline);
        Task<Landline> UpdateLandlineAsync(Int64 landlineID, Landline Landline);
        Task<bool> RemoveLandlineAsync(Int64 landlineID);

        Task<List<NewPSTN>> GetNewPSTNsOnOrder(Int64 orderID);
        Task<NewPSTN> AddNewPSTNAsync(NewPSTN newPSTN);
        Task<NewPSTN> UpdateNewPSTNAsync(Int64 newPSTNID, NewPSTN NewPSTN);
        Task<bool> RemoveNewPSTNAsync(Int64 newPSTNID);

        Task<List<Churn>> GetChurnsOnOrder(Int64 orderID);
        Task<Churn> AddChurnAsync(Churn churn);
        Task<Churn> UpdateChurnAsync(Int64 churnID, Churn churn);
        Task<bool> RemoveChurnAsync(Int64 churnID);

        Task<List<ISDN>> GetISDNsOnOrderAsync(Int64 orderID);
        Task<ISDN> AddISDNAsync(ISDN isdn);
        Task<ISDN> UpdateISDNAsync(Int64 ISDNID, ISDN isdn);
        Task<bool> RemoveISDNAsync(Int64 ISDNID);



    }
}
