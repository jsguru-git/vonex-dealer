using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VonexDealer.API.Models.Order;
using VonexDealer.API.ViewModels;

namespace VonexDealer.API.Repository.Orders
{
    public partial class OrderRepository
    {

        public async Task<List<HandsetView>> getHandsetsAsync(Int64 categoryID = 0, Int64 manufacturerID = 0)
        {
            List<HandsetView> handsets = await _context.Handsets
                .Join(_context.HardwareCategories, x=> x.HardwareCategoryID, y => y.HardwareCategoryID, (x,y) => new
                {
                    Handset = x,
                    HardwareCategory = y
                })
                .Join(_context.Manufacturers, x=>x.Handset.ManufacturerDetailsID, y => y.ManufacturerID, (x,y) => new
                HandsetView {
                     Handset= x.Handset,
                     HardwareCategory= x.HardwareCategory,
                    Manufacturer = y
                })
                //.Include(x => x.hardwareCategory)
                //.Include(x => x.ManufacturerDetails)
                .Where(x => categoryID == 0 || x.HardwareCategory.HardwareCategoryID == categoryID)
                .Where(x => manufacturerID == 0 || x.Manufacturer.ManufacturerID == manufacturerID)
                .ToListAsync();

            foreach (var item in handsets)
            {
                item.Handset.HardwareCategory = item.HardwareCategory;
                item.Handset.Manufacturer = item.Manufacturer;
            }

            return handsets;

        }

        public async Task<List<HardwareCategory>> getHardwareCategoriesAsync()
        {
            List<HardwareCategory> categories = await _context.HardwareCategories
                 .ToListAsync();
            return categories;
        }

        public async Task<List<Manufacturer>> getManufacturersAsync()
        {
            List<Manufacturer> manufacturers = await _context.Manufacturers.ToListAsync();
            return manufacturers;

        }


    }
}
