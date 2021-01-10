using Microsoft.AspNetCore.Mvc;
using RoutingTests.PlanBuilder;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RoutingTests.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PlanBuilderController
    {

        [HttpGet]
        public List<PlanResult> CreatePlan()
        {
            var demoData = new DemoData();
            List<Task> tasks = demoData.GetTasks();
            Dictionary<(Guid, Guid), Route> routes = demoData.GetRoutes(tasks);

            var builder = new Builder();
            var routeResults = builder.Build(tasks, routes);

            var result = new List<PlanResult>();


            foreach (var routeResult in routeResults)
            {
                var tasksPlanned = new List<TaskPlanned>();
                var task = tasks.First(p => p.Id == routeResult.StartTask);

                var startTask = new TaskPlanned(task.Display, 0, task.DueDate, task.DueDate, task.Latitude, task.Longitude);
                tasksPlanned.Add(startTask);
                for (int i = 0; i < routeResult.TaskResults.Count; i++)
                {
                    task = tasks.First(p => p.Id == routeResult.TaskResults[i].Id);

                    var taskPlanned = new TaskPlanned(task.Display, i + 1, task.DueDate, routeResult.TaskResults[i].EstimatedArrival, task.Latitude, task.Longitude);
                    tasksPlanned.Add(taskPlanned);
                }
                var planResult = new PlanResult(tasksPlanned, routeResult.TotalDistance);
                result.Add(planResult);
            }

            return result;
        }
    }

    public class PlanResult
    {
        public PlanResult(List<TaskPlanned> tasks, int distance)
        {
            Tasks = tasks;
            TotalDistance = distance;
        }

        public List<TaskPlanned> Tasks { get; } = new List<TaskPlanned>();

        public int TotalDistance { get; }
    }

    public class TaskPlanned
    {
        public TaskPlanned(string displayName, int order, DateTime dueDate, DateTime arrivalDate, double latitude, double longitude)
        {
            DisplayName = displayName;
            Order = order;
            DueDate = dueDate;
            ArrivalDate = arrivalDate;
            Latitude = latitude;
            Longitude = longitude;
        }

        public string DisplayName { get; }
        public int Order { get; }
        public DateTime DueDate { get; }
        public DateTime ArrivalDate { get; }
        public double Latitude { get; }
        public double Longitude { get; }
    }
}
