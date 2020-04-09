
using UnityEngine;

namespace WebSupport
{
    public class RebuildChecker
    {
        public RebuildChecker( IInputField inputfield)
        {
            this.inputfield = inputfield;
        }
        public bool NeedRebuild( bool debug=false)
        {
            var res = false;

            if( beforeText != inputfield.text)
            {
                if( debug != false)
                {
					Debug.Log( string.Format(" beforeText : {0} != {1}", beforeText, inputfield.text));
				}
                beforeText = inputfield.text;
                res = true;
            }
            if( beforeCaretPosition != inputfield.caretPosition)
            {
                if( debug != false)
                {
					Debug.Log( string.Format( "beforeCaretPosition : {0} != {1}", beforeCaretPosition, inputfield.caretPosition));
				}
                beforeCaretPosition = inputfield.caretPosition;
                res = true;
            }
            if( beforeSelectionFocusPosition != inputfield.selectionFocusPosition)
            {
                if( debug)
                {
					Debug.Log( string.Format( "beforeSelectionFocusPosition : {0} != {1}", beforeSelectionFocusPosition, inputfield.selectionFocusPosition));
				}
                beforeSelectionFocusPosition = inputfield.selectionFocusPosition;
                res = true;
            }
            if( beforeSelectionAnchorPosition != inputfield.selectionAnchorPosition)
            {
                if( debug != false)
                {
					Debug.Log( string.Format( "beforeSelectionAnchorPosition : {0} != {1}", beforeSelectionAnchorPosition, inputfield.selectionAnchorPosition));
				}
                beforeSelectionAnchorPosition = inputfield.selectionAnchorPosition;
                res = true;
            }

            //if( anchoredPosition != inputfield.TextComponentRectTransform().anchoredPosition)
            //{
            //    if( debug) Debug.Log(string.Format("anchoredPosition : {0} != {1}", anchoredPosition, inputfield.TextComponentRectTransform().anchoredPosition));
            //    anchoredPosition = inputfield.TextComponentRectTransform().anchoredPosition;
            //    res = true;
            //}
            return res;
        }
        
        IInputField inputfield;
        string beforeText;
        int beforeCaretPosition;
        int beforeSelectionFocusPosition;
        int beforeSelectionAnchorPosition;
        //Vector2 anchoredPosition;
    }
}
