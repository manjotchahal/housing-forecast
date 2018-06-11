using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Logging;
using Housing.Forecast.Library.Models;
using Housing.Forecast.Library;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Hosting;

namespace Housing.Forecast.Context
{
    public interface IForecastContext
    {
        DbSet<Address> Addresses { get; set; }
        DbSet<Batch> Batches { get; set; }
        DbSet<Name> Names{ get; set; }
        DbSet<Room> Rooms { get; set; }
        DbSet<Snapshot> Snapshots { get; set; }
        DbSet<User> Users { get; set; }
        int SaveChanges();
    }
}
