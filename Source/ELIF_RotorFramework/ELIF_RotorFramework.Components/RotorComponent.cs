using System;
using ELIF_RotorFramework.Defs;
using RimWorld;
using UnityEngine;
using Verse;

namespace ELIF_RotorFramework.Components;

[StaticConstructorOnStartup]
public class RotorComponent : ThingComp
{
	private int ticksSinceLastUpdate = 200;

	private float spinRate;

	private float spinPosition;

	private float spinRotation;

	private CompTransporter cachedCompTransporter => parent.GetComp<CompTransporter>();

	private CompRefuelable cachedCompRefuelable => parent.GetComp<CompRefuelable>();

	public CompProps_Rotor Props => (CompProps_Rotor)props;

	public override void PostDraw()
	{
		base.PostDraw();
		foreach (RotorDef rotor in Props.rotors)
		{
			if (rotor.isVertical)
			{
				Vector3 pos = parent.TrueCenter();
				pos += parent.Rotation.FacingCell.ToVector3() * rotor.verticalOffset;
				pos += parent.Rotation.RighthandCell.ToVector3() * rotor.horizontalOffset;
				pos.y += rotor.drawLayerOffset;
				float num = rotor.bladeSize * Mathf.Sin(spinPosition);
				if (num < 0f)
				{
					num *= -1f;
				}
				bool flag = spinPosition % MathF.PI * 2f < MathF.PI;
				Vector2 vector = new Vector2(num, 1f);
				Vector3 s = new Vector3(vector.x, 1f, vector.y);
				Matrix4x4 matrix = default(Matrix4x4);
				if (rotor.isTail)
				{
					matrix.SetTRS(pos, parent.Rotation.AsQuat * Rot4.East.AsQuat, s);
				}
				else
				{
					matrix.SetTRS(pos, parent.Rotation.AsQuat, s);
				}
				Graphics.DrawMesh(flag ? MeshPool.plane10 : MeshPool.plane10Flip, matrix, rotor.graphic.MatAt(Rot4.North), 0);
				Graphics.DrawMesh(flag ? MeshPool.plane10Flip : MeshPool.plane10, matrix, rotor.graphic.MatAt(Rot4.North), 0);
			}
			else
			{
				Vector3 pos2 = parent.TrueCenter();
				pos2 += parent.Rotation.FacingCell.ToVector3() * rotor.verticalOffset;
				pos2 += parent.Rotation.RighthandCell.ToVector3() * rotor.horizontalOffset;
				pos2.y += rotor.drawLayerOffset;
				Vector3 s2 = new Vector3(rotor.bladeSize, 1f, rotor.bladeSize);
				Matrix4x4 matrix2 = default(Matrix4x4);
				matrix2.SetTRS(pos2, Quaternion.Euler(1f, rotor.antiClockwise ? (0f - spinRotation) : spinRotation, 1f), s2);
				Graphics.DrawMesh(MeshPool.plane10, matrix2, rotor.graphic.MatAt(Rot4.North), 0);
			}
		}
	}

	public override void PostSpawnSetup(bool respawningAfterLoad)
	{
		if (!respawningAfterLoad && cachedCompRefuelable != null && cachedCompRefuelable.HasFuel)
		{
			spinRate = Props.rotationMaxSpeed;
		}
	}

	public override void CompTick()
	{
		base.CompTick();
		if (Current.Game.tickManager.CurTimeSpeed == TimeSpeed.Paused)
		{
			return;
		}
		ticksSinceLastUpdate++;
		if (ticksSinceLastUpdate >= Props.indentTick)
		{
			ticksSinceLastUpdate = 0;
			CompTransporter compTransporter = cachedCompTransporter;
			CompRefuelable compRefuelable = cachedCompRefuelable;
			if (compTransporter != null && compRefuelable != null && compTransporter.LoadingInProgressOrReadyToLaunch && compRefuelable.HasFuel)
			{
				if (spinRate < Props.rotationMaxSpeed)
				{
					if (spinRate <= 0f)
					{
						spinRate = Props.rotationAcceleration;
					}
					else if (spinRate > Props.rotationMaxSpeed)
					{
						spinRate = Props.rotationMaxSpeed;
					}
					else
					{
						spinRate += Props.rotationAcceleration;
					}
				}
			}
			else if (spinRate <= 0f)
			{
				spinRate = 0f;
			}
			else
			{
				spinRate -= Props.rotationDeceleration;
			}
			if (spinRate > 0f && parent.Map != null)
			{
				foreach (RotorDef rotor in Props.rotors)
				{
					if (rotor.dust != null)
					{
						Vector3 exactPosition = parent.TrueCenter();
						exactPosition += parent.Rotation.FacingCell.ToVector3() * rotor.verticalOffset;
						exactPosition += parent.Rotation.RighthandCell.ToVector3() * rotor.horizontalOffset;
						MoteThrown moteThrown = (MoteThrown)ThingMaker.MakeThing(rotor.dust);
						moteThrown.rotationRate = Rand.Range(-90f, 90f);
						moteThrown.exactPosition = exactPosition;
						GenSpawn.Spawn(moteThrown, parent.Position, parent.Map);
					}
				}
			}
		}
		if (spinRate > 0f)
		{
			spinPosition += spinRate / (float)(int)Current.Game.tickManager.CurTimeSpeed;
			spinRotation += spinRate * 10f / (float)(int)Current.Game.tickManager.CurTimeSpeed;
			if (spinRotation > 360f)
			{
				spinRotation -= 360f;
			}
		}
	}

	public override void PostExposeData()
	{
		base.PostExposeData();
		Scribe_Values.Look(ref ticksSinceLastUpdate, "updateCounter", 0);
		Scribe_Values.Look(ref spinRate, "SpinRate", 0f);
		Scribe_Values.Look(ref spinPosition, "SpinPosition", 0f);
	}
}
