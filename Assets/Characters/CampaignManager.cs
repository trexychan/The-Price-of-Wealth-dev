public class CampaignManager : Character {
	
	int cycle;
	
	public CampaignManager () {
		health = 20; maxHP = 20; strength = 2; power = 0; charge = 0; defense = 0; guard = 0; 
		baseAccuracy = 11; accuracy = 11; dexterity = 2; evasion = 0; type = "Campaign Manager"; passive = new Passive(this);
		quirk = Quirk.GetQuirk(this); special = null; player = false; champion = false; recruitable = false;
		cycle = 0; CreateDrops();
	}
	
	public override TimedMethod[] AI () {
		if (cycle == 0) {
			cycle++;
			return Plan();
		} else if (cycle == 1) {
			cycle++;
			return Attack();
		} else {
			return Recover();
		}
	}
	
	public TimedMethod[] Plan() {
		return new TimedMethod[] {new TimedMethod(60, "Log", new object[] {ToString() + " devised a plan"})};
	}
	
	public TimedMethod[] Attack () {
		return new TimedMethod[] {new TimedMethod(60, "Log", new object[] {ToString() + " defended the campaign"}),
		    new TimedMethod(0, "StagnantAttack", new object[] {false, 5, 5, GetAccuracy(), true, true, false})};
	}
	
	public TimedMethod[] Recover () {
		SetAlive(false);
		Party.enemyCount--;
		Party.enemySlot = 1;
		Politician pol = (Politician) Party.enemies[0];
		pol.broken = false;
		pol.cycle = 4;
		Status.NullifyAttack(pol);
		Status.NullifyDefense(pol);
		return new TimedMethod[] {new TimedMethod(60, "Log", new object[] {ToString() + " Caused the politician's grand return"}),
		    new TimedMethod(0, "EnemySwitch", new object[] {0, 1})};
	}
}