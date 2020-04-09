
using UnityEngine;
using System.Collections.Generic;

namespace WebSupport
{
	public sealed partial class WebInputField : MonoBehaviour, System.IComparable<WebInputField>
	{
		static class TabFocus
        {
            public static void Add( WebInputField input)
            {
                inputs.Add( input);
                inputs.Sort();
            }
            public static void Remove( WebInputField input)
            {
                inputs.Remove(input);
            }
            public static void OnTab( WebInputField input, int value)
            {
                if( inputs.Count > 1)
                {
	                var index = inputs.IndexOf( input);
	                index += value;
	                if( index < 0)
	                {
						index = inputs.Count - 1;
					}
	                else if( index >= inputs.Count)
	                {
						index = 0;
					}
	                inputs[ index].input.ActivateInputField();
	            }
            }
            static List<WebInputField> inputs = new List<WebInputField>();
        }
	}
}
