var WebInputField = {
    $instances: [],

    WebInputFieldPlugin_Create: function (canvasId, x, y, width, height, fontsize, text, isMultiLine, isPassword) {
        var container = document.getElementById(Pointer_stringify(canvasId));
		
		var canvas = document.getElementsByTagName('canvas')[0];
		if(canvas)
		{
			var scaleX = container.offsetWidth / canvas.width;
			var scaleY = container.offsetHeight / canvas.height;

			if(scaleX && scaleY)
			{
				x *= scaleX;
				width *= scaleX;
				y *= scaleY;
				height *= scaleY;
			}
		}

        var input = document.createElement(isMultiLine?"textarea":"input");
        input.style.position = "absolute";
        input.style.top = y + "px";
        input.style.left = x + "px";
        input.style.width = width + "px";
        input.style.height = height + "px";
        input.style.backgroundColor = '#00000000';
        input.style.color = '#00000000';
        input.style.outline = "none";
		input.style.border = "hidden";
		input.style.opacity = 0;
		input.style.cursor = "default";
		input.spellcheck = false;
		input.value = Pointer_stringify(text);
		input.style.fontSize = fontsize + "px";
		//input.setSelectionRange(0, input.value.length);
		
		if(isPassword){
			input.type = 'password';
		}

        container.appendChild(input);
        return instances.push(input) - 1;
    },
	WebInputFieldPlugin_EnterSubmit: function(id, falg){
		var input = instances[id];
		// for enter key
		input.addEventListener('keydown', function(e) {
			if ((e.which && e.which === 13) || (e.keyCode && e.keyCode === 13)) {
				if(falg)
				{
					e.preventDefault();
					input.blur();
				}
			}
		});
    },
	WebInputFieldPlugin_Tab:function(id, cb) {
		var input = instances[id];
		// for tab key
        input.addEventListener('keydown', function (e) {
            if ((e.which && e.which === 9) || (e.keyCode && e.keyCode === 9)) {
                e.preventDefault();
                Runtime.dynCall("vii", cb, [id, e.shiftKey ? -1 : 1]);
            }
		});
	},
	WebInputFieldPlugin_Focus: function(id){
		var input = instances[id];
		input.focus();
    },
    WebInputFieldPlugin_OnFocus: function (id, cb) {
        var input = instances[id];
        input.onfocus = function () {
            Runtime.dynCall("vi", cb, [id]);
        };
    },
    WebInputFieldPlugin_OnBlur: function (id, cb) {
        var input = instances[id];
        input.onblur = function () {
            Runtime.dynCall("vi", cb, [id]);
        };
    },
	WebInputFieldPlugin_IsFocus: function (id) {
		return instances[id] === document.activeElement;
	},
	WebInputFieldPlugin_OnValueChange:function(id, cb){
        var input = instances[id];
        input.oninput = function () {
			var value = allocate(intArrayFromString(input.value), 'i8', ALLOC_NORMAL);
            Runtime.dynCall("vii", cb, [id,value]);
        };
    },
	WebInputFieldPlugin_OnEditEnd:function(id, cb){
        var input = instances[id];
        input.onchange = function () {
			var value = allocate(intArrayFromString(input.value), 'i8', ALLOC_NORMAL);
            Runtime.dynCall("vii", cb, [id,value]);
        };
    },
	WebInputFieldPlugin_SelectionStart:function(id){
        var input = instances[id];
		return input.selectionStart;
	},
	WebInputFieldPlugin_SelectionEnd:function(id){
        var input = instances[id];
		return input.selectionEnd;
	},
	WebInputFieldPlugin_SelectionDirection:function(id){
        var input = instances[id];
		return (input.selectionDirection == "backward")?-1:1;
	},
	WebInputFieldPlugin_SetSelectionRange:function(id, start, end){
		var input = instances[id];
		input.setSelectionRange(start, end);
	},
	WebInputFieldPlugin_MaxLength:function(id, maxlength){
        var input = instances[id];
		input.maxLength = maxlength;
	},
	WebInputFieldPlugin_Text:function(id, text){
        var input = instances[id];
		input.value = Pointer_stringify(text);
	},
	WebInputFieldPlugin_Delete:function(id){
        var input = instances[id];
        input.parentNode.removeChild(input);
        instances[id] = null;
    },
}

autoAddDeps(WebInputField, '$instances');
mergeInto(LibraryManager.library, WebInputField);