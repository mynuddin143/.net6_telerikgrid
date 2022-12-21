using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace JMC.Portal.Business {
	public interface IHSSPortalRolling {
		string MaterialNumber { get; set; }
		string Plant { get; set; }
		string PlanningMaterial { get; set; }
		System.DateTime RollingDate { get; set; }
		decimal Planned { get; set; }
		decimal Allocated { get; set; }
		decimal WeightPerFoot { get; set; }
		
	}

	public interface IHSSPortalAPORolling {
		string MaterialNumber { get; set; }
		string Plant { get; set; }
		string PlanningMaterial { get; set; }
		System.DateTime RollingDate { get; set; }
		decimal Planned { get; set; }
		decimal Allocated { get; set; }
		decimal WeightPerFoot { get; set; }
		string ResourceName { get; set; }

	}
}
