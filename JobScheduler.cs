using DieteticaPuchiApi.Interfaces;
using Quartz;
using Quartz.Impl;

namespace DieteticaPuchiApi
{
    public class JobScheduler

    {
        public static void Start()
        {
            IScheduler scheduler = StdSchedulerFactory.GetDefaultScheduler().Result;

            scheduler.Start();

            IJobDetail job = JobBuilder.Create<IDGJobRepository>().Build();

            ITrigger trigger = TriggerBuilder.Create()
                    .WithDailyTimeIntervalSchedule(s => s
                      .WithIntervalInHours(24)
                      .OnEveryDay()
                      .StartingDailyAt(TimeOfDay.HourAndMinuteOfDay(21, 00)))
                    .Build();
                    
            scheduler.ScheduleJob(job, trigger);
        }
    }
}