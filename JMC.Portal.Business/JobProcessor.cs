using JMC.Portal.Business.PortalModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JMC.Portal.Business
{
	public static class JobProcessor
	{
		public static void Process(BackgroundProcessor.ProcessingTask task)
		{
			using (PortalContext db = new PortalContext())
			{
				DBCache dbcache = new DBCache(db);
				if (task._jobtype.Trim().ToUpper() == "BL")
				{
					List<string> soldTos = (from x in task._arguments.Split(';') where x.Trim() != "" select x.Trim()).Distinct().ToList();
					if (soldTos.Any())
					{
						//SapsoldTo.RefreshBacklog(ref dbcache, soldTos, true, true, task._force);
					}
				}
			}
		}
	}
}
