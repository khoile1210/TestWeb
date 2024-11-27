using Microsoft.AspNetCore.Identity;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.General;
using System.Security.Claims;
using WebApp.Controllers;
using WebApp.Data;
namespace WebApp.Models
{
    public class Note
    {
        public int NoteId { get; set; }
        public string Author { get; set; }
        public string NoteTitle { get; set; }
        public string NoteMessage { get; set; }

        public Note() {
        }
    }
}
