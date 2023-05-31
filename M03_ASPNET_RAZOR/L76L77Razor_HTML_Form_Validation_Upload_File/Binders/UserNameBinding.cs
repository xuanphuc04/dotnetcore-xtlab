using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace L76L77Razor_HTML_Form_Validation_Upload_File.Binders
{
	/*
	 * - Chuyển tên thành in hoa
	 * - Cắt khoảng trắng ở đầu và cuối
	 * - Tên không được chứa ký tự xxx
	 */
	public class UserNameBinding : IModelBinder
	{
		public Task BindModelAsync(ModelBindingContext bindingContext)
		{
			if(bindingContext == null)
			{
				throw new ArgumentNullException(nameof(bindingContext));
			}

			// Lấy tên model
			var modelName = bindingContext.ModelName;
			// Đọc giá trị gửi đến
			var valueProviderResult = bindingContext.ValueProvider.GetValue(modelName);
			if(valueProviderResult == ValueProviderResult.None)
			{
				return Task.CompletedTask;
			}

			// Lấy giá trị đầu tiên trong các nguồn dữ liệu gửi đến
			string value = valueProviderResult.FirstValue;
			if(string.IsNullOrEmpty(value))
			{
				return Task.CompletedTask;
			}

			// Chuyển thành chữ hoa
			// Cắt khoảng trống đầu và cuối
			value = value.ToUpper().Trim();

			if(value.Contains("XXX"))
			{
				bindingContext.ModelState.SetModelValue(modelName, valueProviderResult);
				bindingContext.ModelState.TryAddModelError(modelName, "Giá trị không được chứa XXX");
				return Task.CompletedTask;
			}

			bindingContext.ModelState.SetModelValue(modelName, value, value);

			// Gán dữ liệu vào Property
			bindingContext.Result = ModelBindingResult.Success(value);
			return Task.CompletedTask;
			

			// Sử dụng bằng cách thêm attribute [ModelBinder(BinderType = typeof(UserNameBinding))]
		}
	}
}
