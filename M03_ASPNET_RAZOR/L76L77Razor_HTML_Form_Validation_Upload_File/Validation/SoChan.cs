using System.ComponentModel.DataAnnotations;

namespace L76L77Razor_HTML_Form_Validation_Upload_File.Validation
{
	public class SoChan : ValidationAttribute
	{
		public SoChan()
		{
			ErrorMessage = "{0} phải là số chẵn";
		}

		public override bool IsValid(object? value)
		{
			if(value == null)
			{
				return false;
			}

			var _value = int.Parse(value.ToString());

			return _value % 2 == 0;
		}
	}
}
