using CarWash.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarWash.Services
{
    public class AddingEarnings : IHostedService, IDisposable
    {
        private readonly IServiceProvider serviceProvider;
        private Timer timer;

        public AddingEarnings(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }

        public void Dispose()
        {
            timer?.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            timer = new Timer(DoWork, null, TimeSpan.Zero, TimeSpan.FromMinutes(1));
            return Task.CompletedTask;
        }

        private async void DoWork(object state)
        {
            using (var scope = serviceProvider.CreateScope())
            {
                List<Earnings> earningsList = new List<Earnings>();
                var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                var cwopen = await context.Schedulings.Where(x => x.Status == "accepted").Where(x => x.Appointment < DateTime.Now).ToListAsync();

                if (cwopen.Any())
                {
                    foreach (var cwo in cwopen)
                    {
                        var cwid = context.SchedulingEntity.Where(x => x.SchedulingId == cwo.Id).Single<SchedulingEntity>();
                        var srid = context.SchedulingServices.Where(x => x.SchedulingId == cwo.Id).Single<SchedulingServices>();
                        var ownerid = context.CarWashes.Where(x => x.Id == cwid.CarWashEntityId).Single<CarWashEntity>();

                        if (cwid.CarWashEntityId == null && srid.CarWashServiceId == null && srid.CarWashServiceId == null && cwo.Id == null && cwo.Appointment  == null && cwo.Price == null && ownerid.Owner == null) 
                        {
                            break;
                        }
                        earningsList.Add(new Earnings { CarWashId = cwid.CarWashEntityId, ServiceId = srid.CarWashServiceId, SchedulingId = cwo.Id, Appointment = cwo.Appointment, Price = cwo.Price, Owner = ownerid.Owner });
                        await context.Database.ExecuteSqlInterpolatedAsync($"delete from Schedulings where Id = {cwo.Id}");

                    }
                    context.Earnings.AddRange(earningsList);
                    await context.SaveChangesAsync();
                }


            }
        }
        public Task StopAsync(CancellationToken cancellationToken)
        {
            timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

    }
}
