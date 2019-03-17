using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using msix.catalog.lib;
using NewMC.Core.Models;

namespace NewMC.Core.Services
{
    // This class holds sample data used by some generated pages to show how they can be used.
    // TODO WTS: Delete this file once your app is using real data.
    public static class SampleDataService
    {
        private static List<PackageInfo> _allPackages;

        static SampleDataService()
        {
            _allPackages = new List<PackageInfo>();
        }
        
        public static async Task<IEnumerable<PackageInfo>> GetSampleModelDataAsync()
        {
            await Task.CompletedTask;

            var data = await PackageRepository.LoadAllInstalledAppsAsync();
            foreach (var p in data)
            {
                _allPackages.Add(p);
            }


            return _allPackages;
        }


        
        // TODO WTS: Remove this once your ContentGrid page is displaying real data
        public static ObservableCollection<PackageInfo> GetContentGridData()
        {
            if (_allPackages == null)
            {
                var t = GetSampleModelDataAsync();
                t.Wait();
                foreach (var p in t.Result)
                {
                    _allPackages.Add(p);
                }
            }

            return new ObservableCollection<PackageInfo>(_allPackages);
        }

        //private static IEnumerable<PackageInfo> AllOrders()
        //{
        //    // The following is order summary data
        //    var data = new ObservableCollection<PackageInfo>
        //    {
        //        new PackageInfo
        //        {
        //            OrderId = 70,
        //            OrderDate = new DateTime(2017, 05, 24),
        //            Company = "Company F",
        //            ShipTo = "Francisco Pérez-Olaeta",
        //            OrderTotal = 2490.00,
        //            Status = "Closed",
        //            Symbol = (char)57643 // Symbol.Globe
        //        },
        //        new PackageInfo
        //        {
        //            OrderId = 71,
        //            OrderDate = new DateTime(2017, 05, 24),
        //            Company = "Company CC",
        //            ShipTo = "Soo Jung Lee",
        //            OrderTotal = 1760.00,
        //            Status = "Closed",
        //            Symbol = (char)57737 // Symbol.Audio
        //        },
        //        new PackageInfo
        //        {
        //            OrderId = 72,
        //            OrderDate = new DateTime(2017, 06, 03),
        //            Company = "Company Z",
        //            ShipTo = "Run Liu",
        //            OrderTotal = 2310.00,
        //            Status = "Closed",
        //            Symbol = (char)57699 // Symbol.Calendar
        //        },
        //        new PackageInfo
        //        {
        //            OrderId = 73,
        //            OrderDate = new DateTime(2017, 06, 05),
        //            Company = "Company Y",
        //            ShipTo = "John Rodman",
        //            OrderTotal = 665.00,
        //            Status = "Closed",
        //            Symbol = (char)57620 // Symbol.Camera
        //        },
        //        new PackageInfo
        //        {
        //            OrderId = 74,
        //            OrderDate = new DateTime(2017, 06, 07),
        //            Company = "Company H",
        //            ShipTo = "Elizabeth Andersen",
        //            OrderTotal = 560.00,
        //            Status = "Shipped",
        //            Symbol = (char)57633 // Symbol.Clock
        //        },
        //        new PackageInfo
        //        {
        //            OrderId = 75,
        //            OrderDate = new DateTime(2017, 06, 07),
        //            Company = "Company F",
        //            ShipTo = "Francisco Pérez-Olaeta",
        //            OrderTotal = 810.00,
        //            Status = "Shipped",
        //            Symbol = (char)57661 // Symbol.Contact
        //        },
        //        new PackageInfo
        //        {
        //            OrderId = 76,
        //            OrderDate = new DateTime(2017, 06, 11),
        //            Company = "Company I",
        //            ShipTo = "Sven Mortensen",
        //            OrderTotal = 196.50,
        //            Status = "Shipped",
        //            Symbol = (char)57619 // Symbol.Favorite
        //        },
        //        new PackageInfo
        //        {
        //            OrderId = 77,
        //            OrderDate = new DateTime(2017, 06, 14),
        //            Company = "Company BB",
        //            ShipTo = "Amritansh Raghav",
        //            OrderTotal = 270.00,
        //            Status = "New",
        //            Symbol = (char)57615 // Symbol.Home
        //        },
        //        new PackageInfo
        //        {
        //            OrderId = 78,
        //            OrderDate = new DateTime(2017, 06, 14),
        //            Company = "Company A",
        //            ShipTo = "Anna Bedecs",
        //            OrderTotal = 736.00,
        //            Status = "New",
        //            Symbol = (char)57625 // Symbol.Mail
        //        },
        //        new PackageInfo
        //        {
        //            OrderId = 79,
        //            OrderDate = new DateTime(2017, 06, 18),
        //            Company = "Company K",
        //            ShipTo = "Peter Krschne",
        //            OrderTotal = 800.00,
        //            Status = "New",
        //            Symbol = (char)57806 // Symbol.OutlineStar
        //        },
        //    };

        //    return data;
        //}

        // TODO WTS: Remove this once your MasterDetail pages are displaying real data
    }
}
