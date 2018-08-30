// DO NOT REMOVE COPYRIGHT NOTICES OR THIS FILE HEADER.
//
// @Authors:
//       Timotheus Pokorra <tp@tbits.net>
//       Christopher Jäkel <cj@tbits.net>
//
// Copyright 2017-2018 by TBits.net
//
// This file is part of OpenPetra.
//
// OpenPetra is free software: you can redistribute it and/or modify
// it under the terms of the GNU General Public License as published by
// the Free Software Foundation, either version 3 of the License, or
// (at your option) any later version.
//
// OpenPetra is distributed in the hope that it will be useful,
// but WITHOUT ANY WARRANTY; without even the implied warranty of
// MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
// GNU General Public License for more details.
//
// You should have received a copy of the GNU General Public License
// along with OpenPetra.  If not, see <http://www.gnu.org/licenses/>.
//

function replace_val_variables_in_attr(attr, data) {
  if (attr !== undefined && attr.indexOf('{val_') !== -1) {
    for (variable in data) {
      if (data[variable] == null) {
        data[variable] = "";
      }
      attr = attr.replace(new RegExp('{val_'+variable+'}',"g"), data[variable]);
    }
  }
  return attr;
}

// this will replace all {val_something} in this object, by converting it to a string and back
// check all attributes of div, id and onclick, and replace variables
// check all attributes of button, onclick, and replace variables
function replace_val_variables(tpl, data) {
    if (tpl[0] === undefined) {
        console.log("missing tpl for format_tpl");
        return "";
    }
    let id = $(tpl).attr('id');
    if (id !== undefined && id != null) {
        $(tpl).attr('id', replace_val_variables_in_attr(id, data));
    }

    let onclick = $(tpl).attr('onclick');
    if (onclick != undefined && onclick != null) {
        $(tpl).attr('onclick', replace_val_variables_in_attr(onclick, data));
    }

    $(tpl).find('button, div div, div div span, div span').each(function() {
        let id = $(this).attr('id');
        if (id !== undefined && id != null) {
            $(this).attr('id', replace_val_variables_in_attr(id, data));
        }
        let onclick = $(this).attr('onclick');
        if (onclick !== undefined && onclick != null) {
            $(this).attr('onclick', replace_val_variables_in_attr(onclick, data));
        }
        let text = $(this).text();
        if (text !== undefined && text != null && text[0] == "{") {
            $(this).text(replace_val_variables_in_attr(text, data));
        }
    });

    return tpl;
}

// tpl needs to be a DOM object, not just text.
function format_tpl(tpl, data, limit_to_table) {
  if (tpl[0] === undefined) {
    console.log("missing tpl for format_tpl");
    return "";
  }

  tpl = replace_val_variables(tpl, data);
  tpl = set_values_of_input_variables(tpl, data);

  return tpl;
}

// this will fill all possible elements with data, if their name=
// attribute is the same as the given object key
function set_values_of_input_variables(tpl, data, limit_to_table) {

  if (limit_to_table == null) {
    limit_to_table = "";
  }

  for (variable in data) {
    let key = false;
    let f = tpl.find("[name="+variable+"]");
    if (f.length == 0) {
      f = tpl.find("[name="+limit_to_table+variable+"]");
    }
    if (f.length == 0) {
      f = tpl.find("[key-name="+variable+"]");
      key = true;
    }

    if (key == true) {
      //hidden key case
      value = data[variable];
      f.attr('key-value', value);
    } else {
      if (f.is('select')) {
        value = data[variable];
        f.val(value).change();
      }
      else if (f.is('textarea')) {
        f.text(data[variable]);
      }
      else if (f.attr('type') == "checkbox") {
        f.attr('checked', data[variable]);
        f.prop('checked', data[variable]);
      }
      else if (f.attr('type') == "radio") {
        tpl.find('[name='+variable+'][value='+data[variable]+']').prop('checked', true);
        tpl.find('[name='+variable+'][value='+data[variable]+']').attr('checked', 'checked');
      }
      else {
        value = data[variable];
        if (typeof value === 'string' || value instanceof String) {
          value = parseJSONDate(variable, value);
        }
        f.attr('value', value);
        f.val(value);
      }

      // check if this field also has a key-name with the same name as the field (eg. a_motivation_group_code_c)
      f2 = tpl.find("[key-name="+variable+"]");
      if (f2.length != 0) {
        value = data[variable];
        f2.attr('key-value', value);
      }
    }
  }

  return tpl;
}

