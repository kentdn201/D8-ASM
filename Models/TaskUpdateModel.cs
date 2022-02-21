using System.ComponentModel;

namespace D8.Models
{
    public class TaskUpdateModel : TaskCreateModel
    {
        [DefaultValue(0)]
        public bool Completed {get; set;}
    }
}