﻿using System;
using VVVV.PluginInterfaces.V1;
using VVVV.PluginInterfaces.V2;

namespace VVVV.Hosting.Pins.Input
{
	public class EnumInputPin<T> : DiffPin<T>
	{
		protected IEnumIn FEnumInputPin;
		protected Type FEnumType;
		
		public EnumInputPin(IPluginHost host, InputAttribute attribute)
			: base(host, attribute)
		{
			FEnumType = typeof(T);
			
			var entrys = Enum.GetNames(FEnumType);
			var defEntry = (attribute.DefaultEnumEntry != "") ? attribute.DefaultEnumEntry : entrys[0];	
			host.UpdateEnum(FEnumType.Name, defEntry, entrys);
			
			host.CreateEnumInput(attribute.Name, (TSliceMode)attribute.SliceMode, (TPinVisibility)attribute.Visibility, out FEnumInputPin);
			FEnumInputPin.SetSubType(FEnumType.Name);
		}

		public override IPluginIO PluginIO 
		{
			get
			{
				return FEnumInputPin;
			}
		}
		
		public override int SliceCount 
		{
			get
			{
				return FEnumInputPin.SliceCount;
			}
			set
			{
				throw new NotSupportedException();
			}
		}
		
		public override bool IsChanged
		{
			get
			{
				return FEnumInputPin.PinIsChanged;
			}
		}
		
		public override T this[int index]
		{
			get
			{
				string entry;
				FEnumInputPin.GetString(index, out entry);

				return (T)Enum.Parse(FEnumType, entry);
			}
			set
			{
				throw new NotSupportedException();
			}
		}
	}
}