function parseJSONDate(variable, value) {
  // https://www.newtonsoft.com/json/help/html/DatesInJSON.htm
  if (value.substring(0, 6) == "/Date(") {
    d = new Date(parseInt(value.substring(6, value.indexOf(')'))));
    if (variable == "s_modification_id_t") {
      value = d.getTime();
    } else {
      value = d.getFullYear() + "-" + ("0"+(d.getMonth()+1)).slice(-2) + "-" + ("0" + d.getDate()).slice(-2);
      if (d.getHours() != 0 || d.getMinutes() != 0 && d.getSeconds() != 0) {
        value += " " + ("0"+d.getHours()).slice(-2) + ":" + ("0"+d.getMinutes()).slice(-2) + ":" + ("0"+d.getSeconds()).slice(-2);
      }
    }
  }
  return value;
}

function printJSONDate(value) {
  // https://www.newtonsoft.com/json/help/html/DatesInJSON.htm
  if (value.substring(0, 6) == "/Date(") {
    let t = /\((.+)\)/g.exec(value);
    if (t == null || t.length <=1) {return}

    time = new Date(parseInt(t[1])).toLocaleDateString();
    return time;
  }
  return value;
}

function printCurrency(value, currency) {

  if (isNaN(value)) {
    // perhaps the value has been formatted already
    return value;
  }

  var formatter = new Intl.NumberFormat(navigator.language || navigator.userLanguage, {
    style: 'currency',
    currency: currency,
    minimumFractionDigits: 2
  });

  return formatter.format(value);
}

function format_currency(currencyCode) {
	$(".format_currency:contains('-')").addClass('debit');
	$('.format_currency').each(
		function(x, obj) {
			obj = $(obj);
			let t = obj.text();
			if (t == null || t.length <=1) {return}
			obj.text(printCurrency(t, currencyCode));

		}
	)
};

function format_date() {
	$('.format_date').each(
		function(x, obj) {
			obj = $(obj);
			let t = /\((.+)\)/g.exec(obj.text());
			if (t == null || t.length <=1) {return}

			time = new Date(parseInt(t[1])).toLocaleDateString();
			obj.text(time);

		}
	)
};

function format_chk() {
	$('.format_chk').each(
		function(x, obj) {
			obj = $(obj);
			let t = obj.text();
			if (t == null || t.length <=1) {return}

			if (t == "false") {
				obj.html("<i class='fa fa-circle-thin'></i>");
			} else {
				obj.html("<i class='fa fa-check-circle'></i>");
			}

		}
	)
};

// this is the oposite the format one, it will extract all name= objects
// and will return a object in key, values where key is the name and value... well the value
function extract_data(object) {
  var r = {};
  object.find('[name]').each(function (i, obj) {
    obj = $(obj);
    r[obj.attr('name')] = obj.val();
    if (obj.attr('type') == 'checkbox') {

      if (obj.is(':checked')) {
        r[obj.attr('name')] = true;
      } else {
        r[obj.attr('name')] = false;
      }

    }
    if (obj.attr('type') == 'radio') {
      r[obj.attr('name')] = object.find('[name='+obj.attr('name')+']:checked').val();
    }
  });

  object.find('[key-name]').each(function (i, obj) {
    obj = $(obj);
    r[obj.attr('key-name')] = obj.attr('key-value');
  });

  return r;
}

function getKeyValue(object, name) {
  value = undefined;
  object.find('[key-name]').each(function (i, obj) {
    obj = $(obj);
    if (obj.attr('key-name') == name) {
      value = obj.attr('key-value');
    }
  });
  return value;
}

function update_requireClass(object, class_) {
	object = $(object);
	object.find('[requireClass]').hide();
	object.find('.'+class_).show();
}
