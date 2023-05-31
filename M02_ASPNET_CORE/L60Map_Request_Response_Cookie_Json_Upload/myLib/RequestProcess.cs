﻿using System.Text;

namespace L60Map_Request_Response_Cookie_Json_Upload.myLib
{
    public class RequestProcess
    {
        // Đọc các thông tin cơ bản của Request
        // Trả về HTML trình  bày các thông tin đó
        public static string RequestInfo(HttpRequest request)
        {

            var sb = new StringBuilder();

            // Lấy http scheme (http|https)
            var scheme = request.Scheme;
            sb.Append(("scheme".td() + scheme.td()).tr());

            // HOST Header
            var host = (request.Host.HasValue ? request.Host.Value : "no host");
            sb.Append(("host".td() + host.td()).tr());


            // Lấy pathbase (URL Path - cho Map)
            var pathbase = request.PathBase.ToString();
            sb.Append(("pathbase".td() + pathbase.td()).tr());

            // Lấy Path (URL Path)
            var path = request.Path.ToString();
            sb.Append(("path".td() + path.td()).tr());

            // Lấy chuỗi query của URL
            var QueryString = request.QueryString.HasValue ? request.QueryString.Value : "no query string";
            sb.Append(("QueryString".td() + QueryString.td()).tr());

            // Lấy phương thức
            var method = request.Method;
            sb.Append(("Method".td() + method.td()).tr());

            // Lấy giao thức
            var Protocol = request.Protocol;
            sb.Append(("Protocol".td() + Protocol.td()).tr());

            // Lấy ContentType
            var ContentType = request.ContentType;
            sb.Append(("ContentType".td() + ContentType.td()).tr());

            // Lấy danh sách các Header và giá trị  của nó, dùng Linq để lấy
            // Header gửi đến lưu trong thuộc tính Header  kiểu Dictionary
            var listheaderString = request.Headers.Select((header) => $"{header.Key}: {header.Value}".HtmlTag("li"));
            var headerhmtl = string.Join("", listheaderString).HtmlTag("ul"); // nối danh sách thành 1
            sb.Append(("Header".td() + headerhmtl.td()).tr());

            // Lấy danh sách các Header và giá trị  của nó, dùng Linq để lấy
            var listcokie = request.Cookies.Select((header) => $"{header.Key}: {header.Value}".HtmlTag("li"));
            var cockiesHtml = string.Join("", listcokie).HtmlTag("ul");
            sb.Append(("Cookies".td() + cockiesHtml.td()).tr());


            // Lấy tên và giá trí query
            var listquery = request.Query.Select((header) => $"{header.Key}: {header.Value}".HtmlTag("li"));
            var queryhtml = string.Join("", listquery).HtmlTag("ul");
            sb.Append(("Các Query".td() + queryhtml.td()).tr());

            //Kiểm tra thử query tên abc có không
            Microsoft.Extensions.Primitives.StringValues abc;
            bool existabc = request.Query.TryGetValue("abc", out abc);
            string queryVal = existabc ? abc.FirstOrDefault() : "không có giá trị";
            sb.Append(("abc query".td() + queryVal.ToString().td()).tr());

            string info = "Thông tin Request".HtmlTag("h2") + sb.ToString().HtmlTag("table", "table table-sm table-bordered");
            return info;
        }

        public static string Cookies(HttpRequest request, HttpResponse response)
        {
            string noti = "";

            switch (request.Path)
            {
                case "/cookies/read":
                    var listCookies = request.Cookies.Select((cookie) =>
                    {
                        return $"{cookie.Key}: {cookie.Value}".HtmlTag("li");
                    });
                    noti = string.Join("", listCookies).HtmlTag("ul");
                    break;
                case "/cookies/write":
                    //response.Cookies.Append("productId", "123456789");
                    response.Cookies.Append(
                        "productId",
                        "123456789",
                        new CookieOptions()
                        {
                            Path = "/cookies", // url co hieu luc
                            Expires = DateTime.Now.AddDays(1) // Het han sau 1 ngay
                        }

                        ); ;

                    noti = "Da luu cookie: <code>productId</code> (het han sau 1 ngay)"
                        .HtmlTag("div", "alert alert-success");
                    break;
            }

            string manufacture = "<a href=\"/cookies/read\">Read</a> <br> <a href=\"/cookies/write\">Write</a>";

            return manufacture + noti;
        }

        public static string FormProcess(HttpRequest request)
        {
            string hovaten = "";
            bool luachon = false;
            string email = "";
            string password = "";
            string thongbao = "";

            if (request.Method == "POST")
            {
                IFormCollection _form = request.Form;

                email = _form["email"].FirstOrDefault() ?? "";
                hovaten = _form["hovaten"].FirstOrDefault() ?? "";
                password = _form["password"].FirstOrDefault() ?? "";
                luachon = (_form["luachon"].FirstOrDefault() == "on");

                thongbao = $@"Dữ liệu post - email: {email}
                          - hovaten: {hovaten} - password: {password}
                          - luachon: {luachon} ";

                // Xu ly upload file
                if (_form.Files.Count > 0)
                {
                    string thongBaoFile = "Da upload file: ";
                    if (!Directory.Exists("wwwroot/upload"))
                    {
                        Directory.CreateDirectory("wwwroot/upload");
                    }

                    foreach (var file in _form.Files)
                    {
                        if (file.Length > 0)
                        {
                            string filePath = "wwwroot/upload/" + file.FileName;
                            // Mo stream de luu file
                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                file.CopyTo(stream);
                                thongBaoFile += $"{filePath} {file.Length} bytes";
                            }
                        }
                    }

                    thongbao += "<br>" + thongBaoFile;
                }
            }

            string html = File.ReadAllText("formTest.html");
            string htmlResult = string.Format(html, "", "", "unchecked") + thongbao;
            return htmlResult;
        }
    }
}
