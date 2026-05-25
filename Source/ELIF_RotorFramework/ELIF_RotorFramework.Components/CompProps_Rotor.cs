using System.Collections.Generic;
using ELIF_RotorFramework.Defs;
using Verse;

namespace ELIF_RotorFramework.Components;

public class CompProps_Rotor : CompProperties
{
	public float rotationMaxSpeed;

	public float rotationAcceleration;

	public float rotationDeceleration;

	public int indentTick;

	public List<RotorDef> rotors = new List<RotorDef>();

	public CompProps_Rotor()
	{
		compClass = typeof(RotorComponent);
	}
}
