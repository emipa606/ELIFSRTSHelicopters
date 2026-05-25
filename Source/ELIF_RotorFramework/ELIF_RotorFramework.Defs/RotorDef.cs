using System;
using RimWorld;
using Verse;

namespace ELIF_RotorFramework.Defs;

[Serializable]
public class RotorDef : Def
{
	public bool isVertical;

	public bool isTail;

	public bool antiClockwise;

	public float bladeSize;

	public float horizontalOffset;

	public float verticalOffset;

	public float drawLayerOffset;

	public ThingDef dust;

	public DrawerType drawerType = DrawerType.RealtimeOnly;

	public GraphicData graphicData;

	public Graphic graphic = BaseContent.BadGraphic;

	public override void PostLoad()
	{
		if (graphicData == null)
		{
			return;
		}
		LongEventHandler.ExecuteWhenFinished(delegate
		{
			if (graphicData.shaderType == null)
			{
				graphicData.shaderType = ShaderTypeDefOf.Cutout;
			}
			graphic = graphicData.Graphic;
		});
	}
}
