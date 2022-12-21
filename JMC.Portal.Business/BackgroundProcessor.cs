using JMC.Portal.Business.PortalModels;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMC.Portal.Business
{
	public static class BackgroundProcessor
	{
		private const int ThreadCount = 5;
		private static volatile Queue<string> LogMessages = new Queue<string>();
		private static object syncRoot = new Object();
		private static volatile List<ProcessingTask> mainQueue = new List<ProcessingTask>();
		private static volatile List<Thread> threadPool = new List<Thread>();
		private static volatile List<ProcessingTask> runningTasks = new List<ProcessingTask>();
		private static volatile bool pauseQueue = false;
		public static void PauseQueue()
		{
			lock (syncRoot)
			{
				pauseQueue = true;
			}
		}
		public static void Resume()
		{
			lock (syncRoot)
			{
				pauseQueue = false;
			}
			KickOff();
		}
		public static void Start()
		{
			Resume();
		}
		public static ProcessingTask Push(ProcessingTask task, bool autoStart = true)
		{
			if (!task._force && task._jobtype == "BL")
			{
				using (PortalContext db = new PortalContext())
				{
					//SapsoldTo sapSoldTo = db.SapshipTos.OfType<SapsoldTo>().Where(x => x.DivisionId == (long)Enums.Divisions.Atlas).Where(x => x.Number == task._arguments).FirstOrDefault();
					//if (sapSoldTo != null)
					//{
					//	DateTime anHourAgo = DateTime.Now.AddHours(-1);
					//	if (sapSoldTo.LastBacklogRefresh.HasValue && sapSoldTo.LastBacklogRefresh.Value > anHourAgo)
					//	{
					//		task.jobStarted = DateTime.Now;
					//		task.jobCompleted = DateTime.Now;
					//		task.IsComplete = true;
					//		Log("SKIPPED -------- " + task._priority + ":" + task._arguments);
					//		return task;
					//	}
					//}
				}
			}
			lock (syncRoot)
			{
				ProcessingTask alreadyExists = mainQueue.FirstOrDefault(x => x._jobtype == task._jobtype && x._arguments == task._arguments);
				if (alreadyExists != null)
				{
					long minPrioNumber = alreadyExists._priority;
					if (task._priority < minPrioNumber) minPrioNumber = task._priority;
					minPrioNumber--;
					alreadyExists._priority = minPrioNumber;
					if (task._force)
					{
						alreadyExists._force = true;
					}
					Log("Increased Priority -------- " + alreadyExists._priority + ":" + task._arguments);
					return alreadyExists;
				}
				alreadyExists = runningTasks.FirstOrDefault(x => x._jobtype == task._jobtype && x._arguments == task._arguments);
				if (alreadyExists != null)
				{
					Log("Job Already Running ------- " + task._priority + ":" + task._arguments);
					return alreadyExists;
				}
				Log("Added Job -------- " + task._priority + ":" + task._arguments);
				mainQueue.Add(task);
			}
			if (autoStart)
			{
				KickOff();
			}
			return task;
		}
		private static void Log(string message)
		{
			System.Diagnostics.Debug.WriteLine(DateTime.Now.ToShortTimeString() + " " + message);
			LogMessages.Enqueue(message);
			while (LogMessages.Count > 50)
			{
				LogMessages.Dequeue();
			}
		}
		private static void Application_Error(Exception ex, string SapSoldToNumber)
		{
			HttpContext httpContext=null;
			string username = string.Empty;
			try
			{
				username = httpContext.User.Identity.Name.ToString();
			}
			catch { }
			string StackTrace = ex.StackTrace;
			string Message = "";
			Message += "While Updating Sold To " + SapSoldToNumber.Trim() + Environment.NewLine;
			try
			{
				Message += "Url:" + httpContext.Request.Path + Environment.NewLine;
			}
			catch { }
			Message += ex.Message + Environment.NewLine;
			Message += "User-Agent: ";
			try
			{
				Message += httpContext.Request.Headers["User-Agent"].ToString();
			}
			catch { }
			Message += Environment.NewLine;
			while (ex.InnerException != null)
			{
				ex = ex.InnerException;
				Message += ex.Message + Environment.NewLine;
			}
			Message += ex.StackTrace + Environment.NewLine;
			bool html = true;
			if (html)
			{
				Message = Message.Replace(Environment.NewLine, "<br />" + Environment.NewLine);
			}
			//Email.SendMessage(ConfigurationManager.AppSettings["FromEmailTitle"], ConfigurationManager.AppSettings["ErrorEmailAddress"], string.Empty, "Error! Username: " + username, Message);
		}
		public static bool IsInqueue(string jobtype, string args)
		{
			lock (syncRoot)
			{
				ProcessingTask alreadyExists = mainQueue.FirstOrDefault(x => x._jobtype == jobtype && x._arguments == args);
				if (alreadyExists != null)
				{
					return true;
				}
				alreadyExists = runningTasks.FirstOrDefault(x => x._jobtype == jobtype && x._arguments == args);
				if (alreadyExists != null)
				{
					return true;
				}
			}
			return false;
		}
		private static void KickOff()
		{
			lock (syncRoot)
			{
				if (pauseQueue) return;
				threadPool = threadPool.Where(x => x.IsAlive).ToList();
				int y = 0;
				for (y = 0; y < BackgroundProcessor.ThreadCount; y++)
				{
					if (threadPool.Count >= BackgroundProcessor.ThreadCount)
					{
						return;
					}
					long nextprio = mainQueue.Any() ? mainQueue.Min(x => x._priority) : 0;
					ProcessingTask task = mainQueue.FirstOrDefault(x => x._priority == nextprio);
					if (task == null)
					{
						task = mainQueue.FirstOrDefault();
					}
					if (task == null) return;
					mainQueue.Remove(task);
					runningTasks.Add(task);
					Thread thread = new Thread(task.Run);
					thread.IsBackground = true;
					threadPool.Add(thread);
					Log("Job Starting -------- " + task._priority + ":" + task._arguments);
					thread.Start();
				}
			}
		}
		private static void ThreadComplete(ProcessingTask task)
		{
			lock (syncRoot)
			{
				mainQueue.Remove(task);
				runningTasks.Remove(task);
				threadPool = threadPool.Where(x => x.IsAlive).ToList();
				if ((task.exception) != null)
				{
					Log("Job Errored out -------- " + task._priority + ":" + task._arguments);
				}
				else
				{
					Log("Job Complete -------- " + task._priority + ":" + task._arguments);
				}
			}
			KickOff();
			if ((task.exception) != null)
			{
				Exception ee = task.exception;
				Application_Error(ee, task._arguments);
				Log("ex:" + task._arguments + " " + ee.Message);
				if (task._jobtype == "BL")
				{
					try
					{
						using (PortalContext dbs = new PortalContext())
						{
							//SapsoldTo errorSoldTo = dbs.SapshipTos.OfType<SapsoldTo>().Where(x => x.Number == task._arguments).FirstOrDefault();
							//if ((errorSoldTo) != null)
							//{
							//	errorSoldTo.RefreshingBacklog = false;
							//	dbs.SaveChanges();
							//}
						}
					}
					catch { }
				}
				lock (syncRoot)
				{
					while (ee.InnerException != null)
					{
						ee = ee.InnerException;
						Log("ex:" + task._arguments + " " + ee.Message);
					}
				}
			}
		}
		public class ProcessingTask
		{
			public long _priority;
			public string _jobtype;
			public string _arguments;
			public bool _force = false;
			public DateTime jobStarted { get; set; }
			public DateTime jobCompleted { get; set; }
			public bool IsComplete { get; set; }
			public Exception exception { get; private set; }
			public ProcessingTask() { IsComplete = false; }
			public ProcessingTask(long priority, string job, string args, bool force = false)
			{
				_priority = priority;
				_jobtype = job;
				_arguments = args;
				IsComplete = false;
				_force = force;
			}
			public void Run()
			{
				jobStarted = DateTime.Now;
				try
				{
					JobProcessor.Process(this);
				}
				catch (Exception exc)
				{
					exception = exc;
				}
				jobCompleted = DateTime.Now;
				this.IsComplete = true;
				BackgroundProcessor.ThreadComplete(this);
			}
		}
		public static bool AllCompleted(this IEnumerable<BackgroundProcessor.ProcessingTask> tasklist)
		{
			return !(tasklist.Any(x => !x.IsComplete));
		}
		public static void WaitForAll(this IEnumerable<BackgroundProcessor.ProcessingTask> tasklist)
		{
			while (tasklist.Any(x => !x.IsComplete))
			{
				Thread.Sleep(1000);
			}
		}
	}
}
