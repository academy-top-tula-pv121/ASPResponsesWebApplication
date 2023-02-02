using Microsoft.Extensions.FileProviders;

namespace ASPResponsesWebApplication
{
    public class User
    {
        public string? Name { set; get; }
        public int Age { set; get; }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            var app = builder.Build();


            /*            
            app.Run(async (context) =>
            {
                var path = context.Request.Path;
                var localPath = $"html/{path}";

                var response = context.Response;
                response.ContentType = "text/html; charset=utf-8";

                if (File.Exists(localPath))
                    await response.SendFileAsync(localPath);
                else
                {
                    response.StatusCode = 404;
                    await response.WriteAsync("<h2>Page not found</h2>");
                }
            });
            */

            /*
            string fileName = "img01.jpg";

            app.Run(async (context) =>
            {
                var fileProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
                var fileInfo = fileProvider.GetFileInfo(fileName);

                var response = context.Response;
                response.Headers.ContentDisposition = "attachment; filename=orange.jpg";
                await response.SendFileAsync(fileInfo);
            });
            */

            /*
            app.Run(async (context) => {
                var response = context.Response;
                response.ContentType = "text/html; charset=utf-8";

                if (context.Request.Path == "/useraction")
                {
                    var form = context.Request.Form;
                    string name = form["name"];
                    string age = form["age"];
                    
                    string[] skills = form["skill"];
                    string skillsStr = "<ul>";
                    foreach (var skill in skills)
                        skillsStr += "<li>" + skill + "</li>";
                    skillsStr += "</ul>";

                    string answer = @$"<div><p>Name: {name}</p>
                                        <p>Age: {age}</p>
                                        <p>Skills: {skillsStr}</p></div>";
                    await response.WriteAsync(answer);
                }
                else
                    await response.SendFileAsync("html/index.html");
            });
            */
            /*
            app.Run(async (context) => {
                User user = new() { Name = "Bob", Age = 23 };
                await context.Response.WriteAsJsonAsync(user);
            });
            */

            app.Run(async (context) => {
                
                var request = context.Request;
                var response = context.Response;

                string message = "error";

                if (request.Path == "/user")
                {
                    try
                    {
                        var user = await request.ReadFromJsonAsync<User>();
                        if (user is not null)
                            message = $"Name: {user.Name}, Age: {user.Age}";
                    }
                    catch
                    {

                    }

                    await response.WriteAsJsonAsync(new { text = message });
                }
                else
                {
                    response.ContentType = "text/html; charset=utf-8";
                    await response.SendFileAsync("html/index.html");
                }
                    

            });

            app.Run();
        }
    }
